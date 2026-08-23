import { db } from "@/lib/db";
import { users } from "@/lib/db/schema";
import bcrypt from "bcryptjs";

// Seed a default admin user if no users exist
async function seed() {
  const existing = db.select().from(users).all();
  if (existing.length > 0) {
    console.log("DB already has users, skipping seed.");
    return;
  }
  const hash = await bcrypt.hash("admin123", 10);
  db.insert(users).values({
    login: "admin",
    name: "Administrator",
    passwordHash: hash,
    role: "admin",
  }).run();
  console.log("✅ Seeded admin user: login=admin password=admin123");
}

seed().catch(console.error);
