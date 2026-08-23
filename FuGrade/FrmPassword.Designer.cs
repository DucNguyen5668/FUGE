namespace FuGrade
{
	// Token: 0x0200000D RID: 13
	public partial class FrmPassword : global::System.Windows.Forms.Form
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000C42C File Offset: 0x0000A62C
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtPassword = new global::System.Windows.Forms.TextBox();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(13, 29);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(83, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter password:";
			this.txtPassword.Location = new global::System.Drawing.Point(102, 26);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new global::System.Drawing.Size(199, 20);
			this.txtPassword.TabIndex = 1;
			this.btnOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new global::System.Drawing.Point(102, 53);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new global::System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(226, 53);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(319, 88);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FrmPassword";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Open grading file";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000B0 RID: 176
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000B1 RID: 177
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000B2 RID: 178
		private global::System.Windows.Forms.TextBox txtPassword;

		// Token: 0x040000B3 RID: 179
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x040000B4 RID: 180
		private global::System.Windows.Forms.Button btnCancel;
	}
}
