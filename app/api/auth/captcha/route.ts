import { createCaptchaChallenge } from "@/lib/captcha";

export async function GET() {
  try {
    return Response.json(createCaptchaChallenge(), {
      headers: { "Cache-Control": "no-store" },
    });
  } catch {
    return Response.json({ error: "Không thể tạo CAPTCHA" }, { status: 500 });
  }
}
