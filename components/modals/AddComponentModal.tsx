"use client";
import { useState } from "react";
import { toast } from "sonner";

interface Props {
  classId: number;
  onSuccess: (comp: { id: number; name: string; order: number }) => void;
  onClose: () => void;
}

export default function AddComponentModal({ classId, onSuccess, onClose }: Props) {
  const [name, setName] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim()) return;
    setLoading(true);
    try {
      const res = await fetch("/api/components", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ classId, name: name.trim() }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error);
      toast.success(`Đã thêm thành phần "${name}"`);
      onSuccess(data);
      onClose();
    } catch (err: unknown) {
      toast.error(err instanceof Error ? err.message : "Lỗi thêm thành phần");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-box p-6 max-w-sm" onClick={(e) => e.stopPropagation()}>
        <div className="flex items-center justify-between mb-5">
          <h2 className="text-lg font-semibold">Thêm thành phần điểm</h2>
          <button onClick={onClose} className="text-gray-400 hover:text-white text-xl">×</button>
        </div>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-xs font-medium mb-2" style={{ color: "var(--text-muted)" }}>TÊN THÀNH PHẦN *</label>
            <input
              className="input"
              placeholder="TE, TE Rest, PE..."
              value={name}
              onChange={(e) => setName(e.target.value)}
              required
              autoFocus
            />
          </div>
          <div className="flex gap-3 pt-2">
            <button type="button" className="btn btn-secondary flex-1" onClick={onClose}>Hủy</button>
            <button type="submit" className="btn btn-primary flex-1" disabled={loading || !name.trim()}>
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
