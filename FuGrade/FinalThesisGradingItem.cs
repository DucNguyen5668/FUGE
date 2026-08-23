using System;

namespace FuGrade
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class FinalThesisGradingItem
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000241B File Offset: 0x0000061B
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002423 File Offset: 0x00000623
		public string SubjectCode { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002434 File Offset: 0x00000634
		public string Major { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000243D File Offset: 0x0000063D
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002445 File Offset: 0x00000645
		public string Minor { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000244E File Offset: 0x0000064E
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002456 File Offset: 0x00000656
		public string ItemGroup { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000245F File Offset: 0x0000065F
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002467 File Offset: 0x00000667
		public string GradingItem { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002478 File Offset: 0x00000678
		public float Scale { get; set; }
	}
}
