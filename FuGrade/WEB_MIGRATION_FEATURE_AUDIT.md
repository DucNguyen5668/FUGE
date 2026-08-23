# Kiểm kê tính năng và kế hoạch chuyển FU Grading Editor sang web

Ngày rà soát: 2026-08-19
Phạm vi: toàn bộ mã nguồn trong thư mục `FuGrade` hiện tại. Ảnh chụp màn hình chỉ được dùng để đối chiếu giao diện, không được coi là mã nguồn hay yêu cầu bổ sung.

## 1. Kết luận nhanh

- Đây là ứng dụng WinForms C#/.NET Framework 3.5, khởi động bằng `FrmFuGrade` (`Program.cs:11-17`, `FuGrade.csproj:11`).
- Có **46 control kiểu Button** trong 10 form, cộng **2 mục menu chuột phải**, **4 checkbox có nghiệp vụ** và các thao tác sửa trực tiếp trên lưới.
- Chức năng chính đã có: mở/lưu bảng điểm `.fg`, sửa điểm và comment, thêm sinh viên, thêm thành phần điểm, nhập điểm/comment từ nội dung dán, xóa toàn bộ điểm của một thành phần, tạo/sửa nhận xét `.cmt`, tạo/sửa phiếu bảo vệ `.tef`, CRUD tiêu chí chấm, tổng hợp và đưa dữ liệu sang Excel.
- Không có chức năng xóa sinh viên, xóa/đổi tên thành phần điểm, xóa bảng điểm, xóa nhận xét, xóa phiếu chấm hoặc quản trị tài khoản trong mã hiện tại.
- “Import” hiện tại không đọc Excel/CSV: người dùng dán các dòng `MSSV điểm` hoặc `MSSV comment` vào ô văn bản (`FrmImport.cs:67-147`, `FrmImport.cs:151-276`).
- “Export to Excel” không tạo file trên đĩa: chương trình mở một workbook Excel bằng COM rồi để người dùng tự Save As (`FrmSummarizeThesisResult.cs:336-459`).
- Toàn bộ dữ liệu là file cục bộ; không có database, API, đồng bộ mạng, tài khoản người dùng, phân quyền hoặc nhật ký thay đổi.
- Bốn kiểu file cũ (`.fg`, `.cmt`, `.tef`, `.master`) cần được coi là **định dạng nhập dữ liệu legacy**, không nên trở thành nơi lưu trữ chính của bản web.

## 2. Sơ đồ màn hình và file chịu trách nhiệm

| Màn hình | File giao diện | File xử lý | Vai trò khi chuyển sang web |
|---|---|---|---|
| Màn hình bảng điểm chính | `FrmFuGrade.Designer.cs` | `FrmFuGrade.cs` | Trang bảng điểm/lớp học |
| Nhập điểm hoặc comment | `FrmImport.Designer.cs` | `FrmImport.cs` | Modal import/preview/validate |
| Nhận xét đồ án | `FrmThesisComment.Designer.cs` | `FrmThesisComment.cs` | Trang nhận xét của giảng viên hướng dẫn |
| Chuẩn bị/chọn nhóm bảo vệ | `FrmDefenseGrading.Designer.cs` | `FrmDefenseGrading.cs` | Danh sách nhóm và phiên bảo vệ |
| Phiếu chấm bảo vệ | `FrmEvaluationForm.Designer.cs` | `FrmEvaluationForm.cs` | Form chấm của evaluator |
| Tổng hợp kết quả | `FrmSummarizeThesisResult.Designer.cs` | `FrmSummarizeThesisResult.cs` | Dashboard kết quả và báo cáo |
| Quản lý tiêu chí chấm | `FrmCreateFinalCPGradingItems.Designer.cs` | `FrmCreateFinalCPGradingItems.cs` | CRUD rubric/criteria |
| Chọn mã môn | `FrmChooseSujectCode.Designer.cs` | `FrmChooseSujectCode.cs` | Modal chọn mã môn khớp rubric |
| Nhập mật khẩu | `FrmPassword.Designer.cs` | `FrmPassword.cs` | Thay bằng đăng nhập/phân quyền web |
| Đặt mật khẩu file | `FrmSetPassword.Designer.cs` | `FrmSetPassword.cs` | Không bê nguyên sang web; dùng account/RBAC |

Các model nội bộ nằm ở `DefenseGrading.cs`, `DefenseStudentGrade.cs`, `GradedItem.cs`, `FinalThesisGradingItem.cs`, `ThesisComment.cs`, `ThesisStudent.cs`, `FinalGrade.cs` và `FinalGradeOfTeacher.cs`.

Riêng `TeacherGrade`, `SubjectClassGrade`, `Student` và `GradeComponent` thuộc `FuGradeLib.dll`; dự án chỉ tham chiếu DLL, không có source của các lớp này (`FuGrade.csproj:36-38`). Đây là phần cần phục dựng schema trước khi viết backend.

## 3. Toàn bộ nút và thao tác trên từng màn hình

### 3.1. `FrmFuGrade` — bảng điểm chính

Giao diện khai báo tại `FrmFuGrade.Designer.cs`; các handler nằm trong `FrmFuGrade.cs`.

| Nút/thao tác | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| Open Grading File | 72-74 | 49-193 | Mở `.fg`, ưu tiên giải mã AES + JSON; fallback BinaryFormatter; kiểm tra version và password | Upload/import `.fg` vào một bản nháp, preview rồi mới commit vào DB |
| Show | 91-93 | 196-322 | Hiện lớp đã chọn hoặc gộp các lớp, dựng cột điểm động | Route bảng điểm theo lớp; lọc/gộp bằng query, không ghép object tạm trên client |
| Search | 105-107 | 449-479 | Tìm đúng MSSV và chọn dòng | Tìm kiếm không phân biệt hoa thường; hỗ trợ tên/MSSV |
| Nhấn Enter trong ô MSSV | 118 | 482-489 | Gọi nút Search | Giữ shortcut Enter |
| Save | 124-126 | 341-397 | Lấy dữ liệu từ grid, đặt password lần đầu, mã hóa và ghi đè `.fg` | API transaction; lưu revision, người sửa, thời gian; không ghi đè file upload gốc |
| Exit | 132-134 | 32-46 | Hỏi lưu nếu có thay đổi rồi thoát toàn ứng dụng | Không cần nút thoát; cảnh báo khi rời trang có draft chưa lưu |
| Chọn/bỏ chọn grading component | 149-151 | 325-338, 850-866 | Ẩn/hiện cột điểm; chọn item cho menu chuột phải | Column chooser trên bảng |
| Import Mark (menu chuột phải) | 162-163 | 608-630 | Mở `FrmImport` cho component đang chọn | Import CSV/XLSX/clipboard, preview lỗi theo dòng trước khi ghi |
| Clear Mark (menu chuột phải) | 166-167 | 756-789 | Xóa toàn bộ giá trị của component sau xác nhận | Bulk clear có quyền riêng, xác nhận, transaction và audit log |
| Select All | 174-176 | 325-331 | Hiện tất cả cột component | Chọn tất cả cột |
| Add Grading Component | 209-211 | 792-836 | Thêm component và grade rỗng cho mọi sinh viên | Create component; thêm cả sửa tên, sắp xếp, trọng số, xóa an toàn |
| Add Student | 227-229 | 535-605 | Thêm sinh viên nếu MSSV chưa tồn tại | CRUD enrollment; validate MSSV và chống trùng ở database |
| Import Comments | 268-270 | 839-847 | Mở `FrmImport` ở chế độ comment | Import comment với preview và báo dòng lỗi |
| Merge classes | 292-294 | 880-927 | Đổi combobox thành `[All classes]` nếu các lớp có cùng component | Chế độ xem tổng hợp; chỉnh sửa phải vẫn gắn đúng class/enrollment |
| Comment For Thesis | 300-302 | 930-960 | Tạo form `.cmt` cho nhóm; chỉ bật khi số dòng không quá `MaxThesisGroupSize` | Tạo nhận xét đồ án gắn với nhóm trong DB |
| Edit Comment For Thesis File (.cmt) | 307-309 | 963-978 | Mở và sửa một file `.cmt` sau kiểm tra password | Import legacy `.cmt`, sau đó sửa entity web theo quyền |
| Thesis/CP Defense | 314-316 | 981-990 | Mở màn hình bảo vệ và ẩn form chính | Điều hướng sang module Defense |
| Sửa trực tiếp grid | 100 | 492-532 | Comment tự do; điểm phải trong 0..10; riêng Status chỉ 0/1 | Server validate kiểu dữ liệu/range; UI hiển thị lỗi tại ô |

Lưu ý: nút `Comment For Thesis` mặc định bị disable (`FrmFuGrade.Designer.cs:295`) và chỉ được bật khi số sinh viên `<= MaxThesisGroupSize`; cấu hình hiện tại là 6 (`app.config:4`, `FrmFuGrade.cs:312-320`, `FrmFuGrade.cs:633-640`). Vì ảnh đang có 50 sinh viên nên nút bị mờ là đúng logic hiện tại.

### 3.2. `FrmImport` — nhập điểm/comment bằng nội dung dán

| Nút/thao tác | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| Import | 62-64 | 53-64 | Chọn luồng import điểm hoặc comment | Upload/clipboard → parse → preview → confirm |
| Import điểm | — | 151-276 | Mỗi dòng đúng 2 token; điểm 0..10 hoặc Status 0/1; chặn MSSV trùng | Cho phép CSV/XLSX, mapping cột, locale dấu phẩy/chấm, báo lỗi theo dòng |
| Import comment | — | 67-147 | Token đầu là MSSV, phần còn lại ghép thành comment | Giữ nguyên nội dung comment, không làm mất tab/khoảng trắng |
| Exclude the first row | 79 | được đọc ở 77-81 và 161-165 | Bỏ qua dòng tiêu đề | Tự phát hiện header nhưng vẫn cho chọn thủ công |
| Search | 110-112 | 280-299 | Tìm chuỗi tiếp theo trong vùng dữ liệu dán | Search/filter trong preview |
| Close | 70-72 | 40-43 | Đóng form | Đóng modal |

### 3.3. `FrmThesisComment` — nhận xét đồ án `.cmt`

| Nút/thao tác | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| Save | 202-204 | 235-405 | Validate tiêu đề/nội dung/kết luận; tạo hoặc ghi đè `.cmt`; đặt password khi tạo mới | Lưu draft/final trong DB, revision history và chữ ký người nhận xét |
| Sửa kết luận trên grid | 196 | 194-232 | Chỉ nhận `x`/`X`; ba lựa chọn loại trừ nhau | Radio/enum: đồng ý, sửa để bảo vệ lần 2, không đồng ý |
| Show | 268-270 | 472-479 | Mở thư mục chứa file | Web dùng link tới bản ghi/tệp đã xuất |
| Close | 61-63 | 59-62 | Đóng form | Quay lại trang trước |

Các trường nghiệp vụ được lưu: giảng viên, thời gian, mã môn, lớp, học kỳ, tiêu đề VN/EN, nội dung, hình thức, thái độ, thành tựu, hạn chế và kết luận từng sinh viên (`ThesisComment.cs:13-78`, `ThesisStudent.cs:12-37`).

### 3.4. `FrmDefenseGrading` — chuẩn bị và mở phiên chấm bảo vệ

| Nút | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| Browse | 51-53 | 23-31 | Chọn thư mục rồi tự Load | Chọn/import các nhận xét legacy hoặc chọn nhóm có sẵn trong DB |
| Load Presentation Group | 58-60 | 34-85 | Đọc toàn bộ `.cmt` ngay trong thư mục, tạo danh sách nhóm | API tạo defense session từ thesis reviews đã duyệt |
| Show Evaluation Form | 87-89 | 118-177 | Validate tên evaluator chỉ ASCII; đặt password; mở phiếu chấm | Evaluator lấy từ tài khoản đăng nhập; phân công trước, không nhập tên tự do |
| Edit Evaluation Form File (.tef) | 106-108 | 180-237 | Mở `.tef` read-only hoặc edit sau password | Trang xem/sửa evaluation theo quyền và trạng thái khóa |
| Summerize Result | 122-124 | 245-249 | Mở màn hình tổng hợp | Route báo cáo defense |
| Close | 79-81 | 88-91 | Thoát toàn ứng dụng | Không áp dụng; chỉ điều hướng/đăng xuất |

### 3.5. `FrmEvaluationForm` — phiếu chấm `.tef`

| Nút/thao tác | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| Sửa điểm trên grid | 50-52 | 311-379 | Điểm tiêu chí phải 0..max; tự cộng tổng cột | Form rubric; tính tổng ở server và client, server là nguồn chuẩn |
| Copy [Group mark] to students | 82-84 | 382-392 | Chép cột điểm nhóm sang tất cả sinh viên | Bulk action có preview; bỏ qua sinh viên không đủ điều kiện nếu quy tắc yêu cầu |
| Save | 113-115 | 395-489 | Tạo tên `.tef`, chọn thư mục, copy điểm vào model rồi BinaryFormatter serialize | Save draft/submit; khóa sau submit; transaction và audit |
| Show | 142-144 | 492-499 | Mở thư mục lưu `.tef` | Mở trang/tải bản export |
| Show supervisor's comment | 202-204 | 502-511 | Mở nhận xét supervisor ở chế độ read-only | Drawer/modal đọc thesis review liên quan |
| Close | 58-60 | 52-81 | Hỏi lưu nếu chưa lưu | Cảnh báo draft chưa lưu |
| Sửa Note | 99 | 514-517 | Đánh dấu phiếu chưa lưu | Textarea note + autosave/version |

Form này đọc rubric từ file master, sinh cột cho từng sinh viên và lưu `DefenseGrading`, `DefenseStudentGrade`, `GradedItem` (`FrmEvaluationForm.cs:84-308`).

### 3.6. `FrmSummarizeThesisResult` — tổng hợp và export

| Nút/thao tác | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| Browse | 62-64 | 30-37 | Chọn thư mục gốc chứa các thư mục nhóm | Chọn kỳ/đợt bảo vệ trong DB |
| Show Folder Details | 69-71 | 53-69 | Dựng cây thư mục | Danh sách nhóm/evaluation có filter |
| Chọn node cây | 85 | 72-83 | Liệt kê `.tef` của thư mục được chọn | Hiện evaluation của nhóm |
| Validate | 90-92 | 86-197 | Kiểm tra cấu trúc và một phần nội dung `.tef` | Validation toàn bộ evaluation trước khi khóa đợt |
| Result | 103-105 | 200-333 | Tính điểm từng evaluator, trung bình và note | Report query/service có test cho công thức |
| Export to Excel | 111-113 | 336-459 | Tạo 2 sheet `Summary` và `Graded statistics`, mở Excel; chưa SaveAs | Server tạo `.xlsx` thật và trả download URL |
| Create Grading Item | 119-121 | 464-468 | Mở màn hình CRUD rubric | Route quản trị rubric |
| Open Folder | 144-146 | 471-478 | Mở thư mục bằng Explorer | Không áp dụng; dùng trang/tải file |
| Close | 45-47 | 24-27 | Đóng form | Quay lại |

### 3.7. `FrmCreateFinalCPGradingItems` — CRUD rubric/tiêu chí

| Nút/thao tác | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| Load | 130-132 | 118-164 | Lọc tiêu chí theo SubjectCode và hiển thị tổng scale | GET rubric theo môn/version |
| Save | 137-139 | 28-98 | Nếu chưa chọn: Create; nếu đã chọn: Update; ghi file master | POST/PATCH rubric item với unique constraint |
| New | 144-146 | 110-115 | Xóa tên/scale và bỏ chọn current item | Mở form tạo mới |
| Delete | 151-153 | 224-233 | Xóa current item ngay, không hỏi xác nhận | Soft delete/confirm; cấm xóa rubric đã được dùng hoặc tạo version mới |
| Close | 49-51 | 22-25 | Đóng form | Quay lại |
| Chọn dòng tiêu chí | 112 | 207-221 | Đổ dữ liệu vào form để sửa | Chọn/edit item |
| Chọn SubjectCode có sẵn | 176 | 236-244 | Tự Load rubric của môn | Subject selector |

Schema tiêu chí: `SubjectCode`, `Major`, `Minor`, `ItemGroup`, `GradingItem`, `Scale` (`FinalThesisGradingItem.cs:12-37`).

### 3.8. Các dialog phụ

| Form/nút | Designer | Handler | Hành vi hiện tại | Kế hoạch web |
|---|---:|---:|---|---|
| `FrmChooseSujectCode` / Ok | 40-42 | `FrmChooseSujectCode.cs:35-39` | Chọn mã môn khi có nhiều rubric khớp kiểu `Contains` | Select rõ ràng theo rubric ID/version |
| `FrmPassword` / Ok | 41-43 | `FrmPassword.cs:28-39` | Bắt buộc password không rỗng | Login/re-auth theo account |
| `FrmPassword` / Cancel | 49-51 | `FrmPassword.cs:42-46` | Xóa password và đóng | Đóng dialog |
| `FrmSetPassword` / Ok | 39-41 | `FrmSetPassword.cs:33-52` | Bắt buộc nhập và khớp password | Không dùng password theo file |
| `FrmSetPassword` / Cancel | 32-34 | `FrmSetPassword.cs:55-59` | Xóa password và đóng | Đóng dialog |
| `FrmSetPassword` / Show password | 69-71 | `FrmSetPassword.cs:62-75` | Hiện/ẩn 2 ô password | Có thể giữ trong form đổi mật khẩu tài khoản |

## 4. Ma trận import, export, thêm, sửa, xóa

| Đối tượng | Import | Export/lưu | Thêm | Sửa | Xóa | Tình trạng hiện tại |
|---|---|---|---|---|---|---|
| Bảng điểm `.fg` | Open `.fg` | Save ghi đè `.fg` | Không có trong repo | Có | Không | Có fallback định dạng cũ nhưng không có preview/backup |
| Sinh viên trong lớp | Không | Theo `.fg` | Có | Không sửa MSSV/tên | **Không có** | Cần CRUD đầy đủ trên web |
| Thành phần điểm | Import giá trị bằng paste | Theo `.fg` | Có | Sửa giá trị từng sinh viên | Chỉ `Clear Mark`, **không xóa component** | Cần rename/order/weight/delete policy |
| Comment sinh viên | Import bằng paste | Theo `.fg` | Có qua nhập/sửa | Có | Có thể xóa từng ô | Cần lịch sử thay đổi |
| Nhận xét đồ án `.cmt` | Mở file để edit; dùng `.cmt` tạo group | Save file | Có | Có | **Không có** | Cần workflow draft/final/lock |
| Rubric/tiêu chí `.master` | Load file | Save file | Có | Có | Có | CRUD gần đủ; thiếu versioning và ràng buộc sử dụng |
| Phiếu chấm `.tef` | Mở file | Save file | Có | Có/read-only | **Không có** | Cần assignment, submit, reopen policy |
| Kết quả tổng hợp | Đọc nhiều `.tef` | Mở workbook Excel | Tự tính | Không | Không | Export chưa tạo file vật lý |

### Những chức năng nên bổ sung cho web nhưng mã desktop chưa có

1. Đăng nhập, quên mật khẩu, session và phân quyền Admin/Coordinator/Teacher/Evaluator/Viewer.
2. CRUD sinh viên, lớp, môn, thành phần điểm và đợt bảo vệ; soft delete và khôi phục.
3. Import CSV/XLSX thật, mapping cột, preview, dry-run, tải file lỗi.
4. Export `.xlsx`/`.csv` thật, có thời gian tạo, người tạo và checksum.
5. Lịch sử phiên bản, audit log, optimistic locking để hai người không ghi đè nhau.
6. Draft/Submit/Lock/Reopen cho nhận xét và phiếu chấm.
7. Dashboard tiến độ: nhóm chưa nhận xét, evaluator chưa nộp, rubric sai tổng scale, dữ liệu thiếu.
8. Backup, retention, quyền tải file và chống truy cập chéo lớp/đợt.

## 5. Định dạng dữ liệu cũ và cách di trú

| Định dạng | Code đọc/ghi | Nội dung | Cách xử lý khi lên web |
|---|---|---|---|
| `.fg` | `FrmFuGrade.cs:49-193`, `341-395`; `AesOperation.cs:12-81` | AES với key mặc định hard-code + IV toàn 0, payload JSON; fallback BinaryFormatter | Chỉ dùng importer migration. Giữ file gốc read-only, hash, giải mã trong service cô lập, validate schema rồi mới nhập DB |
| `.cmt` | `FrmThesisComment.cs:65-148`, `235-405` | BinaryFormatter của `ThesisComment`; password là MD5 trong object | Không deserialize trực tiếp file người dùng trên web; dùng converter offline tin cậy rồi nhập JSON |
| `.tef` | `FrmDefenseGrading.cs:180-237`, `FrmEvaluationForm.cs:395-489`, `FrmSummarizeThesisResult.cs:96-333` | BinaryFormatter của `DefenseGrading` | Cùng nguyên tắc converter offline; lưu evaluation chuẩn hóa trong DB |
| `.master` | `FrmCreateFinalCPGradingItems.cs:101-106`, `167-203`; `FrmEvaluationForm.cs:96-108` | BinaryFormatter danh sách rubric item | Import một lần thành rubric có version |
| Excel | `FrmSummarizeThesisResult.cs:336-459` | Workbook COM, 2 sheet, chưa SaveAs | Dùng thư viện tạo `.xlsx` phía server; test header, công thức và thứ tự cột |

`BinaryFormatter` không an toàn với dữ liệu không tin cậy và tuyệt đối không được đưa vào endpoint web. Password file hiện dùng MD5 không salt (`Helper.cs:11-27`), còn AES dùng key cố định trong source (`AesOperation.cs:17-28`, `80-81`); cả hai phải được thay bằng xác thực tài khoản, hash mật khẩu hiện đại và mã hóa secret do server quản lý.

## 6. Các lỗi/rủi ro cần sửa trước hoặc trong lúc chuyển web

### P0 — có thể làm sai/mất dữ liệu hoặc gây lỗ hổng

1. **Không dùng BinaryFormatter trên web.** `.cmt`, `.tef`, `.master` và `.fg` fallback đều deserialize object trực tiếp.
2. **Đường dẫn master không thống nhất:** màn hình CRUD ghi `FinalThesisGradingItems.master` theo working directory (`FrmCreateFinalCPGradingItems.cs:101-106`, `250`), nhưng form chấm đọc `MasterFile\FinalThesisGradingItems.master` dưới thư mục executable (`FrmEvaluationForm.cs:96-108`).
3. **Sửa `.cmt` có thể làm mất ClassName/Semester:** khi mở để sửa, caller chỉ truyền `CmtFileName` (`FrmFuGrade.cs:963-975`), nhưng lúc save update lại gán từ thuộc tính form có thể đang `null` (`FrmThesisComment.cs:371-386`).
4. **Xóa điểm đã có trong `.tef` có thể không thật sự xóa:** Save chỉ cập nhật model khi cell khác `null`; cell bị clear giữ lại giá trị cũ trong object (`FrmEvaluationForm.cs:449-470`).
5. **Import file legacy phải giữ nguyên bản gốc.** Không overwrite file upload; lưu hash, tên gốc, người upload và kết quả converter.

### P1 — sai nghiệp vụ hoặc hành vi khó hiểu

1. `Validate` chỉ xử lý thư mục con đầu tiên vì `num` luôn bằng 0 và không có vòng lặp kiểm tra tất cả (`FrmSummarizeThesisResult.cs:136-193`).
2. Kết luận “Revised for the second defense” và “Disagree” đều thành `Conclusion = null`, rồi summary ghi “Disagree to defense” (`FrmDefenseGrading.cs:54-65`, `FrmSummarizeThesisResult.cs:288-307`).
3. Thêm sinh viên dựng row thiếu ô placeholder cho cột Comment: danh sách bắt đầu bằng Roll, Name rồi đi thẳng sang điểm (`FrmFuGrade.cs:571-589`). Điều này có nguy cơ lệch cột hiển thị.
4. Rubric có `ItemGroup` nhưng lúc tạo `GradedItem` không gán `GroupItem`, nên mở lại `.tef` có thể mất nhóm tiêu chí (`FrmEvaluationForm.cs:164-181`).
5. Chế độ edit `.tef`: nếu file không có password và người dùng chọn edit thay vì read-only, code không mở form nào (`FrmDefenseGrading.cs:190-235`).
6. Ở chế độ Merge classes, Add Student/Add Component vẫn có thể hiện là bật nhưng handler tìm class bằng nhãn `[All classes]`, không khớp class thật (`FrmFuGrade.cs:535-603`, `792-834`, `880-927`).
7. `Copy Group Mark` chép cả total row và mọi sinh viên, chưa có rule riêng cho sinh viên không được bảo vệ (`FrmEvaluationForm.cs:382-392`).
8. Tổng scale rubric chỉ được hiển thị, không bị ràng buộc bằng 10 hoặc một giá trị cấu hình (`FrmCreateFinalCPGradingItems.cs:129-159`).
9. Export chỉ nhóm header theo `Title`; hai nhóm khác nhau trùng title có thể bị gộp phần trình bày (`FrmSummarizeThesisResult.cs:420-425`).
10. Label `students` thực tế dùng biến đếm số file `.tef`, không phải số sinh viên (`FrmSummarizeThesisResult.cs:208-218`, `331`).

## 7. Kiến trúc web đề xuất

Một hướng ít rủi ro là giữ C# cho backend (ASP.NET Core), frontend React/Vue hoặc Razor/Blazor tùy đội ngũ, database quan hệ PostgreSQL/SQL Server, object storage cho file gốc và file export. Không tái sử dụng WinForms hoặc các lớp thao tác file trực tiếp trong request web.

### 7.1. Bảng dữ liệu tối thiểu

- `users`, `roles`, `user_roles`
- `semesters`, `subjects`, `classes`, `students`, `enrollments`
- `grade_sheets`, `grade_components`, `grades`, `student_comments`
- `thesis_groups`, `thesis_reviews`, `thesis_review_students`
- `rubrics`, `rubric_versions`, `rubric_items`
- `defense_sessions`, `defense_groups`, `evaluator_assignments`
- `evaluations`, `evaluation_scores`, `evaluation_notes`
- `legacy_imports`, `export_jobs`, `audit_logs`

Các bảng điểm/nhận xét/evaluation nên có `status`, `version`, `created_by`, `updated_by`, timestamps và khóa ngoại rõ ràng. Điểm dùng kiểu decimal có precision cố định, không dùng `float`.

### 7.2. API chính

| Nhóm API | Endpoint gợi ý | Thay cho |
|---|---|---|
| Legacy migration | `POST /api/legacy-imports/fg`, `/cmt`, `/tef` | Open file và đọc BinaryFormatter/AES trực tiếp |
| Bảng điểm | `GET /api/classes/{id}/grade-sheet` | Show/Merge/Search |
| Điểm | `PATCH /api/grades`, `POST /api/grades/bulk-import`, `DELETE /api/components/{id}/grades` | Sửa grid, Import Mark, Clear Mark |
| Sinh viên/component | REST endpoints cho enrollments và components | Add Student/Add Component + CRUD còn thiếu |
| Thesis review | `POST/PATCH /api/thesis-reviews`, `POST /submit` | `.cmt` create/edit/save |
| Rubric | `POST/PATCH/DELETE /api/rubrics/{id}/items`, `POST /versions` | `.master` CRUD |
| Defense | `POST /api/defense-sessions`, assignments, evaluations, submit/reopen | `.tef` workflow |
| Báo cáo | `GET /api/defense-sessions/{id}/results`, `/export.xlsx` | Result/Export to Excel |
| Audit | `GET /api/audit-logs?...` | Chức năng mới bắt buộc cho môi trường nhiều người dùng |

Mọi kiểm tra điểm, quyền truy cập, trạng thái khóa và công thức tổng hợp phải chạy ở backend; validation phía frontend chỉ để phản hồi nhanh.

## 8. Kế hoạch triển khai theo giai đoạn

### Giai đoạn 0 — đóng băng và hiểu dữ liệu legacy

- Thu thập bộ mẫu `.fg`, `.cmt`, `.tef`, `.master`; sao lưu read-only và ghi SHA-256.
- Phục dựng chính xác schema của `FuGradeLib.dll` hoặc xin source gốc.
- Viết converter offline → JSON chuẩn hóa; không cho web gọi BinaryFormatter.
- Tạo fixture/test cho: điểm 0, điểm 10, Status 0/1, comment Unicode, nhiều lớp, nhóm bảo vệ lần 2, nhiều evaluator.
- Chốt công thức điểm, quy tắc scale, quyền sửa sau submit và định nghĩa từng trạng thái kết luận.

**Điều kiện hoàn tất:** cùng một bộ file mẫu cho kết quả đối chiếu giữa desktop và JSON mới; file gốc không bị thay đổi.

### Giai đoạn 1 — MVP bảng điểm online

- Account, RBAC, lớp/môn/sinh viên/component.
- Import `.fg` legacy có preview; xem lớp, merge/filter/search.
- Sửa điểm/comment, lưu transaction, revision và audit.
- CRUD sinh viên/component; bulk clear có xác nhận.
- Import CSV/XLSX/clipboard và export CSV/XLSX thật.

**Điều kiện hoàn tất:** hai người sửa đồng thời không âm thầm ghi đè; mọi thay đổi có audit; validation 0..10 và Status 0/1 chạy ở backend.

### Giai đoạn 2 — nhận xét đồ án và phiếu bảo vệ

- Thesis group/review, ba kết luận dạng enum, draft/submit/lock.
- Rubric có version và tổng scale hợp lệ.
- Defense session, phân công evaluator, form chấm, copy group mark.
- Import `.cmt`/`.tef` legacy qua converter; read-only đối với bản gốc.

**Điều kiện hoàn tất:** evaluator chỉ thấy nhóm được phân công; submit xong bị khóa; coordinator có luồng reopen và lý do được audit.

### Giai đoạn 3 — tổng hợp, export và vận hành

- Dashboard tiến độ và validation toàn bộ đợt.
- Công thức kết quả có automated tests; export `.xlsx` hai sheet tương đương bản desktop.
- Backup/restore, monitoring, rate limit, antivirus/file-type checks, retention.
- UAT với giảng viên trên dữ liệu sao chép đã ẩn thông tin nhạy cảm.

## 9. Checklist nghiệm thu quan trọng

- Không có endpoint nào deserialize BinaryFormatter từ upload.
- Không lưu password kiểu MD5 và không dùng AES key hard-code.
- MSSV unique đúng phạm vi đã chốt; không mất số 0 đầu nếu có.
- Điểm là decimal, làm tròn có quy tắc duy nhất và test được.
- Import là atomic hoặc có báo cáo rõ từng dòng thành công/thất bại.
- Clear/delete cần quyền, xác nhận, audit và khả năng phục hồi theo policy.
- Kết luận bảo vệ lần 2 không bị gộp thành không đồng ý bảo vệ.
- Rubric đang dùng không bị sửa ngược lịch sử; thay đổi tạo version mới.
- File export được tạo thật, tải lại được, có đúng 2 sheet và số liệu khớp màn hình.
- File legacy gốc luôn được bảo toàn và có checksum.

## 10. Thứ tự nên bắt đầu

1. Chốt schema `FuGradeLib` và bộ mẫu file legacy.
2. Sửa/chốt các quy tắc P0/P1 ở mục 6 bằng test nghiệp vụ, không bê nguyên bug desktop lên web.
3. Thiết kế database + RBAC + audit.
4. Làm MVP bảng điểm trước; sau khi ổn mới làm Thesis/Defense.
5. Làm converter legacy độc lập với web runtime.
6. Cuối cùng mới đối chiếu report và Excel export.
