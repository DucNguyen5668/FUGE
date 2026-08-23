namespace FuGrade
{
	// Token: 0x0200000A RID: 10
	public partial class FrmFuGrade : global::System.Windows.Forms.Form
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00007650 File Offset: 0x00005850
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00007688 File Offset: 0x00005888
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.lblSubClass = new global::System.Windows.Forms.Label();
			this.btnOpenGradingFile = new global::System.Windows.Forms.Button();
			this.openFileDialog = new global::System.Windows.Forms.OpenFileDialog();
			this.txtGradingFile = new global::System.Windows.Forms.TextBox();
			this.cboSubClass = new global::System.Windows.Forms.ComboBox();
			this.btnShow = new global::System.Windows.Forms.Button();
			this.dgvGrading = new global::System.Windows.Forms.DataGridView();
			this.btnSearch = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txtRoll = new global::System.Windows.Forms.TextBox();
			this.btnSave = new global::System.Windows.Forms.Button();
			this.btnExit = new global::System.Windows.Forms.Button();
			this.lblGradingDetails = new global::System.Windows.Forms.Label();
			this.chkListBoxComp = new global::System.Windows.Forms.CheckedListBox();
			this.ctxMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.importMarkToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.clearMarkToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.chkBoxAll = new global::System.Windows.Forms.CheckBox();
			this.lblComp = new global::System.Windows.Forms.Label();
			this.lblTeacher = new global::System.Windows.Forms.Label();
			this.groupBoxAddStud = new global::System.Windows.Forms.GroupBox();
			this.btnAddComp = new global::System.Windows.Forms.Button();
			this.txtComp = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.btnAdd = new global::System.Windows.Forms.Button();
			this.txtName = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.txtRollNew = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lblSubjectClass = new global::System.Windows.Forms.Label();
			this.btnImportComments = new global::System.Windows.Forms.Button();
			this.lblGradingComp = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.chbMergeClass = new global::System.Windows.Forms.CheckBox();
			this.btnComment = new global::System.Windows.Forms.Button();
			this.btnEditCmtFile = new global::System.Windows.Forms.Button();
			this.btnDefenseGrading = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dgvGrading).BeginInit();
			this.ctxMenu.SuspendLayout();
			this.groupBoxAddStud.SuspendLayout();
			base.SuspendLayout();
			this.lblSubClass.AutoSize = true;
			this.lblSubClass.Location = new global::System.Drawing.Point(12, 67);
			this.lblSubClass.Name = "lblSubClass";
			this.lblSubClass.Size = new global::System.Drawing.Size(76, 13);
			this.lblSubClass.TabIndex = 0;
			this.lblSubClass.Text = "Subject/Class:";
			this.btnOpenGradingFile.Location = new global::System.Drawing.Point(15, 12);
			this.btnOpenGradingFile.Name = "btnOpenGradingFile";
			this.btnOpenGradingFile.Size = new global::System.Drawing.Size(107, 23);
			this.btnOpenGradingFile.TabIndex = 1;
			this.btnOpenGradingFile.Text = "Open Grading File";
			this.btnOpenGradingFile.UseVisualStyleBackColor = true;
			this.btnOpenGradingFile.Click += new global::System.EventHandler(this.btnOpenGradingFile_Click);
			this.openFileDialog.Filter = "FuGrade files|*.fg";
			this.txtGradingFile.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtGradingFile.Location = new global::System.Drawing.Point(128, 14);
			this.txtGradingFile.Name = "txtGradingFile";
			this.txtGradingFile.ReadOnly = true;
			this.txtGradingFile.Size = new global::System.Drawing.Size(1021, 20);
			this.txtGradingFile.TabIndex = 2;
			this.cboSubClass.FormattingEnabled = true;
			this.cboSubClass.Location = new global::System.Drawing.Point(102, 64);
			this.cboSubClass.Name = "cboSubClass";
			this.cboSubClass.Size = new global::System.Drawing.Size(220, 21);
			this.cboSubClass.TabIndex = 3;
			this.btnShow.Location = new global::System.Drawing.Point(328, 63);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new global::System.Drawing.Size(75, 23);
			this.btnShow.TabIndex = 4;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = true;
			this.btnShow.Click += new global::System.EventHandler(this.btnShow_Click);
			this.dgvGrading.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.dgvGrading.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvGrading.Location = new global::System.Drawing.Point(206, 107);
			this.dgvGrading.Name = "dgvGrading";
			this.dgvGrading.Size = new global::System.Drawing.Size(943, 214);
			this.dgvGrading.TabIndex = 5;
			this.dgvGrading.CellEndEdit += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrading_CellEndEdit);
			this.btnSearch.Location = new global::System.Drawing.Point(600, 62);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new global::System.Drawing.Size(56, 23);
			this.btnSearch.TabIndex = 6;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new global::System.EventHandler(this.btnSearch_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(427, 68);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(66, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Roll number:";
			this.txtRoll.Location = new global::System.Drawing.Point(496, 65);
			this.txtRoll.Name = "txtRoll";
			this.txtRoll.Size = new global::System.Drawing.Size(100, 20);
			this.txtRoll.TabIndex = 8;
			this.txtRoll.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.txtRoll_KeyDown);
			this.btnSave.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnSave.Location = new global::System.Drawing.Point(990, 406);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new global::System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new global::System.EventHandler(this.btnSave_Click);
			this.btnExit.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnExit.Location = new global::System.Drawing.Point(1073, 406);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new global::System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 10;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new global::System.EventHandler(this.btnExit_Click);
			this.lblGradingDetails.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.lblGradingDetails.AutoSize = true;
			this.lblGradingDetails.Location = new global::System.Drawing.Point(203, 91);
			this.lblGradingDetails.Name = "lblGradingDetails";
			this.lblGradingDetails.Size = new global::System.Drawing.Size(80, 13);
			this.lblGradingDetails.TabIndex = 11;
			this.lblGradingDetails.Text = "Grading details:";
			this.chkListBoxComp.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.chkListBoxComp.ContextMenuStrip = this.ctxMenu;
			this.chkListBoxComp.FormattingEnabled = true;
			this.chkListBoxComp.Location = new global::System.Drawing.Point(15, 107);
			this.chkListBoxComp.Name = "chkListBoxComp";
			this.chkListBoxComp.Size = new global::System.Drawing.Size(185, 214);
			this.chkListBoxComp.TabIndex = 13;
			this.chkListBoxComp.ItemCheck += new global::System.Windows.Forms.ItemCheckEventHandler(this.chkListBoxComp_ItemCheck);
			this.chkListBoxComp.SelectedIndexChanged += new global::System.EventHandler(this.chkListBoxComp_SelectedIndexChanged);
			this.chkListBoxComp.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.chkListBoxComp_MouseDown);
			this.ctxMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.importMarkToolStripMenuItem,
				this.clearMarkToolStripMenuItem
			});
			this.ctxMenu.Name = "ctxMenu";
			this.ctxMenu.Size = new global::System.Drawing.Size(141, 48);
			this.ctxMenu.Opening += new global::System.ComponentModel.CancelEventHandler(this.ctxMenu_Opening);
			this.importMarkToolStripMenuItem.Name = "importMarkToolStripMenuItem";
			this.importMarkToolStripMenuItem.Size = new global::System.Drawing.Size(140, 22);
			this.importMarkToolStripMenuItem.Text = "Import Mark";
			this.importMarkToolStripMenuItem.Click += new global::System.EventHandler(this.importMarkToolStripMenuItem_Click);
			this.clearMarkToolStripMenuItem.Name = "clearMarkToolStripMenuItem";
			this.clearMarkToolStripMenuItem.Size = new global::System.Drawing.Size(140, 22);
			this.clearMarkToolStripMenuItem.Text = "Clear Mark";
			this.clearMarkToolStripMenuItem.Click += new global::System.EventHandler(this.clearMarkToolStripMenuItem_Click);
			this.chkBoxAll.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.chkBoxAll.AutoSize = true;
			this.chkBoxAll.Location = new global::System.Drawing.Point(15, 325);
			this.chkBoxAll.Name = "chkBoxAll";
			this.chkBoxAll.Size = new global::System.Drawing.Size(73, 17);
			this.chkBoxAll.TabIndex = 14;
			this.chkBoxAll.Text = "Select All ";
			this.chkBoxAll.UseVisualStyleBackColor = true;
			this.chkBoxAll.CheckedChanged += new global::System.EventHandler(this.chkBoxAll_CheckedChanged);
			this.lblComp.AutoSize = true;
			this.lblComp.Location = new global::System.Drawing.Point(12, 91);
			this.lblComp.Name = "lblComp";
			this.lblComp.Size = new global::System.Drawing.Size(108, 13);
			this.lblComp.TabIndex = 15;
			this.lblComp.Text = "Grading components:";
			this.lblTeacher.AutoSize = true;
			this.lblTeacher.Location = new global::System.Drawing.Point(38, 48);
			this.lblTeacher.Name = "lblTeacher";
			this.lblTeacher.Size = new global::System.Drawing.Size(50, 13);
			this.lblTeacher.TabIndex = 16;
			this.lblTeacher.Text = "Teacher:";
			this.groupBoxAddStud.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxAddStud.Controls.Add(this.btnAddComp);
			this.groupBoxAddStud.Controls.Add(this.txtComp);
			this.groupBoxAddStud.Controls.Add(this.label4);
			this.groupBoxAddStud.Controls.Add(this.btnAdd);
			this.groupBoxAddStud.Controls.Add(this.txtName);
			this.groupBoxAddStud.Controls.Add(this.label3);
			this.groupBoxAddStud.Controls.Add(this.txtRollNew);
			this.groupBoxAddStud.Controls.Add(this.label1);
			this.groupBoxAddStud.Enabled = false;
			this.groupBoxAddStud.Location = new global::System.Drawing.Point(204, 357);
			this.groupBoxAddStud.Name = "groupBoxAddStud";
			this.groupBoxAddStud.Size = new global::System.Drawing.Size(746, 81);
			this.groupBoxAddStud.TabIndex = 17;
			this.groupBoxAddStud.TabStop = false;
			this.groupBoxAddStud.Text = "Add student, mark component";
			this.btnAddComp.Location = new global::System.Drawing.Point(274, 23);
			this.btnAddComp.Name = "btnAddComp";
			this.btnAddComp.Size = new global::System.Drawing.Size(135, 23);
			this.btnAddComp.TabIndex = 22;
			this.btnAddComp.Text = "Add Grading Component";
			this.btnAddComp.UseVisualStyleBackColor = true;
			this.btnAddComp.Click += new global::System.EventHandler(this.btnAddComp_Click);
			this.txtComp.Location = new global::System.Drawing.Point(135, 24);
			this.txtComp.Name = "txtComp";
			this.txtComp.Size = new global::System.Drawing.Size(133, 20);
			this.txtComp.TabIndex = 21;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(8, 29);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(126, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "New grading component:";
			this.btnAdd.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnAdd.Location = new global::System.Drawing.Point(473, 50);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new global::System.Drawing.Size(85, 23);
			this.btnAdd.TabIndex = 13;
			this.btnAdd.Text = "Add Student";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new global::System.EventHandler(this.btnAdd_Click);
			this.txtName.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtName.Location = new global::System.Drawing.Point(242, 53);
			this.txtName.Name = "txtName";
			this.txtName.Size = new global::System.Drawing.Size(225, 20);
			this.txtName.TabIndex = 12;
			this.label3.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(198, 58);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(38, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Name:";
			this.txtRollNew.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtRollNew.Location = new global::System.Drawing.Point(76, 53);
			this.txtRollNew.Name = "txtRollNew";
			this.txtRollNew.Size = new global::System.Drawing.Size(100, 20);
			this.txtRollNew.TabIndex = 10;
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(8, 58);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(66, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Roll number:";
			this.lblSubjectClass.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.lblSubjectClass.AutoSize = true;
			this.lblSubjectClass.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblSubjectClass.ForeColor = global::System.Drawing.Color.Blue;
			this.lblSubjectClass.Location = new global::System.Drawing.Point(312, 89);
			this.lblSubjectClass.Name = "lblSubjectClass";
			this.lblSubjectClass.Size = new global::System.Drawing.Size(106, 15);
			this.lblSubjectClass.TabIndex = 18;
			this.lblSubjectClass.Text = "lblSubjectClass";
			this.btnImportComments.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnImportComments.Location = new global::System.Drawing.Point(16, 387);
			this.btnImportComments.Name = "btnImportComments";
			this.btnImportComments.Size = new global::System.Drawing.Size(127, 23);
			this.btnImportComments.TabIndex = 19;
			this.btnImportComments.Text = "Import Comments";
			this.btnImportComments.UseVisualStyleBackColor = true;
			this.btnImportComments.Click += new global::System.EventHandler(this.btnImportComments_Click);
			this.lblGradingComp.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.lblGradingComp.AutoSize = true;
			this.lblGradingComp.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblGradingComp.ForeColor = global::System.Drawing.Color.Blue;
			this.lblGradingComp.Location = new global::System.Drawing.Point(160, 340);
			this.lblGradingComp.Name = "lblGradingComp";
			this.lblGradingComp.Size = new global::System.Drawing.Size(111, 15);
			this.lblGradingComp.TabIndex = 20;
			this.lblGradingComp.Text = "lblGradingComp";
			this.label5.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(13, 342);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(146, 13);
			this.label5.TabIndex = 21;
			this.label5.Text = "Selected grading component:";
			this.chbMergeClass.AutoSize = true;
			this.chbMergeClass.Location = new global::System.Drawing.Point(328, 40);
			this.chbMergeClass.Name = "chbMergeClass";
			this.chbMergeClass.Size = new global::System.Drawing.Size(94, 17);
			this.chbMergeClass.TabIndex = 22;
			this.chbMergeClass.Text = "Merge classes";
			this.chbMergeClass.UseVisualStyleBackColor = true;
			this.chbMergeClass.CheckedChanged += new global::System.EventHandler(this.chbMergeClass_CheckedChanged);
			this.btnComment.Enabled = false;
			this.btnComment.Location = new global::System.Drawing.Point(669, 62);
			this.btnComment.Name = "btnComment";
			this.btnComment.Size = new global::System.Drawing.Size(120, 23);
			this.btnComment.TabIndex = 23;
			this.btnComment.Text = "Comment For Thesis";
			this.btnComment.UseVisualStyleBackColor = true;
			this.btnComment.Click += new global::System.EventHandler(this.btnComment_Click);
			this.btnEditCmtFile.Location = new global::System.Drawing.Point(795, 62);
			this.btnEditCmtFile.Name = "btnEditCmtFile";
			this.btnEditCmtFile.Size = new global::System.Drawing.Size(200, 23);
			this.btnEditCmtFile.TabIndex = 24;
			this.btnEditCmtFile.Text = "Edit Comment For Thesis File (.cmt)";
			this.btnEditCmtFile.UseVisualStyleBackColor = true;
			this.btnEditCmtFile.Click += new global::System.EventHandler(this.btnEditCmtFile_Click);
			this.btnDefenseGrading.Location = new global::System.Drawing.Point(1007, 62);
			this.btnDefenseGrading.Name = "btnDefenseGrading";
			this.btnDefenseGrading.Size = new global::System.Drawing.Size(142, 23);
			this.btnDefenseGrading.TabIndex = 25;
			this.btnDefenseGrading.Text = "Thesis/CP Defense";
			this.btnDefenseGrading.UseVisualStyleBackColor = true;
			this.btnDefenseGrading.Click += new global::System.EventHandler(this.btnDefenseGrading_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(1161, 441);
			base.Controls.Add(this.btnDefenseGrading);
			base.Controls.Add(this.btnEditCmtFile);
			base.Controls.Add(this.btnComment);
			base.Controls.Add(this.chbMergeClass);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.lblGradingComp);
			base.Controls.Add(this.btnImportComments);
			base.Controls.Add(this.lblTeacher);
			base.Controls.Add(this.lblSubjectClass);
			base.Controls.Add(this.groupBoxAddStud);
			base.Controls.Add(this.lblComp);
			base.Controls.Add(this.chkListBoxComp);
			base.Controls.Add(this.chkBoxAll);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.lblGradingDetails);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.txtRoll);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btnSearch);
			base.Controls.Add(this.dgvGrading);
			base.Controls.Add(this.cboSubClass);
			base.Controls.Add(this.btnShow);
			base.Controls.Add(this.txtGradingFile);
			base.Controls.Add(this.btnOpenGradingFile);
			base.Controls.Add(this.lblSubClass);
			base.Name = "FrmFuGrade";
			this.Text = "FU Grading Editor 1.1 (20.08.20.21)";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.FrmFuGrade_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dgvGrading).EndInit();
			this.ctxMenu.ResumeLayout(false);
			this.groupBoxAddStud.ResumeLayout(false);
			this.groupBoxAddStud.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000057 RID: 87
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.Label lblSubClass;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.Button btnOpenGradingFile;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.OpenFileDialog openFileDialog;

		// Token: 0x0400005B RID: 91
		private global::System.Windows.Forms.TextBox txtGradingFile;

		// Token: 0x0400005C RID: 92
		private global::System.Windows.Forms.ComboBox cboSubClass;

		// Token: 0x0400005D RID: 93
		private global::System.Windows.Forms.Button btnShow;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.DataGridView dgvGrading;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.Button btnSearch;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000061 RID: 97
		private global::System.Windows.Forms.TextBox txtRoll;

		// Token: 0x04000062 RID: 98
		private global::System.Windows.Forms.Button btnSave;

		// Token: 0x04000063 RID: 99
		private global::System.Windows.Forms.Button btnExit;

		// Token: 0x04000064 RID: 100
		private global::System.Windows.Forms.Label lblGradingDetails;

		// Token: 0x04000065 RID: 101
		private global::System.Windows.Forms.CheckedListBox chkListBoxComp;

		// Token: 0x04000066 RID: 102
		private global::System.Windows.Forms.CheckBox chkBoxAll;

		// Token: 0x04000067 RID: 103
		private global::System.Windows.Forms.Label lblComp;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.Label lblTeacher;

		// Token: 0x04000069 RID: 105
		private global::System.Windows.Forms.GroupBox groupBoxAddStud;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.Button btnAdd;

		// Token: 0x0400006B RID: 107
		private global::System.Windows.Forms.TextBox txtName;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.TextBox txtRollNew;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.ContextMenuStrip ctxMenu;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.ToolStripMenuItem importMarkToolStripMenuItem;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.ToolStripMenuItem clearMarkToolStripMenuItem;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.Button btnAddComp;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.TextBox txtComp;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.Label lblSubjectClass;

		// Token: 0x04000076 RID: 118
		private global::System.Windows.Forms.Button btnImportComments;

		// Token: 0x04000077 RID: 119
		private global::System.Windows.Forms.Label lblGradingComp;

		// Token: 0x04000078 RID: 120
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.CheckBox chbMergeClass;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.Button btnComment;

		// Token: 0x0400007B RID: 123
		private global::System.Windows.Forms.Button btnEditCmtFile;

		// Token: 0x0400007C RID: 124
		private global::System.Windows.Forms.Button btnDefenseGrading;
	}
}
