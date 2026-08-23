import { sqliteTable, text, real, integer } from "drizzle-orm/sqlite-core";
import { sql } from "drizzle-orm";

// ── Users ──────────────────────────────────────────────────────────────────
export const users = sqliteTable("users", {
  id: integer("id").primaryKey({ autoIncrement: true }),
  login: text("login").notNull().unique(),
  name: text("name").notNull(),
  passwordHash: text("password_hash").notNull(),
  role: text("role").notNull().default("teacher"),
  createdAt: text("created_at").default(sql`(datetime('now'))`),
});

// ── Grade Sheets (.fg equivalent) ──────────────────────────────────────────
export const gradesheets = sqliteTable("gradesheets", {
  id: integer("id").primaryKey({ autoIncrement: true }),
  userId: integer("user_id").references(() => users.id),
  login: text("login").notNull(),
  semester: text("semester").notNull().default(""),
  version: text("version").notNull().default("1.1"),
  filename: text("filename").notNull().default(""),
  createdAt: text("created_at").default(sql`(datetime('now'))`),
  updatedAt: text("updated_at").default(sql`(datetime('now'))`),
});

// ── Classes (SubjectClassGrade) ─────────────────────────────────────────────
export const classes = sqliteTable("classes", {
  id: integer("id").primaryKey({ autoIncrement: true }),
  gradesheetId: integer("gradesheet_id").references(() => gradesheets.id, { onDelete: "cascade" }),
  subject: text("subject").notNull(),
  className: text("class_name").notNull(),
  createdAt: text("created_at").default(sql`(datetime('now'))`),
});

// ── Grade Components ────────────────────────────────────────────────────────
export const gradeComponents = sqliteTable("grade_components", {
  id: integer("id").primaryKey({ autoIncrement: true }),
  classId: integer("class_id").references(() => classes.id, { onDelete: "cascade" }),
  name: text("name").notNull(),
  order: integer("order").notNull().default(0),
  createdAt: text("created_at").default(sql`(datetime('now'))`),
});

// ── Students ────────────────────────────────────────────────────────────────
export const students = sqliteTable("students", {
  id: integer("id").primaryKey({ autoIncrement: true }),
  classId: integer("class_id").references(() => classes.id, { onDelete: "cascade" }),
  roll: text("roll").notNull(),
  name: text("name").notNull().default(""),
  comment: text("comment"),
  createdAt: text("created_at").default(sql`(datetime('now'))`),
});

// ── Grades ──────────────────────────────────────────────────────────────────
export const grades = sqliteTable("grades", {
  id: integer("id").primaryKey({ autoIncrement: true }),
  studentId: integer("student_id").references(() => students.id, { onDelete: "cascade" }),
  componentId: integer("component_id").references(() => gradeComponents.id, { onDelete: "cascade" }),
  value: real("value"),
  updatedAt: text("updated_at").default(sql`(datetime('now'))`),
});

// Types
export type User = typeof users.$inferSelect;
export type Gradesheet = typeof gradesheets.$inferSelect;
export type Class = typeof classes.$inferSelect;
export type GradeComponent = typeof gradeComponents.$inferSelect;
export type Student = typeof students.$inferSelect;
export type Grade = typeof grades.$inferSelect;
