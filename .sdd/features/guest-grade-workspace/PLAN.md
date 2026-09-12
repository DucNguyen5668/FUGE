# Plan — Guest Grade Workspace

- **Status:** DRAFT — BLOCKED BY SPEC REVIEW
- **Spec:** `SPEC.md` v0.1.0

## Current design

`app/page.tsx` owns the full guest aggregate and saves it to `sessionStorage`. Child table/dialog components validate and send callbacks; immutable updates happen in the page. File and snapshot boundaries are separate HTTP calls.

## Proposed work after approval

1. Run browser scenarios for storage, editing, class switching and dialogs.
2. Decide REQ-012–REQ-013 and align grade parsing across editors.
3. Add focused tests only after the test stack is approved.
4. Refactor state ownership only if a separate plan proves the current page is unsafe to change.

## Expected file scope

`app/page.tsx`, `components/workspace/DraftGradeTable.tsx`, `components/workspace/WorkspaceDialogs.tsx`, `app/globals.css`; file/snapshot changes stay in their feature plans.

## Data flow and risks

Decrypted grade data resides in browser state and session storage. Changes can be lost at tab lifecycle boundaries. Import may partially apply valid rows, so preview and summary must stay consistent.

## Verification

- `npm.cmd run lint`
- `npm.cmd run build`
- Browser scenarios in `SPEC.md` at desktop and small viewport
- Automated tests: N/A until binding approval

## Rollback

Preserve the current storage key/schema or provide a tolerant migration before changing it; UI changes revert without server data migration.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Workspace verification and gap plan.
- Recommendation: Resolve lifecycle/import semantics before state refactor.
- Evidence: Open questions in Context/Spec.
- Risks and assumptions: No automated browser harness exists.
- Alternatives considered: Introduce a state library now; dependency/scope is unjustified without an approved behavior change.
- Required human decision: Approve/revise plan.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
