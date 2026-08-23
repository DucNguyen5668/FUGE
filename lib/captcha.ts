import crypto from "node:crypto";

const CAPTCHA_TTL_MS = 5 * 60 * 1000;
const CAPTCHA_ALPHABET = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

type CaptchaPayload = {
  answerHash: string;
  expiresAt: number;
  nonce: string;
};

const globalCaptcha = globalThis as typeof globalThis & {
  fugradeUsedCaptchaNonces?: Map<string, number>;
};

const usedNonces = globalCaptcha.fugradeUsedCaptchaNonces ?? new Map<string, number>();
globalCaptcha.fugradeUsedCaptchaNonces = usedNonces;

function captchaSecret() {
  const secret = process.env.AUTH_SECRET;
  if (!secret) throw new Error("AUTH_SECRET is required for CAPTCHA");
  return secret;
}

function sign(value: string) {
  return crypto.createHmac("sha256", captchaSecret()).update(value).digest("base64url");
}

function hashAnswer(answer: string, nonce: string) {
  return crypto
    .createHmac("sha256", captchaSecret())
    .update(`${nonce}:${answer.trim().toUpperCase()}`)
    .digest("hex");
}

function secureEqual(left: string, right: string) {
  const leftBuffer = Buffer.from(left);
  const rightBuffer = Buffer.from(right);
  return leftBuffer.length === rightBuffer.length && crypto.timingSafeEqual(leftBuffer, rightBuffer);
}

function createCode() {
  return Array.from({ length: 5 }, () =>
    CAPTCHA_ALPHABET[crypto.randomInt(0, CAPTCHA_ALPHABET.length)]
  ).join("");
}

function captchaSvg(code: string) {
  const letters = code
    .split("")
    .map((letter, index) => {
      const x = 24 + index * 31;
      const y = 38 + crypto.randomInt(-4, 5);
      const rotate = crypto.randomInt(-16, 17);
      return `<text x="${x}" y="${y}" transform="rotate(${rotate} ${x} ${y})">${letter}</text>`;
    })
    .join("");
  const lines = Array.from({ length: 5 }, () => {
    const y1 = crypto.randomInt(5, 50);
    const y2 = crypto.randomInt(5, 50);
    return `<line x1="0" y1="${y1}" x2="190" y2="${y2}" />`;
  }).join("");
  const svg = `<svg xmlns="http://www.w3.org/2000/svg" width="190" height="56" viewBox="0 0 190 56"><rect width="190" height="56" rx="10" fill="#eef8f8"/><g stroke="#79bcc2" stroke-width="1" opacity=".55">${lines}</g><g fill="#087f8c" font-family="ui-monospace,Consolas,monospace" font-size="28" font-weight="800" letter-spacing="3">${letters}</g></svg>`;
  return `data:image/svg+xml;base64,${Buffer.from(svg).toString("base64")}`;
}

export function createCaptchaChallenge() {
  const code = createCode();
  const nonce = crypto.randomBytes(16).toString("hex");
  const payload: CaptchaPayload = {
    answerHash: hashAnswer(code, nonce),
    expiresAt: Date.now() + CAPTCHA_TTL_MS,
    nonce,
  };
  const encoded = Buffer.from(JSON.stringify(payload)).toString("base64url");
  return {
    image: captchaSvg(code),
    token: `${encoded}.${sign(encoded)}`,
    expiresInSeconds: CAPTCHA_TTL_MS / 1000,
  };
}

export function verifyCaptcha(token: string, answer: string) {
  try {
    const [encoded, signature] = token.split(".");
    if (!encoded || !signature || !answer.trim() || !secureEqual(sign(encoded), signature)) return false;

    const payload = JSON.parse(Buffer.from(encoded, "base64url").toString("utf8")) as CaptchaPayload;
    const now = Date.now();
    for (const [nonce, expiresAt] of usedNonces) {
      if (expiresAt < now) usedNonces.delete(nonce);
    }
    if (!payload.nonce || payload.expiresAt < now || usedNonces.has(payload.nonce)) return false;
    if (!secureEqual(hashAnswer(answer, payload.nonce), payload.answerHash)) return false;

    usedNonces.set(payload.nonce, payload.expiresAt);
    return true;
  } catch {
    return false;
  }
}
