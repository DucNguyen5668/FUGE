namespace FuGrade
{
	// Token: 0x0200000B RID: 11
	public partial class FrmEvaluationForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000A400 File Offset: 0x00008600
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000A438 File Offset: 0x00008638
		private void InitializeComponent()
		{
			this.dgvGrade = new global::System.Windows.Forms.DataGridView();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.lblSubjectCode = new global::System.Windows.Forms.Label();
			this.lblSupervisor = new global::System.Windows.Forms.Label();
			this.btnCopyGroupMark = new global::System.Windows.Forms.Button();
			this.label3 = new global::System.Windows.Forms.Label();
			this.txtNote = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btnSave = new global::System.Windows.Forms.Button();
			this.folderBrowserDialog = new global::System.Windows.Forms.FolderBrowserDialog();
			this.lblEvaluator = new global::System.Windows.Forms.Label();
			this.txtSavedAt = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btnShow = new global::System.Windows.Forms.Button();
			this.txtFileName = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.txtTitleVN = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.txtTitleEN = new global::System.Windows.Forms.TextBox();
			this.lblClassSem = new global::System.Windows.Forms.Label();
			this.btnShowSupComment = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dgvGrade).BeginInit();
			base.SuspendLayout();
			this.dgvGrade.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dgvGrade.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvGrade.Location = new global::System.Drawing.Point(12, 158);
			this.dgvGrade.Name = "dgvGrade";
			this.dgvGrade.Size = new global::System.Drawing.Size(1017, 267);
			this.dgvGrade.TabIndex = 0;
			this.dgvGrade.CellEndEdit += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrade_CellEndEdit);
			this.dgvGrade.CellEnter += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrade_CellEnter);
			this.dgvGrade.CellLeave += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrade_CellLeave);
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new global::System.Drawing.Point(954, 558);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.lblSubjectCode.AutoSize = true;
			this.lblSubjectCode.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblSubjectCode.ForeColor = global::System.Drawing.Color.Blue;
			this.lblSubjectCode.Location = new global::System.Drawing.Point(9, 55);
			this.lblSubjectCode.Name = "lblSubjectCode";
			this.lblSubjectCode.Size = new global::System.Drawing.Size(94, 17);
			this.lblSubjectCode.TabIndex = 2;
			this.lblSubjectCode.Text = "Subject code:";
			this.lblSupervisor.AutoSize = true;
			this.lblSupervisor.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblSupervisor.ForeColor = global::System.Drawing.Color.Blue;
			this.lblSupervisor.Location = new global::System.Drawing.Point(528, 55);
			this.lblSupervisor.Name = "lblSupervisor";
			this.lblSupervisor.Size = new global::System.Drawing.Size(80, 17);
			this.lblSupervisor.TabIndex = 3;
			this.lblSupervisor.Text = "Supervisor:";
			this.btnCopyGroupMark.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnCopyGroupMark.Location = new global::System.Drawing.Point(12, 428);
			this.btnCopyGroupMark.Name = "btnCopyGroupMark";
			this.btnCopyGroupMark.Size = new global::System.Drawing.Size(172, 23);
			this.btnCopyGroupMark.TabIndex = 4;
			this.btnCopyGroupMark.Text = "Copy [Group mark] to students";
			this.btnCopyGroupMark.UseVisualStyleBackColor = true;
			this.btnCopyGroupMark.Click += new global::System.EventHandler(this.btnCopyGroupMark_Click);
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 15f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(7, 9);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(451, 25);
			this.label3.TabIndex = 9;
			this.label3.Text = "Thesis/Capstone Project Defense Evaluation Form";
			this.txtNote.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtNote.Location = new global::System.Drawing.Point(142, 458);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txtNote.Size = new global::System.Drawing.Size(791, 71);
			this.txtNote.TabIndex = 10;
			this.txtNote.TextChanged += new global::System.EventHandler(this.txtNote_TextChanged);
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(103, 458);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(33, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Note:";
			this.btnSave.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnSave.BackColor = global::System.Drawing.Color.Yellow;
			this.btnSave.Location = new global::System.Drawing.Point(858, 558);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new global::System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 12;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new global::System.EventHandler(this.btnSave_Click);
			this.lblEvaluator.AutoSize = true;
			this.lblEvaluator.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblEvaluator.ForeColor = global::System.Drawing.Color.Blue;
			this.lblEvaluator.Location = new global::System.Drawing.Point(739, 55);
			this.lblEvaluator.Name = "lblEvaluator";
			this.lblEvaluator.Size = new global::System.Drawing.Size(72, 17);
			this.lblEvaluator.TabIndex = 13;
			this.lblEvaluator.Text = "Evaluator:";
			this.txtSavedAt.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtSavedAt.Location = new global::System.Drawing.Point(142, 561);
			this.txtSavedAt.Name = "txtSavedAt";
			this.txtSavedAt.ReadOnly = true;
			this.txtSavedAt.Size = new global::System.Drawing.Size(627, 20);
			this.txtSavedAt.TabIndex = 14;
			this.label2.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(9, 564);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(127, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Evaluation form saved at:";
			this.btnShow.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnShow.Location = new global::System.Drawing.Point(775, 558);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new global::System.Drawing.Size(67, 23);
			this.btnShow.TabIndex = 16;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = true;
			this.btnShow.Click += new global::System.EventHandler(this.btnShow_Click);
			this.txtFileName.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtFileName.Location = new global::System.Drawing.Point(142, 535);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.ReadOnly = true;
			this.txtFileName.Size = new global::System.Drawing.Size(476, 20);
			this.txtFileName.TabIndex = 18;
			this.label4.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(81, 538);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(55, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "File name:";
			this.txtTitleVN.BackColor = global::System.Drawing.SystemColors.Control;
			this.txtTitleVN.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtTitleVN.Location = new global::System.Drawing.Point(12, 94);
			this.txtTitleVN.Multiline = true;
			this.txtTitleVN.Name = "txtTitleVN";
			this.txtTitleVN.ReadOnly = true;
			this.txtTitleVN.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txtTitleVN.Size = new global::System.Drawing.Size(498, 55);
			this.txtTitleVN.TabIndex = 23;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(9, 78);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(48, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Title VN:";
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(528, 78);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(48, 13);
			this.label6.TabIndex = 26;
			this.label6.Text = "Title EN:";
			this.txtTitleEN.BackColor = global::System.Drawing.SystemColors.Control;
			this.txtTitleEN.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtTitleEN.Location = new global::System.Drawing.Point(531, 94);
			this.txtTitleEN.Multiline = true;
			this.txtTitleEN.Name = "txtTitleEN";
			this.txtTitleEN.ReadOnly = true;
			this.txtTitleEN.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txtTitleEN.Size = new global::System.Drawing.Size(498, 55);
			this.txtTitleEN.TabIndex = 25;
			this.lblClassSem.AutoSize = true;
			this.lblClassSem.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblClassSem.ForeColor = global::System.Drawing.Color.Blue;
			this.lblClassSem.Location = new global::System.Drawing.Point(192, 55);
			this.lblClassSem.Name = "lblClassSem";
			this.lblClassSem.Size = new global::System.Drawing.Size(46, 17);
			this.lblClassSem.TabIndex = 27;
			this.lblClassSem.Text = "Class:";
			this.btnShowSupComment.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnShowSupComment.BackColor = global::System.Drawing.Color.Yellow;
			this.btnShowSupComment.Location = new global::System.Drawing.Point(761, 428);
			this.btnShowSupComment.Name = "btnShowSupComment";
			this.btnShowSupComment.Size = new global::System.Drawing.Size(172, 23);
			this.btnShowSupComment.TabIndex = 28;
			this.btnShowSupComment.Text = "Show supervisor's comment";
			this.btnShowSupComment.UseVisualStyleBackColor = false;
			this.btnShowSupComment.Click += new global::System.EventHandler(this.btnShowSupComment_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(1041, 592);
			base.Controls.Add(this.btnShowSupComment);
			base.Controls.Add(this.lblClassSem);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.txtTitleEN);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.txtTitleVN);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.txtFileName);
			base.Controls.Add(this.btnShow);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtSavedAt);
			base.Controls.Add(this.lblEvaluator);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnCopyGroupMark);
			base.Controls.Add(this.lblSupervisor);
			base.Controls.Add(this.lblSubjectCode);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.dgvGrade);
			base.Name = "FrmEvaluationForm";
			this.Text = "Evaluation Form";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.FrmGroupGrade_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dgvGrade).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000085 RID: 133
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000086 RID: 134
		private global::System.Windows.Forms.DataGridView dgvGrade;

		// Token: 0x04000087 RID: 135
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000088 RID: 136
		private global::System.Windows.Forms.Label lblSubjectCode;

		// Token: 0x04000089 RID: 137
		private global::System.Windows.Forms.Label lblSupervisor;

		// Token: 0x0400008A RID: 138
		private global::System.Windows.Forms.Button btnCopyGroupMark;

		// Token: 0x0400008B RID: 139
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.TextBox txtNote;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400008E RID: 142
		private global::System.Windows.Forms.Button btnSave;

		// Token: 0x0400008F RID: 143
		private global::System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

		// Token: 0x04000090 RID: 144
		private global::System.Windows.Forms.Label lblEvaluator;

		// Token: 0x04000091 RID: 145
		private global::System.Windows.Forms.TextBox txtSavedAt;

		// Token: 0x04000092 RID: 146
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000093 RID: 147
		private global::System.Windows.Forms.Button btnShow;

		// Token: 0x04000094 RID: 148
		private global::System.Windows.Forms.TextBox txtFileName;

		// Token: 0x04000095 RID: 149
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000096 RID: 150
		private global::System.Windows.Forms.TextBox txtTitleVN;

		// Token: 0x04000097 RID: 151
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000098 RID: 152
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000099 RID: 153
		private global::System.Windows.Forms.TextBox txtTitleEN;

		// Token: 0x0400009A RID: 154
		private global::System.Windows.Forms.Label lblClassSem;

		// Token: 0x0400009B RID: 155
		private global::System.Windows.Forms.Button btnShowSupComment;
	}
}
