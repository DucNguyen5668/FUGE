@AGENTS.md

# FuGrade Web — project memory

- Next.js 16 App Router, React 19, TypeScript và Node.js 22.
- Giao diện khách mở/sửa/import/xuất `.fg`; bản nháp nằm trong `sessionStorage`.
- Đăng nhập chỉ bắt buộc khi lưu snapshot. Auth.js hỗ trợ credentials + CAPTCHA và Google OAuth.
- Persistence hiện tại: Drizzle ORM + `better-sqlite3`, database local `fugrade.db` bị Git ignore.
- `.fg` dùng AES-256-CBC/MD5 legacy để tương thích FuGrade desktop; không coi là mã hóa hiện đại.
- Vercel phù hợp preview giao diện nhưng SQLite không phải persistence production; Neon/Postgres vẫn là quyết định kiến trúc chưa được Human review.
- Architecture Profile canonical: `.sdd/architecture-profile.md`.
- Adoption report: `.sdd/reviews/adopt-fugrade.md`.
