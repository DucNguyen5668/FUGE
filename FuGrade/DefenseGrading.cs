using System;
using System.Collections.Generic;

namespace FuGrade
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class DefenseGrading
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002279 File Offset: 0x00000479
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002281 File Offset: 0x00000481
		public string SubjectCode { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000228A File Offset: 0x0000048A
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002292 File Offset: 0x00000492
		public string TitleVN { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000229B File Offset: 0x0000049B
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000022A3 File Offset: 0x000004A3
		public string TitleEN { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000022B4 File Offset: 0x000004B4
		public string Supervisor { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022BD File Offset: 0x000004BD
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022C5 File Offset: 0x000004C5
		public string ClassName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022CE File Offset: 0x000004CE
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022D6 File Offset: 0x000004D6
		public string Semester { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022DF File Offset: 0x000004DF
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022E7 File Offset: 0x000004E7
		public float GroupMark { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022F0 File Offset: 0x000004F0
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022F8 File Offset: 0x000004F8
		public DateTime GradedTime { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002301 File Offset: 0x00000501
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002309 File Offset: 0x00000509
		public string GradedTeacher { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002312 File Offset: 0x00000512
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000231A File Offset: 0x0000051A
		public ThesisComment SupervisorComment { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002323 File Offset: 0x00000523
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000232B File Offset: 0x0000052B
		public string Password { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002334 File Offset: 0x00000534
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000233C File Offset: 0x0000053C
		public string Note { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002345 File Offset: 0x00000545
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000234D File Offset: 0x0000054D
		public List<DefenseStudentGrade> GradeStudents { get; set; }

		// Token: 0x0600001F RID: 31 RVA: 0x00002356 File Offset: 0x00000556
		public DefenseGrading()
		{
			this.GradeStudents = new List<DefenseStudentGrade>();
		}
	}
}
