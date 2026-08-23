using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x0200000E RID: 14
	public partial class FrmSetPassword : Form
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x0000C705 File Offset: 0x0000A905
		public FrmSetPassword()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000C720 File Offset: 0x0000A920
		public string Password
		{
			get
			{
				return this.txtPassword.Text;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004457 File Offset: 0x00002657
		private void FrmSetPassword_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000C740 File Offset: 0x0000A940
		private void btnOk_Click(object sender, EventArgs e)
		{
			bool flag = this.txtPassword.Text.Trim().Equals("");
			if (flag)
			{
				MessageBox.Show("Password cannot empty!", "Set password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				bool flag2 = !this.txtPassword.Text.Equals(this.txtRetypePassword.Text);
				if (flag2)
				{
					MessageBox.Show("The passwords you entered did not match!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					base.Close();
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000C7C5 File Offset: 0x0000A9C5
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.txtPassword.Text = "";
			base.Close();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.chbShowPassword.Checked;
			if (@checked)
			{
				this.txtPassword.PasswordChar = char.Parse("\0");
				this.txtRetypePassword.PasswordChar = char.Parse("\0");
			}
			else
			{
				this.txtPassword.PasswordChar = '*';
				this.txtRetypePassword.PasswordChar = '*';
			}
		}
	}
}
