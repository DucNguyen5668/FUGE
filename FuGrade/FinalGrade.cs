using System;
using System.Collections.Generic;

namespace FuGrade
{
	// Token: 0x02000010 RID: 16
	internal class FinalGrade
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000ED34 File Offset: 0x0000CF34
		public string Roll { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000ED3D File Offset: 0x0000CF3D
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000ED45 File Offset: 0x0000CF45
		public string Name { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000ED4E File Offset: 0x0000CF4E
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000ED56 File Offset: 0x0000CF56
		public string SubjectCode { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000ED5F File Offset: 0x0000CF5F
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000ED67 File Offset: 0x0000CF67
		public string ClassName { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000ED70 File Offset: 0x0000CF70
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public string Semester { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000ED81 File Offset: 0x0000CF81
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000ED89 File Offset: 0x0000CF89
		public string Supervisor { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000ED92 File Offset: 0x0000CF92
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000ED9A File Offset: 0x0000CF9A
		public string Title { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		public float AvgMark
		{
			get
			{
				float num = 0f;
				foreach (FinalGradeOfTeacher finalGradeOfTeacher in this.ListFGOT)
				{
					num += finalGradeOfTeacher.Mark;
				}
				decimal d = (decimal)num / this.ListFGOT.Count;
				return (float)Math.Round(d, 1, MidpointRounding.AwayFromZero);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000EE38 File Offset: 0x0000D038
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000EE40 File Offset: 0x0000D040
		public float Scale { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000EE49 File Offset: 0x0000D049
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000EE51 File Offset: 0x0000D051
		public string Note { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000EE5A File Offset: 0x0000D05A
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000EE62 File Offset: 0x0000D062
		public DateTime DateTime { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000EE6B File Offset: 0x0000D06B
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000EE73 File Offset: 0x0000D073
		public string GradedBy { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000EE7C File Offset: 0x0000D07C
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000EE84 File Offset: 0x0000D084
		public List<FinalGradeOfTeacher> ListFGOT { get; set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x0000EE8D File Offset: 0x0000D08D
		public FinalGrade()
		{
			this.ListFGOT = new List<FinalGradeOfTeacher>();
		}
	}
}
