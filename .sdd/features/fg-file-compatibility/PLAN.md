# Plan — `.fg` File Compatibility

- **Status:** DRAFT — BLOCKED BY SPEC REVIEW
- **Spec:** `SPEC.md` v0.1.0

## Current design

Client dialogs send file/password to public route handlers. `lib/fg-decrypt.ts` owns compatibility crypto, parsing and normalization. No fixture or automated test harness is configured.

## Proposed work after approval

1. Curate non-sensitive fixtures covering the matrix in REQ-009.
2. Build a field-by-field round-trip verifier without changing crypto/schema.
3. Decide plaintext fallback and lossy-normalization policy.
4. Remove or reconcile the unused `UploadFgModal` only in a separately reviewed cleanup task.

## Expected file scope

`lib/fg-decrypt.ts`, `lib/fg-types.ts`, `app/api/fg/**`, `components/workspace/FgFileDialogs.tsx`, future fixture/test paths and desktop reference under `FuGrade/`.

## Data flow and risks

Uploaded bytes and password cross browser/server boundaries. Fixture content must contain synthetic identities only. A wrong change can make existing files unreadable, so changes require before/after fixture evidence and compatibility rollback.

## Verification

- `npm.cmd run lint`
- `npm.cmd run build`
- Desktop↔web matrix in `SPEC.md`
- Automated command: blocked until test binding is approved

## Rollback

Preserve the last compatible implementation and fixtures; do not migrate or overwrite user files in place.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Compatibility verification plan.
- Recommendation: Approve fixture creation before any implementation refactor.
- Evidence: Missing fixture evidence recorded in Context/Spec.
- Risks and assumptions: Access to a working desktop build may be required.
- Alternatives considered: Web-only round trip is cheaper but cannot establish desktop compatibility.
- Required human decision: Approve fixture sources and expected compatibility versions.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
