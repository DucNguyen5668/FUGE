namespace FuGrade
{
	// Token: 0x02000008 RID: 8
	public partial class FrmCreateFinalCPGradingItems : global::System.Windows.Forms.Form
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003004 File Offset: 0x00001204
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000303C File Offset: 0x0000123C
		private void InitializeComponent()
		{
			this.btnClose = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtGradingItem = new global::System.Windows.Forms.TextBox();
			this.txtScale = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txtSubjectCode = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.txtMajor = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.txtMinor = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.dgvGradingItems = new global::System.Windows.Forms.DataGridView();
			this.label6 = new global::System.Windows.Forms.Label();
			this.lblTotalScale = new global::System.Windows.Forms.Label();
			this.btnLoad = new global::System.Windows.Forms.Button();
			this.btnSave = new global::System.Windows.Forms.Button();
			this.button1 = new global::System.Windows.Forms.Button();
			this.btnDelete = new global::System.Windows.Forms.Button();
			this.txtItemGroup = new global::System.Windows.Forms.TextBox();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.lstAvailable = new global::System.Windows.Forms.ListBox();
			((global::System.ComponentModel.ISupportInitialize)this.dgvGradingItems).BeginInit();
			base.SuspendLayout();
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new global::System.Drawing.Point(676, 487);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(189, 93);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(70, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Grading Item:";
			this.txtGradingItem.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtGradingItem.Location = new global::System.Drawing.Point(261, 90);
			this.txtGradingItem.Multiline = true;
			this.txtGradingItem.Name = "txtGradingItem";
			this.txtGradingItem.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txtGradingItem.Size = new global::System.Drawing.Size(489, 123);
			this.txtGradingItem.TabIndex = 2;
			this.txtScale.Location = new global::System.Drawing.Point(261, 219);
			this.txtScale.Name = "txtScale";
			this.txtScale.Size = new global::System.Drawing.Size(39, 20);
			this.txtScale.TabIndex = 4;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(222, 222);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(37, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Scale:";
			this.txtSubjectCode.Location = new global::System.Drawing.Point(216, 12);
			this.txtSubjectCode.Name = "txtSubjectCode";
			this.txtSubjectCode.Size = new global::System.Drawing.Size(148, 20);
			this.txtSubjectCode.TabIndex = 6;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(140, 15);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(73, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Subject code:";
			this.txtMajor.Location = new global::System.Drawing.Point(216, 38);
			this.txtMajor.Name = "txtMajor";
			this.txtMajor.Size = new global::System.Drawing.Size(243, 20);
			this.txtMajor.TabIndex = 8;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(177, 41);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(36, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Major:";
			this.txtMinor.Location = new global::System.Drawing.Point(504, 38);
			this.txtMinor.Name = "txtMinor";
			this.txtMinor.Size = new global::System.Drawing.Size(246, 20);
			this.txtMinor.TabIndex = 10;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(465, 41);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(36, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Minor:";
			this.dgvGradingItems.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dgvGradingItems.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvGradingItems.Location = new global::System.Drawing.Point(7, 283);
			this.dgvGradingItems.Name = "dgvGradingItems";
			this.dgvGradingItems.ReadOnly = true;
			this.dgvGradingItems.Size = new global::System.Drawing.Size(744, 198);
			this.dgvGradingItems.TabIndex = 11;
			this.dgvGradingItems.CellClick += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGradingItems_CellClick);
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(4, 267);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(74, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Grading items:";
			this.lblTotalScale.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.lblTotalScale.AutoSize = true;
			this.lblTotalScale.Location = new global::System.Drawing.Point(4, 487);
			this.lblTotalScale.Name = "lblTotalScale";
			this.lblTotalScale.Size = new global::System.Drawing.Size(77, 13);
			this.lblTotalScale.TabIndex = 13;
			this.lblTotalScale.Text = "Grading Scale:";
			this.btnLoad.Location = new global::System.Drawing.Point(370, 10);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new global::System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 14;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new global::System.EventHandler(this.btnLoad_Click);
			this.btnSave.Location = new global::System.Drawing.Point(189, 245);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new global::System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 15;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new global::System.EventHandler(this.btnSave_Click);
			this.button1.Location = new global::System.Drawing.Point(289, 245);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(75, 23);
			this.button1.TabIndex = 16;
			this.button1.Text = "New";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.btnDelete.Location = new global::System.Drawing.Point(462, 245);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new global::System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 17;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new global::System.EventHandler(this.btnDelete_Click);
			this.txtItemGroup.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtItemGroup.Location = new global::System.Drawing.Point(216, 64);
			this.txtItemGroup.Name = "txtItemGroup";
			this.txtItemGroup.Size = new global::System.Drawing.Size(534, 20);
			this.txtItemGroup.TabIndex = 19;
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(153, 67);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(60, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Item group:";
			this.label8.AutoSize = true;
			this.label8.Location = new global::System.Drawing.Point(4, 12);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(53, 13);
			this.label8.TabIndex = 21;
			this.label8.Text = "Available:";
			this.lstAvailable.FormattingEnabled = true;
			this.lstAvailable.Location = new global::System.Drawing.Point(7, 28);
			this.lstAvailable.Name = "lstAvailable";
			this.lstAvailable.Size = new global::System.Drawing.Size(127, 238);
			this.lstAvailable.TabIndex = 22;
			this.lstAvailable.SelectedIndexChanged += new global::System.EventHandler(this.lstAvailable_SelectedIndexChanged);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(763, 516);
			base.Controls.Add(this.lstAvailable);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.txtItemGroup);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.btnDelete);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.btnLoad);
			base.Controls.Add(this.lblTotalScale);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.dgvGradingItems);
			base.Controls.Add(this.txtMinor);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.txtMajor);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.txtSubjectCode);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.txtScale);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtGradingItem);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnClose);
			base.Name = "FrmCreateFinalCPGradingItems";
			this.Text = "Create Final Thesis Grading Items";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.FrmCreateFinalCPGradingItems_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dgvGradingItems).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000027 RID: 39
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.TextBox txtGradingItem;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.TextBox txtScale;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.TextBox txtSubjectCode;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.TextBox txtMajor;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.TextBox txtMinor;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.DataGridView dgvGradingItems;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.Label lblTotalScale;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.Button btnLoad;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.Button btnSave;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.Button btnDelete;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.TextBox txtItemGroup;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.Label label7;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.ListBox lstAvailable;
	}
}
