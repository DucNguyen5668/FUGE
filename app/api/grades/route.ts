import { NextRequest, NextResponse } from "next/server";
import { db } from "@/lib/db";
import { grades, students } from "@/lib/db/schema";
import { auth } from "@/lib/auth";
import { eq, and } from "drizzle-orm";

// PATCH /api/grades — update a single grade cell
export async function PATCH(req: NextRequest) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await req.json();
  const { studentId, componentId, value } = body as {
    studentId: number;
    componentId: number;
    value: number | null;
  };

  // Validate value
  if (value !== null && value !== undefined) {
    if (isNaN(value) || value < 0 || value > 10) {
      return NextResponse.json({ error: "Grade must be between 0 and 10" }, { status: 400 });
    }
  }

  // Upsert: check if grade row exists
  const existing = db
    .select()
    .from(grades)
    .where(and(eq(grades.studentId, studentId), eq(grades.componentId, componentId)))
    .all();

  if (existing.length > 0) {
    db.update(grades)
      .set({ value: value ?? null, updatedAt: new Date().toISOString() })
      .where(and(eq(grades.studentId, studentId), eq(grades.componentId, componentId)))
      .run();
  } else {
    db.insert(grades)
      .values({ studentId, componentId, value: value ?? null })
      .run();
  }

  return NextResponse.json({ success: true });
}

// POST /api/grades/bulk-import — import marks from pasted text
export async function POST(req: NextRequest) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await req.json();
  const { classId, componentId, rows, mode } = body as {
    classId: number;
    componentId?: number;
    rows: { roll: string; value: string }[];
    mode: "mark" | "comment";
  };

  const stuList = db
    .select()
    .from(students)
    .where(eq(students.classId, classId))
    .all();

  const results: { roll: string; status: "ok" | "not_found" | "invalid" }[] = [];

  for (const row of rows) {
    const stu = stuList.find(
      (s) => s.roll.trim().toUpperCase() === row.roll.trim().toUpperCase()
    );
    if (!stu) {
      results.push({ roll: row.roll, status: "not_found" });
      continue;
    }

    if (mode === "comment") {
      db.update(students)
        .set({ comment: row.value })
        .where(eq(students.id, stu.id))
        .run();
      results.push({ roll: row.roll, status: "ok" });
    } else {
      const numVal = parseFloat(row.value);
      if (isNaN(numVal) || numVal < 0 || numVal > 10) {
        results.push({ roll: row.roll, status: "invalid" });
        continue;
      }
      if (!componentId) {
        results.push({ roll: row.roll, status: "invalid" });
        continue;
      }
      const existing = db
        .select()
        .from(grades)
        .where(and(eq(grades.studentId, stu.id), eq(grades.componentId, componentId)))
        .all();
      if (existing.length > 0) {
        db.update(grades)
          .set({ value: numVal, updatedAt: new Date().toISOString() })
          .where(and(eq(grades.studentId, stu.id), eq(grades.componentId, componentId)))
          .run();
      } else {
        db.insert(grades).values({ studentId: stu.id, componentId, value: numVal }).run();
      }
      results.push({ roll: row.roll, status: "ok" });
    }
  }

  return NextResponse.json({ results });
}

// DELETE /api/grades?componentId=X — clear all grades for a component
export async function DELETE(req: NextRequest) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(req.url);
  const componentId = parseInt(searchParams.get("componentId") ?? "0");
  if (!componentId) return NextResponse.json({ error: "componentId required" }, { status: 400 });

  db.update(grades)
    .set({ value: null, updatedAt: new Date().toISOString() })
    .where(eq(grades.componentId, componentId))
    .run();

  return NextResponse.json({ success: true });
}
