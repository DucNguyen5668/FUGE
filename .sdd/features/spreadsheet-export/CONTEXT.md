# Context — Spreadsheet Export

- **Feature:** `spreadsheet-export`
- **Mode:** Brownfield Reverse Spec with ownership gap
- **Status:** DRAFT — PENDING HUMAN REVIEW
- **Evidence date:** 2026-09-12

## Problem and actor

Người dùng đã đăng nhập có thể tải một lớp trong snapshot thành workbook `.xlsx` để tiếp tục xử lý ngoài FuGrade.

## Scope

In scope: chọn snapshot/class, tạo workbook, columns/rows, download filename và authorization. Out of scope: `.fg` export, workbook import, styling nâng cao và merged-class export.

## Observed evidence

- `app/gradesheet/[id]/page.tsx`: nút Export mở `/api/export?gradesheetId=...&classId=...`; merge mode vẫn chọn class đầu.
- `app/api/export/route.ts`: yêu cầu session; xác minh class thuộc gradesheet id; load components/students/grades; tạo một worksheet bằng ExcelJS.
- Header là `#`, `Roll`, `Name`, `Comment`, rồi components; mỗi student là một row; column width cố định 14.

## Unknowns and risks

- Endpoint không xác minh gradesheet thuộc session user.
- Filename/worksheet name lấy từ subject/class; chưa có contract sanitize ký tự Excel/header.
- Merge mode không export dữ liệu gộp mà export class đầu, có thể trái kỳ vọng UI.
- N+1 grade query theo student có thể chậm với lớp lớn; chưa có performance target.
- Chưa kiểm tra Unicode, formula-like text, Excel limits, null grades hoặc workbook mở bằng Excel/LibreOffice.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Reverse Context cho `.xlsx` export.
- Recommendation: Xác nhận single-class/merge behavior và enforce ownership trước production.
- Evidence: Page and route source.
- Risks and assumptions: Workbook chưa được tạo/mở trong pass này.
- Alternatives considered: Client-side export hoặc export toàn workbook nhiều sheet; đều là behavior mới.
- Required human decision: Chọn merge export, filename/worksheet sanitization và scale target.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
