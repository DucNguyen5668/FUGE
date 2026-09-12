# Plan — Account Access

- **Status:** DRAFT — BLOCKED BY SPEC REVIEW
- **Spec:** `SPEC.md` v0.1.0

## Current design

Auth.js owns providers/session callbacks; SQLite stores local/Google users; CAPTCHA uses signed stateless payload plus process-local nonce replay state; `proxy.ts` applies the public/protected route boundary.

## Proposed work after approval

1. Turn REQ-001–REQ-008 into manual and automated acceptance cases after a test stack is approved.
2. Decide REQ-009–REQ-010 in a production-auth RFC/Spec update.
3. If multi-instance is selected, design a shared CAPTCHA replay adapter with migration/rollback notes.
4. Review error logging and route protection against the approved contract.

## Expected file scope

`lib/auth.ts`, `lib/captcha.ts`, `proxy.ts`, `app/login/page.tsx`, `app/signup/page.tsx`, `app/api/auth/**`; persistence changes require a separately approved plan.

## Trust boundaries and risks

Browser input, OAuth provider claims, signed CAPTCHA payload and database identity cross trust boundaries. Never log passwords, CAPTCHA answers/tokens, OAuth secrets or session tokens.

## Verification

- `npm.cmd run lint`
- `npm.cmd run build`
- Manual scenarios in `SPEC.md`
- Automated tests: N/A until binding is approved

## Rollback

Documentation-only now. Any later auth/storage change needs its own rollback covering sessions and user records.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Verification and gap-resolution plan.
- Recommendation: Do not execute until Spec and production auth decisions are approved.
- Evidence: `SPEC.md` planning gaps.
- Risks and assumptions: Shared store and session policy are unresolved.
- Alternatives considered: Retain single-instance CAPTCHA as a documented deployment constraint.
- Required human decision: Approve/revise plan after Spec review.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
