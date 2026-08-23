"use client";

import { useRef, useState } from "react";
import { toast } from "sonner";
import type { FgTeacherGrade } from "@/lib/fg-types";

export function OpenFgDialog({
  onLoaded,
  onClose,
}: {
  onLoaded: (payload: { data: FgTeacherGrade; filename: string }) => void;
  onClose: () => void;
}) {
  const inputRef = useRef<HTMLInputElement>(null);
  const [file, setFile] = useState<File | null>(null);
  const [password, setPassword] = useState("");
  const [passwordRequired, setPasswordRequired] = useState(false);
  const [dragging, setDragging] = useState(false);
  const [loading, setLoading] = useState(false);

  const chooseFile = (nextFile: File | undefined) => {
    if (!nextFile) return;
    if (!nextFile.name.toLowerCase().endsWith(".fg")) {
      toast.error("Vui lòng chọn đúng file .fg");
      return;
    }
    setFile(nextFile);
    setPassword("");
    setPasswordRequired(false);
  };

  const openFile = async () => {
    if (!file) return;
    setLoading(true);
    try {
      const form = new FormData();
      form.append("file", file);
      if (password) form.append("password", password);
      const response = await fetch("/api/fg/import", { method: "POST", body: form });
      const result = await response.json();

      if (result.code === "PASSWORD_REQUIRED") {
        setPasswordRequired(true);
        toast.info("File này có mật khẩu. Hãy nhập mật khẩu để mở.");
        return;
      }
      if (result.code === "INVALID_PASSWORD") {
        setPasswordRequired(true);
        toast.error("Mật khẩu file không đúng");
        return;
      }
      if (!response.ok) throw new Error(result.error || "Không thể mở file");

      onLoaded({ data: result.data, filename: result.filename });
      toast.success("Đã mở file. Bạn có thể chỉnh sửa mà chưa cần đăng nhập.");
      onClose();
    } catch (error) {
      toast.error(error instanceof Error ? error.message : "Không thể mở file .fg");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay" role="presentation" onMouseDown={onClose}>
      <section className="modal-box file-dialog" role="dialog" aria-modal="true" onMouseDown={(event) => event.stopPropagation()}>
        <div className="dialog-header">
          <div>
            <span className="eyebrow">MỞ BẢNG ĐIỂM</span>
            <h2>Chọn file FuGrade</h2>
          </div>
          <button type="button" className="icon-button" onClick={onClose} aria-label="Đóng">×</button>
        </div>

        <button
          type="button"
          className={`file-dropzone ${dragging ? "dragging" : ""}`}
          onClick={() => inputRef.current?.click()}
          onDragOver={(event) => { event.preventDefault(); setDragging(true); }}
          onDragLeave={() => setDragging(false)}
          onDrop={(event) => {
            event.preventDefault();
            setDragging(false);
            chooseFile(event.dataTransfer.files[0]);
          }}
        >
          <span className="file-icon">FG</span>
          <strong>{file?.name ?? "Kéo thả file .fg vào đây"}</strong>
          <span>{file ? `${(file.size / 1024).toFixed(1)} KB` : "hoặc nhấn để chọn từ máy tính"}</span>
        </button>
        <input
          ref={inputRef}
          type="file"
          accept=".fg"
          hidden
          onChange={(event) => chooseFile(event.target.files?.[0])}
        />

        {passwordRequired && (
          <label className="field-group">
            <span>Mật khẩu file</span>
            <input
              className="input"
              type="password"
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              placeholder="Nhập mật khẩu đã đặt trong FuGrade"
              autoFocus
            />
          </label>
        )}

        <div className="dialog-actions">
          <button type="button" className="btn btn-secondary" onClick={onClose}>Hủy</button>
          <button
            type="button"
            className="btn btn-primary"
            disabled={!file || loading || (passwordRequired && !password)}
            onClick={openFile}
          >
            {loading ? "Đang mở…" : "Mở file"}
          </button>
        </div>
      </section>
    </div>
  );
}

export function ExportFgDialog({
  data,
  filename,
  onClose,
}: {
  data: FgTeacherGrade;
  filename: string;
  onClose: () => void;
}) {
  const [outputName, setOutputName] = useState(filename || "fugrade-export.fg");
  const [password, setPassword] = useState("");
  const [confirmation, setConfirmation] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);

  const exportFile = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!password) return toast.error("Mật khẩu không được để trống");
    if (password !== confirmation) return toast.error("Hai mật khẩu chưa khớp");

    setLoading(true);
    try {
      const response = await fetch("/api/fg/export", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ data, filename: outputName, password }),
      });
      if (!response.ok) {
        const result = await response.json();
        throw new Error(result.error || "Không thể xuất file");
      }

      const blob = await response.blob();
      const url = URL.createObjectURL(blob);
      const anchor = document.createElement("a");
      anchor.href = url;
      anchor.download = outputName.toLowerCase().endsWith(".fg") ? outputName : `${outputName}.fg`;
      document.body.appendChild(anchor);
      anchor.click();
      anchor.remove();
      URL.revokeObjectURL(url);
      toast.success("Đã xuất file .fg có mật khẩu");
      onClose();
    } catch (error) {
      toast.error(error instanceof Error ? error.message : "Không thể xuất file");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay" role="presentation" onMouseDown={onClose}>
      <form className="modal-box file-dialog" onSubmit={exportFile} onMouseDown={(event) => event.stopPropagation()}>
        <div className="dialog-header">
          <div>
            <span className="eyebrow">XUẤT FILE TƯƠNG THÍCH FUGRADE</span>
            <h2>Đặt mật khẩu cho file .fg</h2>
          </div>
          <button type="button" className="icon-button" onClick={onClose} aria-label="Đóng">×</button>
        </div>

        <label className="field-group">
          <span>Tên file</span>
          <input className="input" value={outputName} onChange={(event) => setOutputName(event.target.value)} required />
        </label>
        <label className="field-group">
          <span>Mật khẩu</span>
          <input
            className="input"
            type={showPassword ? "text" : "password"}
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            required
            autoFocus
          />
        </label>
        <label className="field-group">
          <span>Nhập lại mật khẩu</span>
          <input
            className="input"
            type={showPassword ? "text" : "password"}
            value={confirmation}
            onChange={(event) => setConfirmation(event.target.value)}
            required
          />
        </label>
        <label className="check-row">
          <input type="checkbox" checked={showPassword} onChange={(event) => setShowPassword(event.target.checked)} />
          Hiện mật khẩu
        </label>
        <p className="dialog-note">Mật khẩu được băm MD5 và nội dung được mã hóa AES giống bản FuGrade desktop.</p>

        <div className="dialog-actions">
          <button type="button" className="btn btn-secondary" onClick={onClose}>Hủy</button>
          <button type="submit" className="btn btn-primary" disabled={loading}>
            {loading ? "Đang xuất…" : "Tải file .fg"}
          </button>
        </div>
      </form>
    </div>
  );
}
