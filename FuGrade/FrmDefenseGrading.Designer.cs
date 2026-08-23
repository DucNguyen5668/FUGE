namespace FuGrade
{
	// Token: 0x02000009 RID: 9
	public partial class FrmDefenseGrading : global::System.Windows.Forms.Form
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00004478 File Offset: 0x00002678
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000044B0 File Offset: 0x000026B0
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtGroupFolder = new global::System.Windows.Forms.TextBox();
			this.btnBrowse = new global::System.Windows.Forms.Button();
			this.folderBrowserDialog = new global::System.Windows.Forms.FolderBrowserDialog();
			this.btnLoadGroup = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.dgvGroup = new global::System.Windows.Forms.DataGridView();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.btnGrade = new global::System.Windows.Forms.Button();
			this.txtGradedBy = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.btnEdit = new global::System.Windows.Forms.Button();
			this.openFileDialog = new global::System.Windows.Forms.OpenFileDialog();
			this.label4 = new global::System.Windows.Forms.Label();
			this.btnSumerize = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dgvGroup).BeginInit();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(16, 60);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(68, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Group folder:";
			this.txtGroupFolder.Location = new global::System.Drawing.Point(90, 57);
			this.txtGroupFolder.Name = "txtGroupFolder";
			this.txtGroupFolder.Size = new global::System.Drawing.Size(507, 20);
			this.txtGroupFolder.TabIndex = 1;
			this.btnBrowse.Location = new global::System.Drawing.Point(603, 54);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new global::System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new global::System.EventHandler(this.btnBrowse_Click);
			this.btnLoadGroup.Location = new global::System.Drawing.Point(90, 83);
			this.btnLoadGroup.Name = "btnLoadGroup";
			this.btnLoadGroup.Size = new global::System.Drawing.Size(139, 23);
			this.btnLoadGroup.TabIndex = 3;
			this.btnLoadGroup.Text = "Load Presentation Group";
			this.btnLoadGroup.UseVisualStyleBackColor = true;
			this.btnLoadGroup.Click += new global::System.EventHandler(this.btnLoadGroup_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(16, 107);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(85, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Groups/Classes:";
			this.dgvGroup.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dgvGroup.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvGroup.Location = new global::System.Drawing.Point(19, 123);
			this.dgvGroup.Name = "dgvGroup";
			this.dgvGroup.ReadOnly = true;
			this.dgvGroup.Size = new global::System.Drawing.Size(868, 270);
			this.dgvGroup.TabIndex = 5;
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new global::System.Drawing.Point(812, 402);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.btnGrade.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnGrade.Location = new global::System.Drawing.Point(436, 402);
			this.btnGrade.Name = "btnGrade";
			this.btnGrade.Size = new global::System.Drawing.Size(161, 23);
			this.btnGrade.TabIndex = 7;
			this.btnGrade.Text = "Show Evaluation Form";
			this.btnGrade.UseVisualStyleBackColor = true;
			this.btnGrade.Click += new global::System.EventHandler(this.btnGrade_Click);
			this.txtGradedBy.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtGradedBy.Location = new global::System.Drawing.Point(129, 404);
			this.txtGradedBy.Name = "txtGradedBy";
			this.txtGradedBy.Size = new global::System.Drawing.Size(301, 20);
			this.txtGradedBy.TabIndex = 8;
			this.label3.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(16, 406);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(107, 26);
			this.label3.TabIndex = 9;
			this.label3.Text = "Evaluator's full name:\r\n(without accents)";
			this.btnEdit.Location = new global::System.Drawing.Point(719, 54);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new global::System.Drawing.Size(168, 23);
			this.btnEdit.TabIndex = 10;
			this.btnEdit.Text = "Edit Evaluation Form File (.tef)";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new global::System.EventHandler(this.btnEdit_Click);
			this.openFileDialog.Filter = "Ecaluation Form Files|*.tef";
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 15f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(14, 9);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(401, 25);
			this.label4.TabIndex = 11;
			this.label4.Text = "Thesis/Capstone Project Defense Evaluation";
			this.btnSumerize.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnSumerize.Location = new global::System.Drawing.Point(628, 402);
			this.btnSumerize.Name = "btnSumerize";
			this.btnSumerize.Size = new global::System.Drawing.Size(161, 23);
			this.btnSumerize.TabIndex = 12;
			this.btnSumerize.Text = "Summerize Result";
			this.btnSumerize.UseVisualStyleBackColor = true;
			this.btnSumerize.Click += new global::System.EventHandler(this.btnSumerize_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(899, 434);
			base.Controls.Add(this.btnSumerize);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.btnEdit);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.txtGradedBy);
			base.Controls.Add(this.btnGrade);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.dgvGroup);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btnLoadGroup);
			base.Controls.Add(this.btnBrowse);
			base.Controls.Add(this.txtGroupFolder);
			base.Controls.Add(this.label1);
			base.Name = "FrmDefenseGrading";
			this.Text = "Thesis/Capstone Project Defense Evaluation";
			base.Load += new global::System.EventHandler(this.FrmDefenseGrading_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dgvGroup).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000040 RID: 64
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.TextBox txtGroupFolder;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.Button btnBrowse;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.Button btnLoadGroup;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.DataGridView dgvGroup;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.Button btnGrade;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.TextBox txtGradedBy;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.Button btnEdit;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.OpenFileDialog openFileDialog;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.Button btnSumerize;
	}
}
