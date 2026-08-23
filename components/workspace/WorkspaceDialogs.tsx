"use client";

import { useState } from "react";
import { toast } from "sonner";

function DialogFrame({
  eyebrow,
  title,
  children,
  onClose,
  className = "",
}: {
  eyebrow: string;
  title: string;
  children: React.ReactNode;
  onClose: () => void;
  className?: string;
}) {
  return (
    <div className="modal-overlay" role="presentation" onMouseDown={onClose}>
      <section className={`modal-box workspace-dialog ${className}`} role="dialog" aria-modal="true" onMouseDown={(event) => event.stopPropagation()}>
        <div className="dialog-header">
          <div><span className="eyebrow">{eyebrow}</span><h2>{title}</h2></div>
          <button type="button" className="icon-button" onClick={onClose} aria-label="Đóng">×</button>
        </div>
        {children}
      </section>
    </div>
  );
}

export function AddStudentDialog({
  existingRolls,
  onAdd,
  onClose,
}: {
  existingRolls: string[];
  onAdd: (roll: string, name: string) => void;
  onClose: () => void;
}) {
  const [roll, setRoll] = useState("");
  const [name, setName] = useState("");

  return (
    <DialogFrame eyebrow="CHỈNH SỬA NHANH" title="Thêm sinh viên" onClose={onClose}>
      <form onSubmit={(event) => {
        event.preventDefault();
        const normalizedRoll = roll.trim().toUpperCase();
        if (!normalizedRoll) return;
        if (existingRolls.some((item) => item.toUpperCase() === normalizedRoll)) {
          return toast.error("MSSV đã tồn tại trong lớp");
        }
        onAdd(normalizedRoll, name.trim());
        onClose();
      }}>
        <label className="field-group"><span>MSSV *</span><input className="input" value={roll} onChange={(event) => setRoll(event.target.value)} placeholder="HE123456" required autoFocus /></label>
        <label className="field-group"><span>Họ tên</span><input className="input" value={name} onChange={(event) => setName(event.target.value)} placeholder="Nguyễn Văn A" /></label>
        <div className="dialog-actions"><button type="button" className="btn btn-secondary" onClick={onClose}>Hủy</button><button className="btn btn-primary" type="submit">Thêm sinh viên</button></div>
      </form>
    </DialogFrame>
  );
}

export function AddComponentDialog({
  existingComponents,
  onAdd,
  onClose,
}: {
  existingComponents: string[];
  onAdd: (name: string) => void;
  onClose: () => void;
}) {
  const [name, setName] = useState("");

  return (
    <DialogFrame eyebrow="CHỈNH SỬA NHANH" title="Thêm thành phần điểm" onClose={onClose}>
      <form onSubmit={(event) => {
        event.preventDefault();
        const normalizedName = name.trim();
        if (!normalizedName) return;
        if (existingComponents.some((item) => item.toUpperCase() === normalizedName.toUpperCase())) {
          return toast.error("Thành phần điểm đã tồn tại");
        }
        onAdd(normalizedName);
        onClose();
      }}>
        <label className="field-group"><span>Tên thành phần *</span><input className="input" value={name} onChange={(event) => setName(event.target.value)} placeholder="Final exam, Progress Test…" required autoFocus /></label>
        <div className="dialog-actions"><button type="button" className="btn btn-secondary" onClick={onClose}>Hủy</button><button className="btn btn-primary" type="submit">Thêm thành phần</button></div>
      </form>
    </DialogFrame>
  );
}

function parseRows(text: string, skipHeader: boolean) {
  const lines = text.split(/\r?\n/).filter((line) => line.trim());
  return lines.slice(skipHeader ? 1 : 0).map((line) => {
    const parts = line.trim().split(/[\t ]+/);
    return { roll: parts[0] ?? "", value: parts.slice(1).join(" ") };
  });
}

export function ImportRowsDialog({
  mode,
  component,
  validRolls,
  onApply,
  onClose,
}: {
  mode: "grade" | "comment";
  component?: string;
  validRolls: string[];
  onApply: (rows: { roll: string; value: string }[]) => void;
  onClose: () => void;
}) {
  const [text, setText] = useState("");
  const [skipHeader, setSkipHeader] = useState(false);
  const rows = parseRows(text, skipHeader);
  const knownRolls = new Set(validRolls.map((roll) => roll.toUpperCase()));
  const preview = rows.map((row) => {
    let error = "";
    if (!knownRolls.has(row.roll.toUpperCase())) error = "Không tìm thấy MSSV";
    if (mode === "grade") {
      const rawValue = row.value.trim();
      const numberValue = Number(rawValue);
      if (!rawValue) error = "Thiếu điểm";
      else if (!Number.isFinite(numberValue) || numberValue < 0 || numberValue > 10) error = "Điểm phải từ 0–10";
    } else if (!row.value) error = "Thiếu nội dung";
    return { ...row, error };
  });
  const validRows = preview.filter((row) => !row.error);

  return (
    <DialogFrame
      eyebrow="DÁN DỮ LIỆU TỪ EXCEL"
      title={mode === "grade" ? `Import điểm · ${component}` : "Import nhận xét"}
      onClose={onClose}
      className="import-dialog"
    >
      <textarea
        className="input import-textarea"
        value={text}
        onChange={(event) => setText(event.target.value)}
        placeholder={mode === "grade" ? "HE180186\t8.5\nHE180202\t7.0" : "HE180186\tHoàn thành tốt"}
        autoFocus
      />
      <label className="check-row"><input type="checkbox" checked={skipHeader} onChange={(event) => setSkipHeader(event.target.checked)} />Bỏ qua dòng tiêu đề</label>
      {preview.length > 0 && (
        <div className="import-preview">
          <div className="preview-summary">{validRows.length}/{preview.length} dòng hợp lệ</div>
          {preview.map((row, index) => (
            <div className={`preview-row ${row.error ? "has-error" : ""}`} key={`${row.roll}-${index}`}>
              <strong>{row.roll || "—"}</strong><span>{row.value || "—"}</span><small>{row.error || "Hợp lệ"}</small>
            </div>
          ))}
        </div>
      )}
      <div className="dialog-actions">
        <button type="button" className="btn btn-secondary" onClick={onClose}>Hủy</button>
        <button type="button" className="btn btn-primary" disabled={validRows.length === 0} onClick={() => { onApply(validRows); onClose(); }}>
          Áp dụng {validRows.length || ""} dòng
        </button>
      </div>
    </DialogFrame>
  );
}
