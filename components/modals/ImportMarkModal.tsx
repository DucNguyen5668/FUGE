"use client";
import { useState } from "react";
import { toast } from "sonner";

interface Props {
  classId: number;
  componentId?: number;
  componentName?: string;
  mode: "mark" | "comment";
  onSuccess: () => void;
  onClose: () => void;
}

function parseRows(text: string, excludeFirst: boolean) {
  const lines = text.split(/\r?\n/).filter((l) => l.trim());
  const start = excludeFirst ? 1 : 0;
  return lines.slice(start).map((line) => {
    const parts = line.trim().split(/[\t ]+/);
    const roll = parts[0] ?? "";
    const value = parts.slice(1).join(" ");
    return { roll, value };
  });
}

export default function ImportMarkModal({ classId, componentId, componentName, mode, onSuccess, onClose }: Props) {
  const [text, setText] = useState("");
  const [excludeFirst, setExcludeFirst] = useState(false);
  const [preview, setPreview] = useState<{ roll: string; value: string; error?: string }[]>([]);
  const [loading, setLoading] = useState(false);

  const handlePreview = () => {
    const rows = parseRows(text, excludeFirst);
    const seen = new Set<string>();
    const result = rows.map((r) => {
      let error: string | undefined;
      if (!r.roll) { error = "Thiếu MSSV"; }
      else if (seen.has(r.roll.toUpperCase())) { error = "MSSV trùng"; }
      else if (mode === "mark") {
        const v = parseFloat(r.value);
        if (isNaN(v) || v < 0 || v > 10) error = "Điểm không hợp lệ (0–10)";
      } else if (!r.value) {
        error = "Thiếu comment";
      }
      seen.add(r.roll.toUpperCase());
      return { ...r, error };
    });
    setPreview(result);
  };

  const hasError = preview.some((r) => r.error);

  const handleImport = async () => {
    const rows = parseRows(text, excludeFirst);
    setLoading(true);
    try {
      const res = await fetch("/api/grades", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ classId, componentId, rows, mode }),
      });
      const data = await res.json();
      const okCount = data.results?.filter((r: { status: string }) => r.status === "ok").length ?? 0;
      const notFound = data.results?.filter((r: { status: string }) => r.status === "not_found").length ?? 0;
      toast.success(`Đã import ${okCount} dòng${notFound > 0 ? `, không tìm thấy ${notFound} MSSV` : ""}`);
      onSuccess();
      onClose();
    } catch {
      toast.error("Lỗi import");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-box p-6 max-w-xl" onClick={(e) => e.stopPropagation()}>
        <div className="flex items-center justify-between mb-4">
          <div>
            <h2 className="text-lg font-semibold">
              {mode === "mark" ? `Import điểm: ${componentName}` : "Import Comments"}
            </h2>
            <p className="text-xs mt-0.5" style={{ color: "var(--text-muted)" }}>
              {mode === "mark"
                ? "Mỗi dòng: MSSV<tab hoặc khoảng trắng>điểm (0–10)"
                : "Mỗi dòng: MSSV<tab hoặc khoảng trắng>nội dung comment"}
            </p>
          </div>
          <button onClick={onClose} className="text-gray-400 hover:text-white text-xl">×</button>
        </div>

        <textarea
          className="input font-mono text-sm mb-3"
          style={{ height: 160, resize: "vertical" }}
          placeholder={mode === "mark"
            ? "HE180186\t8.5\nHE180202\t7.0\nHE180290\t9.5"
            : "HE180186\tGreat work\nHE180202\tNeeds improvement"}
          value={text}
          onChange={(e) => { setText(e.target.value); setPreview([]); }}
        />

        <div className="flex items-center gap-3 mb-4">
          <label className="flex items-center gap-2 text-sm cursor-pointer" style={{ color: "var(--text-muted)" }}>
            <input
              type="checkbox"
              checked={excludeFirst}
              onChange={(e) => setExcludeFirst(e.target.checked)}
              className="rounded"
            />
            Bỏ qua dòng tiêu đề đầu tiên
          </label>
          <button className="btn btn-secondary btn-sm ml-auto" onClick={handlePreview} disabled={!text.trim()}>
            Kiểm tra
          </button>
        </div>

        {/* Preview */}
        {preview.length > 0 && (
          <div className="mb-4 rounded-lg overflow-hidden border border-[var(--border)]">
            <div className="px-3 py-2 text-xs font-medium" style={{ background: "var(--bg-input)", color: "var(--text-muted)" }}>
              Kết quả kiểm tra ({preview.filter((r) => !r.error).length}/{preview.length} hợp lệ)
            </div>
            <div style={{ maxHeight: 160, overflowY: "auto" }}>
              {preview.map((r, i) => (
                <div key={i} className="flex items-center gap-3 px-3 py-1.5 text-sm border-b border-[var(--border)] last:border-0"
                  style={{ background: r.error ? "var(--danger-light)" : undefined }}>
                  <span className="font-mono font-medium w-24 flex-shrink-0">{r.roll}</span>
                  <span className="flex-1" style={{ color: "var(--text-muted)" }}>{r.value}</span>
                  {r.error
                    ? <span className="text-red-400 text-xs">{r.error}</span>
                    : <span className="text-green-400 text-xs">✓</span>}
                </div>
              ))}
            </div>
          </div>
        )}

        <div className="flex gap-3">
          <button className="btn btn-secondary flex-1" onClick={onClose}>Hủy</button>
          <button
            className="btn btn-primary flex-1"
            disabled={!text.trim() || loading || hasError}
            onClick={handleImport}
          >
            {loading
              ? <span className="inline-block w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin" />
              : "Import"}
          </button>
        </div>
      </div>
    </div>
  );
}
