# Specification — `.fg` File Compatibility

- **Version:** 0.1.0
- **Status:** DRAFT — REVERSE SPEC, PENDING HUMAN REVIEW
- **Architecture Profile:** FuGrade v0.1.0; legacy file binding evidenced, feature approval pending

## Contract status

`OBSERVED` records current source. `REQUIRED GAP` marks evidence required before claiming compatibility or changing format behavior.

## Actors

- **Guest/editor:** imports or exports a local `.fg` file.
- **FuGrade desktop:** external compatibility consumer/producer; behavior needs fixture evidence.
- **Import/export boundary:** parses, normalizes, verifies and encrypts/decrypts without creating a snapshot.

## Data contract

Normalized payload contains `Login`, `Semester`, `Version`, `Password` and `SubjectClassGrades[]`. Each class contains `Subject`, `Class`, `Components[]` and `Students[]`; each student contains `Roll`, `Name`, nullable `Comment` and `Grades[]`; each grade contains component name and nullable finite numeric value.

## Functional requirements

### REQ-001 — Select input (`OBSERVED`)

**WHEN** a user selects a client file, **THE UI SHALL** accept filenames ending `.fg` case-insensitively and reject other extensions before upload.

### REQ-002 — Upload limit (`OBSERVED`)

**IF** uploaded content exceeds 10 MiB, **THEN THE import endpoint SHALL** return `413` without parsing it.

### REQ-003 — Parse order (`OBSERVED`)

**WHEN** content is imported, **THE endpoint SHALL** first attempt legacy decrypt + JSON parse, then attempt plaintext JSON parse; **IF** both fail, **THEN THE endpoint SHALL** return `422`.

### REQ-004 — File password (`OBSERVED`)

**IF** parsed data contains a password hash and no password was supplied, **THEN THE endpoint SHALL** return code `PASSWORD_REQUIRED`; **IF** verification fails, **THEN THE endpoint SHALL** return `INVALID_PASSWORD`; **WHEN** it succeeds, **THE response SHALL** clear the password field.

### REQ-005 — Normalization (`OBSERVED`)

**WHEN** a payload is parsed or exported, **THE system SHALL** retain only the documented data contract, default missing strings, keep only named grade/component entries and convert non-finite/non-number grade values to `null`.

### REQ-006 — Protected export (`OBSERVED`)

**WHEN** a user exports, **THE UI SHALL** require non-empty matching password/confirmation; **THE endpoint SHALL** embed the legacy password hash, encrypt the normalized payload and return a non-cacheable `.fg` attachment.

### REQ-007 — Filename safety (`OBSERVED`)

**WHEN** export receives a filename, **THE endpoint SHALL** add `.fg` if missing and replace characters outside letters, digits, dot, underscore and hyphen.

### REQ-008 — Guest access (`OBSERVED`)

**WHEN** an unauthenticated user imports or exports `.fg`, **THE FuGrade Web SHALL** allow the operation without creating a server snapshot.

### REQ-009 — Desktop round-trip (`REQUIRED GAP`)

**WHEN** the project prepares a desktop-compatibility claim or schema/crypto change, **THE project SHALL** verify representative password/no-password, Unicode, null grade, multi-class and failure fixtures in both desktop→web and web→desktop directions.

### REQ-010 — Loss visibility (`REQUIRED GAP`)

**IF** normalization would discard an unknown field or invalid value, **THEN THE approved product contract SHALL** decide whether import rejects the file, reports the loss or explicitly accepts lossy normalization.

## Error behavior

| Condition | Response |
| :--- | :--- |
| Missing file | `400` |
| File over 10 MiB | `413` |
| Password omitted/incorrect | `423` / `403` with stable code |
| Unparseable payload | `422` |
| Unexpected import failure | `500` generic message |
| Invalid export/password/payload | `400` or `422` generic message |

## Acceptance scenarios

1. Import valid encrypted file without and with correct/incorrect password.
2. Import valid plaintext JSON and document whether fallback remains supported.
3. Reject oversized, malformed and wrong-extension input.
4. Export with sanitized filename and reopen in web.
5. Run desktop↔web fixtures from REQ-009 and compare every field.
6. Exercise unknown fields, invalid grades, Unicode and duplicate identifiers per REQ-010 decision.

## Representative BDD

```gherkin
Scenario: Open a password-protected FuGrade file
  Given a valid encrypted .fg file contains a password hash
  When the editor submits the correct password
  Then the normalized grade payload is returned
  And the returned Password field is empty
```

## Non-functional requirements

- Password/plain payload must not appear in logs or error response.
- Import/export responses use `Cache-Control: no-store` where source currently specifies it.
- No crypto/schema changes without approved fixtures, Spec and rollback.

## Out of scope

Modernizing encryption, persisting bonus metadata, snapshot storage and Excel format.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: REQ-001–REQ-010.
- Recommendation: Treat compatibility as unverified until REQ-009 fixtures pass; resolve loss semantics before accepting REQ-010.
- Evidence: `CONTEXT.md` and source review.
- Risks and assumptions: Legacy source and browser code may diverge at runtime.
- Alternatives considered: Remove plaintext fallback or create a versioned successor format; both change behavior.
- Required human decision: Approve/revise parse fallback, normalization and fixture matrix.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:

## Changelog

- `0.1.0` (2026-09-12): Initial reverse specification.
