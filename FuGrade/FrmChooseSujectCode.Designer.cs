namespace FuGrade
{
	// Token: 0x02000007 RID: 7
	public partial class FrmChooseSujectCode : global::System.Windows.Forms.Form
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000024F4 File Offset: 0x000006F4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000252C File Offset: 0x0000072C
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.cboSubjectCode = new global::System.Windows.Forms.ComboBox();
			this.btnOk = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(9, 22);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(113, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Choose Subject Code:";
			this.cboSubjectCode.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSubjectCode.FormattingEnabled = true;
			this.cboSubjectCode.Location = new global::System.Drawing.Point(128, 19);
			this.cboSubjectCode.Name = "cboSubjectCode";
			this.cboSubjectCode.Size = new global::System.Drawing.Size(227, 21);
			this.cboSubjectCode.TabIndex = 1;
			this.btnOk.Location = new global::System.Drawing.Point(380, 17);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new global::System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(467, 59);
			base.ControlBox = false;
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.cboSubjectCode);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FrmChooseSujectCode";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Choose ...";
			base.Load += new global::System.EventHandler(this.FrmChooseSujectCode_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000020 RID: 32
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000021 RID: 33
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000022 RID: 34
		private global::System.Windows.Forms.ComboBox cboSubjectCode;

		// Token: 0x04000023 RID: 35
		private global::System.Windows.Forms.Button btnOk;
	}
}
