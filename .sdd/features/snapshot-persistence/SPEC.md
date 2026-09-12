# Specification — Snapshot Persistence

- **Version:** 0.1.0
- **Status:** DRAFT — REVERSE SPEC, PENDING HUMAN REVIEW
- **Architecture Profile:** SQLite local approved only as bonus-feature baseline; production persistence unresolved

## Contract status

REQ-001–REQ-004 capture implemented save behavior. REQ-005–REQ-009 are mandatory gaps derived from ownership/data-safety constraints. Existing unscoped endpoints are explicitly noncompliant observations and are not proposed requirements.

## Actors and data contract

- **Guest:** may hold a local draft but cannot access persistence.
- **Owner:** authenticated user whose session id equals `gradesheets.userId`.
- **Snapshot aggregate:** gradesheet metadata with classes, ordered components, students, comments and nullable grades. Client-supplied resource ids never establish ownership.

## Functional requirements

### REQ-001 — Authentication gate (`OBSERVED`)

**WHEN** a guest requests snapshot save/list/detail/delete, **THE API SHALL** return `401`; **WHEN** save returns `401`, **THE UI SHALL** retain the guest draft and navigate to login.

### REQ-002 — Normalize snapshot (`OBSERVED`)

**WHEN** an authenticated user saves, **THE API SHALL** normalize the incoming `.fg` aggregate and use the session user id rather than a client-supplied owner id.

### REQ-003 — Atomic graph creation (`OBSERVED`)

**WHEN** save succeeds, **THE API SHALL** create one gradesheet, its classes/components/students and matching grade rows in a single database transaction; **IF** creation fails, **THEN THE API SHALL** leave no partial graph.

### REQ-004 — Append snapshot (`OBSERVED`)

**WHEN** the same workspace is saved repeatedly, **THE API SHALL** create a new snapshot id on every successful request; successful response SHALL return that id and mark the local workspace clean.

### REQ-005 — Owner-scoped list (`REQUIRED GAP`)

**WHEN** an authenticated user lists snapshots, **THE API SHALL** return only gradesheets whose `userId` equals the session user id.

### REQ-006 — Owner-scoped detail (`REQUIRED GAP`)

**WHEN** an authenticated user requests a snapshot id, **THE API SHALL** verify ownership before returning any class, student, comment or grade; non-owned and absent ids SHALL follow a human-approved `403`/`404` disclosure policy.

### REQ-007 — Owner-scoped deletion (`REQUIRED GAP`)

**WHEN** an authenticated user requests deletion, **THE API SHALL** verify snapshot ownership before mutation and SHALL NOT delete data reachable only through a client-supplied child id.

### REQ-008 — Data lifecycle (`REQUIRED GAP`)

**WHEN** the project prepares to enable delete in production, **THE Product Owner SHALL** define retention, recovery window, cascade scope, audit evidence and hard/soft-delete policy.

### REQ-009 — Production persistence (`REQUIRED GAP`)

**WHEN** the project prepares a multi-instance production deployment, **THE Architecture Profile SHALL** approve a durable persistence binding with migration, backup and rollback behavior.

## Noncompliant observations

- Current list endpoint returns all gradesheets after authentication.
- Current detail/delete endpoints do not query `gradesheets.userId`.
- Current schema cascades child deletion without an approved lifecycle contract.

These observations must not be approved as desired behavior.

## Error behavior

| Condition | Current/required result |
| :--- | :--- |
| Missing session/user id | `401` |
| Invalid save payload or DB error | Current `422` generic message; transaction rollback expected |
| Non-owned snapshot | Required stable `403` or `404`, pending decision |
| Missing snapshot | Required `404`; current detail can return empty data |
| Delete conflict/recovery restriction | Pending lifecycle decision |

## Acceptance scenarios

1. Guest save keeps local draft and redirects to login.
2. Valid save preserves all normalized classes/components/students/grades and returns id.
3. Forced failure leaves no partial graph.
4. Repeated save creates distinct snapshot ids.
5. User A cannot list/read/delete User B data by direct id.
6. Missing id and non-owned id follow approved disclosure policy.
7. Delete respects retention/recovery/cascade policy.

## Representative BDD

```gherkin
Scenario: A user cannot read another user's snapshot
  Given snapshot 20 belongs to user B
  And user A has an authenticated session
  When user A requests snapshot 20 directly
  Then no class, student, comment or grade from snapshot 20 is returned
  And the response follows the approved non-owner disclosure policy
```

## Non-functional requirements

- No response/log contains password hash, auth token or full provider/driver error.
- Owner checks execute server-side in the same trusted data path as mutation.
- Production backup/restore evidence must meet an approved RTO/RPO; none is defined yet.

## Out of scope

Direct grade mutation UX, `.fg` encryption, Excel export, production DB selection implementation and bonus metadata.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: REQ-001–REQ-009.
- Recommendation: Approve observed save semantics only; require REQ-005–REQ-009 before multi-user production.
- Evidence: `CONTEXT.md`, route handlers and schema.
- Risks and assumptions: Database behavior was not mutated or runtime-tested.
- Alternatives considered: Disable persisted access or restrict deployment to one trusted user while remediation is pending.
- Required human decision: Ownership disclosure, deletion lifecycle and production persistence.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:

## Changelog

- `0.1.0` (2026-09-12): Initial reverse specification with ownership gaps.
