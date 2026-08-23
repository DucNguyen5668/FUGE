using System;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x02000014 RID: 20
	internal static class Program
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000116AE File Offset: 0x0000F8AE
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FrmFuGrade());
		}
	}
}
