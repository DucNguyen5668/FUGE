# Context — Persisted Grade Management

- **Feature:** `persisted-grade-management`
- **Mode:** Brownfield Reverse Spec with integrity gaps
- **Status:** DRAFT — PENDING HUMAN REVIEW
- **Evidence date:** 2026-09-12

## Problem and actor

Người dùng đã đăng nhập có một editor riêng tại `/gradesheet/[id]` để xem/sửa snapshot trong database, chọn hoặc gộp lớp, thêm sinh viên/thành phần, import điểm/nhận xét và xóa điểm một cột. Luồng này mutation trực tiếp server, khác với guest workspace.

## Scope

In scope: detail page, grade table, add/import dialogs và component/student/grade/comment mutations. Snapshot creation/deletion và Excel generation thuộc Specs khác.

## Observed evidence

- `app/gradesheet/[id]/page.tsx`: load classes, select/merge classes, component visibility và action toolbar.
- `components/grade-table/GradeTable.tsx`: search and direct grade/comment mutation.
- `components/modals/{ImportMarkModal,AddStudentModal,AddComponentModal}.tsx`: persisted dialogs.
- `app/api/{grades,students,components}/**`: authenticated DB mutations.
- `app/api/gradesheets/[id]/route.ts`: detail aggregate consumed by page.

## Integrity and security gaps

- Mọi route chỉ kiểm tra có session; không chứng minh snapshot/class/student/component thuộc user.
- Merge mode lấy component object/id của lớp đầu nhưng ghép student từ mọi lớp. Grade maps dùng database component id, nên lớp sau có thể hiển thị sai và edit có thể tạo liên kết cross-class.
- Comment save gọi grade PATCH với `componentId=-1` trước comment PATCH; response đầu bị bỏ qua và có thể tạo row grade không hợp lệ.
- Client/server dùng `parseFloat`, nên chuỗi có hậu tố như `8abc` có thể thành `8`; guest editor dùng `Number` và từ chối.
- Add student/component tạo nhiều row không bọc transaction; failure giữa chừng có thể để graph thiếu.
- Bulk import có thể chạy không cần preview và server áp dụng từng dòng, tạo partial success.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Reverse Context cho persisted editor.
- Recommendation: Block production mutation đến khi ownership, merge integrity và comment-save path được đặc tả/sửa.
- Evidence: Source trong `Observed evidence`.
- Risks and assumptions: Gaps được suy ra từ static control/data flow; chưa mutation database để tái hiện.
- Alternatives considered: Tạm disable merge mode và persisted editor mutation, vẫn cho read-only owner-scoped view.
- Required human decision: Xác nhận merge outcome, partial import policy và numeric grammar.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
