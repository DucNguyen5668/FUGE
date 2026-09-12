# Tasks — Snapshot Persistence

**Status:** NOT STARTED — BLOCKED BY SPEC/PLAN REVIEW

- [ ] `SNP-T01` — Approve snapshot append/update, `403`/`404`, retention, recovery and cascade semantics.
- [ ] `SNP-T02` — Inventory every parent/child access path and trace it to session owner.
- [ ] `SNP-T03` — Implement owner-scoped list/detail/delete queries without schema mutation if possible.
- [ ] `SNP-T04` — Apply the approved ownership helper to downstream edit/export APIs.
- [ ] `SNP-T05` — Verify transaction rollback and two-user isolation with an approved test setup.
- [ ] `SNP-T06` — Run lint/build, review diff and complete requirement/security trace.

No task is completed by this Reverse Spec pass.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Recommendation: Enforce owner scope before feature expansion or production use.
- Evidence: `SPEC.md` and `PLAN.md`.
- Risks and assumptions: Lifecycle and production DB remain unresolved.
- Alternatives considered: Disable persisted access while gaps remain.
- Required human decision: Approve/revise Tasks after Plan review.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
