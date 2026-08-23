namespace FuGrade
{
	// Token: 0x02000012 RID: 18
	public partial class FrmThesisComment : global::System.Windows.Forms.Form
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00010210 File Offset: 0x0000E410
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00010248 File Offset: 0x0000E448
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::FuGrade.FrmThesisComment));
			this.btnClose = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txtTitleVN = new global::System.Windows.Forms.TextBox();
			this.txtTitleEN = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.txtContent = new global::System.Windows.Forms.TextBox();
			this.txtForm = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.txtAttitude = new global::System.Windows.Forms.TextBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.txtAchievement = new global::System.Windows.Forms.TextBox();
			this.txtLimitation = new global::System.Windows.Forms.TextBox();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label13 = new global::System.Windows.Forms.Label();
			this.dgvComment = new global::System.Windows.Forms.DataGridView();
			this.btnSave = new global::System.Windows.Forms.Button();
			this.txtStudents = new global::System.Windows.Forms.TextBox();
			this.lblFileName = new global::System.Windows.Forms.Label();
			this.txtCmtFileName = new global::System.Windows.Forms.TextBox();
			this.lblFileExt = new global::System.Windows.Forms.Label();
			this.folderBrowserDialog = new global::System.Windows.Forms.FolderBrowserDialog();
			this.txtSavedFolder = new global::System.Windows.Forms.TextBox();
			this.lblSaveFolder = new global::System.Windows.Forms.Label();
			this.lblHeader = new global::System.Windows.Forms.Label();
			this.lblGuide = new global::System.Windows.Forms.Label();
			this.btnShow = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dgvComment).BeginInit();
			base.SuspendLayout();
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new global::System.Drawing.Point(953, 594);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(55, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(15, 35);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(200, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "1. Tên Khóa luận tốt nghiệp/ Thesis title:";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(37, 50);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(130, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "+ Tiếng Việt/ Vietnamese:";
			this.txtTitleVN.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtTitleVN.Location = new global::System.Drawing.Point(40, 66);
			this.txtTitleVN.Multiline = true;
			this.txtTitleVN.Name = "txtTitleVN";
			this.txtTitleVN.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtTitleVN.Size = new global::System.Drawing.Size(659, 41);
			this.txtTitleVN.TabIndex = 3;
			this.txtTitleEN.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtTitleEN.Location = new global::System.Drawing.Point(40, 126);
			this.txtTitleEN.Multiline = true;
			this.txtTitleEN.Name = "txtTitleEN";
			this.txtTitleEN.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtTitleEN.Size = new global::System.Drawing.Size(659, 42);
			this.txtTitleEN.TabIndex = 5;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(37, 110);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(110, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "+ Tiếng Anh/ English:";
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(709, 35);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(220, 26);
			this.label4.TabIndex = 6;
			this.label4.Text = "2. Họ tên những sinh viên bảo vệ khóa luận/\r\nStudents of the thesis defense:";
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(15, 172);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(367, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "3. Nhận xét của giảng viên hướng dẫn/ Comment from proposed Supervisor:";
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(28, 253);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(0, 13);
			this.label6.TabIndex = 9;
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(37, 187);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(971, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = componentResourceManager.GetString("label7.Text");
			this.txtContent.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtContent.Location = new global::System.Drawing.Point(40, 204);
			this.txtContent.Multiline = true;
			this.txtContent.Name = "txtContent";
			this.txtContent.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtContent.Size = new global::System.Drawing.Size(968, 77);
			this.txtContent.TabIndex = 11;
			this.txtForm.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtForm.Location = new global::System.Drawing.Point(40, 300);
			this.txtForm.Multiline = true;
			this.txtForm.Name = "txtForm";
			this.txtForm.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtForm.Size = new global::System.Drawing.Size(968, 39);
			this.txtForm.TabIndex = 13;
			this.label8.AutoSize = true;
			this.label8.Location = new global::System.Drawing.Point(37, 284);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(671, 13);
			this.label8.TabIndex = 12;
			this.label8.Text = "3.2- Hình thức khóa luận (bố cục, phương pháp trình bày, tiếng Anh, trích dẫn)/ Thesis form (Layout, presentation methods, English, citation):";
			this.txtAttitude.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtAttitude.Location = new global::System.Drawing.Point(40, 373);
			this.txtAttitude.Multiline = true;
			this.txtAttitude.Name = "txtAttitude";
			this.txtAttitude.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtAttitude.Size = new global::System.Drawing.Size(968, 39);
			this.txtAttitude.TabIndex = 15;
			this.label9.AutoSize = true;
			this.label9.Location = new global::System.Drawing.Point(37, 343);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(831, 26);
			this.label9.TabIndex = 14;
			this.label9.Text = componentResourceManager.GetString("label9.Text");
			this.label10.AutoSize = true;
			this.label10.Location = new global::System.Drawing.Point(15, 416);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(425, 13);
			this.label10.TabIndex = 16;
			this.label10.Text = "4. Kết luận: Đạt ở mức nào? (hoặc không đạt)/ Conclusion: Pass at what stage? (Or not)";
			this.label11.AutoSize = true;
			this.label11.Location = new global::System.Drawing.Point(37, 431);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(574, 13);
			this.label11.TabIndex = 17;
			this.label11.Text = "4.1. Mức độ đạt được so với mục tiêu (so với đề cương)/ Achievement level compare to the target (compare to the plan)";
			this.txtAchievement.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtAchievement.Location = new global::System.Drawing.Point(40, 447);
			this.txtAchievement.Multiline = true;
			this.txtAchievement.Name = "txtAchievement";
			this.txtAchievement.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtAchievement.Size = new global::System.Drawing.Size(571, 39);
			this.txtAchievement.TabIndex = 18;
			this.txtLimitation.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtLimitation.Location = new global::System.Drawing.Point(629, 447);
			this.txtLimitation.Multiline = true;
			this.txtLimitation.Name = "txtLimitation";
			this.txtLimitation.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtLimitation.Size = new global::System.Drawing.Size(379, 39);
			this.txtLimitation.TabIndex = 20;
			this.label12.AutoSize = true;
			this.label12.Location = new global::System.Drawing.Point(626, 431);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(121, 13);
			this.label12.TabIndex = 19;
			this.label12.Text = "4.2.Hạn chế/ Limitation ";
			this.label13.AutoSize = true;
			this.label13.Location = new global::System.Drawing.Point(37, 491);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(286, 13);
			this.label13.TabIndex = 21;
			this.label13.Text = "4.3. Ý kiến của giảng viên/ Proposed Supervisor comment ";
			this.dgvComment.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dgvComment.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvComment.Location = new global::System.Drawing.Point(40, 507);
			this.dgvComment.Name = "dgvComment";
			this.dgvComment.Size = new global::System.Drawing.Size(749, 81);
			this.dgvComment.TabIndex = 22;
			this.dgvComment.CellEndEdit += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComment_CellEndEdit);
			this.btnSave.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnSave.Location = new global::System.Drawing.Point(400, 595);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new global::System.Drawing.Size(51, 23);
			this.btnSave.TabIndex = 23;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new global::System.EventHandler(this.btnSave_Click);
			this.txtStudents.BackColor = global::System.Drawing.Color.White;
			this.txtStudents.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtStudents.Location = new global::System.Drawing.Point(712, 66);
			this.txtStudents.Multiline = true;
			this.txtStudents.Name = "txtStudents";
			this.txtStudents.ReadOnly = true;
			this.txtStudents.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txtStudents.Size = new global::System.Drawing.Size(296, 102);
			this.txtStudents.TabIndex = 24;
			this.lblFileName.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.lblFileName.AutoSize = true;
			this.lblFileName.Location = new global::System.Drawing.Point(37, 601);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new global::System.Drawing.Size(55, 13);
			this.lblFileName.TabIndex = 25;
			this.lblFileName.Text = "File name:";
			this.txtCmtFileName.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtCmtFileName.Location = new global::System.Drawing.Point(98, 597);
			this.txtCmtFileName.Name = "txtCmtFileName";
			this.txtCmtFileName.Size = new global::System.Drawing.Size(259, 20);
			this.txtCmtFileName.TabIndex = 26;
			this.lblFileExt.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.lblFileExt.AutoSize = true;
			this.lblFileExt.Location = new global::System.Drawing.Point(359, 602);
			this.lblFileExt.Name = "lblFileExt";
			this.lblFileExt.Size = new global::System.Drawing.Size(33, 13);
			this.lblFileExt.TabIndex = 27;
			this.lblFileExt.Text = "(.cmt)";
			this.txtSavedFolder.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtSavedFolder.Location = new global::System.Drawing.Point(533, 597);
			this.txtSavedFolder.Name = "txtSavedFolder";
			this.txtSavedFolder.ReadOnly = true;
			this.txtSavedFolder.Size = new global::System.Drawing.Size(256, 20);
			this.txtSavedFolder.TabIndex = 28;
			this.lblSaveFolder.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.lblSaveFolder.AutoSize = true;
			this.lblSaveFolder.Location = new global::System.Drawing.Point(457, 601);
			this.lblSaveFolder.Name = "lblSaveFolder";
			this.lblSaveFolder.Size = new global::System.Drawing.Size(70, 13);
			this.lblSaveFolder.TabIndex = 29;
			this.lblSaveFolder.Text = "Saved folder:";
			this.lblHeader.AutoSize = true;
			this.lblHeader.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblHeader.ForeColor = global::System.Drawing.Color.Blue;
			this.lblHeader.Location = new global::System.Drawing.Point(15, 9);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new global::System.Drawing.Size(70, 18);
			this.lblHeader.TabIndex = 30;
			this.lblHeader.Text = "lblHeader";
			this.lblGuide.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.lblGuide.AutoSize = true;
			this.lblGuide.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblGuide.ForeColor = global::System.Drawing.Color.Blue;
			this.lblGuide.Location = new global::System.Drawing.Point(433, 491);
			this.lblGuide.Name = "lblGuide";
			this.lblGuide.Size = new global::System.Drawing.Size(356, 13);
			this.lblGuide.TabIndex = 31;
			this.lblGuide.Text = "Type x or X to the equivalent column to give your conclusion.";
			this.btnShow.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnShow.Location = new global::System.Drawing.Point(795, 594);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new global::System.Drawing.Size(67, 23);
			this.btnShow.TabIndex = 32;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = true;
			this.btnShow.Click += new global::System.EventHandler(this.btnShow_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(1014, 624);
			base.Controls.Add(this.btnShow);
			base.Controls.Add(this.lblGuide);
			base.Controls.Add(this.lblHeader);
			base.Controls.Add(this.lblSaveFolder);
			base.Controls.Add(this.txtSavedFolder);
			base.Controls.Add(this.lblFileExt);
			base.Controls.Add(this.txtCmtFileName);
			base.Controls.Add(this.lblFileName);
			base.Controls.Add(this.txtStudents);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.dgvComment);
			base.Controls.Add(this.label13);
			base.Controls.Add(this.txtLimitation);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.txtAchievement);
			base.Controls.Add(this.label11);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.txtAttitude);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.txtForm);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.txtContent);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.txtTitleEN);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.txtTitleVN);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnClose);
			base.Name = "FrmThesisComment";
			this.Text = "Comment for thesis of proposed supervisor";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.FrmThesisComment_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dgvComment).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000EA RID: 234
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000EB RID: 235
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x040000EC RID: 236
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000ED RID: 237
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040000EE RID: 238
		private global::System.Windows.Forms.TextBox txtTitleVN;

		// Token: 0x040000EF RID: 239
		private global::System.Windows.Forms.TextBox txtTitleEN;

		// Token: 0x040000F0 RID: 240
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040000F1 RID: 241
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040000F2 RID: 242
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040000F3 RID: 243
		private global::System.Windows.Forms.Label label6;

		// Token: 0x040000F4 RID: 244
		private global::System.Windows.Forms.Label label7;

		// Token: 0x040000F5 RID: 245
		private global::System.Windows.Forms.TextBox txtContent;

		// Token: 0x040000F6 RID: 246
		private global::System.Windows.Forms.TextBox txtForm;

		// Token: 0x040000F7 RID: 247
		private global::System.Windows.Forms.Label label8;

		// Token: 0x040000F8 RID: 248
		private global::System.Windows.Forms.TextBox txtAttitude;

		// Token: 0x040000F9 RID: 249
		private global::System.Windows.Forms.Label label9;

		// Token: 0x040000FA RID: 250
		private global::System.Windows.Forms.Label label10;

		// Token: 0x040000FB RID: 251
		private global::System.Windows.Forms.Label label11;

		// Token: 0x040000FC RID: 252
		private global::System.Windows.Forms.TextBox txtAchievement;

		// Token: 0x040000FD RID: 253
		private global::System.Windows.Forms.TextBox txtLimitation;

		// Token: 0x040000FE RID: 254
		private global::System.Windows.Forms.Label label12;

		// Token: 0x040000FF RID: 255
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04000100 RID: 256
		private global::System.Windows.Forms.DataGridView dgvComment;

		// Token: 0x04000101 RID: 257
		private global::System.Windows.Forms.Button btnSave;

		// Token: 0x04000102 RID: 258
		private global::System.Windows.Forms.TextBox txtStudents;

		// Token: 0x04000103 RID: 259
		private global::System.Windows.Forms.Label lblFileName;

		// Token: 0x04000104 RID: 260
		private global::System.Windows.Forms.TextBox txtCmtFileName;

		// Token: 0x04000105 RID: 261
		private global::System.Windows.Forms.Label lblFileExt;

		// Token: 0x04000106 RID: 262
		private global::System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

		// Token: 0x04000107 RID: 263
		private global::System.Windows.Forms.TextBox txtSavedFolder;

		// Token: 0x04000108 RID: 264
		private global::System.Windows.Forms.Label lblSaveFolder;

		// Token: 0x04000109 RID: 265
		private global::System.Windows.Forms.Label lblHeader;

		// Token: 0x0400010A RID: 266
		private global::System.Windows.Forms.Label lblGuide;

		// Token: 0x0400010B RID: 267
		private global::System.Windows.Forms.Button btnShow;
	}
}
