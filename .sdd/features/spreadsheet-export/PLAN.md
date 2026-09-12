# Plan — Spreadsheet Export

- **Status:** DRAFT — BLOCKED BY SPEC AND OWNERSHIP REVIEW
- **Spec:** `SPEC.md` v0.1.0

## Current design

Server route queries one class graph and builds an in-memory ExcelJS workbook. The detail page opens the route in a new window. Merge-mode context is not represented in the request.

## Proposed work after approval

1. Apply snapshot owner-scoped lookup before all export queries.
2. Decide merge-mode export and safe filename/worksheet/cell policy.
3. Replace per-student grade queries if measurement exceeds approved scale target.
4. Create synthetic workbook fixtures and inspect them in approved applications.

## Expected file scope

`app/api/export/route.ts`, `app/gradesheet/[id]/page.tsx`, approved shared owner/sanitization helpers and future test fixtures.

## Data flow and risks

PII moves from database into a downloadable file. IDs are untrusted; owner must be checked before query. Workbook text from users may affect filename, worksheet validity or spreadsheet formula interpretation.

## Verification

- `npm.cmd run lint`
- `npm.cmd run build`
- Open generated workbooks in approved spreadsheet apps
- Field-by-field fixture comparison and owner isolation
- Scale measurement after target approval

## Rollback

Export changes are stateless; revert route/page changes. No database migration is planned.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Secure, explicit workbook export plan.
- Recommendation: Implement ownership first, then merge/sanitization, then performance only with evidence.
- Evidence: Gaps in Context/Spec.
- Risks and assumptions: Formula handling policy may alter visible cell text.
- Alternatives considered: Disable export until owner-scoped query is shared.
- Required human decision: Approve plan after Spec choices.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
