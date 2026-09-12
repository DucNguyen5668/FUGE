# Plan — Persisted Grade Management

- **Status:** DRAFT — BLOCKED BY SPEC AND OWNERSHIP REVIEW
- **Spec:** `SPEC.md` v0.1.0

## Current design

The page hydrates a relational aggregate, then client components call separate mutation routes by numeric child ids. Merge mode creates a view model from first-class components and all students. Server routes query/mutate directly without an owner-scoped usecase boundary.

## Proposed work after approval

1. Reproduce comment and merge findings against a disposable, synthetic local database.
2. Reuse the approved snapshot ownership boundary for all child operations.
3. Remove the unrelated comment grade PATCH and validate its response.
4. Decide merge mutation behavior; map by explicit class/component identity or make merge read-only.
5. Unify numeric parser and batch transaction semantics.
6. Add two-user/relation/rollback regression coverage after test-stack approval.

## Expected file scope

`app/gradesheet/[id]/page.tsx`, `components/grade-table/GradeTable.tsx`, `components/modals/**`, `app/api/grades/route.ts`, `app/api/students/**`, `app/api/components/route.ts`, approved shared authorization/validation helpers.

## Trust boundaries and risks

All numeric ids and row values from browser are untrusted. Server must bind them to session owner and same class. Multi-row writes require transaction boundaries; no production or user database may be used for reproduction.

## Migration and rollback

No schema change is planned. If invalid grade rows already exist, create a separate reviewed data-repair plan with preview counts and backup; do not silently delete them.

## Verification

- `npm.cmd run lint`
- `npm.cmd run build`
- Synthetic two-user and cross-class scenarios from Spec
- Browser checks for editor, dialogs and merge view
- Automated command: pending test binding

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Integrity/ownership remediation plan.
- Recommendation: Execute in order: owner boundary, comment path, relation validation, merge, parser/batch semantics.
- Evidence: Critical gaps in Context/Spec.
- Risks and assumptions: Existing invalid rows may require separately approved cleanup.
- Alternatives considered: Disable all direct server edits and export snapshots back to guest workspace.
- Required human decision: Approve behavior choices and disposable verification environment.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
