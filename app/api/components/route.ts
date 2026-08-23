import { NextRequest, NextResponse } from "next/server";
import { db } from "@/lib/db";
import { gradeComponents, grades, students } from "@/lib/db/schema";
import { auth } from "@/lib/auth";
import { eq } from "drizzle-orm";

// POST /api/components — Add new grading component
export async function POST(req: NextRequest) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { classId, name } = await req.json();
  if (!classId || !name) return NextResponse.json({ error: "classId and name required" }, { status: 400 });

  // Check duplicate
  const existing = db
    .select()
    .from(gradeComponents)
    .where(eq(gradeComponents.classId, classId))
    .all();

  if (existing.find((c) => c.name.trim().toUpperCase() === name.trim().toUpperCase())) {
    return NextResponse.json({ error: "Grading component already exists!" }, { status: 409 });
  }

  const order = existing.length;
  const [comp] = db
    .insert(gradeComponents)
    .values({ classId, name: name.trim(), order })
    .returning()
    .all();

  // Create empty grade rows for all existing students
  const stuList = db
    .select()
    .from(students)
    .where(eq(students.classId, classId))
    .all();

  for (const stu of stuList) {
    db.insert(grades).values({ studentId: stu.id, componentId: comp.id, value: null }).run();
  }

  return NextResponse.json(comp);
}
