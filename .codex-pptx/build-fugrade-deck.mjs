import fs from "node:fs/promises";
import path from "node:path";
import crypto from "node:crypto";
import { pathToFileURL } from "node:url";

const workspaceDir = "C:/Users/Admin/Downloads/FUge/web";
const sourcePath = "C:/Users/Admin/Downloads/Spec-Driven-Development-Tu-Ly-Thuyet-DJen-Thuc-Te.pptx";
const skillDir = "C:/Users/Admin/.codex/plugins/cache/openai-primary-runtime/presentations/26.909.12148/skills/presentations";
const stagingDir = path.join(workspaceDir, ".codex-finalizer");
const outputDir = path.join(workspaceDir, "output");
const finalPath = path.join(outputDir, "FuGrade-SDD-Product-Demo-v3.pptx");

const { importRuntimeModule } = await import(
  pathToFileURL(path.join(skillDir, "container_tools/runtime_helpers.mjs")).href,
);
const { FileBlob, PresentationFile } = await importRuntimeModule("@oai/artifact-tool");
const {
  finalizePresentation,
  makeNativeBulletParagraphs,
} = await import(
  pathToFileURL(path.join(skillDir, "container_tools/artifact_tool_utils.mjs")).href,
);

const presentation = await PresentationFile.importPptx(await FileBlob.load(sourcePath));
const slides = presentation.slides.items;
const RALeway = "Raleway";
const ROBOTO = "Roboto";
const INK = "#17233B";
const MUTED = "#5F6B7A";
const TEAL = "#0C8D96";
const TEAL_DARK = "#08656C";
const PALE = "#F4F7FB";
const LINE = "#DDE4EC";
const WHITE = "#FFFFFF";
const RED = "#C84F58";
const AMBER = "#B67714";

function setExisting(slideNumber, shapeIndex, value) {
  slides[slideNumber - 1].shapes.items[shapeIndex].text = value;
}

function clearSlide(slide) {
  slide.shapes.deleteAll();
  for (const image of [...slide.images.items]) slide.images.deleteById(image.id);
  slide.background.fill = WHITE;
}

function addRect(slide, { left, top, width, height, fill = "none", line = "none", radius = 0 }) {
  return slide.shapes.add({
    geometry: radius ? "roundRect" : "rect",
    position: { left, top, width, height },
    fill,
    line: line === "none" ? { fill: "none", width: 0 } : { fill: line, width: 1 },
    ...(radius ? { borderRadius: radius } : {}),
  });
}

function addText(slide, text, {
  left,
  top,
  width,
  height,
  fontSize = 23,
  color = INK,
  bold = false,
  font = ROBOTO,
  fill = "none",
  line = "none",
  margin = 0,
  align = "left",
  verticalAlign = "top",
  name,
} = {}) {
  const shape = slide.shapes.add({
    geometry: "textbox",
    name,
    position: { left, top, width, height },
    fill,
    line: line === "none" ? { fill: "none", width: 0 } : { fill: line, width: 1 },
  });
  shape.text = text;
  shape.text.style = {
    typeface: font,
    fontSize,
    color,
    bold,
    autoFit: "none",
    marginLeft: margin,
    marginRight: margin,
    marginTop: margin,
    marginBottom: margin,
    horizontalAlignment: align,
    verticalAlignment: verticalAlign,
  };
  return shape;
}

function addTitle(slide, title, subtitle) {
  addText(slide, title, {
    left: 70, top: 48, width: 1140, height: 58,
    fontSize: 38, bold: true, font: RALeway,
  });
  if (subtitle) {
    addText(slide, subtitle, {
      left: 70, top: 108, width: 1110, height: 38,
      fontSize: 18, color: MUTED,
    });
  }
  addRect(slide, { left: 70, top: 147, width: 72, height: 4, fill: TEAL });
}

function addEyebrow(slide, text, left, top, width = 320, color = TEAL) {
  return addText(slide, text.toUpperCase(), {
    left, top, width, height: 24,
    fontSize: 13, color, bold: true, font: ROBOTO,
  });
}

function addBulletList(slide, items, { left, top, width, height, fontSize = 21, color = INK } = {}) {
  const shape = addText(slide, "", { left, top, width, height, fontSize, color });
  shape.text = makeNativeBulletParagraphs(items, {
    marginLeftPoints: 18,
    hangingPoints: 8,
    spaceAfterPoints: 9,
  });
  shape.text.style = { typeface: ROBOTO, fontSize, color, autoFit: "none" };
  return shape;
}

function addFooter(slide, number) {
  addText(slide, `FUGRADE  /  SDD  /  ${String(number).padStart(2, "0")}`, {
    left: 1020, top: 682, width: 190, height: 18,
    fontSize: 10, color: WHITE, bold: true, font: ROBOTO,
    fill: TEAL_DARK, margin: 5, align: "center", verticalAlign: "middle",
  });
}

async function addScreenshot(slide, filename, position, alt, fit = "contain") {
  const bytes = await fs.readFile(path.join(workspaceDir, "presentation-assets", filename));
  return slide.images.add({
    blob: bytes,
    contentType: "image/png",
    alt,
    fit,
    geometry: "roundRect",
    borderRadius: 10,
    position,
  });
}

function setNotes(slide, text) {
  slide.speakerNotes.textFrame.setText(text);
}

// Slides 1-7 retain the source deck's visual structure and illustrations.
setExisting(1, 1, "FuGrade Web: Spec-Driven Development trong sản phẩm thật");
setExisting(1, 2, "Từ ứng dụng desktop legacy đến workspace bảng điểm trên web, có bằng chứng và ranh giới triển khai rõ ràng.");
setNotes(slides[0], "Mở đầu bằng câu hỏi: làm thế nào để AI hỗ trợ chuyển một sản phẩm legacy lên web mà không làm sai nghiệp vụ? FuGrade là case study thực tế của bài trình bày này.");

setExisting(2, 1, "Vấn đề cốt lõi");
setExisting(2, 2, "Yêu cầu nghiệp vụ nằm rải rác");
setExisting(2, 3, "Quy tắc .fg, chỉnh điểm, import và lưu snapshot xuất hiện ở cả desktop legacy lẫn web.");
setExisting(2, 4, "AI thiếu ranh giới triển khai");
setExisting(2, 5, "Nếu không đọc source và artifact, AI dễ coi hành vi dự kiến là tính năng đã hoàn thành.");
setExisting(2, 6, "Demo dễ nói quá hiện trạng");
setExisting(2, 7, "Ảnh sản phẩm và evidence phải tách phần đã chạy khỏi phần đang chờ review.");
setNotes(slides[1], "Nhấn mạnh ba nguồn sự thật: source hiện tại, hành vi chạy được và artifact đã được con người duyệt. Slide không dùng tài liệu lý thuyết như bằng chứng sản phẩm.");

setExisting(3, 0, "Spec-Driven Development");
setExisting(3, 1, "Spec là hợp đồng giữa ý định của con người và phần triển khai có thể kiểm chứng.");
setExisting(3, 3, "Một spec tốt đủ rõ để AI không phải đoán và đủ ngắn để con người review.");
setExisting(3, 4, "Chu trình 5 pha:");
setExisting(3, 5, "Context");
setExisting(3, 6, "Spec");
setExisting(3, 7, "Plan");
setExisting(3, 8, "Tasks");
setExisting(3, 9, "Implement");
setNotes(slides[2], "Giải thích rằng mỗi pha trả lời một câu hỏi khác nhau. Context xác định vấn đề. Spec khóa hành vi. Plan và Tasks giới hạn cách sửa. Implement chỉ bắt đầu sau review phù hợp.");

setExisting(4, 0, "8 thành phần của một Spec có thể kiểm chứng");
setExisting(4, 3, "Bối cảnh và mục tiêu");
setExisting(4, 4, "Tính năng giải quyết vấn đề nào?");
setExisting(4, 7, "Tác nhân và quyền");
setExisting(4, 8, "Ai thao tác và được phép làm gì?");
setExisting(4, 11, "Yêu cầu chức năng");
setExisting(4, 12, "Hệ thống phải làm gì?");
setExisting(4, 15, "Yêu cầu phi chức năng");
setExisting(4, 16, "Mức chất lượng cần đạt là gì?");
setExisting(4, 19, "Dữ liệu");
setExisting(4, 20, "Cấu trúc và vòng đời dữ liệu ra sao?");
setExisting(4, 23, "Xử lý lỗi");
setExisting(4, 24, "Lỗi nào có thể xảy ra và phản hồi thế nào?");
setExisting(4, 27, "Tiêu chí chấp nhận");
setExisting(4, 28, "Bằng chứng nào chứng minh đã hoàn thành?");
setExisting(4, 31, "Ngoài phạm vi");
setExisting(4, 32, "Điều gì chưa làm trong phiên bản này?");
setNotes(slides[3], "Liên hệ với FuGrade: actors phải tách guest editor và authenticated editor. Compatibility của .fg và ownership của snapshot phải xuất hiện rõ trong Spec.");

setExisting(5, 0, "EARS cho ranh giới lưu snapshot");
setExisting(5, 1, "Requirement mô tả đúng thời điểm hệ thống yêu cầu đăng nhập.");
setExisting(5, 2, "Ví dụ từ FuGrade Web:");
setExisting(5, 3, "GIVEN người dùng đang chỉnh sửa file .fg ở chế độ khách\nWHEN người dùng bấm Lưu\nTHEN hệ thống giữ workspace trong sessionStorage và chuyển đến /login");
setExisting(5, 5, "Đăng nhập chỉ cần khi lưu snapshot.");
setNotes(slides[4], "Đối chiếu trực tiếp app/page.tsx: saveSnapshot giữ workspace trong sessionStorage khi API trả 401, sau đó chuyển người dùng đến trang đăng nhập.");

setExisting(6, 2, "PHẦN 2");
setExisting(6, 3, "FuGrade Web: hiện trạng sản phẩm");
setExisting(6, 4, "Kết luận dựa trên source, runtime và ảnh chụp giao diện chạy trên localhost.");
setNotes(slides[5], "Chuyển từ phần khái niệm sang case study. Từ slide sau, mọi claim đều gắn với source hoặc ảnh chụp sản phẩm.");

setExisting(7, 1, "EVIDENCE");
setExisting(7, 2, "Hiện trạng được xác minh");
setExisting(7, 3, "Node.js 22.22.3, Next.js 16.3.2.");
setExisting(7, 5, "Guest: mở, chỉnh, import và xuất .fg.");
setExisting(7, 6, "Lưu: giữ workspace rồi chuyển sang /login.");
setNotes(slides[6], "Phân biệt bằng chứng runtime với bằng chứng nghiệp vụ. Ảnh và source xác nhận luồng guest. Lint và build là kiểm tra kỹ thuật, không tự chứng minh toàn bộ nghiệp vụ đúng.");

// Slide 8: implementation boundary.
clearSlide(slides[7]);
addTitle(slides[7], "Phạm vi sản phẩm đã xác minh", "Deck chỉ demo hành vi có trong source và giao diện đang chạy");
addEyebrow(slides[7], "Đã triển khai", 90, 190, 420, TEAL);
addText(slides[7], "Workspace bảng điểm", { left: 90, top: 222, width: 470, height: 42, fontSize: 27, bold: true, font: RALeway });
addBulletList(slides[7], [
  "Mở file .fg có hoặc không có mật khẩu",
  "Chỉnh điểm, nhận xét, sinh viên và thành phần trong phiên",
  "Import điểm hoặc nhận xét theo MSSV với preview validation",
  "Xuất .fg có mật khẩu và lưu snapshot sau khi đăng nhập",
], { left: 90, top: 275, width: 470, height: 270, fontSize: 20 });
addRect(slides[7], { left: 620, top: 190, width: 2, height: 380, fill: LINE });
addEyebrow(slides[7], "Chưa triển khai", 680, 190, 420, AMBER);
addText(slides[7], "Các hạng mục đang chờ quyết định", { left: 680, top: 222, width: 500, height: 42, fontSize: 27, bold: true, font: RALeway });
addBulletList(slides[7], [
  "Quy đổi điểm bonus: Spec vẫn PENDING HUMAN REVIEW",
  "Database production và chiến lược multi-instance chưa được chọn",
  "Project chưa có automated test command",
  "Ownership cho API snapshot cần được đặc tả và sửa trước production",
], { left: 680, top: 275, width: 500, height: 270, fontSize: 20, color: INK });
addText(slides[7], "Khi thuyết trình, bonus là hướng phát triển đã đặc tả, không phải tính năng hiện có.", { left: 90, top: 590, width: 1090, height: 48, fontSize: 18, color: AMBER, bold: true });
addFooter(slides[7], 8);
setNotes(slides[7], "Đây là slide quan trọng nhất để giữ claim chính xác. Context bonus đã được duyệt, nhưng Spec và Architecture Profile vẫn pending nên không demo bonus như tính năng hoàn thành.");

// Slide 9: guest entry.
clearSlide(slides[8]);
addTitle(slides[8], "Điểm vào của guest workspace", "Người dùng có thể bắt đầu mà chưa cần tạo tài khoản");
addRect(slides[8], { left: 62, top: 174, width: 760, height: 448, fill: PALE, line: LINE, radius: 12 });
await addScreenshot(slides[8], "01-home-guest.png", { left: 74, top: 186, width: 736, height: 424 }, "Trang chủ FuGrade Web ở chế độ khách");
addEyebrow(slides[8], "Giá trị sản phẩm", 870, 205, 300);
addText(slides[8], "Bắt đầu nhanh", { left: 870, top: 240, width: 320, height: 44, fontSize: 28, bold: true, font: RALeway });
addText(slides[8], "Mở và chỉnh sửa bảng điểm ngay trong trình duyệt. Đăng nhập chỉ xuất hiện khi người dùng muốn lưu snapshot vào SQLite.", { left: 870, top: 300, width: 320, height: 148, fontSize: 21, color: MUTED });
addText(slides[8], "Ảnh chụp từ localhost, dữ liệu demo tổng hợp", { left: 870, top: 542, width: 320, height: 30, fontSize: 14, color: MUTED });
addFooter(slides[8], 9);
setNotes(slides[8], "Mở demo bằng trang này. Nêu rõ guest mode là quyết định sản phẩm hiện tại, không phải lỗi thiếu authentication. Nút Lưu là ranh giới chuyển sang authenticated flow.");

// Slide 10: password handling.
clearSlide(slides[9]);
addTitle(slides[9], "Mở file .fg có mật khẩu", "Ứng dụng chỉ yêu cầu mật khẩu sau khi phát hiện payload được bảo vệ");
addRect(slides[9], { left: 62, top: 174, width: 790, height: 450, fill: PALE, line: LINE, radius: 12 });
await addScreenshot(slides[9], "03-password-required.png", { left: 74, top: 186, width: 766, height: 426 }, "Hộp thoại yêu cầu mật khẩu khi mở file .fg");
addEyebrow(slides[9], "Hành vi đã chạy", 890, 210, 300);
addBulletList(slides[9], [
  "Giới hạn kích thước file ở 10 MB",
  "Phân biệt thiếu mật khẩu và mật khẩu không đúng",
  "Trả dữ liệu đã parse về guest workspace sau khi xác minh",
], { left: 890, top: 252, width: 300, height: 240, fontSize: 20 });
addText(slides[9], "Compatibility giữ AES-256-CBC và MD5 theo định dạng legacy.", { left: 890, top: 525, width: 300, height: 74, fontSize: 18, color: MUTED });
addFooter(slides[9], 10);
setNotes(slides[9], "Nhấn mạnh mục tiêu compatibility với FuGrade desktop. Cơ chế legacy chỉ phục vụ trao đổi file, không thay thế authentication hiện đại.");

// Slide 11: main workspace.
clearSlide(slides[10]);
addTitle(slides[10], "Workspace chỉnh sửa bảng điểm", "Một màn hình tập trung cho metadata, cột điểm, tìm kiếm và thao tác nhanh");
addRect(slides[10], { left: 58, top: 166, width: 880, height: 476, fill: PALE, line: LINE, radius: 12 });
await addScreenshot(slides[10], "04-workspace-grade-table.png", { left: 70, top: 178, width: 856, height: 452 }, "Workspace bảng điểm FuGrade Web với dữ liệu demo");
addEyebrow(slides[10], "Workspace trong phiên", 975, 205, 240);
addText(slides[10], "Chỉnh trực tiếp", { left: 975, top: 248, width: 240, height: 34, fontSize: 23, bold: true, font: RALeway });
addText(slides[10], "Nhấp đúp ô điểm hoặc nhận xét để sửa. Enter lưu giá trị, Esc hủy thao tác.", { left: 975, top: 292, width: 240, height: 112, fontSize: 19, color: MUTED });
addText(slides[10], "Giữ trạng thái cục bộ", { left: 975, top: 445, width: 240, height: 34, fontSize: 23, bold: true, font: RALeway });
addText(slides[10], "Workspace nằm trong sessionStorage của tab cho tới khi người dùng chọn Lưu.", { left: 975, top: 489, width: 240, height: 104, fontSize: 19, color: MUTED });
addFooter(slides[10], 11);
setNotes(slides[10], "Demo nhanh ba thao tác: lọc sinh viên, chọn cột hiển thị và nhấp đúp một ô điểm. Dữ liệu trên slide là dữ liệu tổng hợp, không lấy từ database người dùng.");

// Slide 12: import validation.
clearSlide(slides[11]);
addTitle(slides[11], "Import hàng loạt có preview validation", "Dòng hợp lệ và dòng lỗi được tách trước khi áp dụng vào workspace");
addRect(slides[11], { left: 62, top: 174, width: 800, height: 450, fill: PALE, line: LINE, radius: 12 });
await addScreenshot(slides[11], "05-import-validation.png", { left: 74, top: 186, width: 776, height: 426 }, "Preview import điểm với hai dòng hợp lệ và hai dòng lỗi");
addEyebrow(slides[11], "Điểm nổi bật", 900, 205, 270);
addBulletList(slides[11], [
  "Dán dữ liệu trực tiếp từ Excel",
  "Khớp sinh viên theo MSSV",
  "Từ chối điểm ngoài khoảng 0 đến 10",
  "Chỉ áp dụng các dòng hợp lệ",
], { left: 900, top: 248, width: 280, height: 260, fontSize: 20 });
addText(slides[11], "Preview giúp người thuyết trình cho thấy validation trước mutation.", { left: 900, top: 540, width: 280, height: 68, fontSize: 18, color: MUTED });
addFooter(slides[11], 12);
setNotes(slides[11], "Trong demo, dán bốn dòng như ảnh. Hai dòng hợp lệ, một MSSV không tồn tại và một điểm vượt 10. Không cần bấm Áp dụng nếu chỉ muốn minh họa validation.");

// Slide 13: export.
clearSlide(slides[12]);
addTitle(slides[12], "Xuất .fg tương thích FuGrade desktop", "Người dùng đặt mật khẩu mới trước khi tải file");
addRect(slides[12], { left: 62, top: 174, width: 800, height: 450, fill: PALE, line: LINE, radius: 12 });
await addScreenshot(slides[12], "06-export-fg-password.png", { left: 74, top: 186, width: 776, height: 426 }, "Hộp thoại xuất file .fg có mật khẩu");
addEyebrow(slides[12], "Ranh giới kỹ thuật", 900, 205, 280);
addText(slides[12], "Tên file được chuẩn hóa", { left: 900, top: 246, width: 280, height: 32, fontSize: 21, bold: true });
addText(slides[12], "Server tạo payload mã hóa và trả về file tải xuống với Cache-Control no-store.", { left: 900, top: 286, width: 280, height: 92, fontSize: 19, color: MUTED });
addText(slides[12], "Mật khẩu và xác nhận phải khớp", { left: 900, top: 420, width: 280, height: 58, fontSize: 21, bold: true });
addText(slides[12], "UI không cho xuất khi trường mật khẩu trống hoặc hai giá trị khác nhau.", { left: 900, top: 490, width: 280, height: 90, fontSize: 19, color: MUTED });
addFooter(slides[12], 13);
setNotes(slides[12], "Không nhập mật khẩu thật trong buổi thuyết trình. Dùng dữ liệu demo. Nói rõ mã hóa legacy phục vụ compatibility chứ không phải mô hình bảo mật cho dữ liệu lưu trữ hiện đại.");

// Slide 14: authenticated save boundary.
clearSlide(slides[13]);
addTitle(slides[13], "Đăng nhập tại thời điểm lưu snapshot", "Credentials, Google và CAPTCHA cùng tồn tại trong Auth.js flow hiện tại");
addRect(slides[13], { left: 62, top: 174, width: 800, height: 450, fill: PALE, line: LINE, radius: 12 });
await addScreenshot(slides[13], "07-login-captcha.png", { left: 74, top: 186, width: 776, height: 426 }, "Trang đăng nhập FuGrade Web với CAPTCHA và Google");
addEyebrow(slides[13], "Luồng lưu", 900, 205, 270);
addBulletList(slides[13], [
  "CAPTCHA gồm 5 ký tự được xác minh ở server",
  "Credentials dùng bcrypt",
  "Google OAuth chỉ bật khi có cấu hình",
  "Workspace guest được giữ trước khi chuyển trang",
], { left: 900, top: 248, width: 285, height: 265, fontSize: 20 });
addText(slides[13], "Không cần đăng nhập để mở, chỉnh hoặc xuất .fg.", { left: 900, top: 548, width: 280, height: 62, fontSize: 18, color: TEAL_DARK, bold: true });
addFooter(slides[13], 14);
setNotes(slides[13], "Không giải CAPTCHA trong phần trình bày. Chỉ giới thiệu ranh giới: guest cho thao tác cục bộ, authenticated editor cho snapshot persistence.");

// Slide 15: governed lifecycle.
clearSlide(slides[14]);
addTitle(slides[14], "Chu trình phát triển áp dụng cho FuGrade", "Artifact và review gate ngăn thay đổi nghiệp vụ vượt ngoài phạm vi đã duyệt");
addEyebrow(slides[14], "Artifact", 90, 194, 440);
addText(slides[14], "Context\nSpec\nPlan\nTasks", { left: 90, top: 235, width: 430, height: 245, fontSize: 31, font: RALeway, bold: true, color: INK });
addText(slides[14], "Mỗi artifact ghi evidence, rủi ro, quyết định còn thiếu và Human Final Review.", { left: 90, top: 495, width: 450, height: 78, fontSize: 20, color: MUTED });
addRect(slides[14], { left: 610, top: 190, width: 2, height: 395, fill: LINE });
addEyebrow(slides[14], "Kiểm chứng", 680, 194, 460);
addText(slides[14], "npm.cmd run lint\nnpm.cmd run build", { left: 680, top: 235, width: 480, height: 125, fontSize: 27, font: "Consolas", color: TEAL_DARK, bold: true });
addBulletList(slides[14], [
  "Project chưa có automated test command",
  "Thay đổi .fg cần fixture desktop và web",
  "Thay đổi auth cần kiểm tra unauthorized và ownership",
  "Build pass không thay thế acceptance review",
], { left: 680, top: 380, width: 480, height: 215, fontSize: 19 });
addFooter(slides[14], 15);
setNotes(slides[14], "Nêu rõ lint và build là baseline đã có evidence. Với feature mới, Definition of Done còn cần checklist nghiệp vụ, compatibility và authorization tương ứng.");

// Slide 16: risks.
clearSlide(slides[15]);
addTitle(slides[15], "Rủi ro kỹ thuật còn lại", "Các giới hạn này ảnh hưởng trực tiếp tới kế hoạch production");
const risks = [
  ["Ownership API snapshot", "GET, DELETE và một số mutation mới kiểm tra session, chưa lọc dữ liệu theo userId."],
  ["Persistence production", "SQLite cục bộ chưa phù hợp với deployment serverless hoặc nhiều instance."],
  ["Automated tests", "package.json chưa có test script nên chưa thể tuyên bố coverage hoặc regression suite."],
  ["Bonus grade conversion", "Context đã APPROVED nhưng Spec và Architecture Profile vẫn chờ Human Final Review."],
];
risks.forEach(([name, detail], index) => {
  const top = 190 + index * 105;
  addText(slides[15], String(index + 1).padStart(2, "0"), { left: 85, top, width: 60, height: 34, fontSize: 18, color: TEAL, bold: true });
  addText(slides[15], name, { left: 155, top: top - 4, width: 330, height: 42, fontSize: 24, bold: true, font: RALeway });
  addText(slides[15], detail, { left: 505, top: top - 2, width: 680, height: 66, fontSize: 19, color: MUTED });
  if (index < risks.length - 1) addRect(slides[15], { left: 85, top: top + 75, width: 1100, height: 1, fill: LINE });
});
addFooter(slides[15], 16);
setNotes(slides[15], "Không biến các rủi ro này thành claim đã sửa. Đây là backlog cần Spec và review riêng. Ưu tiên ownership trước production vì liên quan dữ liệu giữa người dùng.");

// Slide 17: demo route.
clearSlide(slides[16]);
addTitle(slides[16], "Kịch bản demo 5 phút", "Một luồng ngắn đủ cho người xem hiểu giá trị sản phẩm và cách SDD giữ claim chính xác");
const demoSteps = [
  ["01", "Bắt đầu ở guest workspace", "Giới thiệu mục tiêu và mở file demo có mật khẩu."],
  ["02", "Chỉnh bảng điểm", "Tìm sinh viên, chọn cột và sửa một ô bằng nhấp đúp."],
  ["03", "Preview import", "Dán dữ liệu có cả dòng hợp lệ và lỗi, chưa cần áp dụng."],
  ["04", "Xuất .fg", "Mở hộp thoại mật khẩu và nêu compatibility với desktop."],
  ["05", "Lưu snapshot", "Bấm Lưu để chỉ ra gate đăng nhập và CAPTCHA."],
];
demoSteps.forEach(([num, title, detail], index) => {
  const top = 178 + index * 86;
  addText(slides[16], num, { left: 95, top, width: 64, height: 38, fontSize: 20, color: WHITE, bold: true, fill: TEAL, margin: 8, align: "center", verticalAlign: "middle" });
  addText(slides[16], title, { left: 190, top: top - 2, width: 350, height: 40, fontSize: 23, bold: true, font: RALeway });
  addText(slides[16], detail, { left: 560, top: top - 1, width: 620, height: 48, fontSize: 19, color: MUTED });
});
addText(slides[16], "Kết thúc bằng ranh giới: bonus là feature đã đặc tả, chưa phải demo sản phẩm.", { left: 95, top: 625, width: 1040, height: 36, fontSize: 20, color: AMBER, bold: true });
addFooter(slides[16], 17);
setNotes(slides[16], "Nếu thời gian ít, bỏ bước chỉnh ô và giữ bốn mốc: mở file, workspace, import, lưu. Không giải CAPTCHA hoặc lưu dữ liệu thật trong buổi demo.");

await fs.mkdir(stagingDir, { recursive: true });
await fs.mkdir(outputDir, { recursive: true });
const candidatePath = path.join(stagingDir, "fugrade-sdd-product-demo-v3-candidate.pptx");
await (await PresentationFile.exportPptx(presentation)).save(candidatePath);

const sourceSha256 = crypto.createHash("sha256").update(await fs.readFile(sourcePath)).digest("hex");
const result = await finalizePresentation({
  workspaceDir,
  candidatePath,
  finalPath,
  pythonExecutable: process.env.RUNTIME_PYTHON,
  integrityValidatorPath: path.join(skillDir, "container_tools/inspect_presentation_package_integrity.py"),
  layoutValidatorPath: path.join(skillDir, "container_tools/inspect_presentation_layout_geometry.py"),
  layoutArgs: [
    "--expected-slide-size-emu", "12191365,6858000",
    "--validate-bullet-geometry",
    "--validate-heading-fit",
  ],
  explicitTotalSlideCount: 17,
  requiredNativeTableOwnerSlides: [],
  requiredNativeChartOwnerSlides: [],
  fontPolicy: {
    basis: "reference",
    families: ["Raleway", "Roboto", "Roboto Bold", "Consolas"],
    referencePath: sourcePath,
    referenceSha256: sourceSha256,
  },
  verifyArtifactToolImport: true,
  receiptPath: path.join(stagingDir, "FuGrade-SDD-Product-Demo-v3.validation.json"),
});

console.log(JSON.stringify({ finalPath, candidatePath, result }, null, 2));
