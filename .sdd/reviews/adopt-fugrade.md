# Brownfield Adoption Review — FuGrade Web

**Ngày:** 2026-08-24  
**Toolkit:** `SDDADD-main` v1.2 governance  
**Scope:** Repository `FUGE/web`

## Findings

1. Đã tích hợp 25 project-local slash skills vào `.claude/skills/` cùng protocol dùng chung.
2. Đã thêm `.sdd/`, constraints, MCP ownership profile, constitution và hướng dẫn vận hành.
3. Repository evidence xác nhận Next.js 16.3.2, React 19.2.8, TypeScript, Auth.js v5, Zod, Drizzle và SQLite.
4. Template giả định Clean Architecture trong `src/`, nhưng source FuGrade dùng `app/`, `components/`, `lib/`; tự động di chuyển sẽ có rủi ro cao.
5. Lint/build đã có command và evidence; automated test runner chưa tồn tại.
6. SQLite local, CAPTCHA nonce in-memory và thiếu ownership filter ở một số snapshot API là blocker cho multi-user production trên Vercel.
7. Script `adopt.ps1` ban đầu không parse trên Windows PowerShell 5.1 do UTF-8 không BOM và chuỗi output kết thúc bằng backslash; đã chuẩn hóa encoding và sửa output string.

## AI Agent Recommendation
- Status: PENDING HUMAN REVIEW
- Scope: Áp dụng SDD + ADD cho FuGrade brownfield
- Recommendation: Dùng skill pack cho feature mới và thay đổi business lớn; giữ layout Next.js hiện tại; tạo Reverse Spec cho `fg-file-compatibility`, `guest-workspace` và `snapshot-persistence`; chọn Postgres/test stack trước production hardening.
- Evidence: `.sdd/architecture-profile.md`, repository manifests/config/source, kết quả adoption và inventory 25 `SKILL.md`.
- Risks and assumptions: `CONSTITUTION.md` vẫn chứa target Clean Architecture từ starter; structural rule này chưa phù hợp source và cần Human quyết định/RFC trước khi enforce. Chưa kiểm chứng skill discovery trong một phiên Claude Code mới.
- Alternatives considered: Chỉ copy skill mà không profile hóa (dễ sinh code sai stack); hoặc tái cấu trúc toàn bộ ngay (scope/risk quá lớn).
- Required human decision: Approve hoặc revise Architecture Profile v0.1.0; chọn production DB và test framework; quyết định có tạo ba Reverse Spec đề xuất hay không.

## Human Final Review
- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
