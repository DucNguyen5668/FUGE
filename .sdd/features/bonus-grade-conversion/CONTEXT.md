# Feature Context — Bonus grade conversion

**Feature slug:** `bonus-grade-conversion`  
**Phase:** 0 — Context Discovery  
**Version:** 0.1.0  
**Status:** APPROVED
**Created:** 2026-08-25  
**Architecture Profile:** FuGrade v0.1.0 — APPROVED for this feature scope at `2026-09-11T22:43:00+07:00`

## 1. Problem statement

Giảng viên cần một cột điểm bonus riêng cho từng sinh viên và muốn quy đổi bonus sang một thành phần điểm đã có. Hệ số quy đổi phải cấu hình được, ví dụ `1 bonus = 0.2 điểm` cộng vào `Progress Test 1`. Giá trị bonus phải nhận số thập phân như `0.1`, `0.2`, `1.2` thay vì chỉ số nguyên.

Hệ thống hiện cho phép tạo thành phần điểm thông thường và nhập giá trị `0–10`, nhưng chưa có khái niệm bonus, cột đích, hệ số quy đổi, preview phần cộng hoặc cơ chế tránh cộng lặp.

## 2. Desired outcome

Người dùng có thể:

1. tạo một cột bonus cho lớp hiện tại;
2. nhập/import bonus theo từng sinh viên với số thập phân;
3. chọn một thành phần điểm đích trong cùng lớp;
4. cấu hình hệ số `1 bonus = X điểm đích`;
5. xem rõ số điểm sẽ được cộng trước khi áp dụng;
6. áp dụng nhất quán cho các sinh viên có bonus hợp lệ;
7. tiếp tục dùng workspace ở chế độ khách, lưu snapshot khi đăng nhập và xuất `.fg` có mật khẩu theo policy hiện hành.

## 3. Stakeholders và decision maker

| Stakeholder | Mối quan tâm |
| :--- | :--- |
| Giảng viên | Nhập nhanh, quy đổi đúng, không cộng trùng, có thể sửa lại. |
| Sinh viên | Điểm cuối cùng minh bạch, không vượt rule hoặc bị cộng sai. |
| Maintainer FuGrade Web | Dữ liệu nhất quán giữa guest workspace, snapshot và `.fg`. |
| Người dùng FuGrade desktop | File xuất từ web vẫn mở được bằng ứng dụng legacy. |
| Human Director / Product Owner | Chốt rule giới hạn điểm, cách áp dụng và persistence. |

**Decision maker đề xuất:** Product Owner của FuGrade Web.

## 4. Repository evidence hiện tại

| Quan sát | Evidence | Ảnh hưởng tới feature |
| :--- | :--- | :--- |
| Thành phần điểm chỉ là chuỗi tên. | `lib/fg-types.ts#FgSubjectClassGrade.Components` | Chưa có loại component hoặc metadata bonus. |
| Điểm mỗi sinh viên là `{ Component, Grade }`. | `lib/fg-types.ts#FgGradeComponent` | Bonus có thể hiển thị như cột thường, nhưng rule quy đổi chưa có chỗ lưu. |
| UI và import chỉ nhận điểm từ `0–10`. | `components/workspace/DraftGradeTable.tsx`, `components/workspace/WorkspaceDialogs.tsx` | Cần validation riêng cho bonus; không được vô tình nới validation điểm thường. |
| Workspace guest được giữ trong `sessionStorage`. | `app/page.tsx`, key `fugrade_guest_workspace_v1` | Metadata bonus cần được serialize cùng workspace hoặc có migration/fallback. |
| Export `.fg` dùng payload `FgTeacherGrade`. | `lib/fg-decrypt.ts`, `app/api/fg/export/route.ts` | Thêm metadata có thể ảnh hưởng compatibility; cần fixture round-trip trước khi đổi schema. |
| Save snapshot normalize payload rồi ghi component/grade vào SQLite. | `app/api/fg/save/route.ts`, `lib/db/schema.ts` | Lưu rule bonus bền vững có thể cần schema/migration hoặc chiến lược khác được duyệt. |
| Automated test command chưa tồn tại. | `.sdd/architecture-profile.md`, `package.json` | Chỉ có lint/build và checklist thủ công cho tới khi test stack được chọn. |

## 5. Domain glossary

| Thuật ngữ | Định nghĩa đề xuất |
| :--- | :--- |
| Bonus component | Cột chứa lượng bonus gốc của từng sinh viên; không phải điểm đích sau quy đổi. |
| Bonus value | Giá trị bonus của một sinh viên, có thể là số thập phân không âm. |
| Target component | Thành phần điểm trong cùng lớp nhận phần điểm quy đổi. |
| Conversion rate | Số điểm đích nhận được từ `1` đơn vị bonus. |
| Converted amount | `bonus value × conversion rate`. |
| Base target grade | Điểm đích trước khi áp dụng bonus. |
| Effective target grade | Điểm đích sau khi cộng converted amount và áp dụng rule giới hạn. |
| Apply operation | Hành động biến cấu hình + bonus thành kết quả ở cột đích. |

## 6. Business constraints đã biết

- Bonus và target component phải thuộc cùng một lớp.
- Target component không được chính là bonus component.
- Bonus cần hỗ trợ số thập phân như `0.1`, `0.2`, `1.2`.
- Conversion rate cần là số dương và hỗ trợ số thập phân.
- Điểm thường hiện có contract `0–10`; thay đổi contract này cần quyết định rõ.
- Áp dụng nhiều lần không được vô tình cộng chồng cùng một bonus.
- Dòng bonus trống không được tự hiểu là `0` nếu điều đó làm mất phân biệt “chưa nhập” và “không có bonus”.
- Feature phải hoạt động ở guest workspace; đăng nhập chỉ cần khi lưu snapshot.
- Không được phá khả năng mở `.fg` bằng FuGrade desktop.

## 7. Phạm vi đề xuất

### In scope

- Tạo/cấu hình bonus component cho lớp hiện tại.
- Nhập trực tiếp và import bonus thập phân theo MSSV.
- Chọn target component và conversion rate.
- Preview base grade, converted amount và effective grade.
- Apply có xác nhận và thông báo số dòng được áp dụng/bỏ qua.
- Validation cho bonus, rate, target và grade result.
- Hành vi guest/session, save snapshot và `.fg` được xác định rõ trong Spec.

### Out of scope đề xuất

- Công thức nhiều bonus cùng lúc hoặc chuỗi công thức tùy ý.
- Bonus âm hoặc trừ điểm.
- Quy đổi giữa nhiều lớp.
- Thay đổi trọng số phần trăm của môn học.
- Phân tích/xếp hạng sinh viên dựa trên bonus.
- Tự động deploy hoặc thay production database.

## 8. Các quyết định bắt buộc trước SPEC

### Q1 — Giới hạn điểm đích

Khi `base + converted amount > 10`:

- **A — Cap ở 10 (khuyến nghị):** giữ contract điểm hiện tại và giảm rủi ro export/import.
- **B — Cho phép vượt 10:** phải đổi validation điểm thường và xác nhận desktop chấp nhận.
- **C — Không áp dụng dòng đó:** giữ dữ liệu nhưng yêu cầu người dùng xử lý thủ công.

### Q2 — Cách tránh cộng lặp và hoàn tác

- **A — Derived/recalculable (khuyến nghị):** giữ base grade, tính effective grade từ rule; thay bonus/rate sẽ tính lại, không cộng chồng.
- **B — One-time mutation:** cộng trực tiếp vào target; đơn giản hơn nhưng cần lịch sử/undo hoặc dấu đã áp dụng để tránh double-add.

### Q3 — Persistence và tương thích `.fg`

- **A — Web metadata mở rộng:** lưu rule bonus trong payload/snapshot; `.fg` desktop có thể bỏ qua metadata, nhưng cần fixture xác minh và metadata có thể mất khi desktop mở rồi lưu lại.
- **B — Chỉ materialize kết quả:** bonus là component thường và target đã được cộng; `.fg` không lưu rule, khi mở lại phải cấu hình lại.
- **C — Chỉ lưu rule trong snapshot web:** `.fg` chỉ chứa component/result tương thích; rule đầy đủ chỉ còn khi đăng nhập và mở snapshot.

### Q4 — Miền giá trị bonus

- **A — Không âm, không đặt trần (khuyến nghị ban đầu):** kết quả cuối vẫn theo Q1.
- **B — Từ `0–10`:** nhất quán UI hiện tại nhưng có thể không đúng đơn vị bonus.
- **C — Giới hạn khác:** Product Owner cung cấp min/max cụ thể.

### Q5 — Độ chính xác và làm tròn

Cần chọn số chữ số thập phân cho bonus, rate và effective grade. Khuyến nghị tính bằng decimal string/integer scale hoặc quy tắc làm tròn rõ ràng, không để sai số floating-point quyết định kết quả hiển thị.

## 9. Assumptions tạm thời

- Mỗi bonus component có đúng một target component và một conversion rate tại một thời điểm.
- Bonus được áp theo từng sinh viên, không phải cộng đồng loạt một giá trị cố định.
- Các sinh viên thiếu target grade hoặc bonus cần được preview là “không áp dụng”, không tự tạo điểm từ `null` nếu chưa được duyệt.
- Không thêm package mới trong feature này nếu có thể dùng TypeScript/React hiện tại.

Các assumption trên chưa phải business rule cho tới khi Context được Human Final Review.

## 10. Risks

| Risk | Mức độ | Hướng xử lý ở pha sau |
| :--- | :--- | :--- |
| Double-add khi bấm áp dụng nhiều lần | Cao | Chọn Q2 và đặc tả idempotency. |
| Mất metadata sau export hoặc desktop re-save | Cao | Chọn Q3 và có fixture round-trip. |
| Điểm vượt 10 hoặc làm tròn sai | Cao | Chốt Q1/Q5; acceptance examples cụ thể. |
| Schema migration làm mất snapshot cũ | Cao | Nếu cần DB change, Plan phải có migration + rollback và review riêng. |
| Bonus bị coi như điểm thường và bị validation sai | Trung bình | Tách loại component/validation trong Spec và UI. |
| Guest session cũ không có metadata mới | Trung bình | Có backward-compatible workspace normalization/migration. |

## 11. Success indicators

- Người dùng cấu hình được bonus mà không sửa thủ công target cho từng sinh viên.
- Ví dụ quy đổi trong Spec cho kết quả nhất quán và có thể kiểm tra lại.
- Apply lặp không làm tăng điểm ngoài ý muốn.
- File `.fg` sau feature vẫn mở được bằng desktop theo chiến lược đã duyệt.
- Lint/build pass và checklist thủ công bao phủ decimal input, cap/overflow, null và re-apply.

## AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Context cho `bonus-grade-conversion`
- Recommendation: Duyệt mục tiêu feature; chọn Q1=A (cap 10), Q2=A (derived/recalculable), Q4=A; với Q3 nên ưu tiên C nếu cần giữ `.fg` không đổi, hoặc A chỉ sau khi có fixture desktop-web chứng minh compatibility. Q5 đề xuất lưu/tính tối đa 2 chữ số thập phân và hiển thị bỏ số 0 dư.
- Evidence: `lib/fg-types.ts`, `lib/fg-decrypt.ts`, `app/page.tsx`, `components/workspace/DraftGradeTable.tsx`, `components/workspace/WorkspaceDialogs.tsx`, `app/api/fg/save/route.ts`, `lib/db/schema.ts`.
- Risks and assumptions: Chưa có automated test runner; `.fg` chưa có bonus metadata; persistence đầy đủ có thể cần migration.
- Alternatives considered: Lưu metadata bonus mở rộng trong `.fg` hoặc chỉ lưu rule trong snapshot web; chưa chọn vì tăng rủi ro tương thích legacy hoặc cần database migration.
- Required human decision: Chọn Q1–Q5, xác nhận decision maker và duyệt/revise Context trước khi tạo `SPEC.md`.

## Human Final Review

- Status: APPROVED
- Decision: Đã duyệt mục tiêu, phạm vi và các lựa chọn Q1=A (cap điểm đích ở 10), Q2=A (derived/recalculable, không cộng chồng), Q3=B (chỉ materialize bonus và điểm kết quả; không lưu rule), Q4=A (bonus không âm, không giới hạn trên), Q5=1 chữ số thập phân; cho phép tạo SPEC.
- Reviewer: NguyenND
- Reviewed at: 2026-08-25T14:29:00+07:00
- Follow-up: Tạo `SPEC.md` cho `bonus-grade-conversion` và yêu cầu Human Final Review trước khi lập Plan.

## Administrative changelog

- 2026-09-12: Đồng bộ header với Human Final Review của Context và Architecture Profile approval; không thay đổi quyết định Q1–Q5 hoặc phạm vi đã duyệt.
