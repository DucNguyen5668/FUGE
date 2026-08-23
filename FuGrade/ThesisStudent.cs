using System;

namespace FuGrade
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class ThesisStudent
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000117B7 File Offset: 0x0000F9B7
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000117BF File Offset: 0x0000F9BF
		public string Roll { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000117C8 File Offset: 0x0000F9C8
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000117D0 File Offset: 0x0000F9D0
		public string Name { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000117D9 File Offset: 0x0000F9D9
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000117E1 File Offset: 0x0000F9E1
		public string Agree_to_defense { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000117EA File Offset: 0x0000F9EA
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000117F2 File Offset: 0x0000F9F2
		public string Revised_for_the_second_defense { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000117FB File Offset: 0x0000F9FB
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00011803 File Offset: 0x0000FA03
		public string Disagree_to_defense { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00011814 File Offset: 0x0000FA14
		public string Note { get; set; }
	}
}
