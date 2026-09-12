# Brownfield Reverse-Spec Review — Current FuGrade Features

- **Date:** 2026-09-12
- **Scope:** Source-backed feature baseline at commit `e775e5600c29e7bbe717db79e8c5c804d2a47913`
- **Status:** PENDING HUMAN REVIEW

## Artifacts created

| Feature | Reverse Spec artifacts |
| :--- | :--- |
| `account-access` | Context, Spec, Plan, Tasks |
| `fg-file-compatibility` | Context, Spec, Plan, Tasks |
| `guest-grade-workspace` | Context, Spec, Plan, Tasks |
| `snapshot-persistence` | Context, Spec, Plan, Tasks |
| `persisted-grade-management` | Context, Spec, Plan, Tasks |
| `spreadsheet-export` | Context, Spec, Plan, Tasks |

The planned `bonus-grade-conversion` remains on its existing gate: Context is approved, Spec is draft, and Plan/Tasks must wait for Spec approval.

## Evidence boundary

The baseline was produced through static inspection of current source/configuration. It establishes implementation evidence, not runtime correctness or business approval. No local database, user file, OAuth provider or production environment was accessed.

## Material findings

1. Snapshot, persisted-editor and Excel APIs lack owner-scoped resource checks after authentication.
2. Persisted merge mode combines first-class component ids with students from all classes.
3. Comment save performs an unrelated grade mutation before updating the comment.
4. Guest and persisted editors use different numeric parsing semantics.
5. `.fg` desktop compatibility has no recorded cross-runtime fixture result.
6. CAPTCHA one-time state is process-local.
7. No automated test command has been approved.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Feature boundaries, observed contracts, required gaps and draft execution artifacts listed above.
- Recommendation: Accept the reverse-spec inventory as a documentation baseline, then review each Spec. Address ownership and persisted-editor integrity before production feature growth.
- Evidence: `.sdd/FEATURE_INVENTORY.md`, the six feature folders and source paths cited by each Context.
- Risks and assumptions: Runtime reproduction, `.fg` fixtures, browser accessibility and multi-user isolation tests remain unexecuted.
- Alternatives considered: A monolithic Spec loses per-feature gates; approving legacy behavior wholesale could legitimize known unsafe behavior.
- Required human decision: Approve/revise the six feature boundaries and choose priority/order for Spec reviews.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
