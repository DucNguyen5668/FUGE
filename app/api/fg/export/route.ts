import { NextRequest } from "next/server";
import { encryptFgFile, normalizeFgData } from "@/lib/fg-decrypt";

function safeFilename(value: unknown): string {
  const raw = typeof value === "string" && value.trim() ? value.trim() : "fugrade-export.fg";
  const withExtension = raw.toLowerCase().endsWith(".fg") ? raw : `${raw}.fg`;
  return withExtension.replace(/[^a-zA-Z0-9._-]/g, "_");
}

export async function POST(req: NextRequest) {
  try {
    const body = await req.json();
    const password = typeof body.password === "string" ? body.password : "";
    if (!password.trim()) {
      return Response.json({ error: "Password is required" }, { status: 400 });
    }

    const data = normalizeFgData(body.data);
    const encrypted = encryptFgFile(data, password);
    const filename = safeFilename(body.filename);

    return new Response(encrypted, {
      headers: {
        "Content-Type": "application/octet-stream",
        "Content-Disposition": `attachment; filename="${filename}"`,
        "Cache-Control": "no-store",
      },
    });
  } catch {
    return Response.json({ error: "Cannot export this FuGrade file" }, { status: 422 });
  }
}
