# Specification — Spreadsheet Export

- **Version:** 0.1.0
- **Status:** DRAFT — REVERSE SPEC, PENDING HUMAN REVIEW
- **Architecture Profile:** ExcelJS binding evidenced; owner policy pending

## Contract status

REQ-001–REQ-005 capture the current single-class export. REQ-006–REQ-009 are required gaps before production acceptance.

## Actors and data contract

- **Snapshot owner:** authenticated user requesting a workbook for one owned class.
- **Spreadsheet consumer:** opens the downloaded workbook outside FuGrade.
- Input is an owner-scoped gradesheet/class identity. Output is one XLSX worksheet containing class metadata-derived name, fixed identity/comment columns and ordered component columns.

## Functional requirements

### REQ-001 — Authentication (`OBSERVED INCOMPLETE`)

**WHEN** a guest requests Excel export, **THE endpoint SHALL** return `401`; ownership is additionally required by REQ-006.

### REQ-002 — Input (`OBSERVED`)

**WHEN** `gradesheetId` or `classId` is missing/invalid, **THE endpoint SHALL** return `400`; **IF** the class is not linked to that gradesheet, **THEN THE endpoint SHALL** return `404`.

### REQ-003 — Workbook shape (`OBSERVED`)

**WHEN** export succeeds, **THE endpoint SHALL** create one worksheet with header `#`, `Roll`, `Name`, `Comment` followed by components in stored order.

### REQ-004 — Row values (`OBSERVED`)

**WHEN** the endpoint writes each student in the class, **THE worksheet SHALL** add sequential index, roll, name, empty string for null comment, and each component grade or empty string.

### REQ-005 — Download (`OBSERVED`)

**WHEN** generation succeeds, **THE endpoint SHALL** return XLSX content as an attachment named from subject and class.

### REQ-006 — Ownership (`REQUIRED GAP`)

**WHEN** an authenticated user requests export, **THE endpoint SHALL** verify the gradesheet belongs to that session user before reading class/student/grade data.

### REQ-007 — Safe Excel text (`REQUIRED GAP`)

**WHEN** filename, worksheet name or cell text contains unsupported/control/formula-leading content, **THE approved contract SHALL** sanitize or encode it so the workbook is valid and does not execute unintended formulas when opened.

### REQ-008 — Merge behavior (`REQUIRED GAP`)

**WHEN** the UI is in merged-class mode, **THE product SHALL** explicitly export all merged rows, export separate sheets or disable export; it SHALL NOT silently imply merged export while returning only the first class.

### REQ-009 — Scale (`REQUIRED GAP`)

**WHEN** the project prepares production acceptance, **THE Product Owner SHALL** define maximum students/components and response-time/memory target; implementation SHALL be measured against representative data.

## Error behavior

| Condition | Result |
| :--- | :--- |
| Missing session | `401` |
| Missing/invalid query ids | `400` |
| Class not in gradesheet | `404` |
| Non-owner | Required `403`/`404` per snapshot policy |
| Generation failure | Sanitized 5xx without workbook/PII detail |

## Acceptance scenarios

1. Owner exports typical and empty class; workbook columns/order/values match database.
2. Non-owner direct id cannot export.
3. Missing, mismatched and absent ids return contract errors.
4. Unicode, null values and dangerous leading characters open safely in approved spreadsheet apps.
5. Merge-mode action follows REQ-008 decision.
6. Representative maximum dataset meets REQ-009 target.

## Representative BDD

```gherkin
Scenario: Export an owned class
  Given an authenticated owner has a class with ordered components and students
  When the owner requests Excel export for that class
  Then one XLSX worksheet is downloaded
  And every student grade appears under the matching component column
```

## Non-functional requirements

- Response must use the correct XLSX media type.
- Errors/logs must not leak student data, SQL detail or stack traces.
- Generation must not mutate snapshot data.

## Out of scope

`.fg` export, Excel import, charts/formulas, multiple formatting themes and production DB migration.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: REQ-001–REQ-009.
- Recommendation: Approve only after ownership and merge/export expectations are fixed.
- Evidence: `CONTEXT.md` and route/page source.
- Risks and assumptions: Spreadsheet application behavior and scale remain unverified.
- Alternatives considered: One worksheet per class or CSV; behavior/format change requires product approval.
- Required human decision: REQ-007–REQ-009 and ownership disclosure policy.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:

## Changelog

- `0.1.0` (2026-09-12): Initial reverse specification.
