# Vòng đời SDD và Multi-Feature Registry

Trạng thái feature và đặc tả của hệ thống.

## 1. Global governance

- [`docs/fugrade-development-lifecycle.md`](../docs/fugrade-development-lifecycle.md) — Playbook vận hành chính, phân loại thay đổi và Definition of Ready/Done của FuGrade.
- [`CONSTITUTION.md`](../CONSTITUTION.md) — Hard quality gate và security rule.
- [`AGENTS.md`](../AGENTS.md) — Constitution, phạm vi và quyền tool của Agent.
- [`CLAUDE.md`](../CLAUDE.md) — Bộ nhớ kiến trúc dành cho con người.
- [`architecture-profile.md`](./architecture-profile.md) — Binding tech stack/kiến trúc, evidence và artifact gate canonical.
- [`shared_context.md`](./shared_context.md) — API contract và trạng thái dùng chung.
- [`constraints/`](./constraints/) — Global, business và safety constraints.
- [`mcp-config.yaml`](./mcp-config.yaml) — MCP access control theo Agent.

---

## 2. Feature Registry

| Feature slug | Tên feature | Owner | Status | Path |
| :--- | :--- | :--- | :--- | :--- |
| `bonus-grade-conversion` | Cột bonus và quy đổi sang thành phần điểm | NguyenND / Product Owner | SPEC — DRAFT, PENDING HUMAN REVIEW | [`CONTEXT`](./features/bonus-grade-conversion/CONTEXT.md) · [`SPEC`](./features/bonus-grade-conversion/SPEC.md) |

---

## 3. Cấu trúc feature chuẩn

Mỗi feature nằm trong `.sdd/features/{feature-slug}/` và có bốn artifact:

- `CONTEXT.md` — Pha 0: Context Discovery.
- `SPEC.md` — Pha 1: Executable Specification.
- `PLAN.md` — Pha 2: Architecture Plan.
- `TASKS.md` — Pha 3: Atomic Tasks Breakdown.

`CONTEXT.md` và `SPEC.md` có thể dùng core-only baseline. `PLAN.md` và `TASKS.md` cần binding/command liên quan trong Architecture Profile đã approved.
