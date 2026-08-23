using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x0200000B RID: 11
	public partial class FrmEvaluationForm : Form
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00008CDE File Offset: 0x00006EDE
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00008CE6 File Offset: 0x00006EE6
		public string FinalSubjectCode { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00008CEF File Offset: 0x00006EEF
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00008CF7 File Offset: 0x00006EF7
		public string TefFile { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00008D00 File Offset: 0x00006F00
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00008D08 File Offset: 0x00006F08
		public bool IsEditMode { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00008D11 File Offset: 0x00006F11
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00008D19 File Offset: 0x00006F19
		public DefenseGrading DefenseGroup { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00008D22 File Offset: 0x00006F22
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00008D2A File Offset: 0x00006F2A
		public bool IsSaved { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00008D33 File Offset: 0x00006F33
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00008D3B File Offset: 0x00006F3B
		public ThesisComment TC { get; set; }

		// Token: 0x0600008D RID: 141 RVA: 0x00008D44 File Offset: 0x00006F44
		public FrmEvaluationForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00008D6C File Offset: 0x00006F6C
		private void btnClose_Click(object sender, EventArgs e)
		{
			bool flag = !this.IsSaved;
			if (flag)
			{
				bool flag2 = DialogResult.Yes == MessageBox.Show("Do you want to save the evaluation form?", "Close Evaluation Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (flag2)
				{
					this.btnSave.PerformClick();
					bool flag3 = !this.cancelClosing;
					if (flag3)
					{
						base.Close();
					}
				}
				else
				{
					base.Close();
				}
			}
			else
			{
				base.Close();
			}
			bool flag4 = this.ftc != null;
			if (flag4)
			{
				this.ftc.Close();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00008DF0 File Offset: 0x00006FF0
		private void FrmGroupGrade_Load(object sender, EventArgs e)
		{
			this.txtFileName.Text = Path.GetFileName(this.TefFile);
			this.txtTitleEN.Text = this.DefenseGroup.TitleEN;
			this.txtTitleVN.Text = this.DefenseGroup.TitleVN;
			this.lblSubjectCode.Text = "Subject code: " + this.DefenseGroup.SubjectCode;
			this.lblSupervisor.Text = "Supervisor: " + this.DefenseGroup.Supervisor;
			this.lblEvaluator.Text = "Evaluator: " + this.DefenseGroup.GradedTeacher;
			this.lblClassSem.Text = "Class: " + this.DefenseGroup.ClassName + "; Semester: " + this.DefenseGroup.Semester;
			bool flag = this.TefFile == null;
			if (flag)
			{
				string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
				string path = directoryName + "\\MasterFile\\FinalThesisGradingItems.master";
				bool flag2 = !File.Exists(path);
				if (flag2)
				{
					MessageBox.Show("Cannot find [MasterFile\\FinalThesisGradingItems.master]", "Grade", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					FileStream fileStream = new FileStream(path, FileMode.Open);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					List<FinalThesisGradingItem> list = (List<FinalThesisGradingItem>)binaryFormatter.Deserialize(fileStream);
					fileStream.Close();
					int num = 4 + this.DefenseGroup.GradeStudents.Count;
					this.dgvGrade.ColumnCount = num;
					this.dgvGrade.Columns[0].Name = "";
					this.dgvGrade.Columns[1].Name = "Criteria";
					this.dgvGrade.Columns[2].Name = "Max mark";
					this.dgvGrade.Columns[3].Name = "Group mark";
					this.dgvGrade.Columns[0].ReadOnly = true;
					this.dgvGrade.Columns[1].ReadOnly = true;
					this.dgvGrade.Columns[2].ReadOnly = true;
					num = 4;
					foreach (DefenseStudentGrade defenseStudentGrade in this.DefenseGroup.GradeStudents)
					{
						bool flag3 = defenseStudentGrade.Conclusion != null;
						if (flag3)
						{
							this.dgvGrade.Columns[num].Name = defenseStudentGrade.Name + " - " + defenseStudentGrade.Roll;
						}
						else
						{
							this.dgvGrade.Columns[num].Name = defenseStudentGrade.Name + " - " + defenseStudentGrade.Roll + " (disagree to defense)";
						}
						this.dgvGrade.Columns[num].Visible = true;
						num++;
					}
					float num2 = 0f;
					bool visible = false;
					List<string> list2 = new List<string>();
					foreach (FinalThesisGradingItem finalThesisGradingItem in list)
					{
						bool flag4 = finalThesisGradingItem.SubjectCode.Trim().ToUpper().Contains(this.DefenseGroup.SubjectCode.Trim().ToUpper());
						if (flag4)
						{
							bool flag5 = !list2.Contains(finalThesisGradingItem.SubjectCode.Trim().ToUpper());
							if (flag5)
							{
								list2.Add(finalThesisGradingItem.SubjectCode.Trim().ToUpper());
							}
						}
					}
					bool flag6 = list2.Count > 1;
					if (flag6)
					{
						new FrmChooseSujectCode
						{
							SubjectCodes = list2,
							FeF = this
						}.ShowDialog();
						this.DefenseGroup.SubjectCode = this.FinalSubjectCode;
					}
					object[] values;
					foreach (FinalThesisGradingItem finalThesisGradingItem2 in list)
					{
						bool flag7 = finalThesisGradingItem2.SubjectCode.Trim().ToUpper().Equals(this.DefenseGroup.SubjectCode.Trim().ToUpper());
						if (flag7)
						{
							List<string> list3 = new List<string>();
							list3.Add(finalThesisGradingItem2.ItemGroup);
							bool flag8 = !finalThesisGradingItem2.ItemGroup.Equals("");
							if (flag8)
							{
								visible = true;
							}
							list3.Add(finalThesisGradingItem2.GradingItem);
							num2 += finalThesisGradingItem2.Scale;
							list3.Add(finalThesisGradingItem2.Scale.ToString());
							list3.Add(null);
							foreach (DefenseStudentGrade defenseStudentGrade2 in this.DefenseGroup.GradeStudents)
							{
								list3.Add(null);
								GradedItem gradedItem = new GradedItem();
								gradedItem.ItemName = finalThesisGradingItem2.GradingItem;
								gradedItem.Scale = finalThesisGradingItem2.Scale;
								defenseStudentGrade2.GradedItems.Add(gradedItem);
							}
							DataGridViewRowCollection rows = this.dgvGrade.Rows;
							values = list3.ToArray();
							rows.Add(values);
						}
					}
					List<string> list4 = new List<string>();
					list4.Add(null);
					list4.Add(null);
					list4.Add(num2.ToString());
					list4.Add(null);
					foreach (DefenseStudentGrade defenseStudentGrade3 in this.DefenseGroup.GradeStudents)
					{
						list4.Add(null);
					}
					DataGridViewRowCollection rows2 = this.dgvGrade.Rows;
					values = list4.ToArray();
					rows2.Add(values);
					for (int i = 0; i < this.dgvGrade.Rows.Count - 2; i++)
					{
						this.dgvGrade.Rows[i].HeaderCell.Value = (i + 1).ToString();
					}
					this.dgvGrade.AllowUserToAddRows = false;
					foreach (object obj in this.dgvGrade.Columns)
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
						dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
					}
					this.dgvGrade.Columns[0].Visible = visible;
					this.dgvGrade.RowHeadersWidth = 60;
				}
			}
			else
			{
				this.txtSavedAt.Text = Path.GetDirectoryName(this.TefFile);
				this.txtNote.Text = this.DefenseGroup.Note;
				int num3 = 4 + this.DefenseGroup.GradeStudents.Count;
				this.dgvGrade.ColumnCount = num3;
				this.dgvGrade.Columns[0].Name = "";
				this.dgvGrade.Columns[1].Name = "Criteria";
				this.dgvGrade.Columns[2].Name = "Max mark";
				this.dgvGrade.Columns[3].Name = "Group mark";
				this.dgvGrade.Columns[0].ReadOnly = true;
				this.dgvGrade.Columns[1].ReadOnly = true;
				this.dgvGrade.Columns[2].ReadOnly = true;
				num3 = 4;
				foreach (DefenseStudentGrade defenseStudentGrade4 in this.DefenseGroup.GradeStudents)
				{
					bool flag9 = defenseStudentGrade4.Conclusion != null;
					if (flag9)
					{
						this.dgvGrade.Columns[num3].Name = defenseStudentGrade4.Name + " - " + defenseStudentGrade4.Roll;
					}
					else
					{
						this.dgvGrade.Columns[num3].Name = defenseStudentGrade4.Name + " - " + defenseStudentGrade4.Roll + " (disagree to defense)";
					}
					this.dgvGrade.Columns[num3].Visible = true;
					num3++;
				}
				float num4 = 0f;
				bool visible2 = false;
				float num5 = 0f;
				int num6 = 0;
				object[] values;
				foreach (GradedItem gradedItem2 in this.DefenseGroup.GradeStudents[0].GradedItems)
				{
					List<string> list5 = new List<string>();
					list5.Add(gradedItem2.GroupItem);
					bool flag10 = gradedItem2.GroupItem != null;
					if (flag10)
					{
						visible2 = true;
					}
					list5.Add(gradedItem2.ItemName);
					list5.Add(gradedItem2.Scale.ToString());
					num4 += gradedItem2.Scale;
					list5.Add(gradedItem2.GroupMark.ToString());
					num5 += gradedItem2.GroupMark;
					for (int j = 0; j < this.DefenseGroup.GradeStudents.Count; j++)
					{
						list5.Add(this.DefenseGroup.GradeStudents[j].GradedItems[num6].Mark.ToString());
					}
					num6++;
					DataGridViewRowCollection rows3 = this.dgvGrade.Rows;
					values = list5.ToArray();
					rows3.Add(values);
				}
				List<string> list6 = new List<string>();
				list6.Add(null);
				list6.Add(null);
				list6.Add(num4.ToString());
				list6.Add(num5.ToString());
				foreach (DefenseStudentGrade defenseStudentGrade5 in this.DefenseGroup.GradeStudents)
				{
					float num7 = 0f;
					foreach (GradedItem gradedItem3 in defenseStudentGrade5.GradedItems)
					{
						num7 += gradedItem3.Mark;
					}
					list6.Add(num7.ToString());
				}
				DataGridViewRowCollection rows4 = this.dgvGrade.Rows;
				values = list6.ToArray();
				rows4.Add(values);
				this.txtNote.Text = this.DefenseGroup.Note;
				for (int k = 0; k < this.dgvGrade.Rows.Count - 2; k++)
				{
					this.dgvGrade.Rows[k].HeaderCell.Value = (k + 1).ToString();
				}
				this.dgvGrade.AllowUserToAddRows = false;
				foreach (object obj2 in this.dgvGrade.Columns)
				{
					DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)obj2;
					dataGridViewColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				this.dgvGrade.Columns[0].Visible = visible2;
				this.dgvGrade.RowHeadersWidth = 60;
				bool flag11 = !this.IsEditMode;
				if (flag11)
				{
					this.dgvGrade.ReadOnly = true;
					this.btnSave.Enabled = (this.btnCopyGroupMark.Enabled = false);
					this.txtNote.ReadOnly = true;
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00009B38 File Offset: 0x00007D38
		private void dgvGrade_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.ColumnIndex <= 2;
			if (!flag)
			{
				bool flag2 = this.dgvGrade.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null;
				if (!flag2)
				{
					float num = Convert.ToSingle(this.dgvGrade.Rows[e.RowIndex].Cells[2].Value.ToString());
					string s = this.dgvGrade.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
					float num2 = 0f;
					bool flag3 = float.TryParse(s, out num2);
					bool flag4 = flag3;
					if (flag4)
					{
						bool flag5 = num2 > num;
						if (flag5)
						{
							MessageBox.Show("Invalid input, [" + this.dgvGrade.Rows[e.RowIndex].Cells[1].Value.ToString() + "] mark must be <= " + num.ToString(), "Input mark", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							this.dgvGrade.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
						}
						bool flag6 = num2 < 0f;
						if (flag6)
						{
							MessageBox.Show("Invalid input, mark must be a positive decimal!", "Input mark", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							this.dgvGrade.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
						}
					}
					else
					{
						MessageBox.Show("Invalid input, mark must be a positive decimal!", "Input mark", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.dgvGrade.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
					}
					float num3 = 0f;
					for (int i = 0; i < this.dgvGrade.RowCount - 1; i++)
					{
						float num4 = 0f;
						bool flag7 = this.dgvGrade.Rows[i].Cells[e.ColumnIndex].Value != null;
						if (flag7)
						{
							num4 = Convert.ToSingle(this.dgvGrade.Rows[i].Cells[e.ColumnIndex].Value.ToString());
						}
						num3 += num4;
					}
					this.dgvGrade.Rows[this.dgvGrade.RowCount - 1].Cells[e.ColumnIndex].Value = num3;
					this.IsSaved = false;
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00009E10 File Offset: 0x00008010
		private void dgvGrade_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = this.dgvGrade.Rows.Count - 1 == e.RowIndex;
			if (flag)
			{
				this.dgvGrade.Columns[e.ColumnIndex].ReadOnly = true;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00009E5C File Offset: 0x0000805C
		private void dgvGrade_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = this.dgvGrade.Rows.Count - 1 == e.RowIndex;
			if (flag)
			{
				this.dgvGrade.Columns[e.ColumnIndex].ReadOnly = false;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00009EA8 File Offset: 0x000080A8
		private void btnCopyGroupMark_Click(object sender, EventArgs e)
		{
			int num = 4;
			for (int i = 0; i < this.dgvGrade.Rows.Count; i++)
			{
				for (int j = num; j < this.dgvGrade.ColumnCount; j++)
				{
					this.dgvGrade.Rows[i].Cells[j].Value = this.dgvGrade.Rows[i].Cells[3].Value;
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00009F3C File Offset: 0x0000813C
		private void btnSave_Click(object sender, EventArgs e)
		{
			this.DefenseGroup.GradedTime = DateTime.Now;
			string text = this.DefenseGroup.GradedTeacher;
			text = text.Trim();
			text = text.Replace(" ", "_");
			bool flag = this.TefFile == null;
			if (flag)
			{
				bool flag4;
				do
				{
					bool flag2 = DialogResult.OK == this.folderBrowserDialog.ShowDialog();
					if (!flag2)
					{
						goto IL_199;
					}
					string selectedPath = this.folderBrowserDialog.SelectedPath;
					this.txtSavedAt.Text = selectedPath;
					string roll = this.DefenseGroup.GradeStudents[0].Roll;
					string str = string.Concat(new string[]
					{
						text,
						"_",
						this.DefenseGroup.SubjectCode,
						"_",
						roll,
						"_",
						this.DefenseGroup.GradedTime.Year.ToString(),
						this.DefenseGroup.GradedTime.Month.ToString(),
						this.DefenseGroup.GradedTime.Day.ToString(),
						"_",
						this.DefenseGroup.Supervisor,
						".tef"
					});
					this.TefFile = selectedPath + "\\" + str;
					bool flag3 = File.Exists(this.TefFile);
					if (!flag3)
					{
						break;
					}
					flag4 = (DialogResult.No == MessageBox.Show("The evaluation form file (.tef) exists, do you want to overwrite?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
				}
				while (flag4);
				goto IL_1AE;
				IL_199:
				this.cancelClosing = true;
				return;
				IL_1AE:
				this.txtFileName.Text = Path.GetFileName(this.TefFile);
			}
			bool flag5 = this.TefFile != null;
			if (flag5)
			{
				int index = 3;
				for (int i = 0; i < this.dgvGrade.RowCount - 1; i++)
				{
					bool flag6 = this.dgvGrade.Rows[i].Cells[index].Value != null;
					if (flag6)
					{
						float groupMark = Convert.ToSingle(this.dgvGrade.Rows[i].Cells[index].Value.ToString());
						this.DefenseGroup.GradeStudents[0].GradedItems[i].GroupMark = groupMark;
					}
				}
				int num = 4;
				for (int j = 0; j < this.dgvGrade.RowCount - 1; j++)
				{
					for (int k = num; k < this.dgvGrade.ColumnCount; k++)
					{
						bool flag7 = this.dgvGrade.Rows[j].Cells[k].Value != null;
						if (flag7)
						{
							float mark = Convert.ToSingle(this.dgvGrade.Rows[j].Cells[k].Value.ToString());
							this.DefenseGroup.GradeStudents[k - num].GradedItems[j].Mark = mark;
						}
					}
				}
				this.DefenseGroup.Note = this.txtNote.Text;
				try
				{
					FileStream fileStream = new FileStream(this.TefFile, FileMode.Create);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(fileStream, this.DefenseGroup);
					fileStream.Close();
					this.IsSaved = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error:\r\n" + ex.Message, "Save evaluation form", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.TefFile = null;
					this.txtFileName.Text = "";
					this.txtSavedAt.Text = "";
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000A360 File Offset: 0x00008560
		private void btnShow_Click(object sender, EventArgs e)
		{
			bool flag = this.txtSavedAt.Text.Trim().Equals("");
			if (!flag)
			{
				Process.Start(this.txtSavedAt.Text);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000A3A0 File Offset: 0x000085A0
		private void btnShowSupComment_Click(object sender, EventArgs e)
		{
			bool flag = this.ftc == null || this.ftc.IsDisposed;
			if (flag)
			{
				this.ftc = new FrmThesisComment();
				this.ftc.DisplayThesisComment(this.TC);
			}
			this.ftc.Show();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000A3F3 File Offset: 0x000085F3
		private void txtNote_TextChanged(object sender, EventArgs e)
		{
			this.IsSaved = false;
		}

		// Token: 0x04000083 RID: 131
		private bool cancelClosing = false;

		// Token: 0x04000084 RID: 132
		private FrmThesisComment ftc = null;
	}
}
