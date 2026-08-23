# 📋 FuGrade — Ghi chú kỹ thuật chi tiết
**Phân tích mã nguồn: Nhập/Xuất file `.fg` & các nút màn hình chính**
**Ngày:** 2026-08-23 | **Phạm vi:** `FrmFuGrade.cs`, `FrmImport.cs`, `AesOperation.cs`, `Helper.cs`

---

## 1. TỔNG QUAN DỰ ÁN

FuGrade là ứng dụng **WinForms C# / .NET Framework 3.5** dùng cho giảng viên FPT University chấm điểm, quản lý điểm sinh viên. Dữ liệu lưu trong file `.fg` cục bộ — không có database hay API.

```
Program.cs (Entry point)
  └─► FrmFuGrade (Form chính — bảng điểm)
        ├─► FrmImport         (nhập điểm/comment bằng paste)
        ├─► FrmThesisComment  (nhận xét đồ án .cmt)
        ├─► FrmDefenseGrading (chuẩn bị phiên bảo vệ)
        │     ├─► FrmEvaluationForm  (phiếu chấm .tef)
        │     └─► FrmSummarizeThesisResult (tổng hợp + export Excel)
        │           └─► FrmCreateFinalCPGradingItems (CRUD rubric)
        └─► FrmPassword / FrmSetPassword (dialog mật khẩu)
```

**Các kiểu file được dùng:**

| Kiểu file | Nội dung | Mã hóa |
|-----------|----------|--------|
| `.fg` | Bảng điểm giảng viên (TeacherGrade) | AES-128 + JSON (mới) hoặc BinaryFormatter (cũ) |
| `.cmt` | Nhận xét đồ án (ThesisComment) | BinaryFormatter |
| `.tef` | Phiếu chấm bảo vệ (DefenseGrading) | BinaryFormatter |
| `.master` | Rubric/tiêu chí chấm | BinaryFormatter |

---

## 2. FILE `.fg` — CẤU TRÚC DỮ LIỆU

### 2.1. Object model (từ FuGradeLib.dll)

```
TeacherGrade
├── Login: string                  // tên giảng viên
├── Semester: string               // học kỳ
├── Version: string                // phiên bản file, e.g. "1.1"
├── Password: string               // MD5 hash của mật khẩu (hoặc "" nếu không có)
└── SubjectClassGrades: List<SubjectClassGrade>
      ├── Subject: string          // mã môn, e.g. "ITE302c"
      ├── Class: string            // tên lớp, e.g. "SE1906-NET"
      ├── Components: List<string> // tên thành phần điểm, e.g. ["TE", "TE Rest"]
      └── Students: List<Student>
            ├── Roll: string       // MSSV
            ├── Name: string       // Họ tên
            ├── Comment: string    // Comment tự do
            └── Grades: List<GradeComponent>
                  ├── Component: string  // tên component (khớp với Components)
                  └── Grade: float?      // điểm 0..10, null nếu chưa có
```

### 2.2. Mã hóa AES (file: `AesOperation.cs`)

```csharp
// Key cứng trong source — KHÔNG AN TOÀN
private static readonly string DEFAULT_KEY = "l10ca968o8e4133tyne2ea2315g19377";
// IV toàn 0 (16 byte)
byte[] iv = new byte[16];
```

**Luồng Encrypt (Ghi file):**
```
TeacherGrade object
  → JsonConvert.SerializeObject()   // JSON string
  → AesOperation.EncryptString()    // AES-128-CBC với IV=0
  → Convert.ToBase64String()        // Base64 string
  → File.WriteAllText(path, ...)    // ghi vào file .fg
```

**Luồng Decrypt (Đọc file):**
```
File.ReadAllText(path)              // Base64 string
  → AesOperation.DecryptString()   // giải mã AES
  → JsonConvert.DeserializeObject<TeacherGrade>()  // parse JSON
  → TeacherGrade object
```

---

## 3. PHÂN TÍCH CODE — CÁC ĐOẠN XỬ LÝ FILE `.fg`

### 3.1. ĐỌC FILE — `btnOpenGradingFile_Click` (dòng 49–193)

```csharp
private void btnOpenGradingFile_Click(object sender, EventArgs e)
{
    // BƯỚC 1: Hỏi lưu nếu đang có dữ liệu chưa lưu
    if (NeedSave) { /* hỏi Yes/No lưu trước */ }

    // BƯỚC 2: Mở hộp thoại chọn file .fg
    DialogResult result = openFileDialog.ShowDialog();
    if (result == DialogResult.OK)
    {
        string fileName = openFileDialog.FileName;
        txtGradingFile.Text = fileName;

        // BƯỚC 3A: Thử đọc format MỚI (AES + JSON)
        try {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string cipherText = sr.ReadToEnd();
                string json = AesOperation.DecryptString(null, cipherText); // key=null → dùng DEFAULT_KEY
                tg = JsonConvert.DeserializeObject<TeacherGrade>(json);
            }
        }
        catch
        {
            // BƯỚC 3B: Fallback — thử đọc format CŨ (BinaryFormatter)
            try {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                tg = (TeacherGrade) new BinaryFormatter {
                    AssemblyFormat = FormatterAssemblyStyle.Simple
                }.Deserialize(fs);
                fs.Close();
            }
            catch { }
        }

        // BƯỚC 4: Kiểm tra đọc thành công không
        if (tg == null) {
            MessageBox.Show("Cannot read the [" + fileName + "] file.");
            return;
        }

        // BƯỚC 5: Kiểm tra version
        // Nếu version file > version app → không mở được
        int[] fileVer = tg.Version.Split('.').Select(int.Parse).ToArray();
        int[] appVer  = Version.Split('.').Select(int.Parse).ToArray();
        if (appVer[0] < fileVer[0] || (appVer[0]==fileVer[0] && fileVer[1]>appVer[1]))
        {
            MessageBox.Show("You must use FUGE version " + tg.Version + " ...");
            return;
        }

        // BƯỚC 6: Kiểm tra mật khẩu
        if (!tg.Password.Equals(""))
        {
            FrmPassword dlg = new FrmPassword();
            if (dlg.ShowDialog() != DialogResult.OK) { tg = null; return; }

            MD5 md5 = MD5.Create();
            bool ok = Helper.VerifyMd5Hash(md5, dlg.Password, tg.Password);
            if (!ok) { MessageBox.Show("Incorrect password!"); tg = null; return; }
        }

        // BƯỚC 7: Nạp danh sách lớp vào ComboBox
        List<string> classes = tg.SubjectClassGrades
            .Select(scg => scg.Subject + "/" + scg.Class)
            .ToList();
        cboSubClass.DataSource = classes;
        lblTeacher.Text = "Teacher: " + tg.Login;
        lblSubClass.Text = "Subject/Class(" + tg.SubjectClassGrades.Count + "):";

        // BƯỚC 8: Reset UI
        dgvGrading.Rows.Clear(); dgvGrading.Columns.Clear();
        chkListBoxComp.Items.Clear();
        // ... (ẩn/reset các control)

        // BƯỚC 9: Kiểm tra các lớp có cùng components không (để enable Merge)
        string first = ConvertListToString(tg.SubjectClassGrades[0].Components);
        for (int i = 1; i < tg.SubjectClassGrades.Count; i++) {
            if (!first.Equals(ConvertListToString(tg.SubjectClassGrades[i].Components))) {
                chbMergeClass.Visible = false;
                return;
            }
        }
        chbMergeClass.Visible = true;
    }
}
```

> **Điểm yếu P0:** BinaryFormatter trong fallback có thể bị khai thác nếu file bị giả mạo. Không có backup trước khi đọc.

---

### 3.2. GHI FILE — `btnSave_Click` (dòng 341–397)

```csharp
private void btnSave_Click(object sender, EventArgs e)
{
    if (tg == null || txtGradingFile.Text.Trim() == "") return;

    // BƯỚC 1: Đặt mật khẩu lần đầu nếu chưa có
    if (tg.Password == "")
    {
        FrmSetPassword dlg = new FrmSetPassword();
        dlg.ShowDialog(this);
        if (dlg.Password == "") return; // người dùng hủy
        MD5 md5 = MD5.Create();
        tg.Password = Helper.GetMd5Hash(md5, dlg.Password); // lưu MD5 không salt
    }

    // BƯỚC 2: Đọc lại dữ liệu từ DataGridView → cập nhật tg object
    string currentClass = lblSubjectClass.Text;
    foreach (DataGridViewRow row in dgvGrading.Rows)
    {
        if (row.Cells[0].Value == null) continue;
        string roll = row.Cells[0].Value.ToString();
        Student student = GetStudent(currentClass, roll);

        // Cập nhật Comment (cột index 2)
        student.Comment = row.Cells[2].Value?.ToString();

        // Cập nhật từng thành phần điểm (từ cột 3 trở đi)
        for (int i = 3; i < row.Cells.Count; i++)
        {
            string compName = dgvGrading.Columns[i].Name;
            GradeComponent gc = student.Grades.FirstOrDefault(g => g.Component == compName);
            if (gc != null)
                gc.Grade = (row.Cells[i].Value == null)
                    ? (float?)null
                    : Convert.ToSingle(row.Cells[i].Value.ToString());
        }
    }

    // BƯỚC 3: Serialize → Encrypt → Ghi file (GHI ĐÈ, không backup)
    string encrypted = AesOperation.EncryptString(null, JsonConvert.SerializeObject(tg));
    File.WriteAllText(txtGradingFile.Text, encrypted);

    NeedSave = false;
    MessageBox.Show("File saved!");
}
```

> **Điểm yếu P0:** Ghi đè trực tiếp, không có backup. Password lưu MD5 không salt — dễ crack bằng rainbow table.

---

### 3.3. HÀM TÌM SINH VIÊN — `GetStudent` (dòng 400–446)

```csharp
private Student GetStudent(string subClass, string roll)
{
    // Chế độ [All classes] — tìm trên tất cả lớp
    if (subClass == "[All classes]" && cboSubClass.Items.Count == 1)
    {
        foreach (var scg in tg.SubjectClassGrades)
            foreach (var s in scg.Students)
                if (s.Roll.Trim().ToUpper() == roll.Trim().ToUpper())
                    return s;
    }
    else
    {
        // Tìm trong lớp cụ thể
        foreach (var scg in tg.SubjectClassGrades)
        {
            if ((scg.Subject + "/" + scg.Class) == subClass)
                foreach (var s in scg.Students)
                    if (s.Roll.Trim().ToUpper() == roll.Trim().ToUpper())
                        return s;
        }
    }
    return null;
}
```

---

## 4. CÁC NÚT XỬ LÝ — MÀN HÌNH CHÍNH `FrmFuGrade`

### 4.1. `btnShow` — Hiển thị bảng điểm (dòng 196–322)

**Chức năng:** Nạp dữ liệu của lớp được chọn (hoặc tất cả lớp) vào DataGridView.

```
Trigger: Click nút "Show"

Luồng:
1. Kiểm tra tg != null
2. Hỏi lưu nếu NeedSave = true
3. Reset DataGridView (xóa tất cả rows/columns)
4. Xác định SubjectClassGrade (scg) cần hiển thị:
   - Nếu KHÔNG merge: tìm scg khớp với cboSubClass.Text
   - Nếu merge: tạo scg mới với Class="All", gộp Students từ tất cả lớp
5. Tạo cột: Roll (readonly), Name (readonly, frozen), Comment, + N cột điểm (ẩn ban đầu)
6. Điền từng sinh viên vào row:
   [Roll, Name, Comment, grade1, grade2, ...]
   - Nếu student.Grades.Count == 0 → thêm GradeComponent rỗng
7. Đánh số thứ tự row header
8. Cập nhật label lblGradingDetails với số sinh viên
9. Enable groupBoxAddStud
10. Kiểm tra: nếu số row <= MaxThesisGroupSize (=6) → enable btnComment
```

---

### 4.2. `btnSearch` — Tìm kiếm sinh viên (dòng 449–479)

**Chức năng:** Tìm đúng MSSV và highlight row.

```csharp
private void btnSearch_Click(object sender, EventArgs e)
{
    string query = txtRoll.Text.Trim().ToUpper();
    foreach (DataGridViewRow row in dgvGrading.Rows)
    {
        if (row.Cells[0].Value != null)
            row.Selected = row.Cells[0].Value.ToString().ToUpper() == query;
    }
    if (dgvGrading.SelectedRows.Count > 0)
        dgvGrading.FirstDisplayedScrollingRowIndex = dgvGrading.SelectedRows[0].Index;
    else
        MessageBox.Show("Not found!");
}
// Phím Enter trong txtRoll cũng gọi btnSearch.PerformClick()
```

> **Hạn chế:** Chỉ tìm chính xác MSSV, không tìm theo tên, không tìm mờ.

---

### 4.3. `dgvGrading_CellEndEdit` — Validate điểm khi sửa trực tiếp (dòng 492–532)

```csharp
private void dgvGrading_CellEndEdit(object sender, DataGridViewCellEventArgs e)
{
    NeedSave = true;

    if (e.ColumnIndex == 2) return; // Cột Comment — không validate

    // Cột "Status" (component đặc biệt): chỉ nhận "0" hoặc "1"
    if (dgvGrading.Columns[e.ColumnIndex].Name == "Status")
    {
        string val = dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
        if (val != null && val.Trim() != "1" && val.Trim() != "0")
        {
            MessageBox.Show("Grade value must be 1 or 0 (1=pass, 0=fail)!");
            dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
            return;
        }
    }

    // Các cột điểm thường: phải là số 0..10
    string s = dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
    if (s != null)
    {
        float val;
        if (!float.TryParse(s, out val) || val < 0 || val > 10)
        {
            MessageBox.Show("Mark value is between 0 and 10");
            dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
        }
    }
}
```

---

### 4.4. `btnAdd` — Thêm sinh viên (dòng 535–605)

```csharp
private void btnAdd_Click(object sender, EventArgs e)
{
    string roll = txtRollNew.Text.Trim().ToUpper();
    if (roll == "") return;

    // Kiểm tra MSSV đã tồn tại chưa (trong grid)
    foreach (DataGridViewRow row in dgvGrading.Rows)
    {
        if (row.Cells[0].Value?.ToString().Trim().ToUpper() == roll)
        {
            MessageBox.Show("Student already exists!");
            row.Selected = true;
            dgvGrading.FirstDisplayedScrollingRowIndex = row.Index;
            return;
        }
    }

    // Tìm SubjectClassGrade tương ứng (theo lblSubjectClass.Text)
    string currentClass = lblSubjectClass.Text;
    foreach (var scg in tg.SubjectClassGrades)
    {
        if (scg.Subject + "/" + scg.Class == currentClass)
        {
            // Tạo Student mới
            Student s = new Student { Roll = roll, Name = txtName.Text.Trim() };
            s.Grades = scg.Components.Select(c => new GradeComponent {
                Component = c, Grade = null
            }).ToList();

            // Thêm vào model
            scg.Students.Add(s);

            // Thêm row vào DataGridView
            // BUG: list chỉ có Roll, Name rồi đi thẳng vào grades (thiếu placeholder Comment)
            var row = new List<string> { s.Roll, s.Name };
            row.AddRange(scg.Components.Select(_ => (string)null));
            dgvGrading.Rows.Add(row.ToArray());

            NeedSave = true;
            break;
        }
    }
}
```

> **Bug P1 (dòng 574-585):** Row mới thiếu ô placeholder cho cột `Comment` (index 2). List dựng theo `[Roll, Name, grade1, grade2,...]` — lệch 1 cột so với grid thật có 3 cột cố định `[Roll, Name, Comment]`.

---

### 4.5. `importMarkToolStripMenuItem_Click` — Import Mark (menu chuột phải, dòng 608–630)

```csharp
private void importMarkToolStripMenuItem_Click(object sender, EventArgs e)
{
    if (chkListBoxComp.SelectedItem == null) return;

    string markComponent = chkListBoxComp.SelectedItem.ToString();
    FrmImport dlg = new FrmImport(this);
    dlg.SubjectClass    = lblSubjectClass.Text;
    dlg.MarkComponent   = markComponent;
    dlg.IsImportComments = false;

    // Phát hiện "Status" component (chỉ nhận 0/1)
    dlg.IsInputStatus = (dgvGrading.ColumnCount == 4
                         && dgvGrading.Columns[3].Name == "Status");

    dlg.ShowDialog(this);
}
```

---

### 4.6. `clearMarkToolStripMenuItem_Click` — Clear Mark (menu chuột phải, dòng 756–789)

```csharp
private void clearMarkToolStripMenuItem_Click(object sender, EventArgs e)
{
    if (chkListBoxComp.SelectedItem == null) return;
    string comp = chkListBoxComp.SelectedItem.ToString();

    // Xác nhận trước khi xóa
    if (MessageBox.Show("Do you really want to clear all '" + comp + "' marks?",
        MessageBoxButtons.YesNo) == DialogResult.Yes)
    {
        // Tìm index cột tương ứng
        int colIndex = -1;
        for (int i = 3; i < dgvGrading.ColumnCount; i++)
            if (dgvGrading.Columns[i].Name == comp) { colIndex = i; break; }

        // Xóa toàn bộ giá trị trong cột đó
        foreach (DataGridViewRow row in dgvGrading.Rows)
            if (row.Cells[0].Value != null)
                row.Cells[colIndex].Value = null;

        NeedSave = true;
    }
}
```

> **Lưu ý:** Xóa chỉ ở DataGridView (UI). Dữ liệu thật trong `tg` object chỉ được cập nhật khi bấm **Save**. Không có undo.

---

### 4.7. `btnAddComp_Click` — Thêm Grading Component (dòng 792–836)

```csharp
private void btnAddComp_Click(object sender, EventArgs e)
{
    string compName = txtComp.Text.Trim();
    if (compName == "") return;

    // Kiểm tra tên component đã tồn tại chưa
    foreach (var item in chkListBoxComp.Items)
        if (item.ToString().Trim().ToUpper() == compName.ToUpper())
        { MessageBox.Show("Grading component already exists!"); return; }

    string currentClass = lblSubjectClass.Text;
    foreach (var scg in tg.SubjectClassGrades)
    {
        if (scg.Subject + "/" + scg.Class == currentClass)
        {
            // Thêm cột vào DataGridView
            dgvGrading.Columns.Add(compName, compName);

            // Thêm vào model
            scg.Components.Add(compName);

            // Thêm GradeComponent rỗng cho TỪNG sinh viên
            foreach (Student s in scg.Students)
            {
                if (s.Grades.Count == 0) s.Grades = new List<GradeComponent>();
                s.Grades.Add(new GradeComponent { Component = compName, Grade = null });
            }

            // Thêm vào checkbox list (tự động hiện cột)
            chkListBoxComp.Items.Add(compName, true);
            NeedSave = true;
            break;
        }
    }
}
```

---

### 4.8. `btnImportComments_Click` — Import Comments (dòng 839–847)

```csharp
private void btnImportComments_Click(object sender, EventArgs e)
{
    new FrmImport(this) {
        SubjectClass     = lblSubjectClass.Text,
        MarkComponent    = "Add Comments",
        IsImportComments = true   // ← flag để FrmImport biết là import comment, không phải điểm
    }.ShowDialog(this);
}
```

---

### 4.9. `chbMergeClass_CheckedChanged` — Gộp/tách lớp (dòng 880–927)

```
Checked = true  → combobox thành ["[All classes]"]
Checked = false → combobox thành list lớp thực

Điều kiện bật nút: chỉ hiện khi TẤT CẢ lớp có cùng danh sách components
(kiểm tra ở cuối btnOpenGradingFile_Click)
```

---

### 4.10. `btnComment_Click` — Comment For Thesis (dòng 930–960)

```csharp
private void btnComment_Click(object sender, EventArgs e)
{
    // Chỉ bật khi số sinh viên <= MaxThesisGroupSize (=6)
    FileInfo fi = new FileInfo(txtGradingFile.Text);
    string[] parts = fi.Name.Split('.');           // e.g. "khangpq3Summer2026"
    string[] classParts = cboSubClass.Text.Split('/'); // e.g. ["ITE302c", "SE1906-NET"]

    // Tạo danh sách ThesisStudent từ scg.Students
    List<ThesisStudent> ts = scg.Students
        .Select(s => new ThesisStudent { Roll = s.Roll, Name = s.Name })
        .ToList();

    new FrmThesisComment {
        listTS              = ts,
        RecommendedCmtFileName = parts[0] + "_" + classParts[1],
        SubjectCode         = classParts[0],
        CmtFileName         = null,       // tạo mới
        Login               = tg.Login,
        ClassName           = scg.Class,
        Semester            = tg.Semester
    }.ShowDialog();
}
```

---

### 4.11. `btnEditCmtFile_Click` — Edit Comment For Thesis (.cmt) (dòng 963–978)

```csharp
private void btnEditCmtFile_Click(object sender, EventArgs e)
{
    openFileDialog.Filter = "Comment for thesis files | *.cmt";
    if (DialogResult.OK == openFileDialog.ShowDialog())
    {
        string fileName = openFileDialog.FileName;
        // Mở FrmThesisComment với CmtFileName có sẵn → mode edit
        new FrmThesisComment { CmtFileName = fileName }.ShowDialog();
    }
    // Khôi phục filter .fg
}
```

---

### 4.12. `btnDefenseGrading_Click` — Thesis/CP Defense (dòng 981–990)

```csharp
private void btnDefenseGrading_Click(object sender, EventArgs e)
{
    // Tạo hoặc tái sử dụng FrmDefenseGrading (singleton pattern)
    if (ffg == null || ffg.IsDisposed)
        ffg = new FrmDefenseGrading();
    ffg.Show();
    this.Hide(); // Ẩn form chính, không đóng
}
```

---

### 4.13. `btnExit_Click` — Thoát (dòng 32–46)

```csharp
private void btnExit_Click(object sender, EventArgs e)
{
    if (NeedSave)
    {
        if (MessageBox.Show("Do you want to save?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            btnSave.PerformClick();
        NeedSave = false;
    }
    Application.Exit(); // Đóng toàn ứng dụng
}
```

---

### 4.14. Checkbox `chkBoxAll` — Select All Components (dòng 325–331)

```csharp
private void chkBoxAll_CheckedChanged(object sender, EventArgs e)
{
    for (int i = 0; i < chkListBoxComp.Items.Count; i++)
        chkListBoxComp.SetItemChecked(i, chkBoxAll.Checked);
}
```

### 4.15. `chkListBoxComp_ItemCheck` — Ẩn/hiện cột điểm (dòng 334–338)

```csharp
private void chkListBoxComp_ItemCheck(object sender, ItemCheckEventArgs e)
{
    // Cột điểm bắt đầu từ index 3 (sau Roll, Name, Comment)
    dgvGrading.Columns[e.Index + 3].Visible = (e.NewValue == CheckState.Checked);
}
```

---

## 5. LUỒNG IMPORT MARK/COMMENT — `FrmImport.cs`

### 5.1. Import Mark (điểm) — `ImportGrade()` (dòng 151–277)

```
Input (paste vào TextBox):
  HE180186  8.5
  HE180202  7.0
  HE180290  9.0
  ...

Định dạng: mỗi dòng = "MSSV<TAB hoặc SPACE>điểm"
Validate:
  - Đúng 2 token
  - Nếu component thường: điểm là float, 0 <= x <= 10
  - Nếu component "Status": chỉ "0" hoặc "1"
  - Không trùng MSSV trong input

Nếu PASS → gọi FrmFuGrade.ImportMark(Dictionary<MSSV,điểm>, subClass, component)
          → cập nhật DataGridView cell tương ứng
          → đặt NeedSave = true
```

### 5.2. Import Comments — `ImportComments()` (dòng 67–148)

```
Input (paste vào TextBox):
  HE180186  Great work on the project
  HE180202  Needs improvement
  ...

Định dạng: token đầu là MSSV, phần còn lại ghép thành comment
Validate:
  - Không trùng MSSV
  - Tách bằng TAB hoặc SPACE

Nếu PASS → gọi FrmFuGrade.ImportComments(Dictionary<MSSV,comment>, subClass)
          → cập nhật cột Comment (index 2) trong DataGridView
```

### 5.3. Hàm xử lý trong `FrmFuGrade` (dòng 643–753)

```csharp
// ImportMark: tìm row theo MSSV → ghi giá trị vào cột component
public string ImportMark(Dictionary<string,string> markDic, string subClass, string markComp)
{
    int colIndex = /* tìm cột theo tên component, từ index 3 */;
    int imported = 0;
    foreach (string roll in markDic.Keys)
    {
        int rowIndex = /* tìm row theo MSSV (case-insensitive) */;
        if (found) {
            dgvGrading.Rows[rowIndex].Cells[colIndex].Value = markDic[roll];
            imported++;
        } else {
            log += "\r\nCannot find student with roll = \"" + roll + "\"";
        }
    }
    return log + $"\r\n{imported} (of {total}) marks imported";
}
```

---

## 6. BẢO MẬT — ĐIỂM YẾU CẦN SỬA KHI LÊN WEB

| Vấn đề | File | Dòng | Mức độ |
|--------|------|------|--------|
| AES key hard-code trong source | `AesOperation.cs` | 81 | 🔴 P0 |
| AES IV toàn 0 (không random) | `AesOperation.cs` | 22 | 🔴 P0 |
| Password lưu MD5 không salt | `Helper.cs` | 11-18, `FrmFuGrade.cs` 134,357 | 🔴 P0 |
| BinaryFormatter (fallback đọc .fg) | `FrmFuGrade.cs` | 83-88 | 🔴 P0 |
| Ghi đè file không backup | `FrmFuGrade.cs` | 393 | 🟡 P1 |
| Bug thiếu cột Comment khi Add Student | `FrmFuGrade.cs` | 574-585 | 🟡 P1 |

---

## 7. ĐỀ XUẤT CHUYỂN LÊN WEB — NODE.JS + REACT

### 7.1. Kiến trúc đề xuất (Node.js)

```
┌─────────────────────────────────────────────┐
│             FRONTEND (React + Vite)          │
│  - Bảng điểm (React Table / AG-Grid)        │
│  - Import modal (upload/paste)              │
│  - Auth (JWT, store in httpOnly cookie)     │
└──────────────┬──────────────────────────────┘
               │ REST API (JSON)
┌──────────────▼──────────────────────────────┐
│          BACKEND (Node.js + Express)         │
│  routes/                                    │
│    auth.js       → login, refresh, logout   │
│    gradesheets.js → CRUD bảng điểm          │
│    grades.js     → PATCH điểm, bulk import  │
│    students.js   → CRUD sinh viên           │
│    components.js → CRUD grade components    │
│    imports.js    → upload/migrate .fg file  │
│    thesis.js     → nhận xét đồ án          │
│    defense.js    → phiếu chấm bảo vệ       │
│    reports.js    → export Excel/CSV        │
└──────────────┬──────────────────────────────┘
               │
┌──────────────▼──────────────────────────────┐
│  DATABASE (PostgreSQL)  +  File Storage (S3) │
└─────────────────────────────────────────────┘
```

### 7.2. Ánh xạ chức năng Desktop → Web API

| Chức năng Desktop | Web Endpoint | Ghi chú |
|------------------|--------------|---------|
| `btnOpenGradingFile` | `POST /api/legacy/import-fg` | Upload file, giải mã AES, import vào DB. Chỉ dùng lúc migrate |
| `btnShow` | `GET /api/classes/:id/gradesheet` | Trả JSON, client tự render grid |
| `btnSave` | `PATCH /api/gradesheets/:id` | Transaction, audit log, version |
| `btnSearch` | `GET /api/gradesheets/:id/students?q=HE180186` | Hỗ trợ MSSV + tên |
| `btnAdd` | `POST /api/classes/:id/students` | Validate unique MSSV trong class |
| `importMarkToolStripMenuItem` | `POST /api/grades/bulk-import` | Accept JSON/CSV, preview errors trước |
| `clearMarkToolStripMenuItem` | `DELETE /api/components/:id/grades` | Cần quyền, xác nhận, audit |
| `btnAddComp` | `POST /api/classes/:id/components` | Kiểm tra unique trong class |
| `btnImportComments` | `POST /api/grades/import-comments` | Cùng endpoint, field `mode=comment` |
| `chbMergeClass` | `GET /api/gradesheets?classIds=1,2,3&merge=true` | Server gộp, client không cần logic |
| `btnComment` | `POST /api/thesis/reviews` | Tạo thesis review entity |
| `btnEditCmtFile` | `GET/PATCH /api/thesis/reviews/:id` | Có RBAC, draft/submit/lock |
| `btnDefenseGrading` | `Navigate /defense` | SPA route |

### 7.3. Schema database tối thiểu

```sql
-- Auth
users(id, login, name, password_hash, role, created_at)

-- Học vụ
semesters(id, name, start_date, end_date)
subjects(id, code, name)
classes(id, subject_id, semester_id, name)
students(id, roll, name, created_at)
enrollments(id, class_id, student_id)

-- Bảng điểm (tương đương TeacherGrade + SubjectClassGrade)
grade_sheets(id, class_id, owner_id, version, status, created_at, updated_at)
grade_components(id, sheet_id, name, order, component_type) -- type: SCORE|STATUS
grades(id, enrollment_id, component_id, value DECIMAL(5,2), updated_by, updated_at)
student_comments(id, enrollment_id, comment TEXT, updated_by, updated_at)

-- Legacy migration
legacy_imports(id, original_filename, file_hash, uploaded_by, import_status, imported_at)

-- Audit
audit_logs(id, user_id, action, entity_type, entity_id, old_value, new_value, timestamp)
```

### 7.4. Trình tự triển khai (Node.js)

```
Giai đoạn 0 — Chuẩn bị (1–2 tuần)
  [x] Khởi tạo project: npx create-vite@latest fugrade-web
  [x] Setup Express server với TypeScript
  [x] Setup PostgreSQL + Prisma ORM
  [x] Viết script offline giải mã .fg (AES + JSON) → validate → insert DB
  [x] Viết script convert .cmt/.tef (BinaryFormatter) → JSON (chạy trên .NET CLI riêng)

Giai đoạn 1 — MVP Bảng điểm (3–4 tuần)
  [ ] Auth (JWT + bcrypt, thay MD5)
  [ ] CRUD semesters/subjects/classes/students/enrollments
  [ ] GET gradesheet + render bảng điểm
  [ ] PATCH grade (individual cell edit)
  [ ] POST bulk-import (paste text hoặc upload CSV)
  [ ] POST import-comments
  [ ] Merge view (query-side)
  [ ] Audit log mọi thay đổi điểm

Giai đoạn 2 — Thesis & Defense (3–4 tuần)
  [ ] Thesis review (draft/submit/lock)
  [ ] Rubric CRUD với versioning
  [ ] Defense session, evaluator assignment
  [ ] Evaluation form, copy group mark
  [ ] Legacy .cmt/.tef import

Giai đoạn 3 — Report & Vận hành (2 tuần)
  [ ] Export Excel (dùng ExcelJS)
  [ ] Dashboard tiến độ
  [ ] Backup/restore
  [ ] Rate limiting, antivirus scan upload
```

### 7.5. Stack công nghệ gợi ý

```json
{
  "backend": {
    "runtime": "Node.js 22 LTS",
    "framework": "Express 5 + TypeScript",
    "orm": "Prisma",
    "auth": "JWT (jsonwebtoken) + bcrypt",
    "validation": "Zod",
    "excel": "ExcelJS",
    "testing": "Vitest + Supertest"
  },
  "frontend": {
    "framework": "React 19 + Vite",
    "grid": "AG-Grid Community (tương đương DataGridView)",
    "ui": "shadcn/ui hoặc Mantine",
    "state": "TanStack Query (server state) + Zustand (UI state)",
    "routing": "React Router v7"
  },
  "database": "PostgreSQL 16",
  "file_storage": "MinIO (self-host) hoặc AWS S3",
  "deployment": "Docker Compose (dev) → Railway / Render / VPS (prod)"
}
```

---

## 8. CHECKLIST BẮT ĐẦU NHANH

- [ ] Chạy `npx create-vite@latest fugrade-web --template react-ts`
- [ ] Tạo Express app với route `/api/health`
- [ ] Viết script Node.js đọc file `.fg`: Base64 decode → AES decrypt (dùng `node:crypto`) → parse JSON
- [ ] Xác nhận script ra đúng data với một file `.fg` mẫu thật
- [ ] Thiết kế Prisma schema từ object model ở mục 2.1
- [ ] Migrate script insert vào DB
- [ ] Dựng bảng điểm đơn giản (GET → render table)
- [ ] Thêm PATCH cho cell edit + audit log
- [ ] Thêm import mark (POST với payload JSON)

---

*Tài liệu này được tạo từ phân tích mã nguồn FuGrade WinForms C#. Các dòng code tham chiếu dựa trên phiên bản source hiện tại trong `FrmFuGrade.cs`, `FrmImport.cs`, `AesOperation.cs`, `Helper.cs`.*
