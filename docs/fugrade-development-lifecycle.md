# Chu trình phát triển chuẩn — FuGrade Web

**Phiên bản:** 1.0.0  
**Trạng thái:** PROPOSED — chờ Human Final Review  
**Áp dụng cho:** source `app/`, `components/`, `lib/`, cấu hình web và phần tương thích `FuGrade/`

Tài liệu này là playbook vận hành chính của FuGrade Web. `SDDADD-main/` chỉ là nguồn template và tài liệu tham khảo; khi có khác biệt, quy định trong repository FuGrade được ưu tiên theo thứ tự:

1. `CONSTITUTION.md` và `.sdd/constraints/`;
2. `AGENTS.md`;
3. `.sdd/architecture-profile.md` đã được duyệt;
4. tài liệu này;
5. artifact của feature trong `.sdd/features/<slug>/`;
6. tài liệu trong `SDDADD-main/`.

## 1. Mục tiêu

Chu trình này thay cách làm “vừa nghĩ vừa code” bằng một luồng có bằng chứng:

```text
Yêu cầu
  -> Phân loại thay đổi
  -> Thu thập evidence hiện trạng
  -> Context / Spec
  -> Human review
  -> Plan / Tasks
  -> Thực thi trong phạm vi đã duyệt
  -> Lint / Build / kiểm chứng theo rủi ro
  -> Trace và review diff
  -> Commit / PR / deploy khi được yêu cầu
```

Không coi giao diện chạy được, build pass hoặc câu trả lời của AI là bằng chứng feature đúng nghiệp vụ. Mỗi kết luận phải chỉ ra artifact, source, command hoặc kết quả kiểm chứng tương ứng.

## 2. Vai trò và quyền quyết định

| Vai trò | Trách nhiệm |
| :--- | :--- |
| Product owner / người yêu cầu | Xác nhận mục tiêu, hành vi mong muốn và tiêu chí chấp nhận. |
| Human reviewer | Duyệt hoặc yêu cầu sửa Context, Spec, Plan, Tasks và quyết định rủi ro. |
| AI/Codex | Khảo sát, đề xuất, viết artifact, triển khai và cung cấp evidence; không tự phê duyệt. |
| Maintainer | Review diff, quản lý migration, release, secret và môi trường deploy. |

AI chỉ được ghi `PENDING`, `REVISE` hoặc recommendation. `APPROVED` phải có người duyệt, quyết định cụ thể và thời gian.

## 3. Phân loại trước khi sửa

### Loại A — Thay đổi nhỏ, không đổi behavior

Ví dụ: sửa chính tả, khoảng cách CSS nhỏ, link tài liệu, đổi tên nội bộ không ảnh hưởng contract.

Yêu cầu tối thiểu:

- ghi rõ mục tiêu và file dự kiến sửa trong trao đổi hoặc task;
- kiểm tra diff;
- chạy `npm.cmd run lint` nếu chạm TypeScript/TSX/CSS có liên quan;
- chạy build nếu thay đổi có thể ảnh hưởng render, route hoặc cấu hình.

Không cần đủ bốn artifact nếu không đổi public/business behavior.

### Loại B — Bug fix hoặc UI behavior có phạm vi rõ

Ví dụ: modal không cuộn, kích thước ô nhập lệch, trạng thái đăng nhập hiển thị sai.

Tạo `.sdd/features/<slug>/` và tối thiểu phải có:

- `CONTEXT.md`: hiện tượng, cách tái hiện, expected/actual, evidence;
- `SPEC.md`: behavior sau sửa và acceptance criteria;
- `PLAN.md`: file scope, rủi ro regression và cách kiểm chứng;
- `TASKS.md`: task nhỏ có command hoặc cách kiểm tra cụ thể.

Có thể gộp review Context và Spec cho bug nhỏ, nhưng không được bỏ acceptance criteria.

### Loại C — Feature mới hoặc thay đổi nghiệp vụ

Ví dụ: lưu snapshot, phân quyền bảng điểm, import dữ liệu, chia sẻ file hoặc thay đổi luồng guest/login.

Phải đi đủ chu trình:

1. Context được review;
2. Spec được review và lock;
3. Architecture Profile đủ binding liên quan;
4. Plan được review;
5. Tasks được review;
6. triển khai từng task;
7. kiểm chứng và trace từ requirement tới code.

### Loại D — Reverse Spec cho behavior hiện hữu

Dùng trước khi refactor hoặc sửa phần chưa có đặc tả, đặc biệt:

- `lib/fg-*.ts` và source desktop `FuGrade/`;
- guest workspace và `sessionStorage`;
- Auth.js, CAPTCHA và Google OAuth;
- snapshot persistence và ownership.

Reverse Spec phải phân biệt:

- **Observed:** source/test/fixture hiện đang làm gì;
- **Desired:** con người muốn giữ hoặc thay đổi điều gì;
- **Unknown:** điều chưa đủ evidence;
- **Risk:** khả năng mất tương thích hoặc dữ liệu.

Behavior quan sát được không tự động trở thành behavior đúng.

### Loại E — Thay đổi rủi ro cao

Các thay đổi sau luôn cần Spec, Plan, rollback và Human Final Review riêng:

- schema hoặc thuật toán mã hóa file `.fg`;
- authentication, authorization, session hoặc ownership;
- database schema, migration, retention hoặc xóa dữ liệu;
- production persistence hoặc triển khai multi-instance;
- secret, OAuth callback, logging chứa dữ liệu người dùng;
- thay đổi breaking đối với API hoặc file format.

## 4. Bộ artifact chuẩn

Mỗi feature dùng slug chữ thường có dấu gạch nối, ví dụ `fix-excel-import-scroll`:

```text
.sdd/features/fix-excel-import-scroll/
  CONTEXT.md
  SPEC.md
  PLAN.md
  TASKS.md
```

### `CONTEXT.md`

Phải trả lời:

- vấn đề là gì và ai bị ảnh hưởng;
- phạm vi trong/ngoài yêu cầu;
- evidence hiện trạng nằm ở file, route, ảnh hoặc log nào;
- thuật ngữ và giả định nào cần xác nhận;
- rủi ro dữ liệu, bảo mật và tương thích.

### `SPEC.md`

Phải có:

- requirement ID ổn định (`REQ-001`, `REQ-002`...);
- hành vi chính, trạng thái lỗi và trường hợp biên;
- acceptance criteria kiểm tra được;
- authorization/ownership nếu có mutation hoặc dữ liệu riêng tư;
- compatibility requirement nếu chạm `.fg` hoặc legacy desktop;
- Out of Scope;
- phiên bản và changelog khi contract thay đổi.

### `PLAN.md`

Phải chỉ ra:

- source hiện tại và thiết kế sau thay đổi;
- danh sách file dự kiến sửa/tạo;
- data flow, trust boundary và dependency bị ảnh hưởng;
- migration/rollback nếu cần;
- exact verification command và kiểm tra thủ công còn thiếu;
- quyết định nào đang bị block.

Không áp layout `src/domain|usecase|interface|infra` từ template vào FuGrade nếu chưa có RFC được duyệt. Plan phải bám layout brownfield `app/`, `components/`, `lib/` hiện tại.

### `TASKS.md`

Mỗi task phải nhỏ, có thể review độc lập và chứa:

- requirement liên quan;
- file scope;
- precondition/approval;
- kết quả mong đợi;
- command hoặc thao tác kiểm chứng;
- trạng thái `[ ]`, `[/]`, `[x]` kèm evidence khi hoàn thành.

## 5. Checkpoint review

Cuối mỗi artifact cần block:

```markdown
## AI Agent Recommendation
- Status: PENDING HUMAN REVIEW
- Scope:
- Recommendation:
- Evidence:
- Risks and assumptions:
- Required human decision:

## Human Final Review
- Status: PENDING | APPROVED | REVISE | REJECTED
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
```

Quy tắc gate:

- Context chưa duyệt: không chốt Spec như yêu cầu chính thức.
- Spec chưa duyệt/lock: không triển khai thay đổi behavior.
- Profile thiếu binding: không tự chọn database, test framework hoặc package.
- Plan/Tasks chưa duyệt: không mở rộng sang nhiều file/layer.
- Artifact thay đổi sau approval: approval cũ hết hiệu lực cho phần bị thay đổi.

## 6. Chu trình thực thi từng feature

### Bước 1 — Intake và evidence

1. Viết lại yêu cầu thành outcome quan sát được.
2. Phân loại A–E.
3. Đọc source, cấu hình và tài liệu liên quan.
4. Ghi rõ điều đã xác nhận, suy luận và chưa biết.
5. Không sửa code trong pha khảo sát nếu yêu cầu mới chỉ là phân tích/diagnose.

### Bước 2 — Context và Spec

1. Tạo feature folder.
2. Soạn Context từ evidence của repository.
3. Soạn Spec với requirement và acceptance criteria.
4. Human review; sửa artifact nếu `REVISE`.
5. Lock Spec bằng trạng thái/version được duyệt trước khi code.

### Bước 3 — Plan và Tasks

1. Đối chiếu `.sdd/architecture-profile.md`.
2. Chỉ dùng dependency/adapter đã có evidence hoặc được duyệt.
3. Liệt kê file scope và tránh sửa shared file ngoài plan.
4. Đưa kiểm tra lỗi, bảo mật, ownership và rollback vào task tương ứng.
5. Human review trước execution đối với loại C–E.

### Bước 4 — Implementation

Trước mỗi task, nêu ngắn:

- task/requirement đang làm;
- file sẽ sửa;
- rủi ro chính;
- cách xác minh.

Trong khi làm:

- giữ diff nhỏ và đúng scope;
- không sửa `.env`, database local, `.next/`, `node_modules/`;
- không thêm package nếu Plan/Profile chưa cho phép;
- không thay đổi crypto/schema `.fg` khi chưa có fixture round-trip;
- không biến warning hoặc test fail thành pass bằng skip/filter tùy tiện.

### Bước 5 — Verification

Baseline hiện được xác nhận:

```powershell
npm.cmd run lint
npm.cmd run build
```

CI/Linux:

```bash
npm run lint
npm run build
```

Project chưa có automated test runner. Vì vậy:

- không ghi “all tests passed”;
- ghi rõ `Automated tests: N/A — chưa có test script`;
- bổ sung checklist thủ công theo acceptance criteria;
- thay đổi `.fg` cần fixture desktop ↔ web và kiểm tra sai mật khẩu;
- thay đổi auth cần kiểm tra guest, credentials, Google, unauthorized và ownership;
- thay đổi UI cần kiểm tra viewport, overflow, keyboard và trạng thái loading/error;
- thay đổi database cần migration/rollback evidence trước production.

### Bước 6 — Trace và delivery

Trước khi báo hoàn thành:

1. map từng requirement sang file/code và evidence;
2. review toàn bộ diff, kể cả file ngoài feature scope;
3. liệt kê check đã chạy, kết quả và check chưa chạy;
4. cập nhật `TASKS.md` và review artifact;
5. ghi remaining risks và follow-up;
6. chỉ commit, push, tạo PR hoặc deploy khi người dùng yêu cầu rõ ràng.

## 7. Ma trận kiểm chứng riêng của FuGrade

| Khu vực | Kiểm chứng bắt buộc |
| :--- | :--- |
| Workspace guest | Không cần login để mở/sửa/import/xuất; state chỉ thuộc tab/phiên đã định nghĩa. |
| Save snapshot | Yêu cầu đăng nhập; kiểm tra identity và ownership ở server. |
| `.fg` import/export | Password đúng/sai, round-trip dữ liệu, Unicode, component/student/grade đầy đủ, tương thích desktop. |
| Excel import | Dòng hợp lệ/không hợp lệ, header, dòng trống, duplicate, overflow và danh sách dài có thể cuộn. |
| Grade editing | Giá trị biên, invalid input, keyboard save/cancel, kích thước cột và responsive behavior. |
| Auth | Credentials CAPTCHA, Google OAuth, logout, session hết hạn và không hiển thị giả trạng thái đăng nhập. |
| Persistence | Ownership, transaction/partial failure, retention, migration và rollback. |
| Vercel/production | Không dựa vào SQLite hoặc in-memory CAPTCHA nếu chạy multi-instance khi chưa có quyết định kiến trúc. |

## 8. Xử lý bug/CI failure

Khi có failure:

1. lưu exact command và output;
2. tái hiện tối thiểu;
3. xác định lỗi code, lỗi Spec hay configuration gap;
4. nếu Spec thiếu behavior, cập nhật và review Spec trước khi sửa code;
5. sửa nguyên nhân gốc trong phạm vi task;
6. chạy lại exact command và acceptance check;
7. ghi regression risk còn lại.

Không coi “process exit 0” là đủ nếu đầu ra nghiệp vụ chưa được xác nhận.

## 9. Definition of Ready

Feature loại C–E chỉ sẵn sàng triển khai khi:

- [ ] Context có evidence và được review.
- [ ] Spec có requirement, edge case, acceptance criteria và được duyệt.
- [ ] Architecture Profile có binding cần dùng hoặc ghi rõ `N/A` hợp lệ.
- [ ] Plan có file scope, risk, verification và rollback nếu cần.
- [ ] Tasks đủ nhỏ và có trace tới requirement.
- [ ] Không còn quyết định sản phẩm/kiến trúc quan trọng bị AI tự giả định.

## 10. Definition of Done

Một thay đổi chỉ hoàn thành khi:

- [ ] Code khớp Spec đã duyệt và không vượt scope.
- [ ] Acceptance criteria có evidence.
- [ ] `npm.cmd run lint` pass khi áp dụng.
- [ ] `npm.cmd run build` pass khi áp dụng.
- [ ] Kiểm tra đặc thù trong ma trận FuGrade đã chạy hoặc ghi rõ chưa chạy và lý do.
- [ ] Không có secret, `.env`, database local hoặc build artifact trong diff.
- [ ] Requirement → task → code → verification truy vết được.
- [ ] Remaining risk và follow-up được ghi rõ.
- [ ] Human review hoàn tất cho thay đổi loại C–E/rủi ro cao.

## 11. Cách dùng với Codex và Claude Code

Quy trình là tool-agnostic: có thể yêu cầu trực tiếp bằng ngôn ngữ tự nhiên, ví dụ:

```text
Đọc docs/fugrade-development-lifecycle.md và tạo CONTEXT/SPEC cho
fix-excel-import-scroll. Chỉ khảo sát và viết artifact, chưa sửa code.
```

Claude Code có thể dùng các lệnh `/sdd-*` hiện nằm trong `.claude/skills/`. Codex chỉ tự phát hiện project skill sau khi chúng được chuyển sang `.agents/skills/`; trước thời điểm đó, `AGENTS.md` và prompt phải dẫn chiếu trực tiếp tới playbook này.

## 12. Human Final Review cho playbook

### AI Agent Recommendation

- Status: PENDING HUMAN REVIEW
- Scope: Chu trình phát triển FuGrade Web v1.0.0
- Recommendation: Dùng tài liệu này làm playbook vận hành chính; giữ template làm nguồn tham khảo; áp artifact đầy đủ theo mức rủi ro thay vì bắt mọi chỉnh sửa nhỏ đi cùng một quy trình nặng.
- Evidence: Kiến trúc Next.js brownfield, `.fg` legacy compatibility, Auth.js/SQLite hiện tại và các constraint đã được ghi trong `.sdd/architecture-profile.md`.
- Risks and assumptions: Chưa có automated test runner; Architecture Profile vẫn chờ duyệt; Codex skills chưa được chuyển sang `.agents/skills/`.
- Required human decision: Duyệt/revise playbook, test stack và Architecture Profile baseline.

### Human Final Review

- Status: PENDING
- Decision:
- Reviewer:
- Reviewed at:
- Follow-up:
