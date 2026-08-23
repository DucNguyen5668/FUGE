import crypto from "node:crypto";
import type {
  FgGradeComponent,
  FgStudent,
  FgSubjectClassGrade,
  FgTeacherGrade,
} from "@/lib/fg-types";

export type {
  FgGradeComponent,
  FgStudent,
  FgSubjectClassGrade,
  FgTeacherGrade,
} from "@/lib/fg-types";

// ─── Mirrors AesOperation.cs ──────────────────────────────────────────────
// DEFAULT_KEY = "l10ca968o8e4133tyne2ea2315g19377" (32 bytes)
// IV = new byte[16] (all zeros)
const DEFAULT_KEY = "l10ca968o8e4133tyne2ea2315g19377";
const IV = Buffer.alloc(16, 0); // same as C# new byte[16]

export function decryptFgFile(base64Content: string): string {
  const buf = Buffer.from(base64Content, "base64");
  const decipher = crypto.createDecipheriv(
    "aes-256-cbc",
    Buffer.from(DEFAULT_KEY, "utf8"),
    IV
  );
  decipher.setAutoPadding(true);
  const decrypted = Buffer.concat([decipher.update(buf), decipher.final()]);
  return decrypted.toString("utf8");
}

// ─── .fg JSON types ───────────────────────────────────────────────────────
export function hashFgPassword(password: string): string {
  return crypto.createHash("md5").update(password, "utf8").digest("hex");
}

export function verifyFgPassword(password: string, expectedHash: string): boolean {
  const actual = Buffer.from(hashFgPassword(password), "utf8");
  const expected = Buffer.from(expectedHash.toLowerCase(), "utf8");
  return actual.length === expected.length && crypto.timingSafeEqual(actual, expected);
}

export function encryptFgFile(data: FgTeacherGrade, password: string): string {
  const payload: FgTeacherGrade = {
    ...data,
    Password: hashFgPassword(password),
  };
  const cipher = crypto.createCipheriv(
    "aes-256-cbc",
    Buffer.from(DEFAULT_KEY, "utf8"),
    IV
  );
  cipher.setAutoPadding(true);
  const encrypted = Buffer.concat([
    cipher.update(JSON.stringify(payload), "utf8"),
    cipher.final(),
  ]);
  return encrypted.toString("base64");
}

function asString(value: unknown, fallback = ""): string {
  return typeof value === "string" ? value : fallback;
}

function normalizeGrade(value: unknown): number | null {
  return typeof value === "number" && Number.isFinite(value) ? value : null;
}

export function normalizeFgData(value: unknown): FgTeacherGrade {
  if (!value || typeof value !== "object") throw new Error("Invalid FuGrade payload");
  const source = value as Record<string, unknown>;
  if (!Array.isArray(source.SubjectClassGrades)) {
    throw new Error("Invalid FuGrade classes");
  }

  const subjectClassGrades: FgSubjectClassGrade[] = source.SubjectClassGrades.map((entry) => {
    const rawClass = (entry ?? {}) as Record<string, unknown>;
    const components = Array.isArray(rawClass.Components)
      ? rawClass.Components.map((component) => asString(component)).filter(Boolean)
      : [];
    const classStudents: FgStudent[] = Array.isArray(rawClass.Students)
      ? rawClass.Students.map((entryStudent) => {
          const rawStudent = (entryStudent ?? {}) as Record<string, unknown>;
          const rawGrades = Array.isArray(rawStudent.Grades) ? rawStudent.Grades : [];
          const gradeItems: FgGradeComponent[] = rawGrades
            .map((entryGrade) => {
              const rawGrade = (entryGrade ?? {}) as Record<string, unknown>;
              return {
                Component: asString(rawGrade.Component),
                Grade: normalizeGrade(rawGrade.Grade),
              };
            })
            .filter((grade) => grade.Component);
          return {
            Roll: asString(rawStudent.Roll),
            Name: asString(rawStudent.Name),
            Comment: rawStudent.Comment == null ? null : asString(rawStudent.Comment),
            Grades: gradeItems,
          };
        })
      : [];

    return {
      Subject: asString(rawClass.Subject),
      Class: asString(rawClass.Class),
      Components: components,
      Students: classStudents,
    };
  });

  return {
    Login: asString(source.Login, "unknown"),
    Semester: asString(source.Semester),
    Version: asString(source.Version, "1.1"),
    Password: asString(source.Password),
    SubjectClassGrades: subjectClassGrades,
  };
}

export function parseFgJson(json: string): FgTeacherGrade {
  return normalizeFgData(JSON.parse(json));
}

export function decryptAndParseFg(base64Content: string): FgTeacherGrade {
  const json = decryptFgFile(base64Content);
  return parseFgJson(json);
}
