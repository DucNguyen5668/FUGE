# Feature Specification — Bonus grade conversion

**Feature slug:** `bonus-grade-conversion`  
**Version:** 1.0.0  
**Status:** DRAFT  
**Context:** `CONTEXT.md` APPROVED bởi NguyenND lúc `2026-08-25T14:29:00+07:00`  
**Architecture Profile:** FuGrade v0.1.0 — APPROVED for this feature scope at `2026-09-11T22:43:00+07:00`

## 1. Purpose

Cho phép giảng viên tạo cột bonus, nhập bonus thập phân cho từng sinh viên, cấu hình `1 bonus = X điểm` và cộng phần quy đổi vào một thành phần điểm trong cùng lớp mà không cộng chồng trong phiên chỉnh sửa hiện tại.

Spec này mô tả behavior độc lập framework. `PLAN.md` và code bị block cho tới khi Spec được `APPROVED & LOCKED` và Architecture Profile có review hợp lệ.

## 2. Actors

- **Editor:** người đang chỉnh bảng điểm ở guest workspace hoặc phiên đã đăng nhập.
- **Authenticated editor:** editor đã đăng nhập và được phép lưu snapshot.
- **FuGrade desktop user:** người mở file `.fg` đã materialize bằng ứng dụng legacy.

## 3. Data contract

### 3.1 Bonus rule trong phiên chỉnh sửa

| Field | Contract |
| :--- | :--- |
| Bonus component | Tên duy nhất, không rỗng, thuộc lớp hiện tại. |
| Target component | Thành phần điểm đã có trong cùng lớp và khác bonus component. |
| Conversion rate | Số lớn hơn `0`, tối đa một chữ số thập phân. |
| Base target grade | Điểm đích trước khi rule hiện tại được áp dụng; có thể `null`. |
| Bonus value | Số không âm, không giới hạn trên, tối đa một chữ số thập phân; có thể `null`. |
| Converted amount | `bonus value × conversion rate`, làm tròn tới một chữ số thập phân. |
| Effective target grade | `min(10, base target grade + converted amount)`, làm tròn tới một chữ số thập phân. |
| Applied state | Trạng thái trong phiên dùng để tính lại từ base grade, không cộng lên kết quả cũ. |

Rule, base grade và applied state chỉ tồn tại trong workspace đang mở. Chúng không thuộc payload `.fg` hoặc snapshot theo lựa chọn Context Q3=B.

### 3.2 Decimal input

- Chấp nhận cả dấu chấm và dấu phẩy làm dấu thập phân: `0.1` tương đương `0,1`.
- Không chấp nhận dấu phân cách hàng nghìn hoặc chuỗi có nhiều dấu thập phân.
- Giá trị được chuẩn hóa để tính toán và hiển thị với tối đa một chữ số thập phân.
- Phép làm tròn dùng quy tắc half-up: phần bỏ đi từ `5` trở lên làm tròn lên.

## 4. Functional requirements

### REQ-001 — Tạo bonus component

**WHEN** editor tạo cấu hình bonus cho lớp hiện tại, **THE FuGrade Web SHALL** yêu cầu tên bonus component, target component và conversion rate trước khi cho preview.

**IF** tên bonus component rỗng hoặc trùng không phân biệt hoa thường với component đã có, **THEN THE FuGrade Web SHALL** từ chối và hiển thị lỗi tại trường tương ứng.

### REQ-002 — Chọn target component

**WHEN** editor chọn target, **THE FuGrade Web SHALL** chỉ cung cấp component thuộc lớp hiện tại và loại bonus component khỏi danh sách target.

**IF** target không tồn tại hoặc bằng bonus component, **THEN THE FuGrade Web SHALL** không tạo/apply rule.

### REQ-003 — Cấu hình conversion rate

**WHEN** editor nhập conversion rate, **THE FuGrade Web SHALL** chấp nhận số dương có tối đa một chữ số thập phân bằng dấu chấm hoặc dấu phẩy.

**IF** rate rỗng, bằng `0`, âm, không hữu hạn hoặc có hơn một chữ số thập phân, **THEN THE FuGrade Web SHALL** báo lỗi và không preview/apply.

### REQ-004 — Nhập bonus

**WHILE** bonus rule đang hoạt động, **THE FuGrade Web SHALL** cho phép nhập trực tiếp hoặc import bonus theo MSSV bằng số không âm có tối đa một chữ số thập phân.

**IF** bonus rỗng, **THEN THE FuGrade Web SHALL** giữ giá trị `null` và không coi là `0`.

**IF** bonus âm, không hữu hạn hoặc sai định dạng, **THEN THE FuGrade Web SHALL** từ chối riêng dòng đó mà không thay đổi các dòng hợp lệ khác.

### REQ-005 — Preview quy đổi

**WHEN** bonus, rate hoặc target thay đổi, **THE FuGrade Web SHALL** hiển thị preview theo từng sinh viên gồm base target grade, bonus value, converted amount, effective target grade hoặc lý do bỏ qua.

**IF** bonus hoặc base target grade là `null`, **THEN THE FuGrade Web SHALL** đánh dấu dòng “Không áp dụng” và không tự tạo base grade.

### REQ-006 — Công thức và cap

**WHEN** một dòng có base target grade và bonus hợp lệ, **THE FuGrade Web SHALL** tính converted amount bằng `bonus × rate`, làm tròn half-up một chữ số, cộng vào base grade, cap kết quả ở `10`, rồi làm tròn effective grade tới một chữ số.

### REQ-007 — Apply có xác nhận

**WHEN** editor bấm Apply, **THE FuGrade Web SHALL** yêu cầu xác nhận có nêu bonus component, target component, rate, số dòng áp dụng, số dòng bỏ qua và số dòng bị cap ở `10`.

**WHEN** editor xác nhận, **THE FuGrade Web SHALL** ghi effective grade vào target component cho các dòng hợp lệ và giữ nguyên các dòng bị bỏ qua.

### REQ-008 — Không cộng chồng trong cùng phiên

**WHILE** rule hiện tại còn hoạt động, **THE FuGrade Web SHALL** luôn tính effective grade từ base target grade đã ghi nhận, không tính từ effective grade của lần Apply trước.

**WHEN** editor Apply lại mà input không đổi, **THE FuGrade Web SHALL** tạo cùng kết quả, không tăng thêm điểm.

**WHEN** editor sửa trực tiếp target grade trong lúc rule hoạt động, **THE FuGrade Web SHALL** coi giá trị được sửa là base target grade mới và tính lại preview từ base mới.

### REQ-009 — Đổi rule trong cùng phiên

**WHEN** editor đổi bonus value hoặc rate, **THE FuGrade Web SHALL** tính lại từ base grade ban đầu của rule hiện tại.

**WHEN** editor đổi target component, **THE FuGrade Web SHALL** khôi phục target cũ về base grade của nó trước khi preview/apply sang target mới.

**WHEN** editor hủy rule trong cùng phiên, **THE FuGrade Web SHALL** cho phép chọn khôi phục target về base grade hoặc giữ kết quả đã materialize.

### REQ-010 — Guest và authentication

**WHILE** chưa đăng nhập, **THE FuGrade Web SHALL** cho phép tạo rule, nhập bonus, preview, apply và xuất `.fg` như các thao tác workspace hiện tại.

**WHEN** editor lưu snapshot, **THE FuGrade Web SHALL** áp dụng yêu cầu đăng nhập hiện hành; feature bonus không tạo cơ chế authentication mới.

### REQ-011 — Materialize khi save/export

**WHEN** workspace được lưu snapshot hoặc xuất `.fg`, **THE FuGrade Web SHALL** ghi bonus component như component điểm thông thường và ghi effective grade hiện tại vào target component.

**THE FuGrade Web SHALL NOT** ghi target mapping, conversion rate, base grade hoặc applied state vào snapshot/`.fg`.

**WHEN** snapshot hoặc `.fg` materialized được mở lại, **THE FuGrade Web SHALL** không tự nhận diện hoặc tự apply rule cũ; bonus component được xem như component thông thường cho tới khi editor tạo rule mới.

**WHEN** editor dùng một component đã materialize để tạo rule mới, **THE FuGrade Web SHALL** cảnh báo rằng lịch sử rule cũ không tồn tại và target hiện tại sẽ trở thành base mới.

### REQ-012 — Workspace backward compatibility

**WHEN** mở workspace/session hoặc `.fg` không có bonus rule, **THE FuGrade Web SHALL** giữ behavior component/grade hiện tại và không yêu cầu migration dữ liệu.

### REQ-013 — Summary sau Apply

**WHEN** Apply hoàn tất, **THE FuGrade Web SHALL** thông báo tổng số dòng đã cập nhật, bỏ qua, cap ở `10` và lỗi validation nếu có; thông báo không chứa dữ liệu nhạy cảm ngoài phạm vi lớp đang hiển thị.

## 5. Calculation examples

| Base | Bonus | Rate | Converted | Effective | Kết quả |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `8.0` | `1.2` | `0.5` | `0.6` | `8.6` | Áp dụng |
| `9.8` | `1.2` | `0.5` | `0.6` | `10.0` | Cap ở 10 |
| `8.0` | `0.5` | `0.5` | `0.3` | `8.3` | `0.25` làm tròn half-up thành `0.3` |
| `8.0` | `0,2` | `0,5` | `0.1` | `8.1` | Dấu phẩy được chấp nhận |
| `null` | `1.0` | `0.5` | — | — | Bỏ qua vì thiếu base |
| `8.0` | `null` | `0.5` | — | — | Bỏ qua vì thiếu bonus |

Ví dụ idempotency: base `8.0`, bonus `1.2`, rate `0.5`; Apply lần một và lần hai đều cho `8.6`. Nếu đổi bonus thành `0.2`, kết quả mới là `8.1`, không phải `8.7`.

## 6. BDD acceptance scenarios

### SCN-001 — Tạo và áp dụng bonus hợp lệ

```gherkin
Given target "Progress Test 1" của sinh viên là 8.0
And bonus của sinh viên là 1.2
And rate là 0.5
When editor preview và xác nhận Apply
Then converted amount là 0.6
And target effective grade là 8.6
```

### SCN-002 — Chấp nhận dấu phẩy thập phân

```gherkin
Given target grade là 8.0
When editor nhập bonus "0,2" và rate "0,5"
Then input được coi là bonus 0.2 và rate 0.5
And effective grade preview là 8.1
```

### SCN-003 — Cap ở 10

```gherkin
Given target grade là 9.8
And converted amount là 0.6
When editor Apply
Then target effective grade là 10.0
And summary ghi một dòng bị cap
```

### SCN-004 — Apply lặp không cộng chồng

```gherkin
Given base target grade là 8.0
And rule đã Apply cho effective grade 8.6
When editor Apply lại cùng rule
Then effective grade vẫn là 8.6
```

### SCN-005 — Mở lại file materialized

```gherkin
Given một file .fg đã được export sau khi Apply bonus
When editor mở lại file đó
Then bonus column và target effective grade vẫn có trong dữ liệu
And không có active bonus rule
And hệ thống không tự cộng bonus lần nữa
```

## 7. Error behavior

| Error code khái niệm | Điều kiện | Behavior |
| :--- | :--- | :--- |
| `BONUS_NAME_REQUIRED` | Tên bonus rỗng | Không tạo rule; focus trường tên. |
| `BONUS_NAME_DUPLICATE` | Tên trùng component | Không tạo rule. |
| `BONUS_TARGET_INVALID` | Target thiếu, khác lớp hoặc là bonus | Không preview/apply. |
| `BONUS_RATE_INVALID` | Rate không dương hoặc sai precision | Không preview/apply. |
| `BONUS_VALUE_INVALID` | Bonus âm/sai định dạng | Loại dòng khỏi Apply và hiển thị lỗi dòng. |
| `BONUS_BASE_MISSING` | Target grade là `null` | Bỏ qua dòng, không coi là lỗi toàn thao tác. |
| `BONUS_HISTORY_UNAVAILABLE` | Tạo rule mới từ dữ liệu materialized | Cảnh báo và yêu cầu xác nhận base mới. |

Lỗi client không được làm mất giá trị hợp lệ đã nhập. Lỗi save/export dùng error boundary hiện hành và không làm thay đổi workspace cục bộ.

## 8. Non-functional requirements

- **NFR-001:** Preview và Apply cho 500 sinh viên × 30 components phải phản hồi UI trong tối đa 500 ms trên thiết bị development chuẩn; phép đo cụ thể được xác nhận trong Plan.
- **NFR-002:** Phép tính decimal phải cho kết quả đúng các example trong mục 5, không phụ thuộc sai số hiển thị binary floating-point.
- **NFR-003:** UI bonus phải dùng được bằng bàn phím, có label và thông báo lỗi gắn với field/dòng.
- **NFR-004:** Bảng và dialog phải cuộn được ở viewport nhỏ; không che nút Apply/Cancel.
- **NFR-005:** Không thêm login gate cho edit/import/apply/export ở guest workspace.
- **NFR-006:** Không thay thuật toán mã hóa hoặc schema legacy bắt buộc của `.fg` trong feature này.

## 9. Verification and acceptance

Trước delivery phải có evidence:

- các scenario SCN-001 đến SCN-005 được kiểm tra;
- nhập trực tiếp và import với `0.1`, `0,1`, `1.2`, `1,2`, âm, rỗng và nhiều hơn một chữ số thập phân;
- Apply lặp, đổi bonus, đổi rate, đổi target và hủy rule;
- save snapshot khi chưa/đã đăng nhập;
- export/import `.fg` đúng mật khẩu, sai mật khẩu và mở bằng FuGrade desktop;
- `npm.cmd run lint` pass;
- `npm.cmd run build` pass;
- Automated tests: `N/A` cho tới khi Architecture Profile chọn test stack; không tuyên bố test coverage.

## 10. Out of Scope

- Bonus âm hoặc trừ điểm.
- Nhiều target cho cùng một bonus rule.
- Công thức tùy ý hoặc dependency giữa nhiều bonus rule.
- Persist target/rate/base/applied-state qua snapshot hoặc `.fg`.
- Khôi phục lịch sử rule sau khi đóng/mở lại file.
- Thay đổi database schema hoặc `.fg` encryption/schema để lưu rule.
- Thay đổi production database, authentication hoặc authorization model.

## 11. Planning blockers

- Automated test framework chưa được chọn.
- Cần Human xác nhận chấp nhận rủi ro Q3=B: sau khi mở lại dữ liệu materialized, hệ thống không thể biết target đã từng được cộng bonus; cấu hình rule mới dùng target hiện tại làm base mới.
- `PLAN.md` phải xác định cách giữ ephemeral base/rule trong workspace mà không thay `.fg` và database schema.

## 12. Changelog

### 1.0.0 — 2026-08-25

- Đặc tả ban đầu cho bonus component, decimal normalization, conversion, cap, idempotency và materialized persistence.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: SPEC v1.0.0 cho `bonus-grade-conversion`, REQ-001 đến REQ-013.
- Recommendation: Duyệt và lock Spec nếu công thức half-up một chữ số, null-row skip, idempotency trong phiên và rủi ro materialized-only đã đúng ý Product Owner.
- Evidence: CONTEXT đã APPROVED; behavior hiện tại trong `lib/fg-types.ts`, `lib/fg-decrypt.ts`, `app/page.tsx`, `DraftGradeTable.tsx`, `WorkspaceDialogs.tsx` và save route.
- Risks and assumptions: Rule không tồn tại sau reopen; tạo rule mới có thể dùng effective grade cũ làm base mới; chưa có automated test runner; chưa được lập Plan/code cho tới khi Spec được Human Final Review.
- Alternatives considered: Persist rule trong `.fg` hoặc snapshot web để chống double-add xuyên phiên; không chọn vì Context Q3=B và vì hai hướng đó cần compatibility fixture hoặc database migration.
- Required human decision: Approve/revise REQ-001–REQ-013, đặc biệt rounding, null behavior, materialization và rủi ro reconfigure sau reopen.

## Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
