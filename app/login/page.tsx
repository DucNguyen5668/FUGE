"use client";

import Image from "next/image";
import Link from "next/link";
import { useEffect, useState } from "react";
import { getProviders, signIn } from "next-auth/react";
import { useRouter } from "next/navigation";
import { toast } from "sonner";
import logoImage from "@/img/Logo.jpg";

type CaptchaChallenge = {
  image: string;
  token: string;
  expiresInSeconds: number;
};

function safeCallbackUrl() {
  const requested = new URLSearchParams(window.location.search).get("callbackUrl");
  return requested?.startsWith("/") && !requested.startsWith("//") ? requested : "/";
}

export default function LoginPage() {
  const router = useRouter();
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [captchaAnswer, setCaptchaAnswer] = useState("");
  const [captcha, setCaptcha] = useState<CaptchaChallenge | null>(null);
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [captchaLoading, setCaptchaLoading] = useState(true);
  const [googleStatus, setGoogleStatus] = useState<"loading" | "enabled" | "disabled">("loading");
  const [errors, setErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    let cancelled = false;
    void fetch("/api/auth/captcha", { cache: "no-store" })
      .then((response) => response.ok ? response.json() : Promise.reject())
      .then((challenge: CaptchaChallenge) => {
        if (!cancelled) setCaptcha(challenge);
      })
      .catch(() => {
        if (!cancelled) toast.error("Không thể tải CAPTCHA");
      })
      .finally(() => {
        if (!cancelled) setCaptchaLoading(false);
      });

    void getProviders().then((providers) => {
      if (!cancelled) setGoogleStatus(providers?.google ? "enabled" : "disabled");
    }).catch(() => {
      if (!cancelled) setGoogleStatus("disabled");
    });

    const searchParams = new URLSearchParams(window.location.search);
    if (searchParams.get("registered") === "1") {
      toast.success("Tạo tài khoản thành công. Hãy đăng nhập để tiếp tục.");
    }
    const oauthErrorTimer = searchParams.has("error")
      ? window.setTimeout(() => {
          if (!cancelled) {
            setErrors({ form: "Không thể đăng nhập bằng Google. Hãy kiểm tra tài khoản hoặc thử lại." });
          }
        }, 0)
      : undefined;

    return () => {
      cancelled = true;
      if (oauthErrorTimer !== undefined) window.clearTimeout(oauthErrorTimer);
    };
  }, []);

  async function refreshCaptcha() {
    setCaptchaLoading(true);
    setCaptchaAnswer("");
    try {
      const response = await fetch("/api/auth/captcha", { cache: "no-store" });
      if (!response.ok) throw new Error();
      setCaptcha(await response.json());
    } catch {
      setCaptcha(null);
      toast.error("Không thể tải CAPTCHA mới");
    } finally {
      setCaptchaLoading(false);
    }
  }

  async function handleSubmit(event: React.FormEvent) {
    event.preventDefault();
    const nextErrors: Record<string, string> = {};
    if (!login.trim()) nextErrors.login = "Vui lòng nhập tên đăng nhập hoặc email";
    if (!password) nextErrors.password = "Vui lòng nhập mật khẩu";
    if (captchaAnswer.trim().length !== 5) nextErrors.captcha = "Nhập đủ 5 ký tự trong ảnh";
    if (!captcha) nextErrors.captcha = "CAPTCHA chưa sẵn sàng";
    setErrors(nextErrors);
    if (Object.keys(nextErrors).length > 0 || !captcha) return;

    setLoading(true);
    try {
      const result = await signIn("credentials", {
        login: login.trim(),
        password,
        captchaToken: captcha.token,
        captchaAnswer: captchaAnswer.trim().toUpperCase(),
        redirect: false,
      });

      if (result?.ok) {
        router.push(safeCallbackUrl());
        router.refresh();
        return;
      }

      setErrors({ form: "Thông tin đăng nhập hoặc CAPTCHA không đúng" });
      await refreshCaptcha();
    } catch {
      setErrors({ form: "Không thể đăng nhập lúc này. Vui lòng thử lại." });
      await refreshCaptcha();
    } finally {
      setLoading(false);
    }
  }

  function googleButtonLabel() {
    if (googleStatus === "loading") return "Đang kiểm tra Google…";
    if (googleStatus === "disabled") return "Google chưa được cấu hình";
    return "Tiếp tục với Google";
  }

  return (
    <main className="auth-screen">
      <div className="auth-orb auth-orb-one" />
      <div className="auth-orb auth-orb-two" />
      <section className="auth-layout">
        <aside className="auth-showcase">
          <Link className="auth-brand" href="/">
            <Image src={logoImage} alt="Logo FuGrade" className="auth-logo" priority />
            <span><strong>FuGrade Web</strong><small>Workspace bảng điểm</small></span>
          </Link>
          <div className="auth-showcase-copy">
            <span className="auth-kicker">LƯU TRỮ AN TOÀN</span>
            <h1>Tiếp tục bảng điểm của bạn.</h1>
            <p>Đăng nhập chỉ khi cần lưu snapshot. Mở, chỉnh sửa và xuất file `.fg` vẫn dùng được ở chế độ khách.</p>
          </div>
          <div className="auth-feature-list">
            <span>✓ Bản nháp trong phiên được giữ nguyên</span>
            <span>✓ Xuất `.fg` có mật khẩu</span>
            <span>✓ CAPTCHA được xác thực phía server</span>
          </div>
        </aside>

        <div className="auth-form-column">
          <Link className="back-home-link" href="/">← Về workspace</Link>
          <section className="auth-card">
            <div className="auth-card-heading">
              <span className="eyebrow">CHÀO MỪNG TRỞ LẠI</span>
              <h2>Đăng nhập</h2>
              <p>Dùng tài khoản FuGrade hoặc Google.</p>
            </div>
            <form onSubmit={handleSubmit} className="auth-form" noValidate>
              {errors.form && <div className="form-alert" role="alert">{errors.form}</div>}
              <label className="auth-field">
                <span>Tên đăng nhập</span>
                <input className={`input ${errors.login ? "input-error" : ""}`} value={login} onChange={(event) => { setLogin(event.target.value); setErrors((current) => ({ ...current, login: "", form: "" })); }} autoComplete="username" placeholder="Tên đăng nhập của bạn ..." autoFocus />
                {errors.login && <small className="field-error">{errors.login}</small>}
              </label>

              <label className="auth-field">
                <span>Mật khẩu</span>
                <div className="password-input-wrap">
                  <input className={`input ${errors.password ? "input-error" : ""}`} type={showPassword ? "text" : "password"} value={password} onChange={(event) => { setPassword(event.target.value); setErrors((current) => ({ ...current, password: "", form: "" })); }} autoComplete="current-password" placeholder="Nhập mật khẩu" />
                  <button type="button" onClick={() => setShowPassword((value) => !value)}>{showPassword ? "Ẩn" : "Hiện"}</button>
                </div>
                {errors.password && <small className="field-error">{errors.password}</small>}
              </label>

              <div className="auth-field">
                <span>CAPTCHA</span>
                <div className="captcha-box">
                  <div className="captcha-image-wrap">
                    {captcha ? <Image src={captcha.image} width={190} height={56} unoptimized alt="Mã CAPTCHA gồm 5 ký tự" /> : <span>Không tải được CAPTCHA</span>}
                    <button type="button" onClick={() => void refreshCaptcha()} disabled={captchaLoading} aria-label="Tạo CAPTCHA mới">↻</button>
                  </div>
                  <input className={`input captcha-input ${errors.captcha ? "input-error" : ""}`} value={captchaAnswer} onChange={(event) => { setCaptchaAnswer(event.target.value.toUpperCase().replace(/[^A-Z0-9]/g, "").slice(0, 5)); setErrors((current) => ({ ...current, captcha: "", form: "" })); }} placeholder="5 ký tự" maxLength={5} autoComplete="off" spellCheck={false} />
                </div>
                {errors.captcha && <small className="field-error">{errors.captcha}</small>}
              </div>

              <button type="submit" className="btn btn-primary auth-submit" disabled={loading || captchaLoading}>
                {loading ? "Đang xác thực…" : "Đăng nhập để lưu"}
              </button>
            </form>

            <div className="auth-divider"><span>hoặc đăng nhập bằng Google</span></div>

            <button
              type="button"
              className="google-button"
              disabled={googleStatus !== "enabled" || loading}
              onClick={() => void signIn("google", { callbackUrl: safeCallbackUrl() })}
              title={googleStatus === "enabled" ? "Đăng nhập bằng Google" : googleStatus === "loading" ? "Đang kiểm tra cấu hình Google" : "Cần cấu hình AUTH_GOOGLE_ID và AUTH_GOOGLE_SECRET"}
              aria-busy={googleStatus === "loading"}
            >
              <svg viewBox="0 0 24 24" aria-hidden="true"><path fill="#4285F4" d="M21.35 12.23c0-.71-.06-1.24-.2-1.79H12v3.26h5.37a4.73 4.73 0 0 1-1.99 3.02l-.02.11 2.89 2.24.2.02c1.84-1.7 2.9-4.2 2.9-6.86Z"/><path fill="#34A853" d="M12 21.75c2.63 0 4.84-.87 6.45-2.66l-3.07-2.37c-.82.56-1.92.95-3.38.95-2.53 0-4.68-1.71-5.45-4.08l-.11.01-3 2.32-.04.1A9.75 9.75 0 0 0 12 21.75Z"/><path fill="#FBBC05" d="M6.55 13.59a5.84 5.84 0 0 1-.32-1.89c0-.66.11-1.3.31-1.89V9.7l-3.04-2.36-.1.05a9.75 9.75 0 0 0 0 8.63l3.15-2.43Z"/><path fill="#EA4335" d="M12 5.73c1.83 0 3.06.79 3.76 1.44l2.75-2.69C16.82 2.91 14.63 1.65 12 1.65A9.75 9.75 0 0 0 3.4 7.39l3.14 2.42C7.32 7.44 9.47 5.73 12 5.73Z"/></svg>
              {googleButtonLabel()}
            </button>
            <p className="auth-switch">Chưa có tài khoản? <Link href="/signup">Đăng ký ngay</Link></p>
          </section>
        </div>
      </section>
    </main>
  );
}
