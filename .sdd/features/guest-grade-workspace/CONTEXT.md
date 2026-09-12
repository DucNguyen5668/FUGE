# Context — Guest Grade Workspace

- **Feature:** `guest-grade-workspace`
- **Mode:** Brownfield Reverse Spec
- **Status:** DRAFT — PENDING HUMAN REVIEW
- **Evidence date:** 2026-09-12

## Problem and actor

Giảng viên cần mở và chỉnh bảng điểm nhanh mà không phải tạo tài khoản. Workspace tại `/` giữ bản nháp trong browser, hỗ trợ tìm kiếm, chọn cột, sửa ô, thêm sinh viên/thành phần, import hàng loạt và xóa điểm một cột.

## Scope

In scope: state cục bộ sau khi `.fg` được mở, edit metadata/grade/comment, tìm kiếm, ẩn/hiện cột, thêm row/column, bulk paste và `sessionStorage`. `.fg` parsing/export và server snapshot thuộc feature khác.

## Observed evidence

- `app/page.tsx`: state workspace, key `fugrade_guest_workspace_v1`, dirty state, class selection và thao tác local.
- `components/workspace/DraftGradeTable.tsx`: filter MSSV/tên, double-click edit, Enter/blur save, Escape cancel, grade range 0–10.
- `components/workspace/WorkspaceDialogs.tsx`: add student/component và paste-import preview.
- Tất cả mutation trong feature này cập nhật React state; server chỉ được gọi khi mở/xuất `.fg` hoặc lưu snapshot.

## Unknowns and risks

- Không có prompt khi đóng tab/chuyển trang với `dirty=true`; `sessionStorage` lifecycle tùy browser/tab.
- Import parser tách theo tab hoặc whitespace; comment có nhiều khoảng trắng bị nối lại bằng một khoảng trắng.
- Duplicate roll/component được chặn khi thêm trực tiếp nhưng import duplicate rows không được cảnh báo; `Map` khiến dòng sau ghi đè dòng trước.
- Guest editor dùng `Number`, khác persisted editor dùng `parseFloat`.
- Chưa có product decision về maximum students/components/rows hoặc accessibility focus trap.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Reverse Context cho workspace khách.
- Recommendation: Giữ guest-first workflow, nhưng xác nhận duplicate import, whitespace và dirty-state recovery trước khi lock Spec.
- Evidence: Source trong `Observed evidence`.
- Risks and assumptions: Chưa chạy browser acceptance hoặc kiểm tra nhiều tab.
- Alternatives considered: Persist draft bằng IndexedDB hoặc server; thay đổi retention/privacy nên cần Spec riêng.
- Required human decision: Xác nhận lifecycle bản nháp và quy tắc import.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
