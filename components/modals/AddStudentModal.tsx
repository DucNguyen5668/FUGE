"use client";
import { useState } from "react";
import { toast } from "sonner";

interface Props {
  classId: number;
  onSuccess: (student: { id: number; roll: string; name: string }) => void;
  onClose: () => void;
}

export default function AddStudentModal({ classId, onSuccess, onClose }: Props) {
  const [roll, setRoll] = useState("");
  const [name, setName] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!roll.trim()) return;
    setLoading(true);
    try {
      const res = await fetch("/api/students", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ classId, roll: roll.trim().toUpperCase(), name: name.trim() }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error);
      toast.success(`Đã thêm sinh viên ${roll.toUpperCase()}`);
      onSuccess(data);
      onClose();
    } catch (err: unknown) {
      toast.error(err instanceof Error ? err.message : "Lỗi thêm sinh viên");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-box p-6 max-w-sm" onClick={(e) => e.stopPropagation()}>
        <div className="flex items-center justify-between mb-5">
          <h2 className="text-lg font-semibold">Thêm sinh viên</h2>
          <button onClick={onClose} className="text-gray-400 hover:text-white text-xl">×</button>
        </div>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-xs font-medium mb-2" style={{ color: "var(--text-muted)" }}>MÃ SINH VIÊN (MSSV) *</label>
            <input
              className="input font-mono"
              placeholder="HE123456"
              value={roll}
              onChange={(e) => setRoll(e.target.value)}
              required
              autoFocus
            />
          </div>
          <div>
            <label className="block text-xs font-medium mb-2" style={{ color: "var(--text-muted)" }}>HỌ TÊN</label>
            <input
              className="input"
              placeholder="Nguyễn Văn A"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </div>
          <div className="flex gap-3 pt-2">
            <button type="button" className="btn btn-secondary flex-1" onClick={onClose}>Hủy</button>
            <button type="submit" className="btn btn-primary flex-1" disabled={loading || !roll.trim()}>
              {loading
                ? <span className="inline-block w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin" />
                : "Thêm"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
