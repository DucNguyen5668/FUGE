# FuGrade Architecture Profile
# Version: 0.1.0
# Status: APPROVED
# Last verified: 2026-08-24

## 1. Repository evidence

| Concern | Observed binding | Status | Evidence |
| :--- | :--- | :--- | :--- |
| Language | TypeScript | EVIDENCED | `tsconfig.json`, `app/**/*.tsx`, `lib/**/*.ts` |
| Runtime | Node.js `>=22.22.3` | EVIDENCED | `package.json#engines`, `.nvmrc`; local `node v22.22.3` |
| Web framework | Next.js `16.3.2`, App Router | EVIDENCED | `package.json`, `app/`, `next.config.ts` |
| UI | React `19.2.8`, CSS, Sonner | EVIDENCED | `package.json`, `app/globals.css`, `app/layout.tsx` |
| HTTP transport | Next.js Route Handlers | EVIDENCED | `app/api/**/route.ts` |
| Authentication | Auth.js v5 beta, JWT, credentials + Google | EVIDENCED | `lib/auth.ts`, `lib/auth.config.ts`, `proxy.ts` |
| Validation | Zod `4.4.3` plus boundary checks | EVIDENCED | `package.json`, `app/api/auth/register/route.ts` |
| Local database | SQLite via `better-sqlite3` | EVIDENCED | `lib/db/index.ts`, `drizzle.config.ts` |
| ORM/query layer | Drizzle ORM `0.45.2` | EVIDENCED | `package.json`, `lib/db/schema.ts` |
| Spreadsheet | ExcelJS `4.4.0` | EVIDENCED | `app/api/export/route.ts` |
| Legacy file format | `.fg` AES-256-CBC + MD5 compatibility | EVIDENCED | `lib/fg-decrypt.ts`, `FuGrade/AesOperation.cs` |
| Automated tests | Chưa có test runner/script | BLOCKED | `package.json` không có script `test`; không thấy test config |
| Deployment | Vercel preview được đề xuất; production DB chưa chọn | PENDING | SQLite không phù hợp serverless persistence; chưa có `DATABASE_URL` adapter |

## 2. Brownfield architecture

Source hiện tại dùng cấu trúc Next.js theo feature/framework:

```text
app/           pages, layouts và Route Handlers
components/    workspace, grade table và dialog UI
lib/           auth, DB, schema, `.fg` compatibility và shared types
FuGrade/       source desktop legacy dùng làm reference tương thích
```

Template Clean Architecture `src/domain|usecase|interface|infra` không phản ánh source hiện tại. Không di chuyển source hoặc áp dependency boundary mới nếu chưa có RFC, migration plan và Human Final Review.

## 3. Exact verification commands

| Check | Windows local | CI/Linux | Evidence |
| :--- | :--- | :--- | :--- |
| Lint | `npm.cmd run lint` | `npm run lint` | Script `eslint`; đã pass 2026-08-24 |
| Type/build | `npm.cmd run build` | `npm run build` | Next build + TypeScript; 17 routes đã pass 2026-08-24 |
| Development | `npm.cmd run dev` | `npm run dev` | `package.json#scripts.dev` |
| Production start | `npm.cmd start` | `npm start` | Chỉ sau build |
| Automated test | N/A | N/A | Chưa có test script; cần Human chọn framework |

## 4. Delivery and data gates

- `.env`, `.env.local`, `fugrade.db*`, `.next/` và `node_modules/` không được commit.
- Thay đổi `.fg` crypto/schema cần fixture desktop-web round trip.
- Thay đổi persistence hoặc deployment database cần Spec/RFC, migration và rollback plan.
- API đọc/sửa/xóa snapshot phải lọc theo authenticated `userId`; các endpoint hiện chỉ kiểm tra session là adoption gap.
- CAPTCHA nonce hiện ở process memory; multi-instance deployment cần shared store nếu yêu cầu one-time toàn cục.

## 5. Unresolved decisions

1. Production persistence: Neon Postgres, Supabase Postgres hay server có persistent volume cho SQLite.
2. Automated test stack: Vitest + Testing Library và/hoặc Playwright.
3. Brownfield modularization: giữ layout hiện tại hay migrate dần sang domain/usecase ports.
4. Ownership, retention, revision và audit-log policy cho snapshot bảng điểm.

## AI Agent Recommendation
- Status: PENDING HUMAN REVIEW
- Scope: FuGrade brownfield Architecture Profile v0.1.0
- Recommendation: Chấp nhận stack đã quan sát làm baseline; giữ source layout Next.js hiện tại; ưu tiên bổ sung ownership authorization và chọn Postgres trước production deployment.
- Evidence: `package.json`, `app/`, `components/`, `lib/`, `drizzle.config.ts`, `next.config.ts`, lint/build pass ngày 2026-08-24.
- Risks and assumptions: Template Clean Architecture khác source thực tế; SQLite và in-memory CAPTCHA không phù hợp multi-instance; chưa có automated tests.
- Alternatives considered: Giữ SQLite trên server persistent volume; hoặc tái cấu trúc toàn bộ trước deployment. Cả hai cần quyết định con người và migration scope riêng.
- Required human decision: Approve/revise baseline, chọn production database và test stack.

## Human Final Review
- Status: APPROVED
- Decision: Chấp thuận Next.js 16, React 19, TypeScript, Auth.js, Drizzle và SQLite local làm baseline cho feature `bonus-grade-conversion`. Giữ cấu trúc `app/`, `components/`, `lib/`. Feature không được thêm package, đổi database schema hoặc đổi schema `.fg`. Production database và automated test framework tiếp tục là blocker riêng.
- Reviewer: NguyenND
- Reviewed at: 2026-09-11T22:43:00+07:00
- Follow-up: Tạo `PLAN.md` cho `bonus-grade-conversion` sau khi `SPEC.md` được lock.
