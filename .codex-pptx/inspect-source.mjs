import { pathToFileURL } from "node:url";

const skillDir = "C:/Users/Admin/.codex/plugins/cache/openai-primary-runtime/presentations/26.905.11957/skills/presentations";
const { importRuntimeModule } = await import(
  pathToFileURL(`${skillDir}/container_tools/runtime_helpers.mjs`).href
);
const { FileBlob, PresentationFile } = await importRuntimeModule("@oai/artifact-tool");
const sourcePath = "C:/Users/Admin/Downloads/Spec-Driven-Development-Tu-Ly-Thuyet-DJen-Thuc-Te.pptx";
const presentation = await PresentationFile.importPptx(await FileBlob.load(sourcePath));
const snapshot = await presentation.inspect({
  kind: "deck,slide,textbox,shape,image,table,chart,notes,layout",
  include: "id,slide,name,title,text,textPreview,textChars,textLines,bbox,bboxUnit,alt,isPlaceholder,placeholders",
  maxChars: 50000,
});
console.log(snapshot.ndjson);
console.log(JSON.stringify({
  slideSize: presentation.slideSize,
  slideCount: presentation.slides.items.length,
  masters: presentation.masters.items.map((master) => ({ id: master.id, name: master.name })),
  layouts: presentation.layouts.items.map((layout) => ({
    id: layout.id,
    name: layout.name,
    placeholders: layout.placeholders.summary(),
  })),
}, null, 2));

for (let slideIndex = 0; slideIndex < presentation.slides.items.length; slideIndex += 1) {
  const slide = presentation.slides.items[slideIndex];
  console.log(`\n--- SLIDE ${slideIndex + 1} ---`);
  for (let shapeIndex = 0; shapeIndex < slide.shapes.items.length; shapeIndex += 1) {
    const shape = slide.shapes.items[shapeIndex];
    const textValue = shape.text?.toString?.() ?? "";
    if (textValue || shape.type === "image") {
      console.log(JSON.stringify({
        shapeIndex,
        id: shape.id,
        name: shape.name,
        type: shape.type,
        text: textValue,
        position: shape.position,
        style: shape.text?.style,
      }));
    }
  }
}

const help = presentation.help("*", {
  search: "slide.shapes.delete shape.delete slide.shapes.add deleteAll slides.delete remove clear",
  include: ["index", "examples", "notes"],
  maxChars: 20000,
});
console.log("\n--- HELP ---");
console.log(help.ndjson);
const firstSlide = presentation.slides.items[0];
console.log(JSON.stringify({
  slideMethods: Object.getOwnPropertyNames(Object.getPrototypeOf(firstSlide)),
  shapesMethods: Object.getOwnPropertyNames(Object.getPrototypeOf(firstSlide.shapes)),
  slidesMethods: Object.getOwnPropertyNames(Object.getPrototypeOf(presentation.slides)),
  imageMethods: Object.getOwnPropertyNames(Object.getPrototypeOf(firstSlide.images)),
}, null, 2));
