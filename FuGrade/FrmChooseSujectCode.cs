using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x02000007 RID: 7
	public partial class FrmChooseSujectCode : Form
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002481 File Offset: 0x00000681
		public FrmChooseSujectCode()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002499 File Offset: 0x00000699
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000024A1 File Offset: 0x000006A1
		public List<string> SubjectCodes { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x000024AA File Offset: 0x000006AA
		private void FrmChooseSujectCode_Load(object sender, EventArgs e)
		{
			this.cboSubjectCode.DataSource = this.SubjectCodes;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000024BF File Offset: 0x000006BF
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000024C7 File Offset: 0x000006C7
		public FrmEvaluationForm FeF { get; set; }

		// Token: 0x06000047 RID: 71 RVA: 0x000024D0 File Offset: 0x000006D0
		private void btnOk_Click(object sender, EventArgs e)
		{
			this.FeF.FinalSubjectCode = this.cboSubjectCode.Text;
			base.Close();
		}
	}
}
