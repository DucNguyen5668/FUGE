import { NextResponse } from "next/server";
import { db } from "@/lib/db";
import { users } from "@/lib/db/schema";
import bcrypt from "bcryptjs";

// GET /api/seed — one-time seed endpoint (call from browser once)
export async function GET() {
  if (process.env.NODE_ENV === "production") {
    return NextResponse.json({ error: "Not found" }, { status: 404 });
  }
  const existing = db.select().from(users).all();
  if (existing.length > 0) {
    return NextResponse.json({ message: "Already seeded", users: existing.length });
  }
  const hash = await bcrypt.hash("admin123", 10);
  db.insert(users).values({
    login: "admin",
    name: "Administrator",
    passwordHash: hash,
    role: "admin",
  }).run();
  return NextResponse.json({ success: true, message: "Seeded admin user. Login: admin / admin123" });
}
