import { NextRequest, NextResponse } from "next/server";
import {
  decryptAndParseFg,
  parseFgJson,
  verifyFgPassword,
} from "@/lib/fg-decrypt";

export async function POST(req: NextRequest) {
  try {
    const formData = await req.formData();
    const file = formData.get("file") as File | null;
    const password = String(formData.get("password") ?? "");
    if (!file) return NextResponse.json({ error: "No file" }, { status: 400 });
    if (file.size > 10 * 1024 * 1024) {
      return NextResponse.json({ error: "File is larger than 10 MB" }, { status: 413 });
    }

    const text = await file.text();
    let teacherGrade;

    try {
      teacherGrade = decryptAndParseFg(text);
    } catch {
      try {
        teacherGrade = parseFgJson(text);
      } catch {
        return NextResponse.json(
          { error: "Cannot decrypt or parse this .fg file" },
          { status: 422 }
        );
      }
    }

    if (teacherGrade.Password) {
      if (!password) {
        return NextResponse.json(
          { error: "Password required", code: "PASSWORD_REQUIRED" },
          { status: 423 }
        );
      }
      if (!verifyFgPassword(password, teacherGrade.Password)) {
        return NextResponse.json(
          { error: "Incorrect password", code: "INVALID_PASSWORD" },
          { status: 403 }
        );
      }
    }

    return NextResponse.json({
      success: true,
      filename: file.name,
      data: { ...teacherGrade, Password: "" },
    });
  } catch (error) {
    console.error("FG import error:", error);
    return NextResponse.json({ error: "Cannot import this .fg file" }, { status: 500 });
  }
}
