# Context — `.fg` File Compatibility

- **Feature:** `fg-file-compatibility`
- **Mode:** Brownfield Reverse Spec
- **Status:** DRAFT — PENDING HUMAN REVIEW
- **Evidence date:** 2026-09-12

## Problem and actor

Giảng viên cần mở file từ FuGrade desktop trong browser, sửa dữ liệu và xuất lại file có mật khẩu. Đây là compatibility boundary có nguy cơ làm mất dữ liệu nếu schema, encoding hoặc crypto thay đổi không đúng.

## Scope

In scope: upload/import, decrypt/parse/normalize, password verification, export/encrypt/download và schema TypeScript hiện tại. Out of scope: snapshot database, Excel export, thay crypto legacy và mở rộng schema bonus.

## Observed evidence

- `components/workspace/FgFileDialogs.tsx`: chọn/kéo thả `.fg`, hỏi password khi server yêu cầu và tải file export.
- `app/api/fg/import/route.ts`: giới hạn 10 MB; thử decrypt, sau đó fallback plaintext JSON; password required/invalid có status riêng.
- `app/api/fg/export/route.ts`: bắt buộc password, normalize payload, sanitize filename và trả binary download không cache.
- `lib/fg-decrypt.ts`, `lib/fg-types.ts`: legacy AES-256-CBC, fixed compatibility material, zero IV, MD5 password hash và schema teacher/classes/students/components/grades.
- `FuGrade/`: source desktop tham chiếu cho compatibility.

## Unknowns and risks

- Chưa có fixture round-trip desktop→web→desktop; compatibility hiện là suy luận từ source.
- Crypto/password scheme là legacy compatibility, không phải modern confidentiality guarantee.
- Normalizer bỏ field ngoài schema, chuyển grade không phải finite number thành `null`, và không enforce grade range/duplicate component/roll.
- Import log object lỗi server-side; cần bảo đảm không chứa payload hoặc detail nhạy cảm.
- `components/modals/UploadFgModal.tsx` là flow cũ không được tham chiếu và kỳ vọng response không khớp API hiện tại.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Reverse Context cho import/export `.fg`.
- Recommendation: Freeze schema/crypto hiện tại và tạo fixture desktop↔web trước mọi thay đổi compatibility.
- Evidence: Source files trong `Observed evidence`.
- Risks and assumptions: Chưa chạy fixture bằng desktop thật hoặc file đại diện.
- Alternatives considered: Format mới có versioning; cần RFC/migration và không thuộc reverse-spec này.
- Required human decision: Xác nhận plaintext fallback, giới hạn 10 MB và compatibility target cần giữ.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
