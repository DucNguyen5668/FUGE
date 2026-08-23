using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows.Forms;
using FuGradeLib;
using Newtonsoft.Json;

namespace FuGrade
{
	// Token: 0x0200000A RID: 10
	public partial class FrmFuGrade : Form
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004D18 File Offset: 0x00002F18
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00004D20 File Offset: 0x00002F20
		public bool NeedSave { get; set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00004D29 File Offset: 0x00002F29
		public FrmFuGrade()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004D68 File Offset: 0x00002F68
		private void btnExit_Click(object sender, EventArgs e)
		{
			bool needSave = this.NeedSave;
			if (needSave)
			{
				DialogResult dialogResult = MessageBox.Show("Do you want to save the current mark sheet?", "Save mark sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				bool flag = dialogResult == DialogResult.Yes;
				if (flag)
				{
					this.btnSave.PerformClick();
				}
				this.NeedSave = false;
			}
			Application.Exit();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004DBC File Offset: 0x00002FBC
		private void btnOpenGradingFile_Click(object sender, EventArgs e)
		{
			bool needSave = this.NeedSave;
			if (needSave)
			{
				DialogResult dialogResult = MessageBox.Show("Do you want to save the current mark sheet?", "Save mark sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				bool flag = dialogResult == DialogResult.Yes;
				if (flag)
				{
					this.btnSave.PerformClick();
				}
				this.NeedSave = false;
			}
			DialogResult dialogResult2 = this.openFileDialog.ShowDialog();
			bool flag2 = dialogResult2 == DialogResult.OK;
			if (flag2)
			{
				string fileName = this.openFileDialog.FileName;
				this.txtGradingFile.Text = fileName;
				try
				{
					TeacherGrade teacherGrade = new TeacherGrade();
					using (StreamReader streamReader = new StreamReader(this.txtGradingFile.Text))
					{
						string text = streamReader.ReadToEnd();
						text = AesOperation.DecryptString(null, text);
						teacherGrade = JsonConvert.DeserializeObject<TeacherGrade>(text);
					}
					this.tg = teacherGrade;
				}
				catch
				{
					try
					{
						FileStream fileStream = new FileStream(this.txtGradingFile.Text, FileMode.Open);
						this.tg = (TeacherGrade)new BinaryFormatter
						{
							AssemblyFormat = FormatterAssemblyStyle.Simple
						}.Deserialize(fileStream);
						fileStream.Close();
					}
					catch
					{
					}
				}
				bool flag3 = this.tg == null;
				if (flag3)
				{
					MessageBox.Show("Cannot read the [" + fileName + "] file.", "Open...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				string[] array = this.tg.Version.Split(new char[]
				{
					'.'
				});
				string[] array2 = this.Version.Split(new char[]
				{
					'.'
				});
				int num = Convert.ToInt32(array[0]);
				int num2 = Convert.ToInt32(array[1]);
				int num3 = Convert.ToInt32(array2[0]);
				int num4 = Convert.ToInt32(array2[1]);
				bool flag4 = num3 < num || (num3 == num && num2 > num4);
				if (flag4)
				{
					MessageBox.Show("You must use FUGE version " + this.tg.Version + " to open the file!", "Wrong FUGE version", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				bool flag5 = !this.tg.Password.Equals("");
				if (flag5)
				{
					FrmPassword frmPassword = new FrmPassword();
					DialogResult dialogResult3 = frmPassword.ShowDialog(this);
					bool flag6 = DialogResult.OK == dialogResult3;
					if (!flag6)
					{
						this.tg = null;
						this.txtGradingFile.Text = "";
						this.cboSubClass.DataSource = null;
						this.lblTeacher.Text = "Teacher: ";
						this.lblSubClass.Text = "Subject/Class:";
						return;
					}
					string password = frmPassword.Password;
					MD5 md5Hash = MD5.Create();
					bool flag7 = Helper.VerifyMd5Hash(md5Hash, password, this.tg.Password);
					bool flag8 = flag7;
					if (flag8)
					{
						List<string> list = new List<string>();
						foreach (SubjectClassGrade subjectClassGrade in this.tg.SubjectClassGrades)
						{
							list.Add(subjectClassGrade.Subject + "/" + subjectClassGrade.Class);
						}
						this.cboSubClass.DataSource = list;
						this.lblTeacher.Text = "Teacher: " + this.tg.Login;
						this.lblSubClass.Text = "Subject/Class(" + this.tg.SubjectClassGrades.Count.ToString() + "):";
					}
					else
					{
						this.tg = null;
						this.txtGradingFile.Text = "";
						this.cboSubClass.DataSource = null;
						this.lblTeacher.Text = "Teacher: ";
						this.lblSubClass.Text = "Subject/Class:";
						MessageBox.Show("Incorrect password!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					List<string> list2 = new List<string>();
					foreach (SubjectClassGrade subjectClassGrade2 in this.tg.SubjectClassGrades)
					{
						list2.Add(subjectClassGrade2.Subject + "/" + subjectClassGrade2.Class);
						subjectClassGrade2.Students.Sort();
					}
					this.cboSubClass.DataSource = list2;
					this.lblTeacher.Text = "Teacher: " + this.tg.Login;
					this.lblSubClass.Text = "Subject/Class(" + this.tg.SubjectClassGrades.Count.ToString() + "):";
				}
				this.dgvGrading.Rows.Clear();
				this.dgvGrading.Columns.Clear();
				this.chkListBoxComp.Items.Clear();
				this.lblComp.Text = "Grading components:";
				this.lblGradingDetails.Text = "Grading details:";
				this.chkBoxAll.Checked = false;
				this.groupBoxAddStud.Enabled = false;
				this.lblSubjectClass.Text = "";
				this.btnImportComments.Visible = false;
				string text2 = this.ConvertListToString(this.tg.SubjectClassGrades[0].Components);
				for (int i = 1; i < this.tg.SubjectClassGrades.Count; i++)
				{
					string value = this.ConvertListToString(this.tg.SubjectClassGrades[i].Components);
					bool flag9 = !text2.Equals(value);
					if (flag9)
					{
						this.chbMergeClass.Visible = false;
						return;
					}
				}
			}
			this.chbMergeClass.Visible = true;
			this.chbMergeClass.Checked = false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000053EC File Offset: 0x000035EC
		private void btnShow_Click(object sender, EventArgs e)
		{
			bool flag = this.tg == null;
			if (flag)
			{
				MessageBox.Show("You need choose a .fg file to show.", "Show", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				bool needSave = this.NeedSave;
				if (needSave)
				{
					DialogResult dialogResult = MessageBox.Show("Do you want to save the current mark sheet?", "Save mark sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					bool flag2 = dialogResult == DialogResult.Yes;
					if (flag2)
					{
						this.btnSave.PerformClick();
					}
					this.NeedSave = false;
				}
				this.chkListBoxComp.Items.Clear();
				this.dgvGrading.Rows.Clear();
				this.dgvGrading.Columns.Clear();
				this.chkBoxAll.Checked = false;
				this.btnImportComments.Visible = true;
				string text = this.cboSubClass.Text;
				this.lblSubjectClass.Text = text;
				bool flag3 = !this.chbMergeClass.Checked;
				if (flag3)
				{
					foreach (SubjectClassGrade subjectClassGrade in this.tg.SubjectClassGrades)
					{
						string value = subjectClassGrade.Subject + "/" + subjectClassGrade.Class;
						bool flag4 = text.Equals(value);
						if (flag4)
						{
							this.scg = subjectClassGrade;
							break;
						}
					}
				}
				else
				{
					this.scg = new SubjectClassGrade();
					this.scg.Class = "All";
					this.scg.Students = new List<Student>();
					this.scg.Subject = this.tg.SubjectClassGrades[0].Subject;
					this.scg.Components = this.tg.SubjectClassGrades[0].Components;
					for (int i = 0; i < this.tg.SubjectClassGrades.Count; i++)
					{
						this.scg.Students.AddRange(this.tg.SubjectClassGrades[i].Students);
					}
				}
				int num = 3;
				this.dgvGrading.ColumnCount = num + this.scg.Components.Count;
				this.dgvGrading.Columns[0].Name = "Roll";
				this.dgvGrading.Columns[1].Name = "Name";
				this.dgvGrading.Columns[0].ReadOnly = true;
				this.dgvGrading.Columns[1].ReadOnly = true;
				this.dgvGrading.Columns[1].Frozen = true;
				this.dgvGrading.Columns[2].Name = "Comment";
				foreach (string text2 in this.scg.Components)
				{
					this.dgvGrading.Columns[num].Name = text2;
					this.dgvGrading.Columns[num].Visible = false;
					this.chkListBoxComp.Items.Add(text2);
					num++;
				}
				this.lblComp.Text = "Grading components (" + (num - 3).ToString() + "):";
				foreach (Student student in this.scg.Students)
				{
					List<string> list = new List<string>();
					list.Add(student.Roll);
					list.Add(student.Name);
					list.Add(student.Comment);
					bool flag5 = student.Grades.Count == 0;
					if (flag5)
					{
						student.Grades = new List<GradeComponent>();
						foreach (string component in this.scg.Components)
						{
							GradeComponent gradeComponent = new GradeComponent();
							gradeComponent.Component = component;
							gradeComponent.Grade = null;
							list.Add(null);
							student.Grades.Add(gradeComponent);
						}
					}
					else
					{
						foreach (GradeComponent gradeComponent2 in student.Grades)
						{
							bool flag6 = gradeComponent2.Grade != null;
							if (flag6)
							{
								list.Add(gradeComponent2.Grade.ToString());
							}
							else
							{
								list.Add(null);
							}
						}
					}
					DataGridViewRowCollection rows = this.dgvGrading.Rows;
					object[] values = list.ToArray();
					rows.Add(values);
				}
				foreach (object obj in ((IEnumerable)this.dgvGrading.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.HeaderCell.Value = (dataGridViewRow.Index + 1).ToString();
				}
				this.dgvGrading.RowHeadersWidth = 60;
				this.lblGradingDetails.Text = "Grading details (" + this.scg.Students.Count.ToString() + "):";
				this.groupBoxAddStud.Enabled = true;
				this.dgvGrading.AllowUserToAddRows = false;
				bool flag7 = this.dgvGrading.RowCount <= this.MaxThesisGroupSize;
				if (flag7)
				{
					this.btnComment.Enabled = true;
				}
				else
				{
					this.btnComment.Enabled = false;
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005AA4 File Offset: 0x00003CA4
		private void chkBoxAll_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < this.chkListBoxComp.Items.Count; i++)
			{
				this.chkListBoxComp.SetItemChecked(i, this.chkBoxAll.Checked);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005AEC File Offset: 0x00003CEC
		private void chkListBoxComp_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			string text = this.chkListBoxComp.Items[e.Index].ToString();
			this.dgvGrading.Columns[e.Index + 3].Visible = (e.NewValue == CheckState.Checked);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005B40 File Offset: 0x00003D40
		private void btnSave_Click(object sender, EventArgs e)
		{
			bool flag = this.tg != null && !this.txtGradingFile.Text.Trim().Equals("");
			if (flag)
			{
				bool flag2 = this.tg.Password.Equals("");
				if (flag2)
				{
					FrmSetPassword frmSetPassword = new FrmSetPassword();
					frmSetPassword.ShowDialog(this);
					string password = frmSetPassword.Password;
					bool flag3 = !password.Equals("");
					if (!flag3)
					{
						return;
					}
					MD5 md5Hash = MD5.Create();
					this.tg.Password = Helper.GetMd5Hash(md5Hash, password);
				}
				string text = this.lblSubjectClass.Text;
				foreach (object obj in ((IEnumerable)this.dgvGrading.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					bool flag4 = dataGridViewRow.Cells[0].Value != null && !dataGridViewRow.Cells[0].Value.ToString().Trim().Equals("");
					if (flag4)
					{
						string roll = dataGridViewRow.Cells[0].Value.ToString();
						Student student = this.GetStudent(text, roll);
						student.Comment = ((dataGridViewRow.Cells[2].Value == null) ? null : dataGridViewRow.Cells[2].Value.ToString());
						for (int i = 3; i < dataGridViewRow.Cells.Count; i++)
						{
							string name = this.dgvGrading.Columns[i].Name;
							foreach (GradeComponent gradeComponent in student.Grades)
							{
								bool flag5 = gradeComponent.Component.Equals(name);
								if (flag5)
								{
									bool flag6 = dataGridViewRow.Cells[i].Value == null;
									if (flag6)
									{
										gradeComponent.Grade = null;
									}
									else
									{
										gradeComponent.Grade = new float?(Convert.ToSingle(dataGridViewRow.Cells[i].Value.ToString()));
									}
								}
							}
						}
					}
				}
				string contents = AesOperation.EncryptString(null, JsonConvert.SerializeObject(this.tg));
				File.WriteAllText(this.txtGradingFile.Text, contents);
				this.NeedSave = false;
				MessageBox.Show("File saved!", "Saving File");
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005E44 File Offset: 0x00004044
		private Student GetStudent(string subClass, string roll)
		{
			Student student = null;
			bool flag = subClass.Equals("[All classes]") && this.cboSubClass.Items.Count == 1;
			if (flag)
			{
				foreach (SubjectClassGrade subjectClassGrade in this.tg.SubjectClassGrades)
				{
					foreach (Student student2 in subjectClassGrade.Students)
					{
						bool flag2 = student2.Roll.Trim().ToUpper().Equals(roll.Trim().ToUpper());
						if (flag2)
						{
							student = student2;
							break;
						}
					}
					bool flag3 = student != null;
					if (flag3)
					{
						break;
					}
				}
			}
			else
			{
				foreach (SubjectClassGrade subjectClassGrade2 in this.tg.SubjectClassGrades)
				{
					string text = subjectClassGrade2.Subject + "/" + subjectClassGrade2.Class;
					bool flag4 = text.Equals(subClass);
					if (flag4)
					{
						foreach (Student student3 in subjectClassGrade2.Students)
						{
							bool flag5 = student3.Roll.Trim().ToUpper().Equals(roll.Trim().ToUpper());
							if (flag5)
							{
								student = student3;
								break;
							}
						}
						break;
					}
				}
			}
			return student;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000603C File Offset: 0x0000423C
		private void btnSearch_Click(object sender, EventArgs e)
		{
			string value = this.txtRoll.Text.Trim().ToUpper();
			foreach (object obj in ((IEnumerable)this.dgvGrading.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				bool flag = dataGridViewRow.Cells[0].Value != null && !dataGridViewRow.Cells[0].Value.ToString().Trim().Equals("");
				if (flag)
				{
					string text = dataGridViewRow.Cells[0].Value.ToString().ToUpper();
					bool flag2 = text.Equals(value);
					if (flag2)
					{
						dataGridViewRow.Selected = true;
					}
					else
					{
						dataGridViewRow.Selected = false;
					}
				}
			}
			bool flag3 = this.dgvGrading.SelectedRows.Count > 0;
			if (flag3)
			{
				this.dgvGrading.FirstDisplayedScrollingRowIndex = this.dgvGrading.SelectedRows[0].Index;
			}
			else
			{
				MessageBox.Show("Not found!", "Search student");
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000618C File Offset: 0x0000438C
		private void txtRoll_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				this.btnSearch.PerformClick();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000061B8 File Offset: 0x000043B8
		private void dgvGrading_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			this.NeedSave = true;
			bool flag = e.ColumnIndex == 2;
			if (!flag)
			{
				bool flag2 = this.dgvGrading.ColumnCount == 4;
				if (flag2)
				{
					bool flag3 = this.dgvGrading.Columns[e.ColumnIndex].Name.Equals("Status");
					if (flag3)
					{
						bool flag4 = this.dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null;
						if (flag4)
						{
							string text = this.dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
							bool flag5 = !text.Trim().Equals("1") && !text.Trim().Equals("0");
							if (flag5)
							{
								MessageBox.Show("Invalid grade!\nGrade value must be 1 or 0 (1=pass, 0=fail)!", "Input grade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								this.dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
								return;
							}
						}
					}
				}
				bool flag6 = this.dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null;
				if (flag6)
				{
					string s = this.dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
					float num;
					bool flag7 = float.TryParse(s, out num);
					bool flag8 = !flag7 || num < 0f || num > 10f;
					if (flag8)
					{
						MessageBox.Show("Invalid mark value!\nMark value is between 0 and 10", "Input mark", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.dgvGrading.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
					}
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000063E0 File Offset: 0x000045E0
		private void btnAdd_Click(object sender, EventArgs e)
		{
			string text = this.txtRollNew.Text.Trim().ToUpper();
			bool flag = !text.Equals("");
			if (flag)
			{
				foreach (object obj in ((IEnumerable)this.dgvGrading.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.Selected = false;
				}
				foreach (object obj2 in ((IEnumerable)this.dgvGrading.Rows))
				{
					DataGridViewRow dataGridViewRow2 = (DataGridViewRow)obj2;
					bool flag2 = dataGridViewRow2.Cells[0].Value != null;
					if (flag2)
					{
						string value = dataGridViewRow2.Cells[0].Value.ToString().Trim().ToUpper();
						bool flag3 = text.Equals(value);
						if (flag3)
						{
							MessageBox.Show("Student already exists!", "Add new student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							dataGridViewRow2.Selected = true;
							this.dgvGrading.FirstDisplayedScrollingRowIndex = this.dgvGrading.SelectedRows[0].Index;
							return;
						}
					}
				}
				this.NeedSave = true;
				string text2 = this.lblSubjectClass.Text;
				foreach (SubjectClassGrade subjectClassGrade in this.tg.SubjectClassGrades)
				{
					string value2 = subjectClassGrade.Subject + "/" + subjectClassGrade.Class;
					bool flag4 = text2.Equals(value2);
					if (flag4)
					{
						Student student = new Student();
						student.Roll = text;
						student.Name = this.txtName.Text.Trim();
						List<string> list = new List<string>();
						list.Add(student.Roll);
						list.Add(student.Name);
						student.Grades = new List<GradeComponent>();
						foreach (string component in subjectClassGrade.Components)
						{
							GradeComponent gradeComponent = new GradeComponent();
							gradeComponent.Component = component;
							gradeComponent.Grade = null;
							list.Add(null);
							student.Grades.Add(gradeComponent);
						}
						subjectClassGrade.Students.Add(student);
						DataGridViewRowCollection rows = this.dgvGrading.Rows;
						object[] values = list.ToArray();
						rows.Add(values);
						foreach (object obj3 in ((IEnumerable)this.dgvGrading.Rows))
						{
							DataGridViewRow dataGridViewRow3 = (DataGridViewRow)obj3;
							dataGridViewRow3.HeaderCell.Value = (dataGridViewRow3.Index + 1).ToString();
						}
						this.dgvGrading.Rows[this.dgvGrading.Rows.Count - 1].Selected = true;
						bool flag5 = this.dgvGrading.SelectedRows.Count > 0;
						if (flag5)
						{
							this.dgvGrading.FirstDisplayedScrollingRowIndex = this.dgvGrading.SelectedRows[0].Index;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006804 File Offset: 0x00004A04
		private void importMarkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.chkListBoxComp.SelectedItem != null;
			if (flag)
			{
				string markComponent = this.chkListBoxComp.SelectedItem.ToString();
				string text = this.lblSubjectClass.Text;
				FrmImport frmImport = new FrmImport(this);
				frmImport.SubjectClass = text;
				frmImport.MarkComponent = markComponent;
				frmImport.IsInputStatus = false;
				bool flag2 = this.dgvGrading.ColumnCount == 4;
				if (flag2)
				{
					bool flag3 = this.dgvGrading.Columns[3].Name.Equals("Status");
					if (flag3)
					{
						frmImport.IsInputStatus = true;
					}
				}
				frmImport.ShowDialog(this);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000068B4 File Offset: 0x00004AB4
		private void FrmFuGrade_Load(object sender, EventArgs e)
		{
			this.lblSubjectClass.Text = "";
			this.lblGradingComp.Text = "";
			this.btnImportComments.Visible = false;
			AppSettingsReader appSettingsReader = new AppSettingsReader();
			this.MaxThesisGroupSize = Convert.ToInt32(appSettingsReader.GetValue("MaxThesisGroupSize", typeof(int)).ToString());
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000691C File Offset: 0x00004B1C
		public string ImportMark(Dictionary<string, string> markDic, string subClass, string markComp)
		{
			string text = "";
			int index = -1;
			for (int i = 3; i < this.dgvGrading.ColumnCount; i++)
			{
				bool flag = this.dgvGrading.Columns[i].Name.Equals(markComp);
				if (flag)
				{
					index = i;
					break;
				}
			}
			int num = 0;
			foreach (string text2 in markDic.Keys)
			{
				int num2 = -1;
				bool flag2 = false;
				foreach (object obj in ((IEnumerable)this.dgvGrading.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					num2++;
					bool flag3 = dataGridViewRow.Cells[0].Value == null;
					if (!flag3)
					{
						string text3 = dataGridViewRow.Cells[0].Value.ToString().Trim().ToUpper();
						string value = text2.Trim().ToUpper();
						bool flag4 = text3.Equals(value);
						if (flag4)
						{
							flag2 = true;
							break;
						}
					}
				}
				bool flag5 = flag2;
				if (flag5)
				{
					this.dgvGrading.Rows[num2].Cells[index].Value = markDic[text2];
					num++;
				}
				else
				{
					text = text + "\r\nCannot find student with roll = \"" + text2 + "\"";
				}
			}
			text = string.Concat(new string[]
			{
				text,
				"\r\n\r\n",
				num.ToString(),
				" (of ",
				markDic.Keys.Count.ToString(),
				") '",
				markComp,
				"' mark have been imported to ",
				subClass
			});
			return text;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006B54 File Offset: 0x00004D54
		public string ImportComments(Dictionary<string, string> markDic, string subClass)
		{
			string text = "";
			int index = 2;
			int num = 0;
			foreach (string text2 in markDic.Keys)
			{
				int num2 = -1;
				bool flag = false;
				foreach (object obj in ((IEnumerable)this.dgvGrading.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					num2++;
					bool flag2 = dataGridViewRow.Cells[0].Value == null;
					if (!flag2)
					{
						string text3 = dataGridViewRow.Cells[0].Value.ToString().Trim().ToUpper();
						string value = text2.Trim().ToUpper();
						bool flag3 = text3.Equals(value);
						if (flag3)
						{
							flag = true;
							break;
						}
					}
				}
				bool flag4 = flag;
				if (flag4)
				{
					this.dgvGrading.Rows[num2].Cells[index].Value = markDic[text2];
					num++;
				}
				else
				{
					text = text + "\r\nCannot find student with roll = \"" + text2 + "\"";
				}
			}
			text = string.Concat(new string[]
			{
				text,
				"\r\n\r\n",
				num.ToString(),
				" (of ",
				markDic.Keys.Count.ToString(),
				") 'Comments' have been imported to ",
				subClass
			});
			return text;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006D38 File Offset: 0x00004F38
		private void clearMarkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.chkListBoxComp.SelectedItem != null;
			if (flag)
			{
				string text = this.chkListBoxComp.SelectedItem.ToString();
				string text2 = this.lblSubjectClass.Text;
				DialogResult dialogResult = MessageBox.Show("Do you really want to clear all '" + text + "' marks?", "Clear mark", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				bool flag2 = dialogResult == DialogResult.Yes;
				if (flag2)
				{
					int index = -1;
					for (int i = 3; i < this.dgvGrading.ColumnCount; i++)
					{
						bool flag3 = this.dgvGrading.Columns[i].Name.Equals(text);
						if (flag3)
						{
							index = i;
							break;
						}
					}
					foreach (object obj in ((IEnumerable)this.dgvGrading.Rows))
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
						bool flag4 = dataGridViewRow.Cells[0].Value == null;
						if (!flag4)
						{
							dataGridViewRow.Cells[index].Value = null;
						}
					}
					this.NeedSave = true;
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006E88 File Offset: 0x00005088
		private void btnAddComp_Click(object sender, EventArgs e)
		{
			string text = this.txtComp.Text.Trim();
			bool flag = text.Equals("");
			if (!flag)
			{
				foreach (object obj in this.chkListBoxComp.Items)
				{
					string text2 = (string)obj;
					bool flag2 = text2.Trim().ToUpper().Equals(text.ToUpper());
					if (flag2)
					{
						MessageBox.Show("Grading component already exists!", "Add new grading component", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				this.NeedSave = true;
				string text3 = this.lblSubjectClass.Text;
				foreach (SubjectClassGrade subjectClassGrade in this.tg.SubjectClassGrades)
				{
					string value = subjectClassGrade.Subject + "/" + subjectClassGrade.Class;
					bool flag3 = text3.Equals(value);
					if (flag3)
					{
						this.dgvGrading.Columns.Add(text, text);
						subjectClassGrade.Components.Add(text);
						this.lblComp.Text = "Grading components (" + (subjectClassGrade.Components.Count - 3).ToString() + "):";
						foreach (Student student in subjectClassGrade.Students)
						{
							bool flag4 = student.Grades.Count == 0;
							if (flag4)
							{
								student.Grades = new List<GradeComponent>();
							}
							GradeComponent gradeComponent = new GradeComponent();
							gradeComponent.Component = text;
							gradeComponent.Grade = null;
							student.Grades.Add(gradeComponent);
						}
						this.chkListBoxComp.Items.Add(text, true);
						break;
					}
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000070F0 File Offset: 0x000052F0
		private void btnImportComments_Click(object sender, EventArgs e)
		{
			new FrmImport(this)
			{
				SubjectClass = this.lblSubjectClass.Text,
				MarkComponent = "Add Comments",
				IsImportComments = true
			}.ShowDialog(this);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00007134 File Offset: 0x00005334
		private void chkListBoxComp_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.lblGradingComp.Text = this.chkListBoxComp.Text;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00007150 File Offset: 0x00005350
		private void ctxMenu_Opening(object sender, CancelEventArgs e)
		{
			this.importMarkToolStripMenuItem.Text = "Import Mark of " + this.chkListBoxComp.Text;
			this.clearMarkToolStripMenuItem.Text = "Clear Mark of " + this.chkListBoxComp.Text;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000071A0 File Offset: 0x000053A0
		private void chkListBoxComp_MouseDown(object sender, MouseEventArgs e)
		{
			this.chkListBoxComp.SelectedIndex = this.chkListBoxComp.IndexFromPoint(e.X, e.Y);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000071C8 File Offset: 0x000053C8
		private string ConvertListToString(List<string> list)
		{
			string text = "";
			foreach (string str in list)
			{
				text = text + str + ";";
			}
			return text;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000722C File Offset: 0x0000542C
		private void chbMergeClass_CheckedChanged(object sender, EventArgs e)
		{
			bool flag = this.tg == null;
			if (flag)
			{
				MessageBox.Show("You need choose a .fg file.", "Merge classes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				bool needSave = this.NeedSave;
				if (needSave)
				{
					DialogResult dialogResult = MessageBox.Show("Do you want to save the current mark sheet?", "Save mark sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					bool flag2 = dialogResult == DialogResult.Yes;
					if (flag2)
					{
						this.btnSave.PerformClick();
					}
					this.NeedSave = false;
				}
				this.dgvGrading.Rows.Clear();
				this.dgvGrading.Columns.Clear();
				this.chkListBoxComp.Items.Clear();
				this.lblComp.Text = "Grading components:";
				this.lblGradingDetails.Text = "Grading details:";
				this.chkBoxAll.Checked = false;
				this.groupBoxAddStud.Enabled = false;
				this.lblSubjectClass.Text = "";
				this.btnImportComments.Visible = false;
				List<string> list = new List<string>();
				this.cboSubClass.DataSource = null;
				bool @checked = this.chbMergeClass.Checked;
				if (@checked)
				{
					list.Add("[All classes]");
				}
				else
				{
					foreach (SubjectClassGrade subjectClassGrade in this.tg.SubjectClassGrades)
					{
						list.Add(subjectClassGrade.Subject + "/" + subjectClassGrade.Class);
					}
				}
				this.cboSubClass.DataSource = list;
				this.lblTeacher.Text = "Teacher: " + this.tg.Login;
				this.lblSubClass.Text = "Subject/Class(" + this.tg.SubjectClassGrades.Count.ToString() + "):";
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00007430 File Offset: 0x00005630
		private void btnComment_Click(object sender, EventArgs e)
		{
			FileInfo fileInfo = new FileInfo(this.txtGradingFile.Text);
			string[] array = fileInfo.Name.Split(new char[]
			{
				'.'
			});
			string[] array2 = this.cboSubClass.Text.Split(new char[]
			{
				'/'
			});
			List<ThesisStudent> list = new List<ThesisStudent>();
			foreach (Student student in this.scg.Students)
			{
				list.Add(new ThesisStudent
				{
					Roll = student.Roll,
					Name = student.Name
				});
			}
			new FrmThesisComment
			{
				listTS = list,
				RecommendedCmtFileName = array[0] + "_" + array2[1],
				SubjectCode = array2[0],
				CmtFileName = null,
				Login = this.tg.Login,
				ClassName = this.scg.Class,
				Semester = this.tg.Semester
			}.ShowDialog();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00007580 File Offset: 0x00005780
		private void btnEditCmtFile_Click(object sender, EventArgs e)
		{
			string filter = this.openFileDialog.Filter;
			this.openFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
			this.openFileDialog.Filter = "Comment for thesis files | *.cmt";
			bool flag = DialogResult.OK == this.openFileDialog.ShowDialog();
			if (flag)
			{
				string fileName = this.openFileDialog.FileName;
				new FrmThesisComment
				{
					CmtFileName = fileName
				}.ShowDialog();
			}
			this.openFileDialog.Filter = filter;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00007608 File Offset: 0x00005808
		private void btnDefenseGrading_Click(object sender, EventArgs e)
		{
			bool flag = this.ffg == null || this.ffg.IsDisposed;
			if (flag)
			{
				this.ffg = new FrmDefenseGrading();
			}
			this.ffg.Show();
			base.Hide();
		}

		// Token: 0x04000050 RID: 80
		public string Version = "1.1";

		// Token: 0x04000052 RID: 82
		private const int nonMarkFieldCount = 3;

		// Token: 0x04000053 RID: 83
		private TeacherGrade tg = null;

		// Token: 0x04000054 RID: 84
		private SubjectClassGrade scg = null;

		// Token: 0x04000055 RID: 85
		private int MaxThesisGroupSize = 6;

		// Token: 0x04000056 RID: 86
		private FrmDefenseGrading ffg = null;
	}
}
