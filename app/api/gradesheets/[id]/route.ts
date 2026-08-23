import { NextRequest, NextResponse } from "next/server";
import { db } from "@/lib/db";
import {
  classes,
  gradeComponents,
  students,
  grades,
} from "@/lib/db/schema";
import { auth } from "@/lib/auth";
import { eq } from "drizzle-orm";

export async function GET(
  _req: NextRequest,
  { params }: { params: Promise<{ id: string }> }
) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { id } = await params;
  const gsId = parseInt(id);

  const clsList = db
    .select()
    .from(classes)
    .where(eq(classes.gradesheetId, gsId))
    .all();

  const result = clsList.map((cls) => {
    const components = db
      .select()
      .from(gradeComponents)
      .where(eq(gradeComponents.classId, cls.id))
      .all()
      .sort((a, b) => a.order - b.order);

    const stuList = db
      .select()
      .from(students)
      .where(eq(students.classId, cls.id))
      .all();

    const studentsWithGrades = stuList.map((stu) => {
      const gradeList = db
        .select()
        .from(grades)
        .where(eq(grades.studentId, stu.id))
        .all();

      const gradeMap: Record<number, number | null> = {};
      gradeList.forEach((g) => {
        if (g.componentId !== null) gradeMap[g.componentId] = g.value;
      });

      return { ...stu, grades: gradeMap };
    });

    return { ...cls, components, students: studentsWithGrades };
  });

  return NextResponse.json(result);
}

export async function DELETE(
  _req: NextRequest,
  { params }: { params: Promise<{ id: string }> }
) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { id } = await params;
  const { gradesheets } = await import("@/lib/db/schema");
  db.delete(gradesheets).where(eq(gradesheets.id, parseInt(id))).run();
  return NextResponse.json({ success: true });
}
