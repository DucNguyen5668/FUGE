"use client";
import { useState, useCallback } from "react";
import { toast } from "sonner";

interface Props {
  onSuccess: (gradesheetId: number) => void;
  onClose: () => void;
}

export default function UploadFgModal({ onSuccess, onClose }: Props) {
  const [dragging, setDragging] = useState(false);
  const [file, setFile] = useState<File | null>(null);
  const [loading, setLoading] = useState(false);

  const handleDrop = useCallback((e: React.DragEvent) => {
    e.preventDefault();
    setDragging(false);
    const f = e.dataTransfer.files[0];
    if (f && f.name.endsWith(".fg")) setFile(f);
    else toast.error("Vui lòng chọn file .fg");
  }, []);

  const handleFile = (e: React.ChangeEvent<HTMLInputElement>) => {
    const f = e.target.files?.[0];
    if (f) setFile(f);
  };

  const handleImport = async () => {
    if (!file) return;
    setLoading(true);
    const form = new FormData();
    form.append("file", file);
    try {
      const res = await fetch("/api/fg/import", { method: "POST", body: form });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error);
      toast.success("Đã import thành công!");
      onSuccess(data.gradesheetId);
    } catch (err: unknown) {
      toast.error(err instanceof Error ? err.message : "Lỗi import file");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-box p-6" onClick={(e) => e.stopPropagation()}>
        <div className="flex items-center justify-between mb-5">
          <h2 className="text-lg font-semibold">Mở file bảng điểm (.fg)</h2>
          <button onClick={onClose} className="text-gray-400 hover:text-white text-xl leading-none">×</button>
        </div>

        {/* Drop zone */}
        <div
          className={`border-2 border-dashed rounded-xl p-8 text-center transition-all cursor-pointer mb-4 ${
            dragging ? "border-indigo-500 bg-indigo-500/10" : "border-[var(--border)] hover:border-indigo-400"
          }`}
          onDragOver={(e) => { e.preventDefault(); setDragging(true); }}
          onDragLeave={() => setDragging(false)}
          onDrop={handleDrop}
          onClick={() => document.getElementById("fg-file-input")?.click()}
        >
          <svg className="mx-auto mb-3" width="40" height="40" viewBox="0 0 24 24" fill="none">
            <path d="M14 2H6a2 2 0 00-2 2v16a2 2 0 002 2h12a2 2 0 002-2V8l-6-6z" stroke="var(--accent)" strokeWidth="1.5"/>
            <path d="M14 2v6h6M12 12v6M9 15l3-3 3 3" stroke="var(--accent)" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"/>
          </svg>
          {file ? (
            <div>
              <p className="font-medium text-white">{file.name}</p>
              <p className="text-xs mt-1" style={{ color: "var(--text-muted)" }}>
                {(file.size / 1024).toFixed(1)} KB
              </p>
            </div>
          ) : (
            <>
              <p className="font-medium text-white">Kéo thả hoặc nhấn để chọn file</p>
              <p className="text-xs mt-1" style={{ color: "var(--text-muted)" }}>Chấp nhận file .fg</p>
            </>
          )}
          <input
            id="fg-file-input"
            type="file"
            accept=".fg"
            className="hidden"
            onChange={handleFile}
          />
        </div>

        <div className="flex gap-3">
          <button className="btn btn-secondary flex-1" onClick={onClose}>Hủy</button>
          <button
            className="btn btn-primary flex-1"
            disabled={!file || loading}
            onClick={handleImport}
          >
            {loading
              ? <span className="inline-block w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin" />
              : "Import & Mở"}
          </button>
        </div>
      </div>
    </div>
  );
}
