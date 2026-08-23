"use client";

import { useRef, useState } from "react";
import { toast } from "sonner";
import type { FgSubjectClassGrade } from "@/lib/fg-types";

interface Props {
  classData: FgSubjectClassGrade;
  visibleComponents: Set<string>;
  searchQuery: string;
  onGradeChange: (studentIndex: number, component: string, value: number | null) => void;
  onCommentChange: (studentIndex: number, value: string | null) => void;
}

function gradeTone(value: number | null) {
  if (value === null) return "";
  if (value >= 8) return "grade-high";
  if (value >= 5) return "grade-mid";
  return "grade-low";
}

function EditableCell({
  value,
  comment,
  onSave,
}: {
  value: string | null;
  comment?: boolean;
  onSave: (value: string | null) => void;
}) {
  const [editing, setEditing] = useState(false);
  const [localValue, setLocalValue] = useState(value ?? "");
  const cancelled = useRef(false);

  const commit = () => {
    if (cancelled.current) {
      cancelled.current = false;
      return;
    }
    const normalized = localValue.trim();
    if (!comment && normalized) {
      const numberValue = Number(normalized);
      if (!Number.isFinite(numberValue) || numberValue < 0 || numberValue > 10) {
        toast.error("Điểm phải nằm trong khoảng 0–10");
        setLocalValue(value ?? "");
        setEditing(false);
        return;
      }
    }
    setEditing(false);
    onSave(normalized || null);
  };

  if (editing) {
    return (
      <input
        autoFocus
        className={`cell-input ${comment ? "comment-input" : "grade-input"}`}
        inputMode={comment ? "text" : "decimal"}
        value={localValue}
        onChange={(event) => setLocalValue(event.target.value)}
        onBlur={commit}
        onKeyDown={(event) => {
          if (event.key === "Enter") event.currentTarget.blur();
          if (event.key === "Escape") {
            cancelled.current = true;
            setLocalValue(value ?? "");
            setEditing(false);
          }
        }}
        aria-label={comment ? "Sửa nhận xét" : "Sửa điểm"}
      />
    );
  }

  const numericValue = comment || value === null ? null : Number(value);
  return (
    <button
      type="button"
      className={`cell-display ${numericValue !== null ? gradeTone(numericValue) : ""}`}
      onDoubleClick={() => {
        setLocalValue(value ?? "");
        setEditing(true);
      }}
      title="Nhấp đúp để sửa"
    >
      {value ?? <span className="empty-value">—</span>}
    </button>
  );
}

export default function DraftGradeTable({
  classData,
  visibleComponents,
  searchQuery,
  onGradeChange,
  onCommentChange,
}: Props) {
  const normalizedQuery = searchQuery.trim().toLocaleUpperCase("vi-VN");
  const rows = classData.Students
    .map((student, originalIndex) => ({ student, originalIndex }))
    .filter(({ student }) => {
      if (!normalizedQuery) return true;
      return (
        student.Roll.toLocaleUpperCase("vi-VN").includes(normalizedQuery) ||
        student.Name.toLocaleUpperCase("vi-VN").includes(normalizedQuery)
      );
    });
  const components = classData.Components.filter((component) => visibleComponents.has(component));

  return (
    <div className="grade-grid-scroll">
      <table className="grade-table" style={{ minWidth: 650 + components.length * 132 }}>
        <thead>
          <tr>
            <th className="row-number">#</th>
            <th>MSSV</th>
            <th>Họ tên</th>
            <th>Nhận xét</th>
            {components.map((component) => <th key={component}>{component}</th>)}
          </tr>
        </thead>
        <tbody>
          {rows.length === 0 && (
            <tr>
              <td colSpan={4 + components.length} className="empty-table">
                {searchQuery ? "Không tìm thấy sinh viên phù hợp" : "Lớp chưa có sinh viên"}
              </td>
            </tr>
          )}
          {rows.map(({ student, originalIndex }, displayIndex) => (
            <tr key={`${student.Roll}-${originalIndex}`}>
              <td className="row-number">{displayIndex + 1}</td>
              <td className="student-roll">{student.Roll}</td>
              <td className="student-name">{student.Name || "—"}</td>
              <td className="comment-cell">
                <EditableCell
                  key={`comment-${student.Comment ?? ""}`}
                  value={student.Comment}
                  comment
                  onSave={(value) => onCommentChange(originalIndex, value)}
                />
              </td>
              {components.map((component) => {
                const value = student.Grades.find((grade) => grade.Component === component)?.Grade ?? null;
                return (
                  <td key={component} className="grade-cell">
                    <EditableCell
                      key={`${component}-${value ?? "empty"}`}
                      value={value === null ? null : String(value)}
                      onSave={(nextValue) =>
                        onGradeChange(
                          originalIndex,
                          component,
                          nextValue === null ? null : Number(nextValue)
                        )
                      }
                    />
                  </td>
                );
              })}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
