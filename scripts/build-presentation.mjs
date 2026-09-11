/**
 * build-presentation.mjs
 * FuGrade SDD Product Demo â€” v2 deck builder (pptxgenjs, no Codex runtime)
 */

import fs from "node:fs/promises";
import path from "node:path";
import { fileURLToPath, pathToFileURL } from "node:url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const ROOT = path.resolve(__dirname, "..");
const ASSETS = path.join(ROOT, "presentation-assets");
const OUTPUT = path.join(ROOT, "output");

const PPTX_PATH = "C:\\Users\\Admin\\AppData\\Roaming\\fugrade-pptx-build\\node_modules\\pptxgenjs\\dist\\pptxgen.cjs.js";
const pptxMod = await import(pathToFileURL(PPTX_PATH).href);
const PptxGenJS = pptxMod.default || pptxMod;

const C = {
  INK:"17233B", MUTED:"5F6B7A", TEAL:"0C8D96", TEAL_DARK:"08656C",
  PALE:"F4F7FB", LINE:"DDE4EC", WHITE:"FFFFFF", AMBER:"B67714", BG:"FFFFFF",
};
const FT = "Raleway"; const FB = "Roboto"; const FC = "Consolas";
const W = 13.33; const H = 7.5;

async function imgB64(f) {
  return (await fs.readFile(path.join(ASSETS, f))).toString("base64");
}

function footer(sld, n) {
  sld.addText(`FUGRADE  /  SDD  /  ${String(n).padStart(2,"0")}`, {
    x: W-2.05, y: H-0.3, w:1.9, h:0.22,
    fontFace:FB, fontSize:8, bold:true, color:C.WHITE,
    align:"center", valign:"middle", fill:{color:C.TEAL_DARK},
  });
}
function bar(sld) {
  sld.addShape(sld.type?.rect ?? "rect", {
    x:0.55, y:1.32, w:0.65, h:0.05,
    fill:{color:C.TEAL}, line:{type:"none"},
  });
}

const pptx = new PptxGenJS();
pptx.layout = "LAYOUT_WIDE";
pptx.title = "FuGrade Web: SDD Product Demo";

// helper
function T(sld, txt, x,y,w,h, opts={}) {
  sld.addText(txt, {x,y,w,h, align:"left", valign:"top", ...opts});
}
function addBar(sld) {
  sld.addShape("rect",{x:0.55,y:1.32,w:0.65,h:0.05,fill:{color:C.TEAL},line:{type:"none"}});
}
function addTitle(sld, title, sub) {
  T(sld, title, 0.55,0.38,W-1.1,0.6, {fontFace:FT,fontSize:30,bold:true,color:C.INK});
  addBar(sld);
  if(sub) T(sld, sub, 0.55,0.98,W-1.1,0.32, {fontFace:FB,fontSize:14,color:C.MUTED});
}
function img(sld, f64, x,y,w,h) {
  sld.addImage({data:"image/png;base64,"+f64, x,y,w,h, sizing:{type:"contain",w,h}});
}

// â”€ S1 Cover â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 T(s,"FuGrade Web",0.55,1.8,6.5,0.75,{fontFace:FT,fontSize:44,bold:true,color:C.INK});
 T(s,"Spec-Driven Development trong sáº£n pháº©m tháº­t",0.55,2.6,6.5,0.6,{fontFace:FT,fontSize:26,color:C.TEAL});
 T(s,"Tá»« á»©ng dá»¥ng desktop legacy Ä‘áº¿n workspace báº£ng Ä‘iá»ƒm trÃªn web,\ncÃ³ báº±ng chá»©ng vÃ  ranh giá»›i triá»ƒn khai rÃµ rÃ ng.",0.55,3.4,6.0,0.85,{fontFace:FB,fontSize:15,color:C.MUTED});
 s.addShape("rect",{x:7.5,y:0,w:W-7.5,h:H,fill:{color:"EBF5F6"},line:{type:"none"}});
 s.addShape("rect",{x:8.2,y:1.5,w:4.5,h:3.2,fill:{color:C.TEAL},line:{type:"none"}});
 T(s,"Workspace\nbáº£ng Ä‘iá»ƒm",8.2,1.5,4.5,3.2,{fontFace:FT,fontSize:32,bold:true,color:C.WHITE,align:"center",valign:"middle"});
 T(s,"Node.js 22 Â· Next.js 16 Â· SQLite",8.0,5.2,4.8,0.4,{fontFace:FB,fontSize:13,color:C.MUTED,align:"center"});
 s.addNotes("Má»Ÿ Ä‘áº§u: FuGrade lÃ  case study SDD trong sáº£n pháº©m tháº­t.");}

// â”€ S2 Váº¥n Ä‘á» â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Váº¥n Ä‘á» cá»‘t lÃµi",null);
 const cards=[
   ["YÃªu cáº§u nghiá»‡p vá»¥ náº±m ráº£i rÃ¡c","Quy táº¯c .fg, chá»‰nh Ä‘iá»ƒm, import vÃ  lÆ°u snapshot xuáº¥t hiá»‡n á»Ÿ cáº£ desktop legacy láº«n web."],
   ["AI thiáº¿u ranh giá»›i triá»ƒn khai","Náº¿u khÃ´ng Ä‘á»c source vÃ  artifact, AI dá»… coi hÃ nh vi dá»± kiáº¿n lÃ  tÃ­nh nÄƒng Ä‘Ã£ hoÃ n thÃ nh."],
   ["Demo dá»… nÃ³i quÃ¡ hiá»‡n tráº¡ng","áº¢nh sáº£n pháº©m vÃ  evidence pháº£i tÃ¡ch pháº§n Ä‘Ã£ cháº¡y khá»i pháº§n Ä‘ang chá» review."],
 ];
 cards.forEach(([t,b],i)=>{
   const y=1.55+i*1.65;
   s.addShape("rect",{x:0.55,y,w:W-1.1,h:1.45,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
   s.addShape("rect",{x:0.55,y:y+0.1,w:0.07,h:1.25,fill:{color:C.TEAL},line:{type:"none"}});
   T(s,t,0.78,y+0.12,W-1.45,0.42,{fontFace:FT,fontSize:19,bold:true,color:C.INK});
   T(s,b,0.78,y+0.55,W-1.45,0.6,{fontFace:FB,fontSize:15,color:C.MUTED});
 });
 s.addNotes("Ba nguá»“n sá»± tháº­t: source hiá»‡n táº¡i, hÃ nh vi cháº¡y Ä‘Æ°á»£c, artifact Ä‘Ã£ review.");}

// â”€ S3 SDD + Lifecycle â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Spec-Driven Development","Spec lÃ  há»£p Ä‘á»“ng giá»¯a Ã½ Ä‘á»‹nh cá»§a con ngÆ°á»i vÃ  pháº§n triá»ƒn khai cÃ³ thá»ƒ kiá»ƒm chá»©ng.");
 s.addShape("rect",{x:0.55,y:1.45,w:W-1.1,h:0.62,fill:{color:"E8F6F7"},line:{color:C.TEAL,pt:1}});
 T(s,"âœ“  Má»™t spec tá»‘t Ä‘á»§ rÃµ Ä‘á»ƒ AI khÃ´ng pháº£i Ä‘oÃ¡n vÃ  Ä‘á»§ ngáº¯n Ä‘á»ƒ con ngÆ°á»i review.",0.75,1.52,W-1.5,0.45,{fontFace:FB,fontSize:16,bold:true,color:C.TEAL_DARK});
 T(s,"Chu trÃ¬nh 5 pha:",0.55,2.22,3,0.28,{fontFace:FB,fontSize:13,bold:true,color:C.MUTED});
 const phases=[["01","Context","XÃ¡c Ä‘á»‹nh váº¥n Ä‘á»"],["02","Spec","KhÃ³a hÃ nh vi"],["03","Plan","Giá»›i háº¡n cÃ¡ch sá»­a"],["04","Tasks","Chi tiáº¿t hÃ³a"],["05","Implement","Chá»‰ sau review"]];
 const bw=2.28,gap=0.12;
 phases.forEach((p,i)=>{
   const x=0.55+i*(bw+gap);
   const col=i===0?C.TEAL:i<3?"2BAAB2":"4DC0C8";
   s.addShape("rect",{x,y:2.6,w:bw,h:1.55,fill:{color:col},line:{type:"none"}});
   T(s,p[0],x,2.65,bw,0.38,{fontFace:FC,fontSize:11,color:"DDDDDD",align:"center"});
   T(s,p[1],x,2.98,bw,0.46,{fontFace:FT,fontSize:19,bold:true,color:C.WHITE,align:"center"});
   T(s,p[2],x,3.46,bw,0.55,{fontFace:FB,fontSize:12,color:"EEEEEE",align:"center"});
 });
 T(s,"Má»—i pha tráº£ lá»i má»™t cÃ¢u há»i khÃ¡c nhau. Context xÃ¡c Ä‘á»‹nh váº¥n Ä‘á». Spec khÃ³a hÃ nh vi. Plan vÃ  Tasks giá»›i háº¡n cÃ¡ch sá»­a. Implement chá»‰ báº¯t Ä‘áº§u sau review phÃ¹ há»£p.",0.55,4.35,W-1.1,0.68,{fontFace:FB,fontSize:14,color:C.MUTED});
 s.addNotes("Lifecycle 5 pha. Má»—i pha tráº£ lá»i 1 cÃ¢u há»i.");}

// â”€ S4 8 thÃ nh pháº§n â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"8 thÃ nh pháº§n cá»§a má»™t Spec cÃ³ thá»ƒ kiá»ƒm chá»©ng",null);
 const comps=[["Bá»‘i cáº£nh & má»¥c tiÃªu","TÃ­nh nÄƒng giáº£i quyáº¿t váº¥n Ä‘á» nÃ o?"],["TÃ¡c nhÃ¢n & quyá»n","Ai thao tÃ¡c vÃ  Ä‘Æ°á»£c phÃ©p lÃ m gÃ¬?"],["YÃªu cáº§u chá»©c nÄƒng","Há»‡ thá»‘ng pháº£i lÃ m gÃ¬?"],["YÃªu cáº§u phi chá»©c nÄƒng","Má»©c cháº¥t lÆ°á»£ng cáº§n Ä‘áº¡t?"],["Dá»¯ liá»‡u","Cáº¥u trÃºc vÃ  vÃ²ng Ä‘á»i dá»¯ liá»‡u?"],["Xá»­ lÃ½ lá»—i","Lá»—i nÃ o xáº£y ra vÃ  pháº£n há»“i tháº¿ nÃ o?"],["TiÃªu chÃ­ cháº¥p nháº­n","Báº±ng chá»©ng nÃ o chá»©ng minh hoÃ n thÃ nh?"],["NgoÃ i pháº¡m vi","Äiá»u gÃ¬ chÆ°a lÃ m trong phiÃªn báº£n nÃ y?"]];
 const cols=4,bw=(W-1.2)/cols-0.1,bh=1.45;
 comps.forEach(([t,b],idx)=>{
   const col=idx%cols,row=Math.floor(idx/cols);
   const x=0.6+col*(bw+0.12),y=1.5+row*(bh+0.14);
   s.addShape("rect",{x,y,w:bw,h:bh,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
   s.addShape("rect",{x,y,w:bw,h:0.06,fill:{color:C.TEAL},line:{type:"none"}});
   T(s,String(idx+1).padStart(2,"0"),x+0.12,y+0.12,0.5,0.34,{fontFace:FC,fontSize:12,color:C.TEAL,bold:true});
   T(s,t,x+0.12,y+0.45,bw-0.24,0.5,{fontFace:FT,fontSize:14,bold:true,color:C.INK});
   T(s,b,x+0.12,y+0.92,bw-0.24,0.42,{fontFace:FB,fontSize:12,color:C.MUTED});
 });
 s.addNotes("8 thÃ nh pháº§n. LiÃªn há»‡ FuGrade: actors = guest vs authenticated.");}

// â”€ S5 EARS â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"EARS cho ranh giá»›i lÆ°u snapshot","Requirement mÃ´ táº£ Ä‘Ãºng thá»i Ä‘iá»ƒm há»‡ thá»‘ng yÃªu cáº§u Ä‘Äƒng nháº­p.");
 s.addShape("rect",{x:0.55,y:1.85,w:W-1.1,h:2.2,fill:{color:"1A2B3C"},line:{type:"none"}});
 T(s,"GIVEN  ngÆ°á»i dÃ¹ng Ä‘ang chá»‰nh sá»­a file .fg á»Ÿ cháº¿ Ä‘á»™ khÃ¡ch\nWHEN   ngÆ°á»i dÃ¹ng báº¥m LÆ°u\nTHEN   há»‡ thá»‘ng giá»¯ workspace trong sessionStorage\n       vÃ  chuyá»ƒn Ä‘áº¿n /login",0.85,2.0,W-1.7,1.8,{fontFace:FC,fontSize:16,color:"7FDBCA"});
 s.addShape("rect",{x:0.55,y:4.3,w:W-1.1,h:0.65,fill:{color:"E8F6F7"},line:{color:C.TEAL,pt:1}});
 T(s,"âœ“  ÄÄƒng nháº­p chá»‰ cáº§n khi lÆ°u snapshot. Má»Ÿ, chá»‰nh vÃ  xuáº¥t .fg khÃ´ng yÃªu cáº§u Ä‘Äƒng nháº­p.",0.75,4.38,W-1.5,0.5,{fontFace:FB,fontSize:15,bold:true,color:C.TEAL_DARK});
 s.addNotes("EARS example tá»« app/page.tsx: saveSnapshot â†’ sessionStorage â†’ /login.");}

// â”€ S6 Section break â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 s.addShape("rect",{x:0,y:0,w:5.2,h:H,fill:{color:"EBF5F6"},line:{type:"none"}});
 s.addShape("rect",{x:0.55,y:2.0,w:4.0,h:2.5,fill:{color:C.TEAL},line:{type:"none"}});
 T(s,"FuGrade\nWeb",0.55,2.0,4.0,2.5,{fontFace:FT,fontSize:52,bold:true,color:C.WHITE,align:"center",valign:"middle"});
 T(s,"PHáº¦N 2",5.6,2.4,3.5,0.35,{fontFace:FB,fontSize:11,bold:true,color:C.TEAL});
 T(s,"FuGrade Web:\nhiá»‡n tráº¡ng sáº£n pháº©m",5.6,2.85,7.0,1.4,{fontFace:FT,fontSize:38,bold:true,color:C.INK});
 T(s,"Káº¿t luáº­n dá»±a trÃªn source, runtime vÃ  áº£nh chá»¥p giao diá»‡n cháº¡y trÃªn localhost.",5.6,4.4,7.0,0.55,{fontFace:FB,fontSize:15,color:C.MUTED});
 s.addNotes("Pháº§n 2: case study FuGrade Web.");}

// â”€ S7 Evidence â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Hiá»‡n tráº¡ng Ä‘Æ°á»£c xÃ¡c minh",null);
 const items=[
   "Runtime phÃ¹ há»£p: Node.js 22.22.3, Next.js 16.3.2.",
   "Luá»“ng guest hoáº¡t Ä‘á»™ng: má»Ÿ, chá»‰nh, import vÃ  xuáº¥t .fg khÃ´ng cáº§n Ä‘Äƒng nháº­p.",
   "LÆ°u cÃ³ gate Ä‘Äƒng nháº­p: workspace giá»¯ trong sessionStorage trÆ°á»›c khi chuyá»ƒn /login.",
 ];
 items.forEach((txt,i)=>{
   const y=1.55+i*1.4;
   s.addShape("rect",{x:0.55,y,w:W-1.1,h:1.22,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
   T(s,"â–¶",0.7,y+0.32,0.42,0.5,{fontFace:FB,fontSize:18,color:C.TEAL,bold:true});
   T(s,txt,1.22,y+0.22,W-1.85,0.78,{fontFace:FB,fontSize:18,color:C.INK});
 });
 T(s,"npm.cmd run lint  âœ“    npm.cmd run build  âœ“",0.55,6.65,W-1.1,0.3,{fontFace:FC,fontSize:12,color:C.TEAL_DARK,align:"center"});
 s.addNotes("Lint vÃ  build lÃ  baseline ká»¹ thuáº­t, khÃ´ng thay tháº¿ acceptance review.");}

// â”€ S8 Pháº¡m vi â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Pháº¡m vi sáº£n pháº©m Ä‘Ã£ xÃ¡c minh","Deck chá»‰ demo hÃ nh vi cÃ³ trong source vÃ  giao diá»‡n Ä‘ang cháº¡y");
 T(s,"ÄÃƒ TRIá»‚N KHAI",0.6,1.58,3,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"Workspace báº£ng Ä‘iá»ƒm",0.6,1.85,5.2,0.5,{fontFace:FT,fontSize:22,bold:true,color:C.INK});
 T(s,"â€¢ Má»Ÿ file .fg cÃ³ hoáº·c khÃ´ng cÃ³ máº­t kháº©u\nâ€¢ Chá»‰nh Ä‘iá»ƒm, nháº­n xÃ©t, sinh viÃªn vÃ  thÃ nh pháº§n trong phiÃªn\nâ€¢ Import Ä‘iá»ƒm/nháº­n xÃ©t theo MSSV vá»›i preview validation\nâ€¢ Xuáº¥t .fg cÃ³ máº­t kháº©u vÃ  lÆ°u snapshot sau khi Ä‘Äƒng nháº­p",0.6,2.42,5.2,2.8,{fontFace:FB,fontSize:15,color:C.INK});
 s.addShape("rect",{x:6.3,y:1.55,w:0.02,h:4.2,fill:{color:C.LINE},line:{type:"none"}});
 T(s,"CHÆ¯A TRIá»‚N KHAI",6.5,1.58,6.3,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.AMBER});
 T(s,"CÃ¡c háº¡ng má»¥c Ä‘ang chá» quyáº¿t Ä‘á»‹nh",6.5,1.85,6.3,0.5,{fontFace:FT,fontSize:22,bold:true,color:C.INK});
 T(s,"â€¢ Quy Ä‘á»•i Ä‘iá»ƒm bonus: Spec váº«n PENDING HUMAN REVIEW\nâ€¢ Database production vÃ  chiáº¿n lÆ°á»£c multi-instance chÆ°a Ä‘Æ°á»£c chá»n\nâ€¢ Project chÆ°a cÃ³ automated test command\nâ€¢ Ownership cho API snapshot cáº§n Ä‘Æ°á»£c Ä‘áº·c táº£ trÆ°á»›c production",6.5,2.42,6.3,2.8,{fontFace:FB,fontSize:15,color:C.INK});
 s.addShape("rect",{x:0.55,y:6.15,w:W-1.1,h:0.52,fill:{color:"FFF8E8"},line:{color:C.AMBER,pt:1}});
 T(s,"âš   Khi thuyáº¿t trÃ¬nh, bonus lÃ  hÆ°á»›ng phÃ¡t triá»ƒn Ä‘Ã£ Ä‘áº·c táº£, khÃ´ng pháº£i tÃ­nh nÄƒng hiá»‡n cÃ³.",0.75,6.22,W-1.5,0.38,{fontFace:FB,fontSize:14,bold:true,color:C.AMBER});
 footer(s,8);s.addNotes("Slide quan trá»ng nháº¥t. Bonus: Context APPROVED, Spec váº«n PENDING.");}

// â”€ S9 Guest entry â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgGuest = await imgB64("01-home-guest.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Äiá»ƒm vÃ o cá»§a guest workspace","NgÆ°á»i dÃ¹ng cÃ³ thá»ƒ báº¯t Ä‘áº§u mÃ  chÆ°a cáº§n táº¡o tÃ i khoáº£n");
 s.addShape("rect",{x:0.45,y:1.48,w:7.5,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgGuest,0.55,1.58,7.3,5.2);
 T(s,"GIÃ TRá»Š Sáº¢N PHáº¨M",8.3,1.65,4.5,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"Báº¯t Ä‘áº§u nhanh",8.3,1.95,4.5,0.5,{fontFace:FT,fontSize:24,bold:true,color:C.INK});
 T(s,"Má»Ÿ vÃ  chá»‰nh sá»­a báº£ng Ä‘iá»ƒm ngay trong trÃ¬nh duyá»‡t. ÄÄƒng nháº­p chá»‰ xuáº¥t hiá»‡n khi ngÆ°á»i dÃ¹ng muá»‘n lÆ°u snapshot vÃ o SQLite.",8.3,2.55,4.5,1.5,{fontFace:FB,fontSize:15,color:C.MUTED});
 T(s,"â€¢ Chá»‰nh Ä‘iá»ƒm nhanh\nâ€¢ Import hÃ ng loáº¡t\nâ€¢ Xuáº¥t cÃ³ máº­t kháº©u",8.3,4.68,4.5,1.0,{fontFace:FB,fontSize:14,color:C.INK});
 T(s,"áº¢nh chá»¥p tá»« localhost, dá»¯ liá»‡u demo tá»•ng há»£p",8.3,6.62,4.5,0.3,{fontFace:FB,fontSize:10,color:C.MUTED});
 footer(s,9);s.addNotes("Guest mode lÃ  quyáº¿t Ä‘á»‹nh sáº£n pháº©m. NÃºt LÆ°u = ranh giá»›i auth.");}

// â”€ S10 Password â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgPwd = await imgB64("03-password-required.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Má»Ÿ file .fg cÃ³ máº­t kháº©u","á»¨ng dá»¥ng chá»‰ yÃªu cáº§u máº­t kháº©u sau khi phÃ¡t hiá»‡n payload Ä‘Æ°á»£c báº£o vá»‡");
 s.addShape("rect",{x:0.45,y:1.48,w:7.8,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgPwd,0.55,1.58,7.6,5.2);
 T(s,"HÃ€NH VI ÄÃƒ CHáº Y",8.55,1.78,4.3,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"â€¢ Giá»›i háº¡n kÃ­ch thÆ°á»›c file á»Ÿ 10 MB\nâ€¢ PhÃ¢n biá»‡t thiáº¿u máº­t kháº©u vÃ  máº­t kháº©u khÃ´ng Ä‘Ãºng\nâ€¢ Tráº£ dá»¯ liá»‡u Ä‘Ã£ parse vá» guest workspace sau khi xÃ¡c minh",8.55,2.12,4.3,2.5,{fontFace:FB,fontSize:15,color:C.INK});
 T(s,"Compatibility giá»¯ AES-256-CBC vÃ  MD5 theo Ä‘á»‹nh dáº¡ng legacy.",8.55,5.35,4.3,0.9,{fontFace:FB,fontSize:14,color:C.MUTED});
 footer(s,10);s.addNotes("Compatibility vá»›i FuGrade desktop. CÆ¡ cháº¿ legacy chá»‰ trao Ä‘á»•i file.");}

// â”€ S11 Workspace â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgWs = await imgB64("04-workspace-grade-table.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Workspace chá»‰nh sá»­a báº£ng Ä‘iá»ƒm","Má»™t mÃ n hÃ¬nh táº­p trung cho metadata, cá»™t Ä‘iá»ƒm, tÃ¬m kiáº¿m vÃ  thao tÃ¡c nhanh");
 s.addShape("rect",{x:0.4,y:1.48,w:8.7,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgWs,0.5,1.58,8.5,5.2);
 T(s,"WORKSPACE TRONG PHIÃŠN",9.35,1.7,3.45,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"Chá»‰nh trá»±c tiáº¿p",9.35,2.0,3.45,0.42,{fontFace:FT,fontSize:20,bold:true,color:C.INK});
 T(s,"Nháº¥p Ä‘Ãºp Ã´ Ä‘iá»ƒm hoáº·c nháº­n xÃ©t Ä‘á»ƒ sá»­a. Enter lÆ°u, Esc há»§y.",9.35,2.5,3.45,1.0,{fontFace:FB,fontSize:14,color:C.MUTED});
 T(s,"Giá»¯ tráº¡ng thÃ¡i cá»¥c bá»™",9.35,3.8,3.45,0.42,{fontFace:FT,fontSize:20,bold:true,color:C.INK});
 T(s,"Workspace náº±m trong sessionStorage cá»§a tab cho tá»›i khi ngÆ°á»i dÃ¹ng chá»n LÆ°u.",9.35,4.3,3.45,1.1,{fontFace:FB,fontSize:14,color:C.MUTED});
 footer(s,11);s.addNotes("Demo: lá»c sinh viÃªn, chá»n cá»™t, nháº¥p Ä‘Ãºp Ã´ Ä‘iá»ƒm.");}

// â”€ S12 Inline edit (Má»šI) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgInline = await imgB64("08-inline-edit.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Chá»‰nh Ä‘iá»ƒm inline â€” nháº¥p Ä‘Ãºp vÃ o Ã´","Ã” Ä‘iá»ƒm chuyá»ƒn sang input mode mÃ  khÃ´ng cáº§n rá»i khá»i báº£ng");
 s.addShape("rect",{x:0.4,y:1.48,w:8.7,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgInline,0.5,1.58,8.5,5.2);
 T(s,"UX ÄIá»‚M Ná»”I Báº¬T",9.35,1.7,3.45,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"â€¢ Nháº¥p Ä‘Ãºp Ä‘á»ƒ sá»­a\nâ€¢ Enter Ä‘á»ƒ lÆ°u giÃ¡ trá»‹\nâ€¢ Esc Ä‘á»ƒ há»§y thao tÃ¡c\nâ€¢ Tab Ä‘á»ƒ qua Ã´ káº¿ tiáº¿p",9.35,2.1,3.45,2.5,{fontFace:FB,fontSize:15,color:C.INK});
 T(s,"Tráº¡ng thÃ¡i chá»‰nh sá»­a Ä‘Æ°á»£c giá»¯ trong sessionStorage.",9.35,5.0,3.45,1.4,{fontFace:FB,fontSize:13,color:C.MUTED});
 footer(s,12);s.addNotes("Demo: double-click Ã´ Ä‘iá»ƒm báº¥t ká»³, gÃµ giÃ¡ trá»‹ má»›i, Enter.");}

// â”€ S13 Search (Má»šI) â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgSearch = await imgB64("09-search-filter.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"TÃ¬m kiáº¿m sinh viÃªn theo MSSV hoáº·c tÃªn","Lá»c danh sÃ¡ch realtime â€” khÃ´ng cáº§n táº£i láº¡i trang");
 s.addShape("rect",{x:0.4,y:1.48,w:8.7,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgSearch,0.5,1.58,8.5,5.2);
 T(s,"TÃNH NÄ‚NG Lá»ŒC",9.35,1.7,3.45,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"â€¢ Nháº­p MSSV hoáº·c há» tÃªn\nâ€¢ Káº¿t quáº£ cáº­p nháº­t realtime\nâ€¢ LÃ m ná»•i báº­t sinh viÃªn cáº§n tÃ¬m\nâ€¢ Hoáº¡t Ä‘á»™ng song song vá»›i inline edit",9.35,2.1,3.45,2.5,{fontFace:FB,fontSize:15,color:C.INK});
 T(s,"áº¢nh: tÃ¬m \"Phong\" â†’ lá»c cÃ²n 1 sinh viÃªn",9.35,6.45,3.45,0.42,{fontFace:FB,fontSize:11,color:C.MUTED});
 footer(s,13);s.addNotes("Demo: gÃµ tÃªn vÃ o search bar, báº£ng thu háº¹p ngay láº­p tá»©c.");}

// â”€ S14 Import â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgImport = await imgB64("05-import-validation.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Import hÃ ng loáº¡t cÃ³ preview validation","DÃ²ng há»£p lá»‡ vÃ  dÃ²ng lá»—i Ä‘Æ°á»£c tÃ¡ch trÆ°á»›c khi Ã¡p dá»¥ng vÃ o workspace");
 s.addShape("rect",{x:0.45,y:1.48,w:7.9,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgImport,0.55,1.58,7.7,5.2);
 T(s,"ÄIá»‚M Ná»”I Báº¬T",8.7,1.75,4.2,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"â€¢ DÃ¡n dá»¯ liá»‡u trá»±c tiáº¿p tá»« Excel\nâ€¢ Khá»›p sinh viÃªn theo MSSV\nâ€¢ Tá»« chá»‘i Ä‘iá»ƒm ngoÃ i khoáº£ng 0â€“10\nâ€¢ Chá»‰ Ã¡p dá»¥ng cÃ¡c dÃ²ng há»£p lá»‡",8.7,2.12,4.2,2.5,{fontFace:FB,fontSize:15,color:C.INK});
 T(s,"Preview giÃºp tháº¥y validation trÆ°á»›c mutation.",8.7,5.35,4.2,1.0,{fontFace:FB,fontSize:14,color:C.MUTED});
 footer(s,14);s.addNotes("Demo: dÃ¡n 4 dÃ²ng, 2 há»£p lá»‡, 1 MSSV khÃ´ng tá»“n táº¡i, 1 Ä‘iá»ƒm >10.");}

// â”€ S15 Export â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgExport = await imgB64("06-export-fg-password.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Xuáº¥t .fg tÆ°Æ¡ng thÃ­ch FuGrade desktop","NgÆ°á»i dÃ¹ng Ä‘áº·t máº­t kháº©u má»›i trÆ°á»›c khi táº£i file");
 s.addShape("rect",{x:0.45,y:1.48,w:7.9,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgExport,0.55,1.58,7.7,5.2);
 T(s,"RANH GIá»šI Ká»¸ THUáº¬T",8.7,1.75,4.2,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"TÃªn file Ä‘Æ°á»£c chuáº©n hÃ³a",8.7,2.12,4.2,0.42,{fontFace:FT,fontSize:18,bold:true,color:C.INK});
 T(s,"Server táº¡o payload mÃ£ hÃ³a vÃ  tráº£ vá» file táº£i xuá»‘ng vá»›i Cache-Control no-store.",8.7,2.62,4.2,0.95,{fontFace:FB,fontSize:14,color:C.MUTED});
 T(s,"Máº­t kháº©u vÃ  xÃ¡c nháº­n pháº£i khá»›p",8.7,3.82,4.2,0.42,{fontFace:FT,fontSize:18,bold:true,color:C.INK});
 T(s,"UI khÃ´ng cho xuáº¥t khi trÆ°á»ng máº­t kháº©u trá»‘ng hoáº·c hai giÃ¡ trá»‹ khÃ¡c nhau.",8.7,4.32,4.2,0.95,{fontFace:FB,fontSize:14,color:C.MUTED});
 footer(s,15);s.addNotes("KhÃ´ng nháº­p máº­t kháº©u tháº­t khi demo. AES-256-CBC phá»¥c vá»¥ compatibility, khÃ´ng pháº£i báº£o máº­t hiá»‡n Ä‘áº¡i.");}

// â”€ S16 Login gate â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
const imgLogin = await imgB64("07-login-captcha.png");
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"ÄÄƒng nháº­p táº¡i thá»i Ä‘iá»ƒm lÆ°u snapshot","Credentials, Google vÃ  CAPTCHA cÃ¹ng tá»“n táº¡i trong Auth.js flow hiá»‡n táº¡i");
 s.addShape("rect",{x:0.45,y:1.48,w:7.9,h:5.4,fill:{color:C.PALE},line:{color:C.LINE,pt:1}});
 img(s,imgLogin,0.55,1.58,7.7,5.2);
 T(s,"LUá»’NG LÆ¯U",8.7,1.75,4.2,0.22,{fontFace:FB,fontSize:10,bold:true,color:C.TEAL});
 T(s,"â€¢ CAPTCHA gá»“m 5 kÃ½ tá»± Ä‘Æ°á»£c xÃ¡c minh á»Ÿ server\nâ€¢ Credentials dÃ¹ng bcrypt\nâ€¢ Google OAuth chá»‰ báº­t khi cÃ³ cáº¥u hÃ¬nh\nâ€¢ Workspace guest Ä‘Æ°á»£c giá»¯ trÆ°á»›c khi chuyá»ƒn trang",8.7,2.12,4.2,2.8,{fontFace:FB,fontSize:15,color:C.INK});
 T(s,"KhÃ´ng cáº§n Ä‘Äƒng nháº­p Ä‘á»ƒ má»Ÿ, chá»‰nh hoáº·c xuáº¥t .fg.",8.7,5.42,4.2,0.72,{fontFace:FB,fontSize:15,bold:true,color:C.TEAL_DARK});
 footer(s,16);s.addNotes("KhÃ´ng giáº£i CAPTCHA khi thuyáº¿t trÃ¬nh. Guest = local ops, auth = snapshot persistence.");}

// â”€ S17 Rá»§i ro â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Rá»§i ro ká»¹ thuáº­t cÃ²n láº¡i","CÃ¡c giá»›i háº¡n nÃ y áº£nh hÆ°á»Ÿng trá»±c tiáº¿p tá»›i káº¿ hoáº¡ch production");
 const risks=[
   ["Ownership API snapshot","GET, DELETE vÃ  má»™t sá»‘ mutation má»›i kiá»ƒm tra session, chÆ°a lá»c dá»¯ liá»‡u theo userId."],
   ["Persistence production","SQLite cá»¥c bá»™ chÆ°a phÃ¹ há»£p vá»›i deployment serverless hoáº·c nhiá»u instance."],
   ["Automated tests","package.json chÆ°a cÃ³ test script nÃªn chÆ°a thá»ƒ tuyÃªn bá»‘ coverage hoáº·c regression suite."],
   ["Bonus grade conversion","Context Ä‘Ã£ APPROVED nhÆ°ng Spec vÃ  Architecture Profile váº«n chá» Human Final Review."],
 ];
 risks.forEach(([n,d],i)=>{
   const y=1.55+i*1.15;
   T(s,String(i+1).padStart(2,"0"),0.55,y,0.62,0.55,{fontFace:FC,fontSize:20,bold:true,color:C.TEAL});
   s.addShape("rect",{x:1.25,y:y+0.12,w:0.02,h:0.38,fill:{color:C.LINE},line:{type:"none"}});
   T(s,n,1.42,y,3.5,0.55,{fontFace:FT,fontSize:19,bold:true,color:C.INK});
   T(s,d,5.1,y+0.04,W-5.65,0.72,{fontFace:FB,fontSize:14,color:C.MUTED});
   if(i<risks.length-1)s.addShape("rect",{x:0.55,y:y+0.88,w:W-1.1,h:0.01,fill:{color:C.LINE},line:{type:"none"}});
 });
 footer(s,17);s.addNotes("Rá»§i ro = backlog cáº§n Spec riÃªng. Æ¯u tiÃªn ownership trÆ°á»›c production.");}

// â”€ S18 Demo 5 phÃºt â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
{const s=pptx.addSlide();s.background={color:C.BG};
 addTitle(s,"Ká»‹ch báº£n demo 5 phÃºt","Má»™t luá»“ng ngáº¯n Ä‘á»§ cho ngÆ°á»i xem hiá»ƒu giÃ¡ trá»‹ sáº£n pháº©m vÃ  cÃ¡ch SDD giá»¯ claim chÃ­nh xÃ¡c");
 const steps=[["01","Báº¯t Ä‘áº§u á»Ÿ guest workspace","Giá»›i thiá»‡u má»¥c tiÃªu vÃ  má»Ÿ file demo cÃ³ máº­t kháº©u."],["02","Chá»‰nh báº£ng Ä‘iá»ƒm","TÃ¬m sinh viÃªn, chá»n cá»™t vÃ  sá»­a má»™t Ã´ báº±ng nháº¥p Ä‘Ãºp."],["03","Preview import","DÃ¡n dá»¯ liá»‡u cÃ³ cáº£ dÃ²ng há»£p lá»‡ vÃ  lá»—i, chÆ°a cáº§n Ã¡p dá»¥ng."],["04","Xuáº¥t .fg","Má»Ÿ há»™p thoáº¡i máº­t kháº©u vÃ  nÃªu compatibility vá»›i desktop."],["05","LÆ°u snapshot","Báº¥m LÆ°u Ä‘á»ƒ chá»‰ ra gate Ä‘Äƒng nháº­p vÃ  CAPTCHA."]];
 steps.forEach(([num,title,detail],i)=>{
   const y=1.5+i*0.95;
   s.addShape("rect",{x:0.55,y:y+0.06,w:0.65,h:0.65,fill:{color:C.TEAL},line:{type:"none"}});
   T(s,num,0.55,y+0.06,0.65,0.65,{fontFace:FC,fontSize:16,bold:true,color:C.WHITE,align:"center",valign:"middle"});
   T(s,title,1.42,y,4.2,0.52,{fontFace:FT,fontSize:20,bold:true,color:C.INK});
   T(s,detail,5.85,y+0.05,W-6.4,0.7,{fontFace:FB,fontSize:15,color:C.MUTED});
   if(i<steps.length-1)s.addShape("rect",{x:0.55,y:y+0.82,w:W-1.1,h:0.01,fill:{color:C.LINE},line:{type:"none"}});
 });
 s.addShape("rect",{x:0.55,y:6.42,w:W-1.1,h:0.48,fill:{color:"FFF8E8"},line:{color:C.AMBER,pt:1}});
 T(s,"âš   Káº¿t thÃºc báº±ng ranh giá»›i: bonus lÃ  feature Ä‘Ã£ Ä‘áº·c táº£, chÆ°a pháº£i demo sáº£n pháº©m.",0.75,6.49,W-1.5,0.34,{fontFace:FB,fontSize:14,bold:true,color:C.AMBER});
 footer(s,18);s.addNotes("Náº¿u thá»i gian Ã­t, bá» bÆ°á»›c chá»‰nh Ã´. KhÃ´ng lÆ°u dá»¯ liá»‡u tháº­t trong demo.");}

// â”€ Output â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
await fs.mkdir(OUTPUT, {recursive:true});
const outPath = path.join(OUTPUT, "FuGrade-SDD-Product-Demo-v2.pptx");
await pptx.writeFile({fileName: outPath});
console.log("OK: " + outPath);
console.log("Slides: 18");


