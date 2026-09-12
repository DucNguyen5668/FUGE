# Context — Snapshot Persistence

- **Feature:** `snapshot-persistence`
- **Mode:** Brownfield Reverse Spec with safety gaps
- **Status:** DRAFT — PENDING HUMAN REVIEW
- **Evidence date:** 2026-09-12

## Problem and actor

Người dùng đã đăng nhập có thể lưu toàn bộ guest workspace thành snapshot SQLite và mở dữ liệu đã lưu qua route/API. Persistence chứa bảng điểm và danh tính sinh viên, vì vậy identity, ownership, retention, recovery và deletion phải là contract rõ ràng.

## Scope

In scope: tạo snapshot từ `.fg` aggregate, list/detail/delete snapshot và relational schema liên quan. Direct grade edits và Excel export thuộc feature khác.

## Observed evidence

- `app/page.tsx`: nút Lưu gọi `POST /api/fg/save`; `401` chuyển sang login và giữ local draft.
- `app/api/fg/save/route.ts`: yêu cầu numeric session user id, normalize payload và insert toàn bộ graph trong transaction; mỗi lần save tạo gradesheet mới.
- `app/api/gradesheets/route.ts`: yêu cầu session nhưng list toàn bộ gradesheet trong DB.
- `app/api/gradesheets/[id]/route.ts`: yêu cầu session nhưng read/delete theo id không kiểm tra owner.
- `lib/db/schema.ts`: `gradesheets.userId`; child tables dùng cascade delete; không có revision/audit/retention metadata.
- Không thấy UI điều hướng/list snapshot từ `/`; detail page chỉ hoạt động nếu biết `/gradesheet/<id>`.

## Safety gaps

- User đã đăng nhập có thể đọc/list/xóa snapshot của user khác nếu biết hoặc đoán id.
- API con dùng class/student/component id cũng chưa chứng minh ownership chain.
- Deletion cascade hiện hữu chưa có approved retention/recovery policy theo Constitution.
- Không có optimistic concurrency, version history, backup/restore hoặc audit policy.
- SQLite local chưa phải production persistence binding.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Reverse Context cho snapshot storage.
- Recommendation: Xem feature là chưa sẵn sàng multi-user; phê duyệt owner-scoped contract trước khi sửa API hoặc đưa production.
- Evidence: Routes/schema trong `Observed evidence`.
- Risks and assumptions: Không truy cập database local hoặc chạy mutation trong pass này.
- Alternatives considered: Tắt list/detail/delete và chỉ giữ append-only save cho tới khi ownership được sửa.
- Required human decision: Chọn ownership, retention, deletion/recovery và snapshot revision semantics.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
