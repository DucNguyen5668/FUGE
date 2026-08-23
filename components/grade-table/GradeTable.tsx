"use client";
import { useState, useCallback, useRef } from "react";
import { toast } from "sonner";

interface GradeComponent { id: number; name: string; order: number; }
interface StudentRow {
  id: number; roll: string; name: string; comment: string | null;
  grades: Record<number, number | null>;
}

interface Props {
  components: GradeComponent[];
  students: StudentRow[];
  visibleComponents: Set<number>;
  onDataChange: () => void;
}

function gradeColor(v: number | null) {
  if (v === null) return "";
  if (v >= 8) return "grade-high";
  if (v >= 5) return "grade-mid";
  return "grade-low";
}

function EditableCell({
  value,
  isComment,
  onSave,
}: {
  value: string | null;
  isComment?: boolean;
  onSave: (val: string | null) => Promise<void>;
}) {
  const [editing, setEditing] = useState(false);
  const [local, setLocal] = useState(value ?? "");
  const [invalid, setInvalid] = useState(false);
  const inputRef = useRef<HTMLInputElement>(null);

  const validate = (v: string) => {
    if (isComment) return true;
    if (v === "") return true;
    const n = parseFloat(v);
    return !isNaN(n) && n >= 0 && n <= 10;
  };

  const commit = async () => {
    setEditing(false);
    if (!validate(local)) { setInvalid(true); toast.error("Điểm phải từ 0 đến 10"); return; }
    setInvalid(false);
    const normalized = local.trim() === "" ? null : local.trim();
    if (normalized === (value ?? "").trim() || (normalized === null && value === null)) return;
    await onSave(normalized);
  };

  if (editing) {
    return (
      <input
        ref={inputRef}
        autoFocus
        className={`cell-input ${invalid ? "invalid" : ""}`}
        value={local}
        onChange={(e) => { setLocal(e.target.value); setInvalid(false); }}
        onBlur={commit}
        onKeyDown={(e) => {
          if (e.key === "Enter") { e.preventDefault(); commit(); }
          if (e.key === "Escape") { setLocal(value ?? ""); setEditing(false); }
        }}
        style={{ minWidth: isComment ? 160 : 70 }}
      />
    );
  }

  const numVal = isComment ? null : parseFloat(value ?? "");
  return (
    <span
      className={`cursor-pointer block w-full px-1 rounded hover:bg-white/5 transition-colors ${
        !isComment && !isNaN(numVal!) ? `grade-val ${gradeColor(numVal!)}` : ""
      }`}
      style={{ minWidth: isComment ? 120 : 60, minHeight: 22 }}
      onDoubleClick={() => { setLocal(value ?? ""); setEditing(true); }}
      title="Double-click để sửa"
    >
      {value ?? <span style={{ color: "var(--text-muted)", fontSize: "0.75rem" }}>—</span>}
    </span>
  );
}

export default function GradeTable({ components, students, visibleComponents, onDataChange }: Props) {
  const [searchQuery, setSearchQuery] = useState("");
  const [selectedRow, setSelectedRow] = useState<number | null>(null);

  const filtered = students.filter((s) => {
    const q = searchQuery.trim().toUpperCase();
    if (!q) return true;
    return s.roll.toUpperCase().includes(q) || s.name.toUpperCase().includes(q);
  });

  const saveGrade = useCallback(async (studentId: number, componentId: number, val: string | null) => {
    const value = val === null ? null : parseFloat(val);
    const res = await fetch("/api/grades", {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ studentId, componentId, value }),
    });
    if (!res.ok) { toast.error("Lỗi lưu điểm"); return; }
    onDataChange();
  }, [onDataChange]);

  const saveComment = useCallback(async (studentId: number, val: string | null) => {
    await fetch("/api/grades", {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ studentId, componentId: -1, value: null, comment: val, mode: "comment", sId: studentId }),
    });
    // Update comment via students endpoint
    await fetch(`/api/students/${studentId}/comment`, {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ comment: val }),
    });
    onDataChange();
  }, [onDataChange]);

  const visibleComps = components.filter((c) => visibleComponents.has(c.id));

  return (
    <div className="flex flex-col h-full">
      {/* Search bar */}
      <div className="px-4 py-3 border-b border-[var(--border)] flex items-center gap-3">
        <div className="relative flex-1 max-w-xs">
          <svg className="absolute left-3 top-1/2 -translate-y-1/2 opacity-40" width="15" height="15" viewBox="0 0 24 24" fill="none">
            <circle cx="11" cy="11" r="8" stroke="white" strokeWidth="2"/>
            <path d="M21 21l-4.35-4.35" stroke="white" strokeWidth="2" strokeLinecap="round"/>
          </svg>
          <input
            className="input pl-9 text-sm"
            placeholder="Tìm MSSV hoặc tên..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            onKeyDown={(e) => { if (e.key === "Escape") setSearchQuery(""); }}
          />
        </div>
        <span className="text-xs" style={{ color: "var(--text-muted)" }}>
          {filtered.length} / {students.length} sinh viên
        </span>
      </div>

      {/* Table */}
      <div style={{ flex: 1, overflowY: "auto", overflowX: "auto" }}>
        <table className="grade-table">
          <thead>
            <tr>
              <th style={{ width: 44 }}>#</th>
              <th style={{ width: 110 }}>MSSV</th>
              <th style={{ minWidth: 160 }}>Họ tên</th>
              <th style={{ minWidth: 160 }}>Comment</th>
              {visibleComps.map((c) => (
                <th key={c.id} style={{ minWidth: 80, textAlign: "center" }}>{c.name}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {filtered.length === 0 && (
              <tr>
                <td colSpan={4 + visibleComps.length} className="text-center py-12" style={{ color: "var(--text-muted)" }}>
                  {searchQuery ? "Không tìm thấy sinh viên" : "Chưa có sinh viên"}
                </td>
              </tr>
            )}
            {filtered.map((stu, idx) => (
              <tr
                key={stu.id}
                className={selectedRow === stu.id ? "selected" : ""}
                onClick={() => setSelectedRow(stu.id)}
              >
                <td className="text-center" style={{ color: "var(--text-muted)", fontSize: "0.78rem" }}>
                  {idx + 1}
                </td>
                <td className="font-mono font-medium" style={{ fontSize: "0.82rem" }}>{stu.roll}</td>
                <td>{stu.name}</td>
                <td>
                  <EditableCell
                    value={stu.comment}
                    isComment
                    onSave={(v) => saveComment(stu.id, v)}
                  />
                </td>
                {visibleComps.map((c) => (
                  <td key={c.id} style={{ textAlign: "center" }}>
                    <EditableCell
                      value={stu.grades[c.id] !== null && stu.grades[c.id] !== undefined
                        ? String(stu.grades[c.id])
                        : null}
                      onSave={(v) => saveGrade(stu.id, c.id, v)}
                    />
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
