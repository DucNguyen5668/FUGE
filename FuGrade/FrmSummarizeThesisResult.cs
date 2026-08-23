using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace FuGrade
{
	// Token: 0x0200000F RID: 15
	public partial class FrmSummarizeThesisResult : Form
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		public FrmSummarizeThesisResult()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000027AB File Offset: 0x000009AB
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000CD28 File Offset: 0x0000AF28
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			bool flag = DialogResult.OK == this.folderBrowserDialog.ShowDialog();
			if (flag)
			{
				this.txtFolder.Text = this.folderBrowserDialog.SelectedPath;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000CD64 File Offset: 0x0000AF64
		private void ScanDir(string d, TreeNode node)
		{
			string[] directories = Directory.GetDirectories(d);
			foreach (string text in directories)
			{
				int num = text.LastIndexOf("\\");
				TreeNode node2 = new TreeNode(text.Substring(num + 1));
				node.Nodes.Add(node2);
				this.ScanDir(text, node2);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		private void btnShow_Click(object sender, EventArgs e)
		{
			string text = this.txtFolder.Text;
			bool flag = Directory.Exists(text);
			if (flag)
			{
				this.treeGroups.Nodes.Clear();
				int num = this.txtFolder.Text.LastIndexOf("\\");
				this.root = new TreeNode(this.txtFolder.Text.Substring(num + 1));
				this.ScanDir(this.txtFolder.Text, this.root);
				this.treeGroups.Nodes.Add(this.root);
			}
			else
			{
				MessageBox.Show("The [" + text + "] does not exist!", "Show..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000CE84 File Offset: 0x0000B084
		private void treeGroups_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.listViewTef.Items.Clear();
			int length = this.txtFolder.Text.LastIndexOf("\\");
			string text = this.txtFolder.Text.Substring(0, length);
			text = text + "\\" + e.Node.FullPath;
			string[] files = Directory.GetFiles(text, "*.tef");
			foreach (string path in files)
			{
				this.listViewTef.Items.Add(Path.GetFileName(path));
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000CF24 File Offset: 0x0000B124
		private void btnValidate_Click(object sender, EventArgs e)
		{
			bool flag = this.IsFolderValid();
			if (flag)
			{
				MessageBox.Show(".tef folder is valid", "Validate tef folder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000CF50 File Offset: 0x0000B150
		private bool IsFolderValid()
		{
			string text = this.txtFolder.Text.Trim();
			bool flag = !Directory.Exists(text);
			bool result;
			if (flag)
			{
				MessageBox.Show("Folder [" + text + "] does not exist!", "Validate .tef folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				result = false;
			}
			else
			{
				string[] directories = Directory.GetDirectories(text);
				bool flag2 = directories.Length == 0;
				if (flag2)
				{
					MessageBox.Show("There is not any folders contain .tef files found!", "Validate .tef folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					result = false;
				}
				else
				{
					bool flag3 = false;
					foreach (string path in directories)
					{
						string[] files = Directory.GetFiles(path, "*.tef");
						bool flag4 = files.Length != 0;
						if (flag4)
						{
							flag3 = true;
							break;
						}
					}
					bool flag5 = !flag3;
					if (flag5)
					{
						MessageBox.Show("No .tef files found!", "Validate ,tef folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						result = false;
					}
					else
					{
						List<string> list = new List<string>();
						string[] array2 = directories;
						int num = 0;
						if (num >= array2.Length)
						{
							result = true;
						}
						else
						{
							string text2 = array2[num];
							string[] files2 = Directory.GetFiles(text2, "*.tef");
							foreach (string text3 in files2)
							{
								FileStream fileStream = new FileStream(text3, FileMode.Open);
								BinaryFormatter binaryFormatter = new BinaryFormatter();
								DefenseGrading defenseGrading = null;
								try
								{
									defenseGrading = (DefenseGrading)binaryFormatter.Deserialize(fileStream);
								}
								catch (Exception ex)
								{
									fileStream.Close();
									MessageBox.Show("Error: \r\nCheck [" + text3 + "]\r\n\r\n" + ex.Message, "Validate .tef folder", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return false;
								}
								fileStream.Close();
								string text4 = string.Concat(new string[]
								{
									defenseGrading.Semester,
									"-",
									defenseGrading.SubjectCode,
									"-",
									defenseGrading.ClassName,
									"-"
								});
								foreach (DefenseStudentGrade defenseStudentGrade in defenseGrading.GradeStudents)
								{
									text4 = text4 + defenseStudentGrade.Roll + "-";
								}
								bool flag6 = !list.Contains(text4);
								if (flag6)
								{
									list.Add(text4);
								}
							}
							bool flag7 = list.Count != 1;
							if (flag7)
							{
								MessageBox.Show(".tef files in [" + text2 + "] are not the same group!", "Validate .tef folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								result = false;
							}
							else
							{
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000D204 File Offset: 0x0000B404
		private void btnResult_Click(object sender, EventArgs e)
		{
			bool flag = this.IsFolderValid();
			if (flag)
			{
				string path = this.txtFolder.Text.Trim();
				this.listFG = new List<FinalGrade>();
				string[] directories = Directory.GetDirectories(path);
				int num = 0;
				this.listGT = new List<FrmSummarizeThesisResult.GradedTeacher>();
				foreach (string path2 in directories)
				{
					string[] files = Directory.GetFiles(path2, "*.tef");
					bool flag2 = false;
					float num2 = 0f;
					foreach (string path3 in files)
					{
						num++;
						FileStream fileStream = new FileStream(path3, FileMode.Open);
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						DefenseGrading defenseGrading = (DefenseGrading)binaryFormatter.Deserialize(fileStream);
						bool flag3 = num2 == 0f;
						if (flag3)
						{
							foreach (GradedItem gradedItem in defenseGrading.GradeStudents[0].GradedItems)
							{
								num2 += gradedItem.Scale;
							}
						}
						fileStream.Close();
						FrmSummarizeThesisResult.GradedTeacher gradedTeacher = new FrmSummarizeThesisResult.GradedTeacher();
						gradedTeacher.Name = defenseGrading.GradedTeacher;
						gradedTeacher.Subject = defenseGrading.SubjectCode;
						gradedTeacher.GroupName = defenseGrading.ClassName;
						gradedTeacher.Title = defenseGrading.TitleEN + "/" + defenseGrading.TitleVN;
						gradedTeacher.Supervisor = defenseGrading.Supervisor;
						gradedTeacher.DT = defenseGrading.GradedTime;
						this.listGT.Add(gradedTeacher);
						bool flag4 = !flag2;
						if (flag4)
						{
							foreach (DefenseStudentGrade defenseStudentGrade in defenseGrading.GradeStudents)
							{
								FinalGrade finalGrade = new FinalGrade();
								finalGrade.Roll = defenseStudentGrade.Roll;
								finalGrade.Name = defenseStudentGrade.Name;
								finalGrade.Supervisor = defenseGrading.Supervisor;
								bool flag5 = defenseGrading.TitleEN.Trim().Equals("");
								if (flag5)
								{
									finalGrade.Title = defenseGrading.TitleVN;
								}
								else
								{
									bool flag6 = defenseGrading.TitleVN.Trim().Equals("");
									if (flag6)
									{
										finalGrade.Title = defenseGrading.TitleEN;
									}
									else
									{
										finalGrade.Title = defenseGrading.TitleEN + "/" + defenseGrading.TitleVN;
									}
								}
								finalGrade.SubjectCode = defenseGrading.SubjectCode;
								finalGrade.ClassName = defenseGrading.ClassName;
								finalGrade.Semester = defenseGrading.Semester;
								finalGrade.DateTime = defenseGrading.GradedTime;
								finalGrade.Scale = num2;
								this.listFG.Add(finalGrade);
							}
							flag2 = true;
						}
						foreach (DefenseStudentGrade defenseStudentGrade2 in defenseGrading.GradeStudents)
						{
							foreach (FinalGrade finalGrade2 in this.listFG)
							{
								float num3 = 0f;
								bool flag7 = finalGrade2.Roll.Equals(defenseStudentGrade2.Roll);
								if (flag7)
								{
									foreach (GradedItem gradedItem2 in defenseStudentGrade2.GradedItems)
									{
										num3 += (float)Math.Round((decimal)gradedItem2.Mark, 1, MidpointRounding.AwayFromZero);
									}
									FinalGradeOfTeacher finalGradeOfTeacher = new FinalGradeOfTeacher();
									finalGradeOfTeacher.GradedTeacher = defenseGrading.GradedTeacher;
									finalGradeOfTeacher.Mark = (float)Math.Round((decimal)num3, 1, MidpointRounding.AwayFromZero);
									bool flag8 = defenseStudentGrade2.Conclusion == null;
									if (flag8)
									{
										finalGrade2.Note = "Disagree to defense";
									}
									else
									{
										bool flag9 = !defenseGrading.Note.Trim().Equals("");
										if (flag9)
										{
											FinalGrade finalGrade3 = finalGrade2;
											finalGrade3.Note = string.Concat(new string[]
											{
												finalGrade3.Note,
												defenseGrading.GradedTeacher,
												": ",
												defenseGrading.Note,
												"; "
											});
										}
									}
									FinalGrade finalGrade4 = finalGrade2;
									finalGrade4.GradedBy = finalGrade4.GradedBy + defenseGrading.GradedTeacher + ", ";
									finalGrade2.ListFGOT.Add(finalGradeOfTeacher);
								}
							}
						}
					}
				}
				this.dgvViewResult.DataSource = this.listFG;
				int num4 = 0;
				foreach (object obj in ((IEnumerable)this.dgvViewResult.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.HeaderCell.Value = (num4 + 1).ToString();
					num4++;
					bool flag10 = num4 < this.dgvViewResult.RowCount && !(this.listFG[num4].ClassName + this.listFG[num4].Title).Equals(this.listFG[num4 - 1].ClassName + this.listFG[num4 - 1].Title);
					if (flag10)
					{
						num4 = 0;
					}
				}
				this.dgvViewResult.ReadOnly = true;
				this.lblSum.Text = "Groups: " + directories.Length.ToString() + ", students: " + num.ToString();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		private void btnExport_Click(object sender, EventArgs e)
		{
			bool flag = this.listFG == null || this.listFG.Count == 0;
			if (flag)
			{
				MessageBox.Show("No final defense evaluation found.", "Export...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				_Application application = null;
				_Workbook workbook = null;
				_Worksheet worksheet = null;
				try
				{
					application = new ApplicationClass();
					workbook = application.Workbooks.Add(Type.Missing);
					worksheet = (_Worksheet)workbook.Sheets["Sheet1"];
					worksheet.Name = "Summary";
					int num = 12;
					int num2 = 0;
					int num3 = 0;
					int num4 = num + 1;
					int num5 = 0;
					for (int i = 0; i < this.listFG.Count; i++)
					{
						bool flag2 = num2 == 0;
						if (flag2)
						{
							num3++;
							worksheet.Cells[num3, 1] = "No";
							worksheet.Cells[num3, 2] = "Roll Number";
							worksheet.Cells[num3, 3] = "Full Name";
							worksheet.Cells[num3, 4] = "Subject code";
							worksheet.Cells[num3, 5] = "Class";
							worksheet.Cells[num3, 6] = "Semester";
							worksheet.Cells[num3, 7] = "Thesis title";
							worksheet.Cells[num3, 8] = "Supervisor";
							worksheet.Cells[num3, 9] = "Date-Time";
							worksheet.Cells[num3, 10] = "Group Note";
							worksheet.Cells[num3, 11] = "Mark";
							worksheet.Cells[num3, 12] = "Scale";
							num2++;
							FinalGrade finalGrade = this.listFG[i];
							num4 = num + 1;
							foreach (FinalGradeOfTeacher finalGradeOfTeacher in this.listFG[i].ListFGOT)
							{
								worksheet.Cells[num3, num4] = finalGradeOfTeacher.GradedTeacher;
								num4++;
							}
							num2++;
						}
						num3++;
						Range cells = worksheet.Cells;
						object rowIndex = num3;
						object columnIndex = 1;
						int num6;
						num5 = (num6 = num5 + 1);
						cells[rowIndex, columnIndex] = (num6.ToString() ?? "");
						worksheet.Cells[num3, 2] = this.listFG[i].Roll;
						worksheet.Cells[num3, 3] = this.listFG[i].Name;
						worksheet.Cells[num3, 4] = this.listFG[i].SubjectCode;
						worksheet.Cells[num3, 5] = this.listFG[i].ClassName;
						worksheet.Cells[num3, 6] = this.listFG[i].Semester;
						worksheet.Cells[num3, 7] = this.listFG[i].Title;
						worksheet.Cells[num3, 8] = this.listFG[i].Supervisor;
						worksheet.Cells[num3, 9] = this.listFG[i].DateTime.ToString();
						bool flag3 = num5 == 1;
						if (flag3)
						{
							worksheet.Cells[num3, 10] = this.listFG[i].Note;
						}
						bool flag4 = this.listFG[i].Note != null && this.listFG[i].Note.Equals("Disagree to defense");
						if (flag4)
						{
							worksheet.Cells[num3, 10] = "Disagree to defense";
						}
						worksheet.Cells[num3, 11] = this.listFG[i].AvgMark.ToString();
						worksheet.Cells[num3, 12] = this.listFG[i].Scale.ToString();
						num4 = num + 1;
						foreach (FinalGradeOfTeacher finalGradeOfTeacher2 in this.listFG[i].ListFGOT)
						{
							worksheet.Cells[num3, num4] = finalGradeOfTeacher2.Mark.ToString();
							num4++;
						}
						bool flag5 = i < this.listFG.Count - 1 && !this.listFG[i].Title.Equals(this.listFG[i + 1].Title);
						if (flag5)
						{
							num2 = 0;
							num5 = 0;
						}
					}
					_Worksheet worksheet2 = (_Worksheet)workbook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
					worksheet2.Name = "Graded statistics";
					worksheet2.Cells[1, 1] = "No";
					worksheet2.Cells[1, 2] = "Teacher";
					worksheet2.Cells[1, 3] = "Subject";
					worksheet2.Cells[1, 4] = "Group Name";
					worksheet2.Cells[1, 5] = "Title";
					worksheet2.Cells[1, 6] = "Supervisor";
					worksheet2.Cells[1, 7] = "Time";
					int num7 = 0;
					foreach (FrmSummarizeThesisResult.GradedTeacher gradedTeacher in this.listGT)
					{
						num7++;
						worksheet2.Cells[num7 + 1, 1] = num7;
						worksheet2.Cells[num7 + 1, 2] = gradedTeacher.Name;
						worksheet2.Cells[num7 + 1, 3] = gradedTeacher.Subject;
						worksheet2.Cells[num7 + 1, 4] = gradedTeacher.GroupName;
						worksheet2.Cells[num7 + 1, 5] = gradedTeacher.Title;
						worksheet2.Cells[num7 + 1, 6] = gradedTeacher.Supervisor;
						worksheet2.Cells[num7 + 1, 7] = gradedTeacher.DT.ToString();
					}
					MessageBox.Show("Export final defense evaluation successfully.", "Export...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					application.Visible = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Export final defense evaluation error.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				finally
				{
					workbook = null;
					worksheet = null;
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		private void btnCreateGradingItem_Click(object sender, EventArgs e)
		{
			FrmCreateFinalCPGradingItems frmCreateFinalCPGradingItems = new FrmCreateFinalCPGradingItems();
			frmCreateFinalCPGradingItems.Show();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		private void btnOpenFolder_Click(object sender, EventArgs e)
		{
			bool flag = this.txtFolder.Text.Trim().Equals("");
			if (!flag)
			{
				Process.Start(this.txtFolder.Text);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000E22F File Offset: 0x0000C42F
		private void FrmSummarizeThesisResult_Load(object sender, EventArgs e)
		{
			this.lblSum.Text = "";
		}

		// Token: 0x040000BD RID: 189
		private TreeNode root = null;

		// Token: 0x040000BE RID: 190
		private List<FrmSummarizeThesisResult.GradedTeacher> listGT = null;

		// Token: 0x040000BF RID: 191
		private List<FinalGrade> listFG = null;

		// Token: 0x02000019 RID: 25
		private class GradedTeacher
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000134 RID: 308 RVA: 0x000118C6 File Offset: 0x0000FAC6
			// (set) Token: 0x06000135 RID: 309 RVA: 0x000118CE File Offset: 0x0000FACE
			public string Name { get; set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000136 RID: 310 RVA: 0x000118D7 File Offset: 0x0000FAD7
			// (set) Token: 0x06000137 RID: 311 RVA: 0x000118DF File Offset: 0x0000FADF
			public string Subject { get; set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000138 RID: 312 RVA: 0x000118E8 File Offset: 0x0000FAE8
			// (set) Token: 0x06000139 RID: 313 RVA: 0x000118F0 File Offset: 0x0000FAF0
			public string GroupName { get; set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600013A RID: 314 RVA: 0x000118F9 File Offset: 0x0000FAF9
			// (set) Token: 0x0600013B RID: 315 RVA: 0x00011901 File Offset: 0x0000FB01
			public string Title { get; set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600013C RID: 316 RVA: 0x0001190A File Offset: 0x0000FB0A
			// (set) Token: 0x0600013D RID: 317 RVA: 0x00011912 File Offset: 0x0000FB12
			public string Supervisor { get; set; }

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600013E RID: 318 RVA: 0x0001191B File Offset: 0x0000FB1B
			// (set) Token: 0x0600013F RID: 319 RVA: 0x00011923 File Offset: 0x0000FB23
			public DateTime DT { get; set; }
		}
	}
}
