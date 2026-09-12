# Specification — Guest Grade Workspace

- **Version:** 0.1.0
- **Status:** DRAFT — REVERSE SPEC, PENDING HUMAN REVIEW
- **Architecture Profile:** FuGrade v0.1.0; browser/React bindings evidenced, feature approval pending

## Contract status

Requirements are observed behavior unless marked `REQUIRED GAP`. Approval decides whether the captured behavior becomes the desired product contract.

## Actors and data contract

- **Guest editor:** edits one loaded workspace without authenticated persistence.
- **Authenticated editor:** has the same local behavior and may invoke snapshot save.
- Workspace data follows `FgWorkspace`: normalized `.fg` data, filename, optional snapshot id and dirty flag. Per-class state includes components, students, nullable comments and nullable numeric grades.

## Functional requirements

### REQ-001 — Restore draft (`OBSERVED`)

**WHEN** `/` loads in a browser tab, **THE workspace SHALL** restore valid JSON from session key `fugrade_guest_workspace_v1`; **IF** parsing fails, **THEN THE workspace SHALL** remove that entry and start empty.

### REQ-002 — Persist local state (`OBSERVED`)

**WHEN** workspace state changes, **THE UI SHALL** store it in `sessionStorage`; **WHEN** workspace becomes empty, **THE UI SHALL** remove the entry.

### REQ-003 — Opened file baseline (`OBSERVED`)

**WHEN** a `.fg` file is loaded, **THE workspace SHALL** select its first class and first component, show all first-class components, reset search and mark the draft clean.

### REQ-004 — Metadata and class navigation (`OBSERVED`)

**WHEN** the user edits teacher/semester or selects another class, **THE workspace SHALL** update local state; class selection SHALL reset visible/selected components and search for that class.

### REQ-005 — Find and choose columns (`OBSERVED`)

**WHEN** the user searches, **THE table SHALL** filter case-insensitively by MSSV or name; **WHEN** component visibility changes, **THE table SHALL** render only chosen grade columns without deleting data.

### REQ-006 — Edit cells (`OBSERVED`)

**WHEN** the user double-clicks a grade/comment cell, **THE UI SHALL** enter edit mode; Enter or blur SHALL commit, Escape SHALL restore the prior value; non-empty grades SHALL be finite numbers from 0 through 10.

### REQ-007 — Add student (`OBSERVED`)

**WHEN** a non-empty, case-insensitively unique MSSV is submitted, **THE workspace SHALL** uppercase it, append the student and create a null grade for every existing component.

### REQ-008 — Add component (`OBSERVED`)

**WHEN** a non-empty, case-insensitively unique component name is submitted, **THE workspace SHALL** append it, add a null grade to every student, make it visible and select it.

### REQ-009 — Paste import (`OBSERVED`)

**WHEN** pasted rows are previewed, **THE dialog SHALL** optionally skip the first non-empty line, parse the first token as MSSV and the remainder as value, reject unknown MSSV, missing values and grades outside 0–10, then apply only valid rows.

### REQ-010 — Clear component (`OBSERVED`)

**WHEN** the user confirms clearing a selected component, **THE workspace SHALL** set that component grade to `null` for every student without deleting the component.

### REQ-011 — Dirty state (`OBSERVED`)

**WHEN** local data is mutated, **THE workspace SHALL** mark itself dirty; successful snapshot save SHALL mark it clean and attach the returned snapshot id.

### REQ-012 — Draft lifecycle (`REQUIRED GAP`)

**WHEN** the project prepares to treat session recovery as durable behavior, **THE Product Owner SHALL** define reload, duplicate-tab, close-tab, logout and expired-session outcomes, including whether dirty-state warning is required.

### REQ-013 — Duplicate/whitespace import (`REQUIRED GAP`)

**WHEN** pasted input repeats an MSSV or contains comments with significant whitespace, **THE approved contract SHALL** define reject/first-wins/last-wins and whitespace preservation behavior.

## Error behavior

Invalid direct input stays in local UI with a toast. Invalid import rows are excluded while valid rows remain applicable. Failed session JSON is silently discarded. No server mutation occurs in this feature until snapshot save.

## Acceptance scenarios

1. Restore valid draft after reload and discard malformed JSON.
2. Switch classes and verify selected/visible component state.
3. Edit grade/comment with Enter, blur, Escape, empty and boundary values.
4. Add unique/duplicate students and components.
5. Import grade/comment rows with header, unknown MSSV, invalid score and mixed valid rows.
6. Clear a component after confirm/cancel.
7. Decide and verify repeated MSSV, comment whitespace and tab lifecycle.

## Representative BDD

```gherkin
Scenario: Valid and invalid pasted grades are previewed
  Given the selected class contains student HE180186
  When the editor pastes HE180186 with grade 8.5 and UNKNOWN with grade 7
  Then HE180186 is listed as valid
  And UNKNOWN is excluded with an unknown-MSSV error
```

## Non-functional requirements

- Guest workflow must remain usable without authenticated session.
- Dialog/table must support keyboard access and scrolling at small viewports.
- Browser storage must not contain file passwords.

## Out of scope

`.fg` crypto, snapshot ownership, persisted editor, bonus calculation and Excel export.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: REQ-001–REQ-013.
- Recommendation: Approve after resolving REQ-012–REQ-013 and aligning numeric parsing with persisted editor.
- Evidence: `CONTEXT.md` and static source review.
- Risks and assumptions: Browser storage behavior is unverified.
- Alternatives considered: Split each dialog into a separate Spec; current grouping follows one local-workspace outcome.
- Required human decision: Approve/revise draft lifecycle and import semantics.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:

## Changelog

- `0.1.0` (2026-09-12): Initial reverse specification.
