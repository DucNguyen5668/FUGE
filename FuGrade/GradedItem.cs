using System;

namespace FuGrade
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class GradedItem
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023C6 File Offset: 0x000005C6
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000023CE File Offset: 0x000005CE
		public string GroupItem { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023D7 File Offset: 0x000005D7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000023DF File Offset: 0x000005DF
		public string ItemName { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000023E8 File Offset: 0x000005E8
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000023F0 File Offset: 0x000005F0
		public float Scale { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000023F9 File Offset: 0x000005F9
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002401 File Offset: 0x00000601
		public float GroupMark { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000240A File Offset: 0x0000060A
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002412 File Offset: 0x00000612
		public float Mark { get; set; }
	}
}
