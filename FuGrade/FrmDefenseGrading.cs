using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x02000009 RID: 9
	public partial class FrmDefenseGrading : Form
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003CCD File Offset: 0x00001ECD
		public FrmDefenseGrading()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003CF4 File Offset: 0x00001EF4
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			bool flag = DialogResult.OK == this.folderBrowserDialog.ShowDialog();
			if (flag)
			{
				this.txtGroupFolder.Text = this.folderBrowserDialog.SelectedPath;
				this.btnLoadGroup.PerformClick();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003D3C File Offset: 0x00001F3C
		private void btnLoadGroup_Click(object sender, EventArgs e)
		{
			try
			{
				this.listDG = new List<DefenseGrading>();
				string[] files = Directory.GetFiles(this.txtGroupFolder.Text, "*.cmt");
				foreach (string path in files)
				{
					FileStream fileStream = new FileStream(path, FileMode.Open);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					ThesisComment thesisComment = (ThesisComment)binaryFormatter.Deserialize(fileStream);
					fileStream.Close();
					DefenseGrading defenseGrading = new DefenseGrading();
					defenseGrading.SubjectCode = thesisComment.SubjectCode;
					defenseGrading.TitleVN = thesisComment.TitleVN;
					defenseGrading.TitleEN = thesisComment.TitleEN;
					defenseGrading.Supervisor = thesisComment.Teacher;
					defenseGrading.ClassName = thesisComment.ClassName;
					defenseGrading.Semester = thesisComment.Semester;
					defenseGrading.SupervisorComment = thesisComment;
					foreach (ThesisStudent thesisStudent in thesisComment.Conclusion)
					{
						DefenseStudentGrade defenseStudentGrade = new DefenseStudentGrade();
						defenseStudentGrade.Roll = thesisStudent.Roll;
						defenseStudentGrade.Name = thesisStudent.Name;
						bool flag = thesisStudent.Agree_to_defense != null && thesisStudent.Agree_to_defense.Trim().ToLower().Equals("x");
						if (flag)
						{
							defenseStudentGrade.Conclusion = "Agree to defense";
						}
						defenseGrading.GradeStudents.Add(defenseStudentGrade);
					}
					this.listDG.Add(defenseGrading);
				}
				this.dgvGroup.DataSource = this.listDG;
				foreach (object obj in ((IEnumerable)this.dgvGroup.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.HeaderCell.Value = (dataGridViewRow.Index + 1).ToString();
				}
				this.dgvGroup.ReadOnly = true;
				for (int j = 6; j < this.dgvGroup.ColumnCount; j++)
				{
					this.dgvGroup.Columns[j].Visible = false;
				}
			}
			catch (Exception ex)
			{
				this.dgvGroup.DataSource = null;
				MessageBox.Show("Error:\r\n" + ex.Message, "Load group", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004010 File Offset: 0x00002210
		private void btnClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000401C File Offset: 0x0000221C
		private bool IsWithoutAccents(string name)
		{
			string text = "";
			for (char c = 'a'; c <= 'z'; c += '\u0001')
			{
				text += c.ToString();
			}
			for (char c2 = 'A'; c2 <= 'Z'; c2 += '\u0001')
			{
				text += c2.ToString();
			}
			text += " ";
			foreach (char c3 in name.ToCharArray())
			{
				bool flag = !text.Contains(c3.ToString());
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000040D0 File Offset: 0x000022D0
		private void btnGrade_Click(object sender, EventArgs e)
		{
			bool flag = this.dgvGroup.SelectedCells.Count == 0;
			if (!flag)
			{
				bool flag2 = this.txtGradedBy.Text.Trim().Equals("");
				if (flag2)
				{
					MessageBox.Show("[Full name] cannot be empty!", "Show evaluation form", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					string name = this.txtGradedBy.Text.Trim();
					bool flag3 = !this.IsWithoutAccents(name);
					if (flag3)
					{
						MessageBox.Show("[Full name] CANNOT have accents/non-alphabetic characters!", "Show evaluation form", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						int rowIndex = this.dgvGroup.SelectedCells[0].RowIndex;
						bool flag4 = rowIndex >= 0;
						if (flag4)
						{
							bool flag5 = this.password == null;
							if (flag5)
							{
								FrmSetPassword frmSetPassword = new FrmSetPassword();
								frmSetPassword.Text = "Set password for Thesis Evaluation Form (.tef file)";
								frmSetPassword.ShowDialog(this);
								string text = frmSetPassword.Password;
								bool flag6 = !text.Equals("");
								if (!flag6)
								{
									return;
								}
								MD5 md5Hash = MD5.Create();
								this.password = Helper.GetMd5Hash(md5Hash, text);
							}
							DefenseGrading defenseGrading = this.listDG[rowIndex];
							FrmEvaluationForm frmEvaluationForm = new FrmEvaluationForm();
							foreach (DefenseStudentGrade defenseStudentGrade in defenseGrading.GradeStudents)
							{
								defenseStudentGrade.GradedItems = new List<GradedItem>();
							}
							frmEvaluationForm.DefenseGroup = defenseGrading;
							defenseGrading.Password = this.password;
							frmEvaluationForm.DefenseGroup.GradedTeacher = this.txtGradedBy.Text;
							frmEvaluationForm.IsSaved = true;
							frmEvaluationForm.TC = defenseGrading.SupervisorComment;
							frmEvaluationForm.Show();
						}
						else
						{
							MessageBox.Show("You need select a group!", "Grade", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000042CC File Offset: 0x000024CC
		private void btnEdit_Click(object sender, EventArgs e)
		{
			bool flag = DialogResult.OK == this.openFileDialog.ShowDialog();
			if (flag)
			{
				string fileName = this.openFileDialog.FileName;
				FileStream fileStream = new FileStream(fileName, FileMode.Open);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				DefenseGrading defenseGrading = (DefenseGrading)binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
				bool flag2 = DialogResult.Yes == MessageBox.Show("Do you want to open this file in READ ONLY mode?", "Open .tef file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (flag2)
				{
					bool isEditMode = false;
					FrmEvaluationForm frmEvaluationForm = new FrmEvaluationForm();
					frmEvaluationForm.IsEditMode = isEditMode;
					frmEvaluationForm.TefFile = fileName;
					frmEvaluationForm.DefenseGroup = defenseGrading;
					FrmEvaluationForm frmEvaluationForm2 = frmEvaluationForm;
					frmEvaluationForm2.Text += " (READ ONLY)";
					frmEvaluationForm.IsSaved = true;
					frmEvaluationForm.Show();
				}
				else
				{
					bool flag3 = defenseGrading.Password != null;
					if (flag3)
					{
						FrmPassword frmPassword = new FrmPassword();
						frmPassword.Text = "Provide password to open Evaluation Form File (.tef)";
						DialogResult dialogResult = frmPassword.ShowDialog(this);
						bool flag4 = DialogResult.OK == dialogResult;
						if (flag4)
						{
							string input = frmPassword.Password;
							MD5 md5Hash = MD5.Create();
							bool flag5 = Helper.VerifyMd5Hash(md5Hash, input, defenseGrading.Password);
							bool flag6 = flag5;
							if (flag6)
							{
								bool isEditMode = true;
								new FrmEvaluationForm
								{
									IsEditMode = isEditMode,
									TefFile = fileName,
									DefenseGroup = defenseGrading,
									IsSaved = false
								}.Show();
							}
							else
							{
								MessageBox.Show("Incorrect password!", ".tef file password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004457 File Offset: 0x00002657
		private void FrmDefenseGrading_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000445C File Offset: 0x0000265C
		private void btnSumerize_Click(object sender, EventArgs e)
		{
			FrmSummarizeThesisResult frmSummarizeThesisResult = new FrmSummarizeThesisResult();
			frmSummarizeThesisResult.Show();
		}

		// Token: 0x0400003E RID: 62
		private List<DefenseGrading> listDG = null;

		// Token: 0x0400003F RID: 63
		private string password = null;
	}
}
