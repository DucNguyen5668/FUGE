import { NextResponse } from "next/server";
import { db } from "@/lib/db";
import { gradesheets, classes } from "@/lib/db/schema";
import { auth } from "@/lib/auth";
import { eq } from "drizzle-orm";

export async function GET() {
  const session = await auth();
  if (!session?.user) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const list = db.select().from(gradesheets).all();
  // Attach class count
  const result = list.map((gs) => {
    const cls = db.select().from(classes).where(eq(classes.gradesheetId, gs.id)).all();
    return { ...gs, classCount: cls.length };
  });

  return NextResponse.json(result);
}
