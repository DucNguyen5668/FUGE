import NextAuth from "next-auth";
import Credentials from "next-auth/providers/credentials";
import Google from "next-auth/providers/google";
import bcrypt from "bcryptjs";
import { db } from "@/lib/db";
import { users } from "@/lib/db/schema";
import { eq } from "drizzle-orm";
import { verifyCaptcha } from "@/lib/captcha";

const googleClientId = process.env.AUTH_GOOGLE_ID;
const googleClientSecret = process.env.AUTH_GOOGLE_SECRET;

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    Credentials({
      credentials: {
        login: { label: "Username or email", type: "text" },
        password: { label: "Password", type: "password" },
        captchaToken: { label: "CAPTCHA token", type: "hidden" },
        captchaAnswer: { label: "CAPTCHA", type: "text" },
      },
      async authorize(credentials) {
        const login = typeof credentials?.login === "string"
          ? credentials.login.trim().toLowerCase()
          : "";
        const password = typeof credentials?.password === "string" ? credentials.password : "";
        const captchaToken = typeof credentials?.captchaToken === "string" ? credentials.captchaToken : "";
        const captchaAnswer = typeof credentials?.captchaAnswer === "string" ? credentials.captchaAnswer : "";

        if (!login || !password || !verifyCaptcha(captchaToken, captchaAnswer)) return null;

        const user = db.select().from(users).where(eq(users.login, login)).get();
        if (!user || !user.passwordHash.startsWith("$2")) return null;

        const ok = await bcrypt.compare(password, user.passwordHash);
        if (!ok) return null;

        return { id: String(user.id), name: user.name, email: user.login };
      },
    }),
    ...(googleClientId && googleClientSecret
      ? [Google({ clientId: googleClientId, clientSecret: googleClientSecret })]
      : []),
  ],
  session: { strategy: "jwt" },
  pages: { signIn: "/login" },
  callbacks: {
    async signIn({ user, account, profile }) {
      if (account?.provider !== "google") return true;

      const googleProfile = profile as { email_verified?: boolean } | undefined;
      const email = user.email?.trim().toLowerCase();
      if (!email || googleProfile?.email_verified !== true) return false;

      let databaseUser = db.select().from(users).where(eq(users.login, email)).get();
      if (!databaseUser) {
        databaseUser = db
          .insert(users)
          .values({
            login: email,
            name: user.name?.trim() || email.split("@")[0],
            passwordHash: "oauth:google",
            role: "teacher",
          })
          .returning()
          .get();
      }

      user.id = String(databaseUser.id);
      return true;
    },
    jwt({ token, user }) {
      if (user?.id) token.id = user.id;
      return token;
    },
    session({ session, token }) {
      if (token?.id) session.user.id = token.id as string;
      return session;
    },
  },
});
