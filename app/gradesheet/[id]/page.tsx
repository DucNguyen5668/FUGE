"use client";
import { useState, useEffect, useCallback } from "react";
import { useParams, useRouter } from "next/navigation";
import { toast } from "sonner";
import GradeTable from "@/components/grade-table/GradeTable";
import ImportMarkModal from "@/components/modals/ImportMarkModal";
import AddStudentModal from "@/components/modals/AddStudentModal";
import AddComponentModal from "@/components/modals/AddComponentModal";

interface GradeComponent { id: number; name: string; order: number; }
interface StudentRow {
  id: number; roll: string; name: string; comment: string | null;
  grades: Record<number, number | null>;
}
interface ClassData {
  id: number; subject: string; className: string;
  components: GradeComponent[];
  students: StudentRow[];
}

type Modal =
  | { type: "import-mark"; componentId: number; componentName: string }
  | { type: "import-comment" }
  | { type: "add-student" }
  | { type: "add-component" }
  | null;

export default function GradeSheetPage() {
  const params = useParams();
  const router = useRouter();
  const gsId = params.id as string;

  const [classes, setClasses] = useState<ClassData[]>([]);
  const [selectedClassId, setSelectedClassId] = useState<number | null>(null);
  const [mergeMode, setMergeMode] = useState(false);
  const [visibleComponents, setVisibleComponents] = useState<Set<number>>(new Set());
  const [modal, setModal] = useState<Modal>(null);
  const [loading, setLoading] = useState(true);
  const [selectedComp, setSelectedComp] = useState<number | null>(null);

  const requestData = useCallback(async (): Promise<ClassData[] | null> => {
    const res = await fetch(`/api/gradesheets/${gsId}`);
    if (res.status === 401) { router.push("/login"); return null; }
    if (!res.ok) throw new Error("Failed to load gradesheet");
    return res.json();
  }, [gsId, router]);

  const refreshData = useCallback(async () => {
    const data = await requestData();
    if (!data) return;
    setClasses(data);
    setSelectedClassId((currentId) =>
      data.some((cls) => cls.id === currentId) ? currentId : (data[0]?.id ?? null)
    );
    setLoading(false);
  }, [requestData]);

  useEffect(() => {
    let cancelled = false;

    void requestData()
      .then((data) => {
        if (cancelled || !data) return;
        const firstClass = data[0];
        setClasses(data);
        setSelectedClassId(firstClass?.id ?? null);
        setVisibleComponents(new Set(firstClass?.components.map((c) => c.id) ?? []));
        setLoading(false);
      })
      .catch(() => {
        if (cancelled) return;
        toast.error("Không thể tải bảng điểm");
        setLoading(false);
      });

    return () => { cancelled = true; };
  }, [requestData]);

  // When class changes, reset visible components
  const handleClassChange = (classId: number) => {
    setSelectedClassId(classId);
    const cls = classes.find((c) => c.id === classId);
    if (cls) setVisibleComponents(new Set(cls.components.map((c) => c.id)));
  };

  // Merge mode: combine all students
  const currentClass = mergeMode
    ? {
        id: -1,
        subject: classes[0]?.subject ?? "",
        className: "Tất cả lớp",
        components: classes[0]?.components ?? [],
        students: classes.flatMap((c) => c.students),
      }
    : classes.find((c) => c.id === selectedClassId) ?? null;

  const canMerge = classes.length > 1 &&
    classes.every((c) =>
      c.components.map((x) => x.name).join(",") === classes[0].components.map((x) => x.name).join(",")
    );

  const toggleComponent = (id: number) => {
    setVisibleComponents((prev) => {
      const next = new Set(prev);
      if (next.has(id)) next.delete(id);
      else next.add(id);
      return next;
    });
  };

  const toggleAllComponents = () => {
    if (!currentClass) return;
    if (visibleComponents.size === currentClass.components.length) {
      setVisibleComponents(new Set());
    } else {
      setVisibleComponents(new Set(currentClass.components.map((c) => c.id)));
    }
  };

  const handleClearMark = async () => {
    if (!selectedComp) { toast.error("Chọn thành phần điểm trước"); return; }
    const comp = currentClass?.components.find((c) => c.id === selectedComp);
    if (!window.confirm(`Xóa toàn bộ điểm "${comp?.name}"?`)) return;
    const res = await fetch(`/api/grades?componentId=${selectedComp}`, { method: "DELETE" });
    if (res.ok) { toast.success("Đã xóa điểm"); await refreshData(); }
  };

  const handleExport = () => {
    if (!currentClass) return;
    const classId = mergeMode ? classes[0].id : (currentClass.id);
    window.open(`/api/export?gradesheetId=${gsId}&classId=${classId}`, "_blank");
  };

  if (loading) {
    return (
      <div className="flex items-center justify-center h-full min-h-screen">
        <div className="text-center">
          <div className="inline-block w-8 h-8 border-2 border-indigo-500 border-t-transparent rounded-full animate-spin mb-3" />
          <p style={{ color: "var(--text-muted)" }}>Đang tải dữ liệu...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-col h-screen overflow-hidden">
      {/* Top bar */}
      <header className="flex items-center gap-3 px-4 py-3 border-b border-[var(--border)] flex-shrink-0"
        style={{ background: "var(--bg-card)" }}>
        <button className="btn btn-secondary btn-sm" onClick={() => router.push("/")}>
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none">
            <path d="M19 12H5M12 19l-7-7 7-7" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
          </svg>
          Trang chủ
        </button>

        <div className="h-5 w-px" style={{ background: "var(--border)" }} />

        {/* Class selector */}
        <div className="flex items-center gap-2">
          <span className="text-xs" style={{ color: "var(--text-muted)" }}>Lớp:</span>
          {mergeMode ? (
            <span className="badge badge-accent">Tất cả lớp</span>
          ) : (
            <select
              className="input text-sm"
              style={{ width: "auto", padding: "4px 8px" }}
              value={selectedClassId ?? ""}
              onChange={(e) => handleClassChange(Number(e.target.value))}
            >
              {classes.map((c) => (
                <option key={c.id} value={c.id}>{c.subject}/{c.className}</option>
              ))}
            </select>
          )}
        </div>

        {canMerge && (
          <button
            className={`btn btn-sm ${mergeMode ? "btn-primary" : "btn-secondary"}`}
            onClick={() => setMergeMode((v) => !v)}
          >
            {mergeMode ? "Tách lớp" : "Gộp lớp"}
          </button>
        )}

        <div className="ml-auto flex items-center gap-2">
          {/* Import comments */}
          <button className="btn btn-secondary btn-sm" onClick={() => setModal({ type: "import-comment" })}>
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none">
              <path d="M21 15a2 2 0 01-2 2H7l-4 4V5a2 2 0 012-2h14a2 2 0 012 2z" stroke="currentColor" strokeWidth="2"/>
            </svg>
            Import Comments
          </button>

          {/* Export */}
          <button className="btn btn-secondary btn-sm" onClick={handleExport}>
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none">
              <path d="M21 15v4a2 2 0 01-2 2H5a2 2 0 01-2-2v-4M7 10l5 5 5-5M12 15V3" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
            </svg>
            Export Excel
          </button>
        </div>
      </header>

      {/* Main content */}
      <div className="flex flex-1 overflow-hidden">
        {/* LEFT sidebar: components + actions */}
        <aside className="w-56 border-r border-[var(--border)] flex flex-col overflow-y-auto flex-shrink-0"
          style={{ background: "var(--bg-card)" }}>
          {/* Components section */}
          <div className="p-3 border-b border-[var(--border)]">
            <div className="flex items-center justify-between mb-2">
              <span className="text-xs font-semibold uppercase tracking-wide" style={{ color: "var(--text-muted)" }}>
                Thành phần điểm ({currentClass?.components.length ?? 0})
              </span>
              <button className="text-xs hover:underline" style={{ color: "var(--accent)" }} onClick={toggleAllComponents}>
                {visibleComponents.size === currentClass?.components.length ? "Bỏ chọn" : "Chọn tất"}
              </button>
            </div>
            <div className="space-y-1">
              {currentClass?.components.map((c) => (
                <label key={c.id}
                  className={`flex items-center gap-2 px-2 py-1.5 rounded-lg cursor-pointer transition-all text-sm ${
                    selectedComp === c.id ? "ring-1 ring-indigo-500" : ""
                  }`}
                  style={{ background: visibleComponents.has(c.id) ? "var(--accent-light)" : "transparent" }}
                  onClick={() => setSelectedComp(c.id)}
                >
                  <input
                    type="checkbox"
                    checked={visibleComponents.has(c.id)}
                    onChange={() => toggleComponent(c.id)}
                    onClick={(e) => e.stopPropagation()}
                    className="rounded accent-indigo-500"
                  />
                  <span className={visibleComponents.has(c.id) ? "text-indigo-300" : ""} style={{ color: visibleComponents.has(c.id) ? undefined : "var(--text-muted)" }}>
                    {c.name}
                  </span>
                </label>
              ))}
            </div>
          </div>

          {/* Action buttons */}
          <div className="p-3 space-y-2">
            <p className="text-xs font-semibold uppercase tracking-wide mb-2" style={{ color: "var(--text-muted)" }}>Thao tác</p>

            {!mergeMode && (
              <>
                {selectedComp && (
                  <button
                    className="btn btn-secondary btn-sm w-full justify-start"
                    onClick={() => {
                      const comp = currentClass?.components.find((c) => c.id === selectedComp);
                      if (comp) setModal({ type: "import-mark", componentId: comp.id, componentName: comp.name });
                    }}
                  >
                    <svg width="12" height="12" viewBox="0 0 24 24" fill="none">
                      <path d="M12 5v14M5 12l7-7 7 7" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                    </svg>
                    Import điểm: {currentClass?.components.find((c) => c.id === selectedComp)?.name}
                  </button>
                )}

                {selectedComp && (
                  <button className="btn btn-danger btn-sm w-full justify-start" onClick={handleClearMark}>
                    <svg width="12" height="12" viewBox="0 0 24 24" fill="none">
                      <path d="M3 6h18M8 6V4h8v2M19 6l-1 14H6L5 6" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                    </svg>
                    Xóa điểm
                  </button>
                )}

                <div className="border-t border-[var(--border)] pt-2 space-y-2">
                  <button className="btn btn-secondary btn-sm w-full justify-start"
                    onClick={() => setModal({ type: "add-student" })}>
                    <svg width="12" height="12" viewBox="0 0 24 24" fill="none">
                      <path d="M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2" stroke="currentColor" strokeWidth="2"/>
                      <circle cx="12" cy="7" r="4" stroke="currentColor" strokeWidth="2"/>
                      <path d="M16 11h6M19 8v6" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                    </svg>
                    Thêm sinh viên
                  </button>

                  <button className="btn btn-secondary btn-sm w-full justify-start"
                    onClick={() => setModal({ type: "add-component" })}>
                    <svg width="12" height="12" viewBox="0 0 24 24" fill="none">
                      <rect x="3" y="3" width="18" height="18" rx="2" stroke="currentColor" strokeWidth="2"/>
                      <path d="M12 8v8M8 12h8" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                    </svg>
                    Thêm thành phần
                  </button>
                </div>
              </>
            )}
          </div>

          {/* Stats */}
          <div className="mt-auto p-3 border-t border-[var(--border)]">
            <div className="text-xs space-y-1" style={{ color: "var(--text-muted)" }}>
              <div className="flex justify-between">
                <span>Sinh viên:</span>
                <span className="text-white font-medium">{currentClass?.students.length ?? 0}</span>
              </div>
              <div className="flex justify-between">
                <span>Thành phần:</span>
                <span className="text-white font-medium">{currentClass?.components.length ?? 0}</span>
              </div>
            </div>
          </div>
        </aside>

        {/* RIGHT: grade table */}
        <main className="flex-1 overflow-hidden flex flex-col">
          {currentClass ? (
            <>
              <div className="px-4 py-2 border-b border-[var(--border)] flex items-center gap-3"
                style={{ background: "var(--bg-card)" }}>
                <span className="font-semibold text-white">{currentClass.subject}/{currentClass.className}</span>
                <span className="badge badge-accent">{currentClass.students.length} SV</span>
                <span className="text-xs" style={{ color: "var(--text-muted)" }}>
                  Double-click vào ô để sửa • Enter để lưu • Esc để hủy
                </span>
              </div>
              <GradeTable
                components={currentClass.components}
                students={currentClass.students}
                visibleComponents={visibleComponents}
                onDataChange={refreshData}
              />
            </>
          ) : (
            <div className="flex items-center justify-center h-full" style={{ color: "var(--text-muted)" }}>
              Chọn lớp để xem bảng điểm
            </div>
          )}
        </main>
      </div>

      {/* Modals */}
      {modal?.type === "import-mark" && (
        <ImportMarkModal
          classId={currentClass?.id ?? 0}
          componentId={modal.componentId}
          componentName={modal.componentName}
          mode="mark"
          onSuccess={refreshData}
          onClose={() => setModal(null)}
        />
      )}
      {modal?.type === "import-comment" && (
        <ImportMarkModal
          classId={currentClass?.id ?? 0}
          mode="comment"
          onSuccess={refreshData}
          onClose={() => setModal(null)}
        />
      )}
      {modal?.type === "add-student" && !mergeMode && (
        <AddStudentModal
          classId={currentClass?.id ?? 0}
          onSuccess={refreshData}
          onClose={() => setModal(null)}
        />
      )}
      {modal?.type === "add-component" && !mergeMode && (
        <AddComponentModal
          classId={currentClass?.id ?? 0}
          onSuccess={refreshData}
          onClose={() => setModal(null)}
        />
      )}
    </div>
  );
}
