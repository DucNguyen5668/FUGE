namespace FuGrade
{
	// Token: 0x0200000F RID: 15
	public partial class FrmSummarizeThesisResult : global::System.Windows.Forms.Form
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000E244 File Offset: 0x0000C444
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000E27C File Offset: 0x0000C47C
		private void InitializeComponent()
		{
			this.btnClose = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtFolder = new global::System.Windows.Forms.TextBox();
			this.btnBrowse = new global::System.Windows.Forms.Button();
			this.folderBrowserDialog = new global::System.Windows.Forms.FolderBrowserDialog();
			this.btnShow = new global::System.Windows.Forms.Button();
			this.listViewTef = new global::System.Windows.Forms.ListView();
			this.treeGroups = new global::System.Windows.Forms.TreeView();
			this.btnValidate = new global::System.Windows.Forms.Button();
			this.dgvViewResult = new global::System.Windows.Forms.DataGridView();
			this.btnResult = new global::System.Windows.Forms.Button();
			this.btnExport = new global::System.Windows.Forms.Button();
			this.btnCreateGradingItem = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.btnOpenFolder = new global::System.Windows.Forms.Button();
			this.lblSum = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.dgvViewResult).BeginInit();
			base.SuspendLayout();
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new global::System.Drawing.Point(772, 447);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(11, 28);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(54, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = ".tef folder:";
			this.txtFolder.Location = new global::System.Drawing.Point(69, 25);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new global::System.Drawing.Size(405, 20);
			this.txtFolder.TabIndex = 2;
			this.btnBrowse.Location = new global::System.Drawing.Point(480, 23);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new global::System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 3;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new global::System.EventHandler(this.btnBrowse_Click);
			this.btnShow.Location = new global::System.Drawing.Point(69, 51);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new global::System.Drawing.Size(137, 23);
			this.btnShow.TabIndex = 4;
			this.btnShow.Text = "Show Folder Details";
			this.btnShow.UseVisualStyleBackColor = true;
			this.btnShow.Click += new global::System.EventHandler(this.btnShow_Click);
			this.listViewTef.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.listViewTef.FullRowSelect = true;
			this.listViewTef.GridLines = true;
			this.listViewTef.Location = new global::System.Drawing.Point(351, 108);
			this.listViewTef.Name = "listViewTef";
			this.listViewTef.Size = new global::System.Drawing.Size(496, 154);
			this.listViewTef.TabIndex = 18;
			this.listViewTef.UseCompatibleStateImageBehavior = false;
			this.listViewTef.View = global::System.Windows.Forms.View.List;
			this.treeGroups.Location = new global::System.Drawing.Point(12, 108);
			this.treeGroups.Name = "treeGroups";
			this.treeGroups.Size = new global::System.Drawing.Size(333, 154);
			this.treeGroups.TabIndex = 17;
			this.treeGroups.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.treeGroups_AfterSelect);
			this.btnValidate.Location = new global::System.Drawing.Point(14, 268);
			this.btnValidate.Name = "btnValidate";
			this.btnValidate.Size = new global::System.Drawing.Size(75, 23);
			this.btnValidate.TabIndex = 19;
			this.btnValidate.Text = "Validate";
			this.btnValidate.UseVisualStyleBackColor = true;
			this.btnValidate.Click += new global::System.EventHandler(this.btnValidate_Click);
			this.dgvViewResult.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dgvViewResult.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvViewResult.Location = new global::System.Drawing.Point(14, 314);
			this.dgvViewResult.Name = "dgvViewResult";
			this.dgvViewResult.Size = new global::System.Drawing.Size(832, 124);
			this.dgvViewResult.TabIndex = 20;
			this.btnResult.Location = new global::System.Drawing.Point(111, 268);
			this.btnResult.Name = "btnResult";
			this.btnResult.Size = new global::System.Drawing.Size(75, 23);
			this.btnResult.TabIndex = 21;
			this.btnResult.Text = "Result";
			this.btnResult.UseVisualStyleBackColor = true;
			this.btnResult.Click += new global::System.EventHandler(this.btnResult_Click);
			this.btnExport.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnExport.Location = new global::System.Drawing.Point(14, 447);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new global::System.Drawing.Size(103, 23);
			this.btnExport.TabIndex = 22;
			this.btnExport.Text = "Export to Excel";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new global::System.EventHandler(this.btnExport_Click);
			this.btnCreateGradingItem.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnCreateGradingItem.Location = new global::System.Drawing.Point(734, 23);
			this.btnCreateGradingItem.Name = "btnCreateGradingItem";
			this.btnCreateGradingItem.Size = new global::System.Drawing.Size(112, 23);
			this.btnCreateGradingItem.TabIndex = 23;
			this.btnCreateGradingItem.Text = "Create Grading Item";
			this.btnCreateGradingItem.UseVisualStyleBackColor = true;
			this.btnCreateGradingItem.Click += new global::System.EventHandler(this.btnCreateGradingItem_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(12, 92);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(87, 13);
			this.label2.TabIndex = 24;
			this.label2.Text = ".tef folder details:";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(348, 92);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(46, 13);
			this.label3.TabIndex = 25;
			this.label3.Text = ".tef files:";
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(12, 298);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(34, 13);
			this.label4.TabIndex = 26;
			this.label4.Text = "Mark:";
			this.btnOpenFolder.Location = new global::System.Drawing.Point(579, 23);
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new global::System.Drawing.Size(85, 23);
			this.btnOpenFolder.TabIndex = 27;
			this.btnOpenFolder.Text = "Open Folder";
			this.btnOpenFolder.UseVisualStyleBackColor = true;
			this.btnOpenFolder.Click += new global::System.EventHandler(this.btnOpenFolder_Click);
			this.lblSum.AutoSize = true;
			this.lblSum.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblSum.Location = new global::System.Drawing.Point(207, 274);
			this.lblSum.Name = "lblSum";
			this.lblSum.Size = new global::System.Drawing.Size(50, 17);
			this.lblSum.TabIndex = 28;
			this.lblSum.Text = "lblSum";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(859, 482);
			base.Controls.Add(this.lblSum);
			base.Controls.Add(this.btnOpenFolder);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btnCreateGradingItem);
			base.Controls.Add(this.btnExport);
			base.Controls.Add(this.btnResult);
			base.Controls.Add(this.dgvViewResult);
			base.Controls.Add(this.btnValidate);
			base.Controls.Add(this.listViewTef);
			base.Controls.Add(this.treeGroups);
			base.Controls.Add(this.btnShow);
			base.Controls.Add(this.btnBrowse);
			base.Controls.Add(this.txtFolder);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnClose);
			base.Name = "FrmSummarizeThesisResult";
			this.Text = "Summarize Thesis Defense Result (16.09.20.21)";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.FrmSummarizeThesisResult_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dgvViewResult).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000C0 RID: 192
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000C1 RID: 193
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x040000C2 RID: 194
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000C3 RID: 195
		private global::System.Windows.Forms.TextBox txtFolder;

		// Token: 0x040000C4 RID: 196
		private global::System.Windows.Forms.Button btnBrowse;

		// Token: 0x040000C5 RID: 197
		private global::System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

		// Token: 0x040000C6 RID: 198
		private global::System.Windows.Forms.Button btnShow;

		// Token: 0x040000C7 RID: 199
		private global::System.Windows.Forms.ListView listViewTef;

		// Token: 0x040000C8 RID: 200
		private global::System.Windows.Forms.TreeView treeGroups;

		// Token: 0x040000C9 RID: 201
		private global::System.Windows.Forms.Button btnValidate;

		// Token: 0x040000CA RID: 202
		private global::System.Windows.Forms.DataGridView dgvViewResult;

		// Token: 0x040000CB RID: 203
		private global::System.Windows.Forms.Button btnResult;

		// Token: 0x040000CC RID: 204
		private global::System.Windows.Forms.Button btnExport;

		// Token: 0x040000CD RID: 205
		private global::System.Windows.Forms.Button btnCreateGradingItem;

		// Token: 0x040000CE RID: 206
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040000D0 RID: 208
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040000D1 RID: 209
		private global::System.Windows.Forms.Button btnOpenFolder;

		// Token: 0x040000D2 RID: 210
		private global::System.Windows.Forms.Label lblSum;
	}
}
