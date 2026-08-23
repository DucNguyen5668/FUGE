"use client";

import Image from "next/image";
import Link from "next/link";
import { useEffect, useState } from "react";
import { getProviders, signIn } from "next-auth/react";
import { useRouter } from "next/navigation";
import { toast } from "sonner";
import logoImage from "@/img/Logo.jpg";

type SignupForm = {
  name: string;
  email: string;
  password: string;
  confirmPassword: string;
};

function validateForm(form: SignupForm) {
  const errors: Record<string, string> = {};
  if (form.name.trim().length < 2) errors.name = "Họ tên phải có ít nhất 2 ký tự";
  else if (form.name.trim().length > 80) errors.name = "Họ tên không được vượt quá 80 ký tự";
  if (form.email.trim().length > 160) errors.email = "Email không được vượt quá 160 ký tự";
  else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email.trim())) errors.email = "Email không hợp lệ";
  if (form.password.length > 72) errors.password = "Mật khẩu không được vượt quá 72 ký tự";
  else if (form.password.length < 8) errors.password = "Mật khẩu phải có ít nhất 8 ký tự";
  else if (!/[a-z]/.test(form.password) || !/[A-Z]/.test(form.password) || !/[0-9]/.test(form.password)) errors.password = "Cần có chữ hoa, chữ thường và chữ số";
  if (form.confirmPassword !== form.password) errors.confirmPassword = "Hai mật khẩu chưa khớp";
  return errors;
}

export default function SignupPage() {
  const router = useRouter();
  const [form, setForm] = useState<SignupForm>({ name: "", email: "", password: "", confirmPassword: "" });
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [googleStatus, setGoogleStatus] = useState<"loading" | "enabled" | "disabled">("loading");

  useEffect(() => {
    let cancelled = false;
    void getProviders().then((providers) => {
      if (!cancelled) setGoogleStatus(providers?.google ? "enabled" : "disabled");
    }).catch(() => {
      if (!cancelled) setGoogleStatus("disabled");
    });
    return () => { cancelled = true; };
  }, []);

  const updateField = (field: keyof SignupForm, value: string) => {
    setForm((current) => ({ ...current, [field]: value }));
    setErrors((current) => ({ ...current, [field]: "", form: "" }));
  };

  async function handleSubmit(event: React.FormEvent) {
    event.preventDefault();
    const nextErrors = validateForm(form);
    setErrors(nextErrors);
    if (Object.keys(nextErrors).length > 0) return;

    setLoading(true);
    try {
      const response = await fetch("/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      const result = await response.json();
      if (!response.ok) {
        setErrors(result.field ? { [result.field]: result.error } : { form: result.error });
        return;
      }

      toast.success("Tạo tài khoản thành công");
      router.push("/login?registered=1");
    } catch {
      setErrors({ form: "Không thể tạo tài khoản lúc này. Vui lòng thử lại." });
    } finally {
      setLoading(false);
    }
  }

  const passwordRules = [
    { label: "Ít nhất 8 ký tự", valid: form.password.length >= 8 },
    { label: "Có chữ hoa và chữ thường", valid: /[A-Z]/.test(form.password) && /[a-z]/.test(form.password) },
    { label: "Có ít nhất một chữ số", valid: /[0-9]/.test(form.password) },
  ];

  function googleButtonLabel() {
    if (googleStatus === "loading") return "Đang kiểm tra Google…";
    if (googleStatus === "disabled") return "Google chưa được cấu hình";
    return "Đăng ký với Google";
  }

  return (
    <main className="auth-screen">
      <div className="auth-orb auth-orb-one" />
      <div className="auth-orb auth-orb-two" />
      <section className="auth-layout signup-layout">
        <aside className="auth-showcase">
          <Link className="auth-brand" href="/">
            <Image src={logoImage} alt="Logo FuGrade" className="auth-logo" priority />
            <span><strong>FuGrade Web</strong><small>Workspace bảng điểm</small></span>
          </Link>
          <div className="auth-showcase-copy">
            <span className="auth-kicker">TÀI KHOẢN GIẢNG VIÊN</span>
            <h1>Tạo tài khoản để lưu công việc.</h1>
            <p>Sau khi đăng ký, bạn có thể lưu các snapshot bảng điểm trong SQLite và tiếp tục sử dụng định dạng `.fg` tương thích FuGrade.</p>
          </div>
          <div className="auth-feature-list">
            <span>✓ Kiểm tra dữ liệu ở cả client và server</span>
            <span>✓ Mật khẩu được băm bằng bcrypt</span>
            <span>✓ Có thể dùng tài khoản Google</span>
          </div>
        </aside>

        <div className="auth-form-column signup-column">
          <Link className="back-home-link" href="/">← Về workspace</Link>
          <section className="auth-card">
            <div className="auth-card-heading">
              <span className="eyebrow">BẮT ĐẦU VỚI FUGRADE</span>
              <h2>Đăng ký tài khoản</h2>
              <p>Dùng email thật để đăng nhập và quản lý snapshot.</p>
            </div>

            <button type="button" className="google-button" disabled={googleStatus !== "enabled" || loading} onClick={() => void signIn("google", { callbackUrl: "/" })} title={googleStatus === "enabled" ? "Đăng ký bằng Google" : googleStatus === "loading" ? "Đang kiểm tra cấu hình Google" : "Cần cấu hình AUTH_GOOGLE_ID và AUTH_GOOGLE_SECRET"} aria-busy={googleStatus === "loading"}>
              <svg viewBox="0 0 24 24" aria-hidden="true"><path fill="#4285F4" d="M21.35 12.23c0-.71-.06-1.24-.2-1.79H12v3.26h5.37a4.73 4.73 0 0 1-1.99 3.02l-.02.11 2.89 2.24.2.02c1.84-1.7 2.9-4.2 2.9-6.86Z"/><path fill="#34A853" d="M12 21.75c2.63 0 4.84-.87 6.45-2.66l-3.07-2.37c-.82.56-1.92.95-3.38.95-2.53 0-4.68-1.71-5.45-4.08l-.11.01-3 2.32-.04.1A9.75 9.75 0 0 0 12 21.75Z"/><path fill="#FBBC05" d="M6.55 13.59a5.84 5.84 0 0 1-.32-1.89c0-.66.11-1.3.31-1.89V9.7l-3.04-2.36-.1.05a9.75 9.75 0 0 0 0 8.63l3.15-2.43Z"/><path fill="#EA4335" d="M12 5.73c1.83 0 3.06.79 3.76 1.44l2.75-2.69C16.82 2.91 14.63 1.65 12 1.65A9.75 9.75 0 0 0 3.4 7.39l3.14 2.42C7.32 7.44 9.47 5.73 12 5.73Z"/></svg>
              {googleButtonLabel()}
            </button>

            <div className="auth-divider"><span>hoặc đăng ký bằng email</span></div>

            <form onSubmit={handleSubmit} className="auth-form signup-form" noValidate>
              {errors.form && <div className="form-alert" role="alert">{errors.form}</div>}
              <label className="auth-field"><span>Họ và tên</span><input className={`input ${errors.name ? "input-error" : ""}`} value={form.name} onChange={(event) => updateField("name", event.target.value)} placeholder="Nguyễn Văn A" autoComplete="name" maxLength={80} autoFocus />{errors.name && <small className="field-error">{errors.name}</small>}</label>
              <label className="auth-field"><span>Email</span><input className={`input ${errors.email ? "input-error" : ""}`} type="email" value={form.email} onChange={(event) => updateField("email", event.target.value)} placeholder="teacher@fpt.edu.vn" autoComplete="email" maxLength={160} />{errors.email && <small className="field-error">{errors.email}</small>}</label>
              <label className="auth-field"><span>Mật khẩu</span><div className="password-input-wrap"><input className={`input ${errors.password ? "input-error" : ""}`} type={showPassword ? "text" : "password"} value={form.password} onChange={(event) => updateField("password", event.target.value)} placeholder="Tối thiểu 8 ký tự" autoComplete="new-password" maxLength={72} /><button type="button" onClick={() => setShowPassword((value) => !value)}>{showPassword ? "Ẩn" : "Hiện"}</button></div>{errors.password && <small className="field-error">{errors.password}</small>}</label>
              <div className="password-rules">{passwordRules.map((rule) => <span className={rule.valid ? "valid" : ""} key={rule.label}>{rule.valid ? "✓" : "○"} {rule.label}</span>)}</div>
              <label className="auth-field"><span>Nhập lại mật khẩu</span><input className={`input ${errors.confirmPassword ? "input-error" : ""}`} type={showPassword ? "text" : "password"} value={form.confirmPassword} onChange={(event) => updateField("confirmPassword", event.target.value)} placeholder="Nhập lại mật khẩu" autoComplete="new-password" maxLength={72} />{errors.confirmPassword && <small className="field-error">{errors.confirmPassword}</small>}</label>
              <button type="submit" className="btn btn-primary auth-submit" disabled={loading}>{loading ? "Đang tạo tài khoản…" : "Tạo tài khoản"}</button>
            </form>

            <p className="auth-switch">Đã có tài khoản? <Link href="/login">Đăng nhập</Link></p>
          </section>
          <p className="auth-helper">Bằng việc đăng ký, bạn đồng ý sử dụng tài khoản cho mục đích quản lý bảng điểm.</p>
        </div>
      </section>
    </main>
  );
}
