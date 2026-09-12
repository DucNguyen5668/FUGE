# Vòng đời SDD và Multi-Feature Registry

Trạng thái feature và đặc tả của hệ thống.

## 1. Global governance

- [`docs/fugrade-development-lifecycle.md`](../docs/fugrade-development-lifecycle.md) — Playbook vận hành chính, phân loại thay đổi và Definition of Ready/Done của FuGrade.
- [`CONSTITUTION.md`](../CONSTITUTION.md) — Hard quality gate và security rule.
- [`AGENTS.md`](../AGENTS.md) — Constitution, phạm vi và quyền tool của Agent.
- [`CLAUDE.md`](../CLAUDE.md) — Bộ nhớ kiến trúc dành cho con người.
- [`architecture-profile.md`](./architecture-profile.md) — Binding tech stack/kiến trúc, evidence và artifact gate canonical.
- [`FEATURE_INVENTORY.md`](./FEATURE_INVENTORY.md) — Bản đồ capability hiện hữu, maturity, evidence boundary và cross-feature gap.
- [`shared_context.md`](./shared_context.md) — API contract và trạng thái dùng chung.
- [`constraints/`](./constraints/) — Global, business và safety constraints.
- [`mcp-config.yaml`](./mcp-config.yaml) — MCP access control theo Agent.

---

## 2. Feature Registry

| Feature slug | Tên feature | Owner | Status | Path |
| :--- | :--- | :--- | :--- | :--- |
| `account-access` | Đăng ký, đăng nhập, CAPTCHA, Google và đăng xuất | Chưa chỉ định | REVERSE SPEC — DRAFT | [`CONTEXT`](./features/account-access/CONTEXT.md) · [`SPEC`](./features/account-access/SPEC.md) · [`PLAN`](./features/account-access/PLAN.md) · [`TASKS`](./features/account-access/TASKS.md) |
| `fg-file-compatibility` | Mở/xuất `.fg` và tương thích FuGrade desktop | Chưa chỉ định | REVERSE SPEC — DRAFT | [`CONTEXT`](./features/fg-file-compatibility/CONTEXT.md) · [`SPEC`](./features/fg-file-compatibility/SPEC.md) · [`PLAN`](./features/fg-file-compatibility/PLAN.md) · [`TASKS`](./features/fg-file-compatibility/TASKS.md) |
| `guest-grade-workspace` | Workspace bảng điểm cục bộ không cần đăng nhập | Chưa chỉ định | REVERSE SPEC — DRAFT | [`CONTEXT`](./features/guest-grade-workspace/CONTEXT.md) · [`SPEC`](./features/guest-grade-workspace/SPEC.md) · [`PLAN`](./features/guest-grade-workspace/PLAN.md) · [`TASKS`](./features/guest-grade-workspace/TASKS.md) |
| `snapshot-persistence` | Lưu và quản lý snapshot bảng điểm | Chưa chỉ định | REVERSE SPEC — DRAFT, OWNERSHIP GAP | [`CONTEXT`](./features/snapshot-persistence/CONTEXT.md) · [`SPEC`](./features/snapshot-persistence/SPEC.md) · [`PLAN`](./features/snapshot-persistence/PLAN.md) · [`TASKS`](./features/snapshot-persistence/TASKS.md) |
| `persisted-grade-management` | Sửa bảng điểm đã lưu và import hàng loạt | Chưa chỉ định | REVERSE SPEC — DRAFT, INTEGRITY GAP | [`CONTEXT`](./features/persisted-grade-management/CONTEXT.md) · [`SPEC`](./features/persisted-grade-management/SPEC.md) · [`PLAN`](./features/persisted-grade-management/PLAN.md) · [`TASKS`](./features/persisted-grade-management/TASKS.md) |
| `spreadsheet-export` | Xuất lớp đã lưu thành Excel | Chưa chỉ định | REVERSE SPEC — DRAFT, OWNERSHIP GAP | [`CONTEXT`](./features/spreadsheet-export/CONTEXT.md) · [`SPEC`](./features/spreadsheet-export/SPEC.md) · [`PLAN`](./features/spreadsheet-export/PLAN.md) · [`TASKS`](./features/spreadsheet-export/TASKS.md) |
| `bonus-grade-conversion` | Cột bonus và quy đổi sang thành phần điểm | NguyenND / Product Owner | SPEC — DRAFT, PENDING HUMAN REVIEW | [`CONTEXT`](./features/bonus-grade-conversion/CONTEXT.md) · [`SPEC`](./features/bonus-grade-conversion/SPEC.md) |

---

## 3. Cấu trúc feature chuẩn

Mỗi feature nằm trong `.sdd/features/{feature-slug}/` và có bốn artifact:

- `CONTEXT.md` — Pha 0: Context Discovery.
- `SPEC.md` — Pha 1: Executable Specification.
- `PLAN.md` — Pha 2: Architecture Plan.
- `TASKS.md` — Pha 3: Atomic Tasks Breakdown.

`CONTEXT.md` và `SPEC.md` có thể dùng core-only baseline. `PLAN.md` và `TASKS.md` cần binding/command liên quan trong Architecture Profile đã approved.
