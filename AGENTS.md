<!-- BEGIN:nextjs-agent-rules -->

# This is NOT the Next.js you know

This version has breaking changes — APIs, conventions, and file structure may all differ from your training data. Read the relevant guide in `node_modules/next/dist/docs/` (resolved from this file's directory; in monorepos the `next` package may not be visible from the repo root) before writing any code. Heed deprecation notices.

This block is written and re-added by `next dev` — verify at `node_modules/next/dist/server/lib/generate-agent-files.js`. Removing it from a diff only re-creates the uncommitted change; committing it with your work keeps the tree clean.

<!-- END:nextjs-agent-rules -->

# FuGrade — SDD + ADD project rules

## Canonical context

- Quy trình phát triển chuẩn của repository nằm tại `docs/fugrade-development-lifecycle.md`; dùng tài liệu này để phân loại thay đổi, chọn artifact, gate và Definition of Done.
- Trước thay đổi feature không tầm thường, đọc `CONSTITUTION.md`, `.sdd/architecture-profile.md` và `.sdd/constraints/{global,business,safety}.md`.
- Dùng `.sdd/features/<slug>/{CONTEXT,SPEC,PLAN,TASKS}.md` cho feature mới hoặc thay đổi business behavior lớn.
- AI recommendation luôn bắt đầu `PENDING HUMAN REVIEW`; Agent không tự ghi `APPROVED` thay con người.
- `SDDADD-main/` là nguồn template tham khảo, không phải operational authority của FuGrade. Skill Claude nằm tại `.claude/skills/`; Codex chỉ tự phát hiện skill sau khi có bản tương ứng trong `.agents/skills/`.

## Brownfield boundaries

- Source hiện hành: `app/`, `components/`, `lib/`, `proxy.ts`, `next.config.ts`.
- Legacy compatibility reference: `FuGrade/`; không thay đổi schema/crypto tương thích `.fg` nếu chưa có fixture và Spec được review.
- Không ép chuyển source sang layout `src/domain|usecase|interface|infra`; đây chỉ là kiến trúc mục tiêu cần RFC/Plan riêng.
- Được sửa manifest/config khi task yêu cầu và đã kiểm tra tài liệu Next.js cục bộ; không sửa `node_modules/`, `.next/`, `.git/`.

## Safety and verification

- Không đọc, ghi, log hoặc commit `.env`, credential, private key hay database local.
- Mutation dữ liệu phải kiểm tra identity và ownership; không chỉ kiểm tra “đã đăng nhập”.
- Exact command đã có evidence: `npm.cmd run lint` và `npm.cmd run build` trên Windows, tương ứng `npm run lint` và `npm run build` trong CI/Linux.
- Project hiện chưa có automated test command; không tuyên bố test coverage cho tới khi profile chọn test framework.
- Git commit/push/deploy chỉ thực hiện khi người dùng yêu cầu rõ ràng và sau khi kiểm tra secret, diff và trạng thái build phù hợp.
