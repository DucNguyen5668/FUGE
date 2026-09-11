# FuGrade Web

Ứng dụng Next.js quản lý và chỉnh sửa bảng điểm FuGrade, sử dụng SQLite cục bộ.

## Yêu cầu

- Node.js 22.22.3 trở lên. Phiên bản 22.11.0 trên Windows đã được xác nhận gây crash `better-sqlite3` khi mở database.
- npm đi kèm Node.js.

Nếu dùng NVM for Windows:

```powershell
nvm install 22.22.3
nvm use 22.22.3
node --version
```

## Cài đặt

```powershell
npm install
```

Tạo `.env.local` với các biến sau:

```dotenv
AUTH_SECRET=thay-bang-chuoi-bi-mat-ngau-nhien
NEXTAUTH_URL=http://localhost:3000
# Tùy chọn: bật nút đăng nhập Google
AUTH_GOOGLE_ID=google-oauth-client-id
AUTH_GOOGLE_SECRET=google-oauth-client-secret
```

Để bật Google OAuth, tạo OAuth Client loại **Web application** trong Google Cloud và thêm callback URL `http://localhost:3000/api/auth/callback/google`. Nếu chưa cấu hình hai biến Google, nút Google sẽ hiển thị ở trạng thái chưa khả dụng.

## Chạy development

```powershell
npm run dev
```

Mở <http://localhost:3000>. Ở lần chạy đầu, truy cập <http://localhost:3000/api/seed> để tạo tài khoản local mặc định:

```text
Username: admin
Password: admin123
```

Endpoint seed chỉ hoạt động trong development. Hãy đổi thông tin đăng nhập trước khi dùng ứng dụng với dữ liệu thật.

Trang `/signup` cho phép tạo tài khoản bằng email. Đăng nhập bằng mật khẩu yêu cầu CAPTCHA 5 ký tự được xác minh ở server; CAPTCHA hết hạn sau 5 phút và chỉ dùng được một lần.

## Kiểm tra chất lượng và build

```powershell
npm run lint
npm run build
npm run start
```

Database được lưu tại `fugrade.db` trong thư mục project. Các file SQLite cục bộ đã được loại khỏi Git.

## Chu trình phát triển

Mọi thay đổi không tầm thường phải được phân loại và thực hiện theo [Chu trình phát triển chuẩn của FuGrade Web](./docs/fugrade-development-lifecycle.md). Tài liệu này quy định artifact Context/Spec/Plan/Tasks, checkpoint review, kiểm chứng riêng cho `.fg`, authentication, Excel import, persistence và Definition of Done.

Thư mục `SDDADD-main/` được giữ làm nguồn template tham khảo; tài liệu trong `docs/`, `.sdd/`, `AGENTS.md` và `CONSTITUTION.md` của FuGrade mới là chuẩn vận hành của repository.

## Cách dùng workspace

- Trang Home, mở file `.fg`, sửa điểm/nhận xét, thêm sinh viên/thành phần, import dữ liệu và xuất file đều dùng được khi chưa đăng nhập.
- Bản đang sửa được giữ trong `sessionStorage` của tab hiện tại. Đăng nhập chỉ được yêu cầu khi bấm **Lưu** để tạo một snapshot mới trong SQLite.
- **Xuất .fg** luôn yêu cầu nhập và xác nhận mật khẩu. File tải xuống có thể mở lại bằng FuGrade desktop hoặc workspace web bằng đúng mật khẩu đó.

## Tương thích file FuGrade desktop

Định dạng `.fg` tương thích ứng dụng cũ dùng AES-256-CBC với khóa/IV legacy và lưu MD5 của mật khẩu trong payload. Cơ chế này chỉ nhằm trao đổi file với FuGrade desktop; không nên xem đây là mã hóa hiện đại cho dữ liệu nhạy cảm hoặc dùng thay cho cơ chế đăng nhập của hệ thống web.
