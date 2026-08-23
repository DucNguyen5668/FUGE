import { NextRequest, NextResponse } from "next/server";
import { db } from "@/lib/db";
import { students, grades, gradeComponents } from "@/lib/db/schema";
import { auth } from "@/lib/auth";
import { eq } from "drizzle-orm";

// POST /api/students — Add new student
export async function POST(req: NextRequest) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { classId, roll, name } = await req.json();
  if (!classId || !roll) return NextResponse.json({ error: "classId and roll required" }, { status: 400 });

  // Check duplicate
  const existing = db
    .select()
    .from(students)
    .where(eq(students.classId, classId))
    .all();

  if (existing.find((s) => s.roll.trim().toUpperCase() === roll.trim().toUpperCase())) {
    return NextResponse.json({ error: "Student already exists!" }, { status: 409 });
  }

  const [stu] = db
    .insert(students)
    .values({ classId, roll: roll.trim().toUpperCase(), name: name?.trim() ?? "" })
    .returning()
    .all();

  // Create empty grade rows for all components
  const comps = db
    .select()
    .from(gradeComponents)
    .where(eq(gradeComponents.classId, classId))
    .all();

  for (const comp of comps) {
    db.insert(grades).values({ studentId: stu.id, componentId: comp.id, value: null }).run();
  }

  return NextResponse.json(stu);
}
