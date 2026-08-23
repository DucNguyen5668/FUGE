using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x0200000C RID: 12
	public partial class FrmImport : Form
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000B2C5 File Offset: 0x000094C5
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000B2CD File Offset: 0x000094CD
		public string SubjectClass { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000B2D6 File Offset: 0x000094D6
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000B2DE File Offset: 0x000094DE
		public string MarkComponent { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000B2E7 File Offset: 0x000094E7
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000B2EF File Offset: 0x000094EF
		public bool IsInputStatus { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000B2F8 File Offset: 0x000094F8
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000B300 File Offset: 0x00009500
		public bool IsImportComments { get; set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x0000B309 File Offset: 0x00009509
		public FrmImport(FrmFuGrade fg)
		{
			this.InitializeComponent();
			this.fg = fg;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000027AB File Offset: 0x000009AB
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000B336 File Offset: 0x00009536
		private void FrmImport_Load(object sender, EventArgs e)
		{
			this.lblSubClass.Text = "Subject/Class: " + this.SubjectClass;
			this.lblMarkComponent.Text = "Mark component: " + this.MarkComponent;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000B374 File Offset: 0x00009574
		private void btnImport_Click(object sender, EventArgs e)
		{
			bool isImportComments = this.IsImportComments;
			if (isImportComments)
			{
				this.ImportComments();
			}
			else
			{
				this.ImportGrade();
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000B3A0 File Offset: 0x000095A0
		private void ImportComments()
		{
			string[] array = this.txtMark.Text.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string text = "Invalid input:\r\n";
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			bool @checked = this.chkExcludeFirstRow.Checked;
			if (@checked)
			{
				num = 1;
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			for (int i = num; i < array.Length; i++)
			{
				bool flag3 = array[i].Trim().Equals("");
				if (!flag3)
				{
					string[] array2 = array[i].Split(new string[]
					{
						"\t",
						" "
					}, StringSplitOptions.RemoveEmptyEntries);
					bool flag4 = list.Contains(array2[0].Trim().ToUpper());
					if (flag4)
					{
						flag2 = true;
						bool flag5 = !list2.Contains(array2[0]);
						if (flag5)
						{
							text = text + "\r\nThere are more than one student with roll='" + array2[0] + "' in the list!";
						}
						list2.Add(array2[0]);
					}
					else
					{
						list.Add(array2[0].Trim().ToUpper());
					}
				}
			}
			bool flag6 = flag2;
			if (flag6)
			{
				flag = false;
			}
			bool flag7 = !flag;
			if (flag7)
			{
				MessageBox.Show(text, "Import comments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				text = text + "\r\n\r\nNo 'Comments' have been imported to " + this.SubjectClass;
				this.txtLog.Text = text;
			}
			else
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int j = num; j < array.Length; j++)
				{
					bool flag8 = array[j].Trim().Equals("");
					if (!flag8)
					{
						string[] array3 = array[j].Split(new string[]
						{
							"\t",
							" "
						}, StringSplitOptions.RemoveEmptyEntries);
						string text2 = "";
						for (int k = 1; k < array3.Length; k++)
						{
							text2 = text2 + array3[k] + " ";
						}
						dictionary[array3[0]] = text2.Trim();
					}
				}
				text = this.fg.ImportComments(dictionary, this.SubjectClass);
				this.txtLog.Text = "Import result:\r\n" + text;
				this.fg.NeedSave = true;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000B5FC File Offset: 0x000097FC
		private void ImportGrade()
		{
			string[] array = this.txtMark.Text.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string text = "Invalid input:\r\n";
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			bool @checked = this.chkExcludeFirstRow.Checked;
			if (@checked)
			{
				num = 1;
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			for (int i = num; i < array.Length; i++)
			{
				bool flag3 = array[i].Trim().Equals("");
				if (!flag3)
				{
					string[] array2 = array[i].Split(new string[]
					{
						"\t",
						" "
					}, StringSplitOptions.RemoveEmptyEntries);
					bool flag4 = array2.Length != 2;
					if (flag4)
					{
						text = text + "\r\nWrong format: " + array[i];
						flag = false;
					}
					else
					{
						bool flag5 = !this.IsInputStatus;
						if (flag5)
						{
							float num2;
							bool flag6 = float.TryParse(array2[1], out num2);
							bool flag7 = flag6;
							if (flag7)
							{
								bool flag8 = num2 < 0f | num2 > 10f;
								if (flag8)
								{
									text = text + "\r\nMark is NOT in range [1.0, 10], check line: " + array[i].Trim();
									flag = false;
								}
							}
							else
							{
								text = text + "\r\nMark must be a number in range [1.0, 10], check line: " + array[i].Trim();
								flag = false;
							}
						}
						else
						{
							bool flag9 = !array2[1].Trim().Equals("1") && !array2[1].Trim().Equals("0");
							if (flag9)
							{
								text = text + "\r\nGrade value must be 1 or 0 (1=pass, 0=fail), check line: " + array[i].Trim();
								flag = false;
							}
						}
					}
					bool flag10 = flag;
					if (flag10)
					{
						bool flag11 = list.Contains(array2[0].Trim().ToUpper());
						if (flag11)
						{
							flag2 = true;
							bool flag12 = !list2.Contains(array2[0]);
							if (flag12)
							{
								text = text + "\r\nThere are more than one student with roll='" + array2[0] + "' in the list!";
							}
							list2.Add(array2[0]);
						}
						else
						{
							list.Add(array2[0].Trim().ToUpper());
						}
					}
				}
			}
			bool flag13 = flag2;
			if (flag13)
			{
				flag = false;
			}
			bool flag14 = !flag;
			if (flag14)
			{
				MessageBox.Show(text, "Import mark", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				text = string.Concat(new string[]
				{
					text,
					"\r\n\r\nNo '",
					this.MarkComponent,
					"' mark have been imported to ",
					this.SubjectClass
				});
				this.txtLog.Text = text;
			}
			else
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int j = num; j < array.Length; j++)
				{
					bool flag15 = array[j].Trim().Equals("");
					if (!flag15)
					{
						string[] array3 = array[j].Split(new string[]
						{
							"\t",
							" "
						}, StringSplitOptions.RemoveEmptyEntries);
						dictionary[array3[0]] = array3[1];
					}
				}
				text = this.fg.ImportMark(dictionary, this.SubjectClass, this.MarkComponent);
				this.txtLog.Text = "Import result:\r\n" + text;
				this.fg.NeedSave = true;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000B93C File Offset: 0x00009B3C
		private void btnSearch_Click(object sender, EventArgs e)
		{
			string text = this.txtValue.Text.Trim().ToUpper();
			int num = (this.pos >= 0) ? (this.pos + text.Length) : 0;
			this.pos = this.txtMark.Text.ToUpper().IndexOf(text, num);
			bool flag = this.pos >= 0;
			if (flag)
			{
				this.txtMark.Select(this.pos, text.Length);
				this.txtMark.Focus();
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					MessageBox.Show("Not found!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000B9EC File Offset: 0x00009BEC
		private void txtValue_TextChanged(object sender, EventArgs e)
		{
			this.pos = -1;
		}

		// Token: 0x040000A0 RID: 160
		private FrmFuGrade fg = null;

		// Token: 0x040000A1 RID: 161
		private int pos = -1;
	}
}
