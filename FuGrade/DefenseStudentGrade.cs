using System;
using System.Collections.Generic;

namespace FuGrade
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class DefenseStudentGrade
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000236C File Offset: 0x0000056C
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002374 File Offset: 0x00000574
		public string Roll { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000237D File Offset: 0x0000057D
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002385 File Offset: 0x00000585
		public string Name { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000238E File Offset: 0x0000058E
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002396 File Offset: 0x00000596
		public string Conclusion { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000239F File Offset: 0x0000059F
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000023A7 File Offset: 0x000005A7
		public List<GradedItem> GradedItems { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000023B0 File Offset: 0x000005B0
		public DefenseStudentGrade()
		{
			this.GradedItems = new List<GradedItem>();
		}
	}
}
