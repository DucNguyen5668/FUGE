"use client";

import Link from "next/link";
import Image from "next/image";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { signOut } from "next-auth/react";
import { toast } from "sonner";
import DraftGradeTable from "@/components/workspace/DraftGradeTable";
import { ExportFgDialog, OpenFgDialog } from "@/components/workspace/FgFileDialogs";
import {
  AddComponentDialog,
  AddStudentDialog,
  ImportRowsDialog,
} from "@/components/workspace/WorkspaceDialogs";
import type { FgSubjectClassGrade, FgWorkspace } from "@/lib/fg-types";
import logoImage from "@/img/Logo.jpg";

const WORKSPACE_KEY = "fugrade_guest_workspace_v1";

type SessionUser = {
  name?: string | null;
  email?: string | null;
};

type ActiveDialog =
  | "open"
  | "export"
  | "add-student"
  | "add-component"
  | "import-grade"
  | "import-comment"
  | null;

function classLabel(item: FgSubjectClassGrade) {
  return [item.Subject, item.Class].filter(Boolean).join("/") || "Lớp chưa đặt tên";
}

export default function HomePage() {
  const router = useRouter();
  const [workspace, setWorkspace] = useState<FgWorkspace | null>(null);
  const [selectedClassIndex, setSelectedClassIndex] = useState(0);
  const [visibleComponents, setVisibleComponents] = useState<Set<string>>(new Set());
  const [selectedComponent, setSelectedComponent] = useState<string | null>(null);
  const [searchQuery, setSearchQuery] = useState("");
  const [dialog, setDialog] = useState<ActiveDialog>(null);
  const [saving, setSaving] = useState(false);
  const [sessionUser, setSessionUser] = useState<SessionUser | null>(null);
  const [sessionChecked, setSessionChecked] = useState(false);

  useEffect(() => {
    let cancelled = false;
    const stored = sessionStorage.getItem(WORKSPACE_KEY);
    if (stored) {
      Promise.resolve().then(() => {
        if (cancelled) return;
        try {
          const restored = JSON.parse(stored) as FgWorkspace;
          const firstClass = restored.data.SubjectClassGrades[0];
          setWorkspace(restored);
          setVisibleComponents(new Set(firstClass?.Components ?? []));
          setSelectedComponent(firstClass?.Components[0] ?? null);
        } catch {
          sessionStorage.removeItem(WORKSPACE_KEY);
        }
      });
    }

    void fetch("/api/auth/session", {
      cache: "no-store",
      credentials: "same-origin",
    })
      .then((response) => response.ok ? response.json() : null)
      .then((session) => {
        if (!cancelled) setSessionUser(session?.user ?? null);
      })
      .catch(() => {
        if (!cancelled) setSessionUser(null);
      })
      .finally(() => {
        if (!cancelled) setSessionChecked(true);
      });

    return () => { cancelled = true; };
  }, []);

  useEffect(() => {
    if (workspace) sessionStorage.setItem(WORKSPACE_KEY, JSON.stringify(workspace));
    else sessionStorage.removeItem(WORKSPACE_KEY);
  }, [workspace]);

  const currentClass = workspace?.data.SubjectClassGrades[selectedClassIndex] ?? null;

  const replaceCurrentClass = (updater: (value: FgSubjectClassGrade) => FgSubjectClassGrade) => {
    setWorkspace((current) => {
      if (!current) return current;
      const nextClasses = current.data.SubjectClassGrades.map((item, index) =>
        index === selectedClassIndex ? updater(item) : item
      );
      return {
        ...current,
        dirty: true,
        data: { ...current.data, SubjectClassGrades: nextClasses },
      };
    });
  };

  const handleLoaded = ({ data, filename }: { data: FgWorkspace["data"]; filename: string }) => {
    const next: FgWorkspace = { data, filename, dirty: false };
    const firstClass = data.SubjectClassGrades[0];
    setWorkspace(next);
    setSelectedClassIndex(0);
    setVisibleComponents(new Set(firstClass?.Components ?? []));
    setSelectedComponent(firstClass?.Components[0] ?? null);
    setSearchQuery("");
  };

  const handleClassChange = (index: number) => {
    const nextClass = workspace?.data.SubjectClassGrades[index];
    setSelectedClassIndex(index);
    setVisibleComponents(new Set(nextClass?.Components ?? []));
    setSelectedComponent(nextClass?.Components[0] ?? null);
    setSearchQuery("");
  };

  const updateMetadata = (field: "Login" | "Semester", value: string) => {
    setWorkspace((current) => current ? {
      ...current,
      dirty: true,
      data: { ...current.data, [field]: value },
    } : current);
  };

  const updateGrade = (studentIndex: number, component: string, value: number | null) => {
    replaceCurrentClass((sourceClass) => ({
      ...sourceClass,
      Students: sourceClass.Students.map((student, index) => {
        if (index !== studentIndex) return student;
        const exists = student.Grades.some((grade) => grade.Component === component);
        return {
          ...student,
          Grades: exists
            ? student.Grades.map((grade) => grade.Component === component ? { ...grade, Grade: value } : grade)
            : [...student.Grades, { Component: component, Grade: value }],
        };
      }),
    }));
  };

  const updateComment = (studentIndex: number, value: string | null) => {
    replaceCurrentClass((sourceClass) => ({
      ...sourceClass,
      Students: sourceClass.Students.map((student, index) =>
        index === studentIndex ? { ...student, Comment: value } : student
      ),
    }));
  };

  const addStudent = (roll: string, name: string) => {
    replaceCurrentClass((sourceClass) => ({
      ...sourceClass,
      Students: [...sourceClass.Students, {
        Roll: roll,
        Name: name,
        Comment: null,
        Grades: sourceClass.Components.map((component) => ({ Component: component, Grade: null })),
      }],
    }));
    toast.success(`Đã thêm sinh viên ${roll}`);
  };

  const addComponent = (name: string) => {
    replaceCurrentClass((sourceClass) => ({
      ...sourceClass,
      Components: [...sourceClass.Components, name],
      Students: sourceClass.Students.map((student) => ({
        ...student,
        Grades: [...student.Grades, { Component: name, Grade: null }],
      })),
    }));
    setVisibleComponents((current) => new Set(current).add(name));
    setSelectedComponent(name);
    toast.success(`Đã thêm thành phần ${name}`);
  };

  const clearSelectedComponent = () => {
    if (!selectedComponent) return;
    if (!window.confirm(`Xóa toàn bộ điểm của "${selectedComponent}" trong bản đang sửa?`)) return;
    replaceCurrentClass((sourceClass) => ({
      ...sourceClass,
      Students: sourceClass.Students.map((student) => ({
        ...student,
        Grades: student.Grades.map((grade) =>
          grade.Component === selectedComponent ? { ...grade, Grade: null } : grade
        ),
      })),
    }));
  };

  const applyImportedRows = (mode: "grade" | "comment", rows: { roll: string; value: string }[]) => {
    const values = new Map(rows.map((row) => [row.roll.toUpperCase(), row.value]));
    replaceCurrentClass((sourceClass) => ({
      ...sourceClass,
      Students: sourceClass.Students.map((student) => {
        const imported = values.get(student.Roll.toUpperCase());
        if (imported === undefined) return student;
        if (mode === "comment") return { ...student, Comment: imported };
        const gradeValue = Number(imported);
        const hasComponent = student.Grades.some(
          (grade) => grade.Component === selectedComponent
        );
        return {
          ...student,
          Grades: hasComponent
            ? student.Grades.map((grade) =>
                grade.Component === selectedComponent
                  ? { ...grade, Grade: gradeValue }
                  : grade
              )
            : [
                ...student.Grades,
                { Component: selectedComponent ?? "", Grade: gradeValue },
              ],
        };
      }),
    }));
    toast.success(`Đã áp dụng ${rows.length} dòng vào bản chỉnh sửa cục bộ`);
  };

  const saveSnapshot = async () => {
    if (!workspace) return;
    setSaving(true);
    try {
      const response = await fetch("/api/fg/save", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ data: workspace.data, filename: workspace.filename }),
      });
      if (response.status === 401) {
        sessionStorage.setItem(WORKSPACE_KEY, JSON.stringify(workspace));
        toast.info("Đăng nhập để lưu. Bản đang sửa đã được giữ lại trong phiên này.");
        router.push("/login?callbackUrl=/");
        return;
      }
      const result = await response.json();
      if (!response.ok) throw new Error(result.error || "Không thể lưu bảng điểm");
      setWorkspace((current) => current ? {
        ...current,
        dirty: false,
        gradesheetId: result.gradesheetId,
      } : current);
      toast.success(`Đã lưu snapshot #${result.gradesheetId}`);
    } catch (error) {
      toast.error(error instanceof Error ? error.message : "Không thể lưu bảng điểm");
    } finally {
      setSaving(false);
    }
  };

  const toggleComponent = (component: string) => {
    setVisibleComponents((current) => {
      const next = new Set(current);
      if (next.has(component)) next.delete(component); else next.add(component);
      return next;
    });
  };

  const handleSignOut = async () => {
    await signOut({ redirect: false });
    setSessionUser(null);
    setSessionChecked(true);
    router.refresh();
    toast.success("Đã đăng xuất. Bạn vẫn có thể tiếp tục chỉnh sửa ở chế độ khách.");
  };

  return (
    <div className="workspace-shell">
      <header className="workspace-header">
        <div className="brand-block">
          <Image className="brand-logo" src={logoImage} alt="Logo FuGrade" priority />
          <div><strong>FuGrade Web</strong><span>Workspace bảng điểm</span></div>
        </div>

        <div className="file-identity">
          <span className="file-status-dot" />
          <div><strong>{workspace?.filename ?? "Chưa mở file"}</strong><span>{workspace ? (workspace.dirty ? "Đang chỉnh sửa cục bộ" : workspace.gradesheetId ? `Đã lưu snapshot #${workspace.gradesheetId}` : "Sẵn sàng chỉnh sửa") : "Mở một file .fg để bắt đầu"}</span></div>
        </div>

        <div className="header-actions">
          <button className="btn btn-secondary" type="button" onClick={() => setDialog("open")}>Mở file .fg</button>
          <button className="btn btn-secondary" type="button" disabled={!workspace} onClick={() => setDialog("export")}>Xuất .fg</button>
          <button className="btn btn-primary" type="button" disabled={!workspace || saving} onClick={saveSnapshot}>{saving ? "Đang lưu…" : "Lưu"}</button>
          {!sessionChecked ? (
            <span className="session-pending">Đang kiểm tra phiên…</span>
          ) : sessionUser ? (
            <button
              type="button"
              className="session-pill session-button"
              onClick={handleSignOut}
              title="Nhấn để đăng xuất"
            >
              <strong>{sessionUser.email ?? sessionUser.name ?? "Tài khoản"}</strong>
              <span>Đăng xuất</span>
            </button>
          ) : (
            <Link href="/login?callbackUrl=/" className="login-link">Đăng nhập</Link>
          )}
        </div>
      </header>

      {!workspace ? (
        <main className="empty-workspace">
          <div className="empty-illustration">
            <Image className="empty-logo" src={logoImage} alt="Logo FuGrade" priority />
          </div>
          <span className="eyebrow">KHÔNG CẦN ĐĂNG NHẬP</span>
          <h1>Mở và chỉnh sửa bảng điểm ngay</h1>
          <p>Mọi thay đổi được giữ cục bộ trong phiên trình duyệt. Chỉ khi bấm <strong>Lưu</strong> bạn mới cần đăng nhập.</p>
          <div className="empty-actions">
            <button className="btn btn-primary btn-large" type="button" onClick={() => setDialog("open")}>Chọn file .fg</button>
            <span>Hỗ trợ file có mật khẩu từ FuGrade desktop</span>
          </div>
          <div className="feature-strip">
            <div><strong>Chỉnh điểm nhanh</strong><span>Nhấp đúp vào ô để sửa</span></div>
            <div><strong>Import hàng loạt</strong><span>Dán dữ liệu từ Excel</span></div>
            <div><strong>Xuất có mật khẩu</strong><span>Tương thích FuGrade local</span></div>
          </div>
        </main>
      ) : (
        <main className="workspace-content">
          <section className="workspace-toolbar card">
            <label><span>Giảng viên</span><input className="input" value={workspace.data.Login} onChange={(event) => updateMetadata("Login", event.target.value)} /></label>
            <label><span>Học kỳ</span><input className="input" value={workspace.data.Semester} onChange={(event) => updateMetadata("Semester", event.target.value)} /></label>
            <label className="class-picker"><span>Môn học / lớp</span><select className="input" value={selectedClassIndex} onChange={(event) => handleClassChange(Number(event.target.value))}>{workspace.data.SubjectClassGrades.map((item, index) => <option value={index} key={`${classLabel(item)}-${index}`}>{classLabel(item)}</option>)}</select></label>
            <div className="workspace-hint"><strong>{currentClass?.Students.length ?? 0}</strong><span>sinh viên</span></div>
            <div className="workspace-hint"><strong>{currentClass?.Components.length ?? 0}</strong><span>thành phần</span></div>
          </section>

          {currentClass ? (
            <div className="editor-layout">
              <aside className="editor-sidebar card">
                <div className="sidebar-section">
                  <div className="section-heading"><div><span className="eyebrow">HIỂN THỊ CỘT</span><h2>Thành phần điểm</h2></div><button type="button" className="text-button" onClick={() => setVisibleComponents(visibleComponents.size === currentClass.Components.length ? new Set() : new Set(currentClass.Components))}>{visibleComponents.size === currentClass.Components.length ? "Bỏ chọn" : "Chọn tất"}</button></div>
                  <div className="component-list">
                    {currentClass.Components.map((component) => (
                      <label className={`component-row ${selectedComponent === component ? "selected" : ""}`} key={component} onClick={() => setSelectedComponent(component)}>
                        <input type="checkbox" checked={visibleComponents.has(component)} onChange={() => toggleComponent(component)} onClick={(event) => event.stopPropagation()} />
                        <span>{component}</span>
                      </label>
                    ))}
                  </div>
                </div>
                <div className="sidebar-section action-stack">
                  <span className="eyebrow">THAO TÁC NHANH</span>
                  <button className="btn btn-secondary" type="button" onClick={() => setDialog("add-student")}>Thêm sinh viên</button>
                  <button className="btn btn-secondary" type="button" onClick={() => setDialog("add-component")}>Thêm thành phần</button>
                  <button className="btn btn-secondary" type="button" onClick={() => setDialog("import-comment")}>Import nhận xét</button>
                  <button className="btn btn-secondary" type="button" disabled={!selectedComponent} onClick={() => setDialog("import-grade")}>Import điểm{selectedComponent ? ` · ${selectedComponent}` : ""}</button>
                  <button className="btn btn-danger" type="button" disabled={!selectedComponent} onClick={clearSelectedComponent}>Xóa điểm cột đã chọn</button>
                </div>
                <div className="guest-card"><strong>{sessionUser ? `Đã đăng nhập: ${sessionUser.email ?? sessionUser.name ?? "tài khoản"}` : "Chế độ khách"}</strong><span>{sessionUser ? "Bạn có thể lưu snapshot vào hệ thống." : "Mọi chỉnh sửa vẫn dùng được. Đăng nhập chỉ khi lưu."}</span></div>
              </aside>

              <section className="grade-panel card">
                <div className="table-toolbar">
                  <div><span className="eyebrow">BẢNG ĐIỂM</span><h2>{classLabel(currentClass)}</h2></div>
                  <label className="search-box"><span aria-hidden="true">⌕</span><input value={searchQuery} onChange={(event) => setSearchQuery(event.target.value)} placeholder="Tìm MSSV hoặc họ tên…" /></label>
                  <span className="edit-tip">Nhấp đúp ô để sửa · Enter để lưu · Esc để hủy</span>
                </div>
                <DraftGradeTable classData={currentClass} visibleComponents={visibleComponents} searchQuery={searchQuery} onGradeChange={updateGrade} onCommentChange={updateComment} />
              </section>
            </div>
          ) : (
            <section className="card no-class">File không có lớp học để hiển thị.</section>
          )}
        </main>
      )}

      {dialog === "open" && <OpenFgDialog onLoaded={handleLoaded} onClose={() => setDialog(null)} />}
      {dialog === "export" && workspace && <ExportFgDialog data={workspace.data} filename={workspace.filename} onClose={() => setDialog(null)} />}
      {dialog === "add-student" && currentClass && <AddStudentDialog existingRolls={currentClass.Students.map((student) => student.Roll)} onAdd={addStudent} onClose={() => setDialog(null)} />}
      {dialog === "add-component" && currentClass && <AddComponentDialog existingComponents={currentClass.Components} onAdd={addComponent} onClose={() => setDialog(null)} />}
      {dialog === "import-grade" && currentClass && selectedComponent && <ImportRowsDialog mode="grade" component={selectedComponent} validRolls={currentClass.Students.map((student) => student.Roll)} onApply={(rows) => applyImportedRows("grade", rows)} onClose={() => setDialog(null)} />}
      {dialog === "import-comment" && currentClass && <ImportRowsDialog mode="comment" validRolls={currentClass.Students.map((student) => student.Roll)} onApply={(rows) => applyImportedRows("comment", rows)} onClose={() => setDialog(null)} />}
    </div>
  );
}
