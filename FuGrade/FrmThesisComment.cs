using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x02000012 RID: 18
	public partial class FrmThesisComment : Form
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000EEC5 File Offset: 0x0000D0C5
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000EECD File Offset: 0x0000D0CD
		public List<ThesisStudent> listTS { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000EED6 File Offset: 0x0000D0D6
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000EEDE File Offset: 0x0000D0DE
		public string CmtFileName { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000EEE7 File Offset: 0x0000D0E7
		// (set) Token: 0x060000ED RID: 237 RVA: 0x0000EEEF File Offset: 0x0000D0EF
		public string RecommendedCmtFileName { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000EF00 File Offset: 0x0000D100
		public string SubjectCode { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000EF09 File Offset: 0x0000D109
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000EF11 File Offset: 0x0000D111
		public string ClassName { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000EF1A File Offset: 0x0000D11A
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000EF22 File Offset: 0x0000D122
		public string Semester { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000EF2B File Offset: 0x0000D12B
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000EF33 File Offset: 0x0000D133
		public string Login { get; set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x0000EF3C File Offset: 0x0000D13C
		public FrmThesisComment()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000027AB File Offset: 0x000009AB
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000EF64 File Offset: 0x0000D164
		private void FrmThesisComment_Load(object sender, EventArgs e)
		{
			bool flag = this.isShowAtEvaluationForm;
			if (!flag)
			{
				this.lblHeader.Text = "";
				bool flag2 = this.CmtFileName != null;
				if (flag2)
				{
					FileStream fileStream = new FileStream(this.CmtFileName, FileMode.Open);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					this.tc = (ThesisComment)binaryFormatter.Deserialize(fileStream);
					fileStream.Close();
					bool flag3 = !this.tc.Password.Equals("");
					if (flag3)
					{
						FrmPassword frmPassword = new FrmPassword();
						frmPassword.Text = "Provide password to edit Thesis Comment File (.cmt)";
						DialogResult dialogResult = frmPassword.ShowDialog(this);
						bool flag4 = DialogResult.OK == dialogResult;
						if (flag4)
						{
							string password = frmPassword.Password;
							MD5 md5Hash = MD5.Create();
							bool flag5 = Helper.VerifyMd5Hash(md5Hash, password, this.tc.Password);
							bool flag6 = !flag5;
							if (flag6)
							{
								MessageBox.Show("Incorrect password!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								base.Close();
							}
						}
						else
						{
							base.Close();
						}
					}
					this.txtTitleVN.Text = this.tc.TitleVN;
					this.txtTitleEN.Text = this.tc.TitleEN;
					string text = "";
					int num = 1;
					foreach (ThesisStudent thesisStudent in this.tc.Conclusion)
					{
						text = string.Concat(new string[]
						{
							text,
							num++.ToString(),
							") ",
							thesisStudent.Name,
							" - ",
							thesisStudent.Roll,
							"\r\n"
						});
					}
					this.txtStudents.Text = text;
					this.txtContent.Text = this.tc.Content;
					this.txtForm.Text = this.tc.Form;
					this.txtAttitude.Text = this.tc.Attitude;
					this.txtAchievement.Text = this.tc.Achievement;
					this.txtLimitation.Text = this.tc.Limitation;
					this.dgvComment.DataSource = this.tc.Conclusion;
					foreach (object obj in ((IEnumerable)this.dgvComment.Rows))
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
						dataGridViewRow.HeaderCell.Value = (dataGridViewRow.Index + 1).ToString();
					}
					this.dgvComment.Columns[0].ReadOnly = true;
					this.dgvComment.Columns[1].ReadOnly = true;
					this.dgvComment.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
					this.dgvComment.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
					this.dgvComment.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
					this.txtSavedFolder.Text = Path.GetDirectoryName(this.CmtFileName);
					this.txtCmtFileName.Text = Path.GetFileNameWithoutExtension(this.CmtFileName);
					this.txtCmtFileName.Enabled = false;
					this.lblHeader.Text = string.Concat(new string[]
					{
						"Subject code: ",
						this.tc.SubjectCode,
						"; Teacher: ",
						this.tc.Teacher,
						"; Last updated: ",
						this.tc.DT.ToString()
					});
				}
				else
				{
					int num2 = 1;
					foreach (ThesisStudent thesisStudent2 in this.listTS)
					{
						TextBox textBox = this.txtStudents;
						textBox.Text = string.Concat(new string[]
						{
							textBox.Text,
							num2++.ToString(),
							") ",
							thesisStudent2.Name,
							" - ",
							thesisStudent2.Roll,
							"\r\n"
						});
					}
					this.dgvComment.DataSource = this.listTS;
					foreach (object obj2 in ((IEnumerable)this.dgvComment.Rows))
					{
						DataGridViewRow dataGridViewRow2 = (DataGridViewRow)obj2;
						dataGridViewRow2.HeaderCell.Value = (dataGridViewRow2.Index + 1).ToString();
					}
					this.dgvComment.Columns[0].ReadOnly = true;
					this.dgvComment.Columns[1].ReadOnly = true;
					this.dgvComment.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
					this.dgvComment.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
					this.dgvComment.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
					this.txtCmtFileName.Text = this.RecommendedCmtFileName;
					this.lblHeader.Text = string.Concat(new string[]
					{
						"Subject code: ",
						this.SubjectCode,
						"; Class: ",
						this.ClassName,
						"; Semester: ",
						this.Semester,
						"; Teacher: ",
						this.Login
					});
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000F57C File Offset: 0x0000D77C
		private void dgvComment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4;
			if (flag)
			{
				bool flag2 = this.dgvComment.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null;
				if (flag2)
				{
					string text = this.dgvComment.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
					bool flag3 = !text.Trim().Equals("x") && !text.Trim().Equals("X");
					if (flag3)
					{
						MessageBox.Show("Invalid value!\nThe value must be x or X!", "Input conclusion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.dgvComment.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
					}
					else
					{
						bool flag4 = e.ColumnIndex == 2;
						if (flag4)
						{
							this.dgvComment.Rows[e.RowIndex].Cells[3].Value = null;
							this.dgvComment.Rows[e.RowIndex].Cells[4].Value = null;
						}
						bool flag5 = e.ColumnIndex == 3;
						if (flag5)
						{
							this.dgvComment.Rows[e.RowIndex].Cells[2].Value = null;
							this.dgvComment.Rows[e.RowIndex].Cells[4].Value = null;
						}
						bool flag6 = e.ColumnIndex == 4;
						if (flag6)
						{
							this.dgvComment.Rows[e.RowIndex].Cells[3].Value = null;
							this.dgvComment.Rows[e.RowIndex].Cells[2].Value = null;
						}
					}
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		private void btnSave_Click(object sender, EventArgs e)
		{
			string text = "";
			bool flag = this.txtTitleVN.Text.Trim().Equals("");
			if (flag)
			{
				text += "\tTiếng Việt/ Vietnamese\r\n";
			}
			bool flag2 = this.txtTitleEN.Text.Trim().Equals("");
			if (flag2)
			{
				text += "\tTiếng Anh/ English\r\n";
			}
			bool flag3 = this.txtContent.Text.Trim().Equals("");
			if (flag3)
			{
				text += "\t3.1 Thesis content...\r\n";
			}
			bool flag4 = this.txtForm.Text.Trim().Equals("");
			if (flag4)
			{
				text += "\t3.2 Thesis form...\r\n";
			}
			bool flag5 = this.txtAttitude.Text.Trim().Equals("");
			if (flag5)
			{
				text += "\t3.3 Student's attitude\r\n";
			}
			bool flag6 = this.txtAchievement.Text.Trim().Equals("");
			if (flag6)
			{
				text += "\t4.1 Achievement level...\r\n";
			}
			bool flag7 = this.txtLimitation.Text.Trim().Equals("");
			if (flag7)
			{
				text += "\t4.2 Limitation\r\n";
			}
			bool flag8 = !text.Equals("");
			if (flag8)
			{
				text = "The following inputs cannot empty:\r\n" + text;
				MessageBox.Show(text, "Save comment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				List<ThesisStudent> list = (List<ThesisStudent>)this.dgvComment.DataSource;
				foreach (ThesisStudent thesisStudent in list)
				{
					string text2 = thesisStudent.Agree_to_defense + thesisStudent.Revised_for_the_second_defense + thesisStudent.Disagree_to_defense;
					bool flag9 = text2 == null || text2.Equals("");
					if (flag9)
					{
						MessageBox.Show("You must give conclusion to each student.", "Save comment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				try
				{
					bool flag10 = this.CmtFileName == null;
					if (flag10)
					{
						bool flag11 = DialogResult.OK == this.folderBrowserDialog.ShowDialog();
						if (!flag11)
						{
							return;
						}
						string selectedPath = this.folderBrowserDialog.SelectedPath;
						string text3 = selectedPath + "\\" + this.txtCmtFileName.Text + ".cmt";
						bool flag12 = File.Exists(text3);
						bool flag14;
						if (flag12)
						{
							bool flag13 = DialogResult.Yes == MessageBox.Show("The file already exists, do you want to overwrite?", "Save .cmt file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							if (!flag13)
							{
								return;
							}
							flag14 = true;
						}
						else
						{
							flag14 = true;
						}
						ThesisComment thesisComment = new ThesisComment();
						bool flag15 = thesisComment.Password == null;
						if (flag15)
						{
							FrmSetPassword frmSetPassword = new FrmSetPassword();
							frmSetPassword.Text = "Set password for Thesis Comment File (.cmt)";
							frmSetPassword.ShowDialog(this);
							string password = frmSetPassword.Password;
							bool flag16 = !password.Equals("");
							if (!flag16)
							{
								return;
							}
							MD5 md5Hash = MD5.Create();
							thesisComment.Password = Helper.GetMd5Hash(md5Hash, password);
						}
						this.CmtFileName = text3;
						thesisComment.Teacher = this.Login;
						thesisComment.SubjectCode = this.SubjectCode;
						thesisComment.DT = DateTime.Now;
						thesisComment.TitleVN = this.txtTitleVN.Text.Trim();
						thesisComment.TitleEN = this.txtTitleEN.Text.Trim();
						thesisComment.Content = this.txtContent.Text.Trim();
						thesisComment.Form = this.txtForm.Text.Trim();
						thesisComment.Attitude = this.txtAttitude.Text.Trim();
						thesisComment.Achievement = this.txtAchievement.Text.Trim();
						thesisComment.Limitation = this.txtLimitation.Text.Trim();
						thesisComment.Conclusion = this.listTS;
						thesisComment.Semester = this.Semester;
						thesisComment.ClassName = this.ClassName;
						bool flag17 = flag14;
						if (flag17)
						{
							FileStream fileStream = new FileStream(text3, FileMode.Create);
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							binaryFormatter.Serialize(fileStream, thesisComment);
							fileStream.Close();
						}
						this.txtSavedFolder.Text = selectedPath;
						this.lblHeader.Text = string.Concat(new string[]
						{
							"Subject code: ",
							this.SubjectCode,
							"; Teacher: ",
							this.Login,
							"; Last updated: ",
							thesisComment.DT.ToString()
						});
						this.tc = thesisComment;
					}
					else
					{
						string directoryName = Path.GetDirectoryName(this.CmtFileName);
						this.tc.DT = DateTime.Now;
						this.tc.TitleVN = this.txtTitleVN.Text.Trim();
						this.tc.TitleEN = this.txtTitleEN.Text.Trim();
						this.tc.Content = this.txtContent.Text.Trim();
						this.tc.Form = this.txtForm.Text.Trim();
						this.tc.Attitude = this.txtAttitude.Text.Trim();
						this.tc.Achievement = this.txtAchievement.Text.Trim();
						this.tc.Limitation = this.txtLimitation.Text.Trim();
						this.tc.Conclusion = (List<ThesisStudent>)this.dgvComment.DataSource;
						this.tc.Semester = this.Semester;
						this.tc.ClassName = this.ClassName;
						FileStream fileStream2 = new FileStream(this.CmtFileName, FileMode.Create);
						BinaryFormatter binaryFormatter2 = new BinaryFormatter();
						binaryFormatter2.Serialize(fileStream2, this.tc);
						fileStream2.Close();
						this.txtSavedFolder.Text = directoryName;
						this.lblHeader.Text = string.Concat(new string[]
						{
							"Subject code: ",
							this.tc.SubjectCode,
							"; Teacher: ",
							this.tc.Teacher,
							"; Last updated: ",
							this.tc.DT.ToString()
						});
					}
					MessageBox.Show("Save successfully!", "Save .cmt file", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Save failure!\r\n" + ex.Message, "Save .cmt file", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public void DisplayThesisComment(ThesisComment tc)
		{
			this.isShowAtEvaluationForm = true;
			this.txtTitleVN.Text = tc.TitleVN;
			this.txtTitleVN.ReadOnly = true;
			this.txtTitleEN.Text = tc.TitleEN;
			this.txtTitleEN.ReadOnly = true;
			string text = "";
			int num = 1;
			foreach (ThesisStudent thesisStudent in tc.Conclusion)
			{
				text = string.Concat(new string[]
				{
					text,
					num++.ToString(),
					") ",
					thesisStudent.Name,
					" - ",
					thesisStudent.Roll,
					"\r\n"
				});
			}
			this.txtStudents.Text = text;
			this.txtStudents.ReadOnly = true;
			this.txtContent.Text = tc.Content;
			this.txtContent.ReadOnly = true;
			this.txtForm.Text = tc.Form;
			this.txtForm.ReadOnly = true;
			this.txtAttitude.Text = tc.Attitude;
			this.txtAttitude.ReadOnly = true;
			this.txtAchievement.Text = tc.Achievement;
			this.txtAchievement.ReadOnly = true;
			this.txtLimitation.Text = tc.Limitation;
			this.txtLimitation.ReadOnly = true;
			this.dgvComment.DataSource = tc.Conclusion;
			foreach (object obj in ((IEnumerable)this.dgvComment.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				dataGridViewRow.HeaderCell.Value = (dataGridViewRow.Index + 1).ToString();
			}
			this.dgvComment.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.dgvComment.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.dgvComment.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.dgvComment.ReadOnly = true;
			this.txtCmtFileName.Enabled = false;
			this.lblFileExt.Visible = false;
			this.lblFileName.Visible = false;
			this.lblGuide.Visible = false;
			this.lblSaveFolder.Visible = false;
			this.btnSave.Visible = false;
			this.txtSavedFolder.Visible = false;
			this.txtCmtFileName.Visible = false;
			this.lblHeader.Text = string.Concat(new string[]
			{
				"Subject code: ",
				tc.SubjectCode,
				"; Teacher: ",
				tc.Teacher,
				"; Last updated: ",
				tc.DT.ToString()
			});
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000101D0 File Offset: 0x0000E3D0
		private void btnShow_Click(object sender, EventArgs e)
		{
			bool flag = this.txtSavedFolder.Text.Trim().Equals("");
			if (!flag)
			{
				Process.Start(this.txtSavedFolder.Text);
			}
		}

		// Token: 0x040000E8 RID: 232
		private ThesisComment tc = null;

		// Token: 0x040000E9 RID: 233
		private bool isShowAtEvaluationForm = false;
	}
}
