import { NextRequest, NextResponse } from "next/server";
import { db } from "@/lib/db";
import {
  classes,
  gradeComponents,
  students,
  grades,
} from "@/lib/db/schema";
import { auth } from "@/lib/auth";
import { and, eq } from "drizzle-orm";
import ExcelJS from "exceljs";

export async function GET(req: NextRequest) {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(req.url);
  const gradesheetId = parseInt(searchParams.get("gradesheetId") ?? "0");
  const classId = parseInt(searchParams.get("classId") ?? "0");

  if (!gradesheetId || !classId) {
    return NextResponse.json({ error: "gradesheetId and classId required" }, { status: 400 });
  }

  const cls = db
    .select()
    .from(classes)
    .where(and(eq(classes.id, classId), eq(classes.gradesheetId, gradesheetId)))
    .all()[0];
  if (!cls) return NextResponse.json({ error: "Class not found" }, { status: 404 });
  const comps = db
    .select()
    .from(gradeComponents)
    .where(eq(gradeComponents.classId, classId))
    .all()
    .sort((a, b) => a.order - b.order);

  const stuList = db
    .select()
    .from(students)
    .where(eq(students.classId, classId))
    .all();

  const workbook = new ExcelJS.Workbook();
  const sheet = workbook.addWorksheet(`${cls?.subject}_${cls?.className}`);

  // Header row
  sheet.addRow(["#", "Roll", "Name", "Comment", ...comps.map((c) => c.name)]);
  sheet.getRow(1).font = { bold: true };

  // Data rows
  stuList.forEach((stu, idx) => {
    const gradeList = db
      .select()
      .from(grades)
      .where(eq(grades.studentId, stu.id))
      .all();
    const gradeMap: Record<number, number | null> = {};
    gradeList.forEach((g) => {
      if (g.componentId !== null) gradeMap[g.componentId] = g.value;
    });

    sheet.addRow([
      idx + 1,
      stu.roll,
      stu.name,
      stu.comment ?? "",
      ...comps.map((c) => gradeMap[c.id] ?? ""),
    ]);
  });

  // Auto width
  sheet.columns.forEach((col) => {
    col.width = 14;
  });

  const buf = await workbook.xlsx.writeBuffer();
  return new NextResponse(buf, {
    headers: {
      "Content-Type": "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      "Content-Disposition": `attachment; filename="${cls?.subject}_${cls?.className}.xlsx"`,
    },
  });
}
