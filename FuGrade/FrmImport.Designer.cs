namespace FuGrade
{
	// Token: 0x0200000C RID: 12
	public partial class FrmImport : global::System.Windows.Forms.Form
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000BA30 File Offset: 0x00009C30
		private void InitializeComponent()
		{
			this.lblSubClass = new global::System.Windows.Forms.Label();
			this.lblMarkComponent = new global::System.Windows.Forms.Label();
			this.txtMark = new global::System.Windows.Forms.TextBox();
			this.btnImport = new global::System.Windows.Forms.Button();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.chkExcludeFirstRow = new global::System.Windows.Forms.CheckBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtLog = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btnSearch = new global::System.Windows.Forms.Button();
			this.label3 = new global::System.Windows.Forms.Label();
			this.txtValue = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.lblSubClass.AutoSize = true;
			this.lblSubClass.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblSubClass.ForeColor = global::System.Drawing.Color.Blue;
			this.lblSubClass.Location = new global::System.Drawing.Point(12, 9);
			this.lblSubClass.Name = "lblSubClass";
			this.lblSubClass.Size = new global::System.Drawing.Size(111, 17);
			this.lblSubClass.TabIndex = 0;
			this.lblSubClass.Text = "Subject/Class:";
			this.lblMarkComponent.AutoSize = true;
			this.lblMarkComponent.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblMarkComponent.ForeColor = global::System.Drawing.Color.Blue;
			this.lblMarkComponent.Location = new global::System.Drawing.Point(321, 9);
			this.lblMarkComponent.Name = "lblMarkComponent";
			this.lblMarkComponent.Size = new global::System.Drawing.Size(132, 17);
			this.lblMarkComponent.TabIndex = 1;
			this.lblMarkComponent.Text = "Mark component:";
			this.txtMark.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtMark.Location = new global::System.Drawing.Point(12, 70);
			this.txtMark.Multiline = true;
			this.txtMark.Name = "txtMark";
			this.txtMark.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txtMark.Size = new global::System.Drawing.Size(440, 439);
			this.txtMark.TabIndex = 2;
			this.btnImport.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnImport.Location = new global::System.Drawing.Point(12, 534);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new global::System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 3;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new global::System.EventHandler(this.btnImport_Click);
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new global::System.Drawing.Point(852, 533);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.chkExcludeFirstRow.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.chkExcludeFirstRow.AutoSize = true;
			this.chkExcludeFirstRow.Location = new global::System.Drawing.Point(12, 515);
			this.chkExcludeFirstRow.Name = "chkExcludeFirstRow";
			this.chkExcludeFirstRow.Size = new global::System.Drawing.Size(121, 17);
			this.chkExcludeFirstRow.TabIndex = 5;
			this.chkExcludeFirstRow.Text = "Exclude the first row";
			this.chkExcludeFirstRow.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(11, 49);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(150, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "Roll          Mark/Comments";
			this.txtLog.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtLog.BackColor = global::System.Drawing.SystemColors.ControlLightLight;
			this.txtLog.Location = new global::System.Drawing.Point(458, 70);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new global::System.Drawing.Size(469, 439);
			this.txtLog.TabIndex = 7;
			this.label2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(456, 49);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(82, 15);
			this.label2.TabIndex = 8;
			this.label2.Text = "Importing log:";
			this.btnSearch.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnSearch.Location = new global::System.Drawing.Point(405, 534);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new global::System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 9;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new global::System.EventHandler(this.btnSearch_Click);
			this.label3.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(175, 539);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(122, 15);
			this.label3.TabIndex = 10;
			this.label3.Text = "Search (roll or mark):";
			this.txtValue.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtValue.Location = new global::System.Drawing.Point(299, 536);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new global::System.Drawing.Size(100, 20);
			this.txtValue.TabIndex = 11;
			this.txtValue.TextChanged += new global::System.EventHandler(this.txtValue_TextChanged);
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(12, 31);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(106, 15);
			this.label4.TabIndex = 12;
			this.label4.Text = "Paste marks here:";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(939, 568);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.txtValue);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnSearch);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtLog);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.chkExcludeFirstRow);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnImport);
			base.Controls.Add(this.txtMark);
			base.Controls.Add(this.lblMarkComponent);
			base.Controls.Add(this.lblSubClass);
			base.Name = "FrmImport";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import Mark";
			base.Load += new global::System.EventHandler(this.FrmImport_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000A2 RID: 162
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000A3 RID: 163
		private global::System.Windows.Forms.Label lblSubClass;

		// Token: 0x040000A4 RID: 164
		private global::System.Windows.Forms.Label lblMarkComponent;

		// Token: 0x040000A5 RID: 165
		private global::System.Windows.Forms.TextBox txtMark;

		// Token: 0x040000A6 RID: 166
		private global::System.Windows.Forms.Button btnImport;

		// Token: 0x040000A7 RID: 167
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x040000A8 RID: 168
		private global::System.Windows.Forms.CheckBox chkExcludeFirstRow;

		// Token: 0x040000A9 RID: 169
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000AA RID: 170
		private global::System.Windows.Forms.TextBox txtLog;

		// Token: 0x040000AB RID: 171
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040000AC RID: 172
		private global::System.Windows.Forms.Button btnSearch;

		// Token: 0x040000AD RID: 173
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040000AE RID: 174
		private global::System.Windows.Forms.TextBox txtValue;

		// Token: 0x040000AF RID: 175
		private global::System.Windows.Forms.Label label4;
	}
}
