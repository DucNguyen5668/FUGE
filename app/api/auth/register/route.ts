import bcrypt from "bcryptjs";
import { eq } from "drizzle-orm";
import { z } from "zod";
import { db } from "@/lib/db";
import { users } from "@/lib/db/schema";

const registerSchema = z
  .object({
    name: z.string().trim().min(2, "Họ tên phải có ít nhất 2 ký tự").max(80, "Họ tên quá dài"),
    email: z.string().trim().toLowerCase().email("Email không hợp lệ").max(160, "Email quá dài"),
    password: z
      .string()
      .min(8, "Mật khẩu phải có ít nhất 8 ký tự")
      .max(72, "Mật khẩu không được vượt quá 72 ký tự")
      .regex(/[a-z]/, "Mật khẩu cần có chữ thường")
      .regex(/[A-Z]/, "Mật khẩu cần có chữ hoa")
      .regex(/[0-9]/, "Mật khẩu cần có chữ số"),
    confirmPassword: z.string(),
  })
  .refine((value) => value.password === value.confirmPassword, {
    message: "Hai mật khẩu chưa khớp",
    path: ["confirmPassword"],
  });

export async function POST(request: Request) {
  try {
    const parsed = registerSchema.safeParse(await request.json());
    if (!parsed.success) {
      const issue = parsed.error.issues[0];
      return Response.json(
        { error: issue?.message ?? "Dữ liệu đăng ký không hợp lệ", field: issue?.path[0] ?? null },
        { status: 400 }
      );
    }

    const { name, email, password } = parsed.data;
    const existing = db.select({ id: users.id }).from(users).where(eq(users.login, email)).get();
    if (existing) {
      return Response.json({ error: "Email này đã được sử dụng", field: "email" }, { status: 409 });
    }

    const passwordHash = await bcrypt.hash(password, 12);
    const user = db
      .insert(users)
      .values({ login: email, name, passwordHash, role: "teacher" })
      .returning({ id: users.id, login: users.login, name: users.name })
      .get();

    return Response.json({ success: true, user }, { status: 201 });
  } catch (error) {
    if (
      error &&
      typeof error === "object" &&
      "code" in error &&
      error.code === "SQLITE_CONSTRAINT_UNIQUE"
    ) {
      return Response.json({ error: "Email này đã được sử dụng", field: "email" }, { status: 409 });
    }
    console.error("Register error:", error);
    return Response.json({ error: "Không thể tạo tài khoản" }, { status: 500 });
  }
}
