using System;
using System.Collections.Generic;

namespace FuGrade
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class ThesisComment
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000116C9 File Offset: 0x0000F8C9
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000116D1 File Offset: 0x0000F8D1
		public string Teacher { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000116DA File Offset: 0x0000F8DA
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000116E2 File Offset: 0x0000F8E2
		public DateTime DT { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000116EB File Offset: 0x0000F8EB
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000116F3 File Offset: 0x0000F8F3
		public string SubjectCode { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000116FC File Offset: 0x0000F8FC
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00011704 File Offset: 0x0000F904
		public string ClassName { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0001170D File Offset: 0x0000F90D
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00011715 File Offset: 0x0000F915
		public string Semester { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0001171E File Offset: 0x0000F91E
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00011726 File Offset: 0x0000F926
		public string Password { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0001172F File Offset: 0x0000F92F
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00011737 File Offset: 0x0000F937
		public string TitleVN { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00011740 File Offset: 0x0000F940
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00011748 File Offset: 0x0000F948
		public string TitleEN { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00011751 File Offset: 0x0000F951
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00011759 File Offset: 0x0000F959
		public string Content { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00011762 File Offset: 0x0000F962
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0001176A File Offset: 0x0000F96A
		public string Form { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00011773 File Offset: 0x0000F973
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0001177B File Offset: 0x0000F97B
		public string Attitude { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00011784 File Offset: 0x0000F984
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0001178C File Offset: 0x0000F98C
		public string Achievement { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00011795 File Offset: 0x0000F995
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0001179D File Offset: 0x0000F99D
		public string Limitation { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000117A6 File Offset: 0x0000F9A6
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000117AE File Offset: 0x0000F9AE
		public List<ThesisStudent> Conclusion { get; set; }
	}
}
