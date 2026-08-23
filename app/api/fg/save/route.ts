import { NextRequest, NextResponse } from "next/server";
import { auth } from "@/lib/auth";
import { db } from "@/lib/db";
import {
  classes,
  gradeComponents,
  grades,
  gradesheets,
  students,
} from "@/lib/db/schema";
import { normalizeFgData } from "@/lib/fg-decrypt";

export async function POST(req: NextRequest) {
  const session = await auth();
  const userId = Number(session?.user?.id);
  if (!userId) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  try {
    const body = await req.json();
    const data = normalizeFgData(body.data);
    const filename = typeof body.filename === "string" ? body.filename : "fugrade.fg";

    const gradesheetId = db.transaction((tx) => {
      const createdSheet = tx
        .insert(gradesheets)
        .values({
          userId,
          login: data.Login,
          semester: data.Semester,
          version: data.Version,
          filename,
        })
        .returning()
        .all()[0];

      for (const sourceClass of data.SubjectClassGrades) {
        const createdClass = tx
          .insert(classes)
          .values({
            gradesheetId: createdSheet.id,
            subject: sourceClass.Subject,
            className: sourceClass.Class,
          })
          .returning()
          .all()[0];

        const componentIds = new Map<string, number>();
        sourceClass.Components.forEach((name, order) => {
          const component = tx
            .insert(gradeComponents)
            .values({ classId: createdClass.id, name, order })
            .returning()
            .all()[0];
          componentIds.set(name, component.id);
        });

        for (const sourceStudent of sourceClass.Students) {
          const student = tx
            .insert(students)
            .values({
              classId: createdClass.id,
              roll: sourceStudent.Roll,
              name: sourceStudent.Name,
              comment: sourceStudent.Comment,
            })
            .returning()
            .all()[0];

          for (const sourceGrade of sourceStudent.Grades) {
            const componentId = componentIds.get(sourceGrade.Component);
            if (componentId) {
              tx.insert(grades)
                .values({ studentId: student.id, componentId, value: sourceGrade.Grade })
                .run();
            }
          }
        }
      }

      return createdSheet.id;
    });

    return NextResponse.json({ success: true, gradesheetId });
  } catch (error) {
    console.error("FG save error:", error);
    return NextResponse.json({ error: "Cannot save gradesheet" }, { status: 422 });
  }
}
