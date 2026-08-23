using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x0200000D RID: 13
	public partial class FrmPassword : Form
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000C354 File Offset: 0x0000A554
		public string Password
		{
			get
			{
				return this.txtPassword.Text;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000C371 File Offset: 0x0000A571
		public FrmPassword()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000C38C File Offset: 0x0000A58C
		private void btnOk_Click(object sender, EventArgs e)
		{
			bool flag = this.txtPassword.Text.Trim().Equals("");
			if (flag)
			{
				MessageBox.Show("Password cannot empty!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				base.Close();
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.txtPassword.Text = "";
			base.Close();
		}
	}
}
