export interface FgGradeComponent {
  Component: string;
  Grade: number | null;
}

export interface FgStudent {
  Roll: string;
  Name: string;
  Comment: string | null;
  Grades: FgGradeComponent[];
}

export interface FgSubjectClassGrade {
  Subject: string;
  Class: string;
  Components: string[];
  Students: FgStudent[];
}

export interface FgTeacherGrade {
  Login: string;
  Semester: string;
  Version: string;
  Password: string;
  SubjectClassGrades: FgSubjectClassGrade[];
}

export interface FgWorkspace {
  data: FgTeacherGrade;
  filename: string;
  gradesheetId?: number;
  dirty: boolean;
}
