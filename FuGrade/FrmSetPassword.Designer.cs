namespace FuGrade
{
	// Token: 0x0200000E RID: 14
	public partial class FrmSetPassword : global::System.Windows.Forms.Form
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000C84C File Offset: 0x0000AA4C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000C884 File Offset: 0x0000AA84
		private void InitializeComponent()
		{
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.txtPassword = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtRetypePassword = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.chbShowPassword = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.btnCancel.Location = new global::System.Drawing.Point(218, 84);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.btnOk.Location = new global::System.Drawing.Point(101, 84);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new global::System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.txtPassword.Location = new global::System.Drawing.Point(101, 13);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new global::System.Drawing.Size(192, 20);
			this.txtPassword.TabIndex = 5;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(44, 16);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(56, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Password:";
			this.txtRetypePassword.Location = new global::System.Drawing.Point(101, 39);
			this.txtRetypePassword.Name = "txtRetypePassword";
			this.txtRetypePassword.PasswordChar = '*';
			this.txtRetypePassword.Size = new global::System.Drawing.Size(192, 20);
			this.txtRetypePassword.TabIndex = 9;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(5, 42);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(95, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Re-type password:";
			this.chbShowPassword.AutoSize = true;
			this.chbShowPassword.Location = new global::System.Drawing.Point(101, 65);
			this.chbShowPassword.Name = "chbShowPassword";
			this.chbShowPassword.Size = new global::System.Drawing.Size(101, 17);
			this.chbShowPassword.TabIndex = 10;
			this.chbShowPassword.Text = "Show password";
			this.chbShowPassword.UseVisualStyleBackColor = true;
			this.chbShowPassword.CheckedChanged += new global::System.EventHandler(this.chbShowPassword_CheckedChanged);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(311, 119);
			base.Controls.Add(this.chbShowPassword);
			base.Controls.Add(this.txtRetypePassword);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FrmSetPassword";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set Password";
			base.Load += new global::System.EventHandler(this.FrmSetPassword_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000B5 RID: 181
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000B6 RID: 182
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x040000B7 RID: 183
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x040000B8 RID: 184
		private global::System.Windows.Forms.TextBox txtPassword;

		// Token: 0x040000B9 RID: 185
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000BA RID: 186
		private global::System.Windows.Forms.TextBox txtRetypePassword;

		// Token: 0x040000BB RID: 187
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040000BC RID: 188
		private global::System.Windows.Forms.CheckBox chbShowPassword;
	}
}
