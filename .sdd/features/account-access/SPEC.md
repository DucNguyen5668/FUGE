# Specification — Account Access

- **Version:** 0.1.0
- **Status:** DRAFT — REVERSE SPEC, PENDING HUMAN REVIEW
- **Architecture Profile:** FuGrade v0.1.0; Auth.js/SQLite bindings evidenced, feature approval pending

## Contract status

Các requirement `OBSERVED` mô tả source hiện tại, chưa phải business-approved. Các mục `REQUIRED GAP` là điều kiện an toàn cần được quyết định hoặc triển khai trước production.

## Actors and data contract

- **Guest:** chưa có session, vẫn dùng được public workspace.
- **Local user:** đăng ký/đăng nhập bằng email và password.
- **Google user:** đăng nhập qua verified Google email khi provider được cấu hình.
- Input auth gồm registration fields hoặc login/password/CAPTCHA token+answer. Session output chứa public user identity/id; password hash, CAPTCHA answer và provider secret không thuộc response contract.

## Functional requirements

### REQ-001 — Public workspace (`OBSERVED`)

**WHEN** người dùng chưa có session mở `/`, **THE FuGrade Web SHALL** cho phép dùng guest workspace và chỉ yêu cầu đăng nhập khi gọi capability persistence.

### REQ-002 — Local registration (`OBSERVED`)

**WHEN** người dùng đăng ký local, **THE FuGrade Web SHALL** chuẩn hóa email thành chữ thường, kiểm tra tên 2–80 ký tự, email hợp lệ tối đa 160 ký tự, password 8–72 ký tự có chữ thường/chữ hoa/chữ số và confirmation trùng khớp.

### REQ-003 — Registration result (`OBSERVED`)

**IF** email đã tồn tại, **THEN THE FuGrade Web SHALL** trả conflict; **OTHERWISE THE FuGrade Web SHALL** hash password và tạo user role `teacher`.

### REQ-004 — CAPTCHA (`OBSERVED`)

**WHEN** client yêu cầu CAPTCHA, **THE FuGrade Web SHALL** trả ảnh, signed token không cache và TTL 300 giây; **WHEN** credentials được xác minh trong cùng process, **THE system SHALL** chỉ chấp nhận token đúng, chưa hết hạn và nonce chưa dùng.

### REQ-005 — Credentials login (`OBSERVED`)

**WHEN** login, password và CAPTCHA hợp lệ khớp user local, **THE FuGrade Web SHALL** tạo JWT session chứa user id; **IF** bất kỳ điều kiện nào sai, **THEN THE system SHALL** từ chối đăng nhập mà không tiết lộ điều kiện nào sai.

### REQ-006 — Google login (`OBSERVED`)

**WHERE** Google provider được cấu hình, **WHEN** Google trả email đã verified, **THE FuGrade Web SHALL** dùng user hiện có hoặc tạo user role `teacher`, rồi tạo session; **IF** provider chưa cấu hình, **THEN THE UI SHALL** không cho kích hoạt nút Google.

### REQ-007 — Safe callback (`OBSERVED`)

**WHEN** login thành công, **THE FuGrade Web SHALL** chỉ redirect đến callback path nội bộ bắt đầu bằng một dấu `/`; callback thiếu hoặc không an toàn SHALL trở về `/`.

### REQ-008 — Logout (`OBSERVED`)

**WHEN** user đăng xuất tại workspace, **THE FuGrade Web SHALL** xóa session server/client nhưng giữ bản guest workspace trong phiên trình duyệt.

### REQ-009 — Distributed replay protection (`REQUIRED GAP`)

**WHERE** deployment có nhiều instance, **THE FuGrade Web SHALL** dùng replay state dùng chung hoặc ghi rõ CAPTCHA chỉ bảo vệ trên từng instance; behavior phải kiểm chứng được qua hai request vào instance khác nhau.

### REQ-010 — Account and session lifecycle (`REQUIRED GAP`)

**WHEN** the project prepares a production release, **THE Product Owner SHALL** xác định session expiry/revocation, login throttling, password recovery, email verification và account deletion/retention behavior.

## Error behavior

| Condition | Observed response |
| :--- | :--- |
| Registration payload invalid | `400`, message và field đầu tiên |
| Duplicate email | `409` |
| Invalid credentials/CAPTCHA | Login thất bại chung |
| CAPTCHA cannot be created | `500` generic message |
| Protected API without session | `401` |

## Acceptance scenarios

1. Guest mở `/` và sửa local mà không đăng nhập.
2. Local registration kiểm tra từng rule và duplicate email.
3. CAPTCHA đúng dùng được một lần trong một process; sai/hết hạn/reuse bị từ chối.
4. Credentials đúng/sai và callback nội bộ/ngoại bộ cho kết quả đúng contract.
5. Google enabled/disabled và verified/unverified email được kiểm tra tại môi trường có cấu hình.
6. Logout giữ guest workspace nhưng API protected trả `401`.

## Representative BDD

```gherkin
Scenario: Credentials login consumes a valid CAPTCHA
  Given a local user and an unused CAPTCHA token exist
  When the user submits valid login, password and CAPTCHA answer
  Then a JWT session containing the database user id is created
  And reusing the same CAPTCHA in the same process is rejected
```

## Non-functional requirements

- Password hash và token không xuất hiện trong response/log.
- Auth error không leak stack trace hoặc phân biệt account có tồn tại.
- Automated tests: N/A cho tới khi test binding được duyệt.

## Out of scope

RBAC chi tiết, admin UI, password reset, email verification và production shared CAPTCHA store implementation.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: REQ-001–REQ-010.
- Recommendation: Approve observed guest/auth behavior only after deciding REQ-009–REQ-010.
- Evidence: `CONTEXT.md` và static source review.
- Risks and assumptions: OAuth and distributed deployment remain unverified.
- Alternatives considered: Cookie/database sessions hoặc external identity service; chưa có approved binding.
- Required human decision: Approve/revise requirements and gaps.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:

## Changelog

- `0.1.0` (2026-09-12): Initial reverse specification.
