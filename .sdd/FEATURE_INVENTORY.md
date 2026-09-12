# FuGrade Web Feature Inventory

- **Version:** 1.0.0
- **Status:** DRAFT — PENDING HUMAN REVIEW
- **Evidence snapshot:** source tree at commit `e775e5600c29e7bbe717db79e8c5c804d2a47913`, reviewed 2026-09-12

## Purpose

Inventory này chia hệ thống theo outcome người dùng thay vì theo từng file hoặc nút. Mỗi capability hiện hữu có một bộ Reverse Spec trong `.sdd/features/<slug>/`. Reverse Spec ghi lại điều source đang làm; nó không tự chứng minh behavior đó đúng nghiệp vụ.

`PLAN.md` và `TASKS.md` của các Reverse Spec là kế hoạch xác minh và xử lý gap. Tất cả đều bị block cho tới khi Human Final Review chấp nhận hoặc sửa `CONTEXT.md` và `SPEC.md` tương ứng.

## Capability map

| Slug | User outcome | Primary surface | Maturity |
| :--- | :--- | :--- | :--- |
| `account-access` | Đăng ký, đăng nhập, Google sign-in, CAPTCHA và đăng xuất | `/login`, `/signup`, `/api/auth/*` | Implemented; reverse spec draft |
| `fg-file-compatibility` | Mở và xuất file `.fg` có mật khẩu | `/api/fg/import`, `/api/fg/export` | Implemented; compatibility chưa fixture-test |
| `guest-grade-workspace` | Sửa bảng điểm cục bộ mà chưa đăng nhập | `/`, workspace components | Implemented; reverse spec draft |
| `snapshot-persistence` | Lưu workspace thành snapshot và truy cập dữ liệu đã lưu | `/api/fg/save`, `/api/gradesheets*` | Partially implemented; ownership gap |
| `persisted-grade-management` | Sửa bảng điểm đã lưu, thêm dữ liệu và import hàng loạt | `/gradesheet/[id]`, grade/student/component APIs | Implemented; ownership và data-integrity gap |
| `spreadsheet-export` | Xuất một lớp đã lưu thành `.xlsx` | `/api/export` | Implemented; ownership gap |
| `bonus-grade-conversion` | Quy đổi cột bonus sang thành phần điểm | Chưa triển khai | Context approved; Spec draft |

## Cross-feature flows

```text
.fg file -> import -> guest workspace -> local edits -> export .fg
                                      -> authenticated snapshot save
snapshot -> persisted editor -> direct database mutations -> Excel export
account access -----------^             ^
bonus conversion (planned) ------------|
```

## Known gaps requiring product or architecture decision

| Gap | Affected feature | Evidence | Delivery effect |
| :--- | :--- | :--- | :--- |
| Snapshot and mutation APIs generally check authentication but do not filter resources by `userId` | snapshot, persisted editor, Excel export | `app/api/gradesheets/**`, `app/api/grades.ts`, `app/api/students/**`, `app/api/components/route.ts`, `app/api/export/route.ts` | Block multi-user production |
| CAPTCHA replay state is process-local | account access | `lib/captcha.ts` | One-time guarantee is not global in multi-instance deployment |
| `.fg` compatibility has no desktop↔web fixture evidence | file compatibility | `lib/fg-decrypt.ts`, `FuGrade/` | Block compatibility claim after crypto/schema changes |
| Guest and persisted editors parse numeric input differently | guest workspace, persisted editor | `Number(...)` versus `parseFloat(...)` | Input such as `8abc` can behave differently |
| Merge mode reuses component IDs from the first class while combining students from all classes | persisted editor | `app/gradesheet/[id]/page.tsx` | Can display or mutate mismatched grade cells |
| Comment save performs an unrelated grade PATCH before the comment PATCH | persisted editor | `components/grade-table/GradeTable.tsx` | Can create an invalid/orphan grade record |
| No automated test command exists | all | `package.json`, Architecture Profile | Acceptance requires manual evidence until test stack is approved |

## Documentation policy

- `Observed` means confirmed by static source inspection only.
- `Required gap` means a Constitution/safety requirement not satisfied by observed code.
- `Unknown` means no reliable evidence was found; it is not an implicit requirement.
- A human may approve, revise or reject each feature boundary and behavior in `Human Final Review`.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Feature inventory and reverse-spec boundaries for the current FuGrade Web source.
- Recommendation: Accept the seven capability boundaries, then review safety gaps before approving implementation plans. Prioritize ownership enforcement and persisted-editor integrity before production use.
- Evidence: Static inspection of `app/`, `components/`, `lib/`, `package.json` and `.sdd/architecture-profile.md` at the evidence snapshot above.
- Risks and assumptions: Runtime behavior, browser compatibility and desktop `.fg` round-trip were not executed in this documentation pass.
- Alternatives considered: One monolithic system spec would hide ownership and compatibility gates; one spec per UI control would fragment the user flows.
- Required human decision: Approve or revise feature boundaries and prioritization.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
