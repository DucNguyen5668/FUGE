# FuGrade shared context và ownership

# Version: 0.1.0
# Last-Updated: 2026-08-24
# Status: PENDING HUMAN REVIEW

## 1. Ownership boundaries đề xuất

| Role | Responsibility | Primary paths |
| :--- | :--- | :--- |
| Lead/architect | Spec, plan, integration contract | `.sdd/`, `docs/`, `CLAUDE.md` |
| Web/interface | App Router pages, route boundary, middleware | `app/`, `proxy.ts` |
| UI/workspace | Grade workspace, dialogs, table, styling | `components/`, `app/globals.css` |
| Persistence/auth | Auth, CAPTCHA, SQLite/Drizzle, server mutation | `lib/auth*.ts`, `lib/captcha.ts`, `lib/db/`, `app/api/` |
| Compatibility | `.fg` types/crypto and desktop reference | `lib/fg-*.ts`, `FuGrade/` |
| Verification | Lint/build and future automated tests | `tests/` (future), configured commands only |

Không chạy parallel nếu hai role cần sửa cùng file. Lead giữ shared-file ownership và tổng hợp contract.

## 2. Current contracts

- Guest workspace state: `sessionStorage` key `fugrade_guest_workspace_v1`.
- `.fg` import/export boundary: `/api/fg/import`, `/api/fg/export`.
- Auth boundary: `/api/auth/*`; save snapshot requires authenticated user.
- Snapshot persistence: normalized `gradesheets → classes → grade_components/students → grades`.
- Production database contract chưa frozen.

## 3. Active blockers

- Architecture Profile chưa được Human Final Review.
- Chưa có automated test command.
- Production persistence chưa chọn.
- Snapshot ownership enforcement cần được đặc tả và sửa trước multi-user production.
