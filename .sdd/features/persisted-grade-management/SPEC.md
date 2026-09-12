# Specification — Persisted Grade Management

- **Version:** 0.1.0
- **Status:** DRAFT — REVERSE SPEC, PENDING HUMAN REVIEW
- **Architecture Profile:** Next.js/SQLite local evidenced; ownership and production persistence unresolved

## Contract status

Observed UI behavior is captured below. Requirements marked `REQUIRED GAP` replace unsafe or ambiguous current behavior and must be resolved before implementation approval.

## Actors and data contract

- **Snapshot owner/editor:** authenticated user allowed to read and mutate the selected snapshot.
- **Non-owner:** authenticated user with no access to another user's resource ids.
- Editor data is a snapshot aggregate of classes, ordered component identities, students and grade maps keyed by component id. Mutation ids are valid only when owner and same-class relationships are proven server-side.

## Functional requirements

### REQ-001 — Load editor (`OBSERVED INCOMPLETE`)

**WHEN** an authenticated user opens `/gradesheet/{id}`, **THE UI SHALL** load classes/components/students/grades, select the first class and show its components; owner verification is required by REQ-010.

### REQ-002 — Class and column selection (`OBSERVED`)

**WHEN** the user changes class, **THE UI SHALL** select it and reset visible components to that class; component toggles SHALL affect display only.

### REQ-003 — Search (`OBSERVED`)

**WHEN** the user enters a query, **THE table SHALL** filter case-insensitively by MSSV or student name; Escape SHALL clear the query.

### REQ-004 — Direct grade edit (`OBSERVED INCOMPLETE`)

**WHEN** a grade cell is committed, **THE client SHALL** send a nullable value and **THE API SHALL** accept only numbers from 0 through 10, then update or create the grade row; relation/ownership validation is required by REQ-010–REQ-011.

### REQ-005 — Comment edit (`REQUIRED GAP`)

**WHEN** a comment is committed, **THE UI SHALL** call only the comment mutation; **THE API SHALL** update the owner-scoped student and SHALL NOT create or update a grade row.

### REQ-006 — Add student (`OBSERVED INCOMPLETE`)

**WHEN** a unique MSSV is submitted for a class, **THE API SHALL** uppercase/trim it, create the student and one null grade per class component; atomicity and owner validation are required by REQ-010 and REQ-012.

### REQ-007 — Add component (`OBSERVED INCOMPLETE`)

**WHEN** a unique component name is submitted for a class, **THE API SHALL** append it in order and create one null grade per class student; atomicity and owner validation are required by REQ-010 and REQ-012.

### REQ-008 — Bulk import (`OBSERVED, POLICY GAP`)

**WHEN** rows are imported, **THE API SHALL** match MSSV case-insensitively within the selected class, apply valid grades/comments and return per-row `ok`, `not_found` or `invalid`; **THE Product Owner SHALL** decide whether any invalid row rejects the whole batch or partial success is allowed.

### REQ-009 — Clear component (`OBSERVED INCOMPLETE`)

**WHEN** a user confirms clear, **THE API SHALL** set every grade for the selected component to `null` without deleting component/student rows; owner validation is required by REQ-010.

### REQ-010 — Ownership (`REQUIRED GAP`)

**WHEN** any detail or mutation request supplies snapshot/class/student/component ids, **THE API SHALL** verify the full resource chain belongs to the session user before reading or mutating data.

### REQ-011 — Relation integrity (`REQUIRED GAP`)

**WHEN** a grade is created/updated, **THE API SHALL** verify student and component belong to the same class; **THE UI SHALL NOT** reuse component ids across classes merely because names match.

### REQ-012 — Mutation atomicity (`REQUIRED GAP`)

**WHEN** add-student/add-component/bulk-import changes multiple rows, **THE approved contract SHALL** define transaction boundaries and partial-failure response.

### REQ-013 — Numeric grammar (`REQUIRED GAP`)

**WHEN** the user submits a grade, **THE guest and persisted editors SHALL** share an explicit grammar; strings containing non-numeric suffixes SHALL be accepted or rejected consistently.

### REQ-014 — Merge classes (`REQUIRED GAP`)

**WHEN** classes have equivalent component-name sequences and the user enables merge, **THE UI SHALL** map grades by component identity/name without cross-class ids; mutation in merged mode SHALL be explicitly disabled or mapped safely per an approved product decision.

## Noncompliant observations

- Current comment flow sends an unrelated `componentId=-1` grade mutation.
- Current merge flow combines students with first-class component ids.
- Current APIs accept child ids without owner/same-class validation.

## Error behavior

| Condition | Required behavior |
| :--- | :--- |
| No session | `401` |
| Non-owner or mismatched relation | Approved `403`/`404`; no mutation |
| Grade outside grammar/range | `400` with stable sanitized error |
| Duplicate student/component | `409` |
| Batch contains errors | Atomic or partial result per REQ-008/REQ-012 decision |
| Database failure | Sanitized 5xx; no partial graph when atomicity applies |

## Acceptance scenarios

1. Owner loads and edits one class; non-owner direct ids cannot read/mutate.
2. Grade boundaries, null and invalid suffix follow one shared grammar.
3. Comment edit changes only comment and creates no grade row.
4. Add student/component is complete or fully rolled back on injected failure.
5. Bulk import covers duplicate, invalid, missing MSSV and policy-selected partial behavior.
6. Clear sets only owner-scoped selected component grades to null.
7. Merge two compatible classes displays correct values and follows approved mutation policy.
8. Mismatched student/component ids are rejected.

## Representative BDD

```gherkin
Scenario: Comment edit does not mutate grades
  Given the owner opened a persisted student row
  When the owner commits a new comment
  Then only that student's comment is changed
  And no grade row is created or updated
```

```gherkin
Scenario: Merge view keeps class identities isolated
  Given two classes have the same component-name sequence and different component ids
  When the owner enables merge mode
  Then each student's value is resolved from that student's class component
  And no cross-class mutation is issued
```

## Non-functional requirements

- Mutation responses must not leak PII, SQL/driver detail or stack traces.
- Table/dialogs must be keyboard usable and scroll at small viewports.
- Multi-row operations need bounded, reviewable transaction behavior.

## Out of scope

Snapshot lifecycle, `.fg` compatibility, Excel generation, grading formulas and production DB migration.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: REQ-001–REQ-014.
- Recommendation: Prioritize REQ-005 and REQ-010–REQ-014; treat current merge/edit behavior as unsafe until verified and corrected.
- Evidence: `CONTEXT.md` and route/component data flow.
- Risks and assumptions: Database foreign-key enforcement was not inspected at runtime.
- Alternatives considered: Make persisted editor read-only while retaining all editing in guest workspace.
- Required human decision: Merge mutation, import atomicity, error disclosure and numeric grammar.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:

## Changelog

- `0.1.0` (2026-09-12): Initial reverse specification with integrity/ownership gaps.
