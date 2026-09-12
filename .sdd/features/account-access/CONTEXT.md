# Context — Account Access

- **Feature:** `account-access`
- **Mode:** Brownfield Reverse Spec
- **Status:** DRAFT — PENDING HUMAN REVIEW
- **Evidence date:** 2026-09-12

## Problem and actor

Giảng viên có thể tạo tài khoản, đăng nhập bằng credentials hoặc Google, rồi lưu và chỉnh sửa dữ liệu phía server. Khách vẫn được dùng workspace tại `/`; đăng nhập chỉ là gate cho persistence. Behavior này đã có code nhưng chưa có Spec thống nhất về session lifecycle, abuse protection và account lifecycle.

## Scope

In scope: đăng ký, credentials login, CAPTCHA, Google sign-in, JWT session, callback nội bộ và logout. Out of scope: phân quyền chi tiết, quên/đổi mật khẩu, xác minh email, khóa tài khoản và quản trị user.

## Observed evidence

- `app/login/page.tsx`: credentials/Google login, callback chỉ chấp nhận path bắt đầu `/` và không bắt đầu `//`.
- `app/signup/page.tsx`, `app/api/auth/register/route.ts`: đăng ký local và validation bằng Zod.
- `lib/auth.ts`: Credentials + Google providers, JWT session, user Google được tạo với role `teacher`.
- `lib/captcha.ts`, `app/api/auth/captcha/route.ts`: challenge HMAC, TTL 5 phút, nonce dùng một lần trong process.
- `proxy.ts`: `/`, login/signup, auth và `.fg` import/export là public; route khác cần token.
- `app/page.tsx`: logout không xóa guest workspace.

## Unknowns and risks

- Chưa có quyết định về session expiry/revocation, login throttling, password reset, email verification và account deletion.
- CAPTCHA nonce nằm trong memory nên replay protection không đồng nhất giữa instance và mất khi restart.
- Google provider phụ thuộc runtime configuration; chưa có evidence kiểm thử callback thực tế.
- Role `teacher` được gán nhưng chưa có policy sử dụng role.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Reverse Context cho account access hiện hữu.
- Recommendation: Giữ guest-first flow; xác nhận account lifecycle và abuse policy trước production; không coi CAPTCHA hiện tại là one-time toàn cục.
- Evidence: Các file source liệt kê trong `Observed evidence`.
- Risks and assumptions: Chỉ static inspection; không chạy OAuth hoặc đăng nhập thật.
- Alternatives considered: Buộc login trước khi mở workspace; behavior này khác UX hiện tại và cần product decision.
- Required human decision: Xác nhận guest-first flow và chọn các capability account còn thiếu.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
