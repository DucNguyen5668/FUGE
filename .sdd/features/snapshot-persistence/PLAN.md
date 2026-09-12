# Plan — Snapshot Persistence

- **Status:** DRAFT — BLOCKED BY SPEC AND DATA-LIFECYCLE REVIEW
- **Spec:** `SPEC.md` v0.1.0

## Current design

One route transaction materializes guest data into normalized SQLite tables. Other routes list, hydrate or delete snapshots, but only verify that some session exists.

## Proposed work after approval

1. Choose `403`/`404`, retention/recovery, cascade and revision semantics.
2. Centralize an owner-scoped snapshot lookup using session `userId`.
3. Apply ownership through snapshot→class→student/component chains before every mutation/read/export.
4. Add transaction and two-user regression coverage after test binding approval.
5. Keep production DB migration in a separate approved RFC/Plan.

## Expected file scope

`app/api/fg/save/route.ts`, `app/api/gradesheets/**`, shared authorization/query helper under `lib/`, `lib/db/schema.ts` only if separately approved; downstream grade/export routes coordinate with their feature plans.

## Data flow and trust boundary

Session user id is trusted server context. All route params/body ids are untrusted and must be joined to owner-scoped resources before returning or mutating PII. Schema change is a hard approval gate.

## Migration and rollback

No schema change is proposed yet. Any ownership backfill or deletion-policy migration requires row counts, backup verification and rollback before execution.

## Verification

- `npm.cmd run lint`
- `npm.cmd run build`
- Two-user isolation scenarios for list/detail/delete and child-resource operations
- Transaction rollback scenario
- Automated tests: blocked until binding approval

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Owner-scoped persistence remediation plan.
- Recommendation: Implement query-level ownership before any persistence feature expansion.
- Evidence: Noncompliant observations in Spec.
- Risks and assumptions: Schema migration may be avoidable for current rows with valid `userId`.
- Alternatives considered: Middleware-only id filtering is insufficient because ownership is data-dependent.
- Required human decision: Approve Spec/lifecycle before plan execution.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
