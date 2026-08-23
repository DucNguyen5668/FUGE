using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace FuGrade
{
	// Token: 0x02000008 RID: 8
	public partial class FrmCreateFinalCPGradingItems : Form
	{
		// Token: 0x0600004A RID: 74 RVA: 0x0000277A File Offset: 0x0000097A
		public FrmCreateFinalCPGradingItems()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000027AB File Offset: 0x000009AB
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000027B8 File Offset: 0x000009B8
		private void btnSave_Click(object sender, EventArgs e)
		{
			bool flag = this.txtSubjectCode.Text.Trim().Equals("");
			if (flag)
			{
				MessageBox.Show("[Subject code] cannot empty!", "Add new grading item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				bool flag2 = this.txtGradingItem.Text.Trim().Equals("");
				if (flag2)
				{
					MessageBox.Show("[Grading item] cannot empty!", "Add new grading item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					float num = 0f;
					bool flag3 = float.TryParse(this.txtScale.Text.Trim(), out num);
					bool flag4 = !flag3 || num <= 0f;
					if (flag4)
					{
						MessageBox.Show("[Scale] must be a positive decimal!", "Add new grading item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						bool flag5 = this.current_ftgi == null;
						if (flag5)
						{
							foreach (FinalThesisGradingItem finalThesisGradingItem in this.listFTGI)
							{
								bool flag6 = finalThesisGradingItem.SubjectCode.Equals(this.txtSubjectCode.Text.Trim().ToUpper());
								if (flag6)
								{
									bool flag7 = finalThesisGradingItem.GradingItem.ToLower().Equals(this.txtGradingItem.Text.Trim().ToLower());
									if (flag7)
									{
										MessageBox.Show("[Grading item] already exits!", "Add new grading item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
										return;
									}
								}
							}
							FinalThesisGradingItem finalThesisGradingItem2 = new FinalThesisGradingItem();
							finalThesisGradingItem2.SubjectCode = this.txtSubjectCode.Text.Trim().ToUpper();
							finalThesisGradingItem2.Major = this.txtMajor.Text.Trim().ToUpper();
							finalThesisGradingItem2.Minor = this.txtMinor.Text.Trim().ToUpper();
							finalThesisGradingItem2.ItemGroup = this.txtItemGroup.Text.Trim();
							finalThesisGradingItem2.GradingItem = this.txtGradingItem.Text.Trim();
							finalThesisGradingItem2.Scale = num;
							this.listFTGI.Add(finalThesisGradingItem2);
							bool flag8 = !this.lstAvailable.Items.Contains(finalThesisGradingItem2.SubjectCode);
							if (flag8)
							{
								this.lstAvailable.Items.Add(finalThesisGradingItem2.SubjectCode);
							}
							this.current_ftgi = finalThesisGradingItem2;
						}
						else
						{
							this.current_ftgi.SubjectCode = this.txtSubjectCode.Text.Trim().ToUpper();
							this.current_ftgi.Major = this.txtMajor.Text.Trim().ToUpper();
							this.current_ftgi.Minor = this.txtMinor.Text.Trim().ToUpper();
							this.current_ftgi.ItemGroup = this.txtItemGroup.Text.Trim();
							this.current_ftgi.GradingItem = this.txtGradingItem.Text.Trim();
							this.current_ftgi.Scale = num;
						}
						this.SaveMasterFile();
						this.LoadGrid();
					}
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AF0 File Offset: 0x00000CF0
		private void SaveMasterFile()
		{
			FileStream fileStream = new FileStream(this.masterFile, FileMode.Create);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(fileStream, this.listFTGI);
			fileStream.Close();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B26 File Offset: 0x00000D26
		private void button1_Click(object sender, EventArgs e)
		{
			this.txtGradingItem.Text = "";
			this.txtScale.Text = "";
			this.current_ftgi = null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B54 File Offset: 0x00000D54
		private void btnLoad_Click(object sender, EventArgs e)
		{
			this.txtGradingItem.Text = "";
			this.txtMajor.Text = "";
			this.txtMinor.Text = "";
			this.txtScale.Text = "";
			this.txtItemGroup.Text = "";
			this.LoadGrid();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BC0 File Offset: 0x00000DC0
		private void LoadGrid()
		{
			string text = this.txtSubjectCode.Text.Trim().ToUpper();
			bool flag = !text.Equals("");
			if (flag)
			{
				List<FinalThesisGradingItem> list = new List<FinalThesisGradingItem>();
				float num = 0f;
				foreach (FinalThesisGradingItem finalThesisGradingItem in this.listFTGI)
				{
					bool flag2 = finalThesisGradingItem.SubjectCode.Equals(text);
					if (flag2)
					{
						list.Add(finalThesisGradingItem);
						num += finalThesisGradingItem.Scale;
					}
				}
				this.dgvGradingItems.DataSource = list;
				bool flag3 = list.Count > 0;
				if (flag3)
				{
					this.current_ftgi = list[0];
				}
				this.lblTotalScale.Text = "Grading scale: " + num.ToString() + "; Number of grading items: " + list.Count.ToString();
				foreach (object obj in ((IEnumerable)this.dgvGradingItems.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.HeaderCell.Value = (dataGridViewRow.Index + 1).ToString();
				}
				this.dgvGradingItems.RowHeadersWidth = 60;
			}
			else
			{
				this.dgvGradingItems.DataSource = null;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D5C File Offset: 0x00000F5C
		private void FrmCreateFinalCPGradingItems_Load(object sender, EventArgs e)
		{
			bool flag = File.Exists(this.masterFile);
			if (flag)
			{
				FileStream fileStream = new FileStream(this.masterFile, FileMode.Open);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				this.listFTGI = (List<FinalThesisGradingItem>)binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
				bool flag2 = this.listFTGI != null;
				if (flag2)
				{
					foreach (FinalThesisGradingItem finalThesisGradingItem in this.listFTGI)
					{
						bool flag3 = !this.lstAvailable.Items.Contains(finalThesisGradingItem.SubjectCode);
						if (flag3)
						{
							this.lstAvailable.Items.Add(finalThesisGradingItem.SubjectCode);
						}
					}
				}
				FileInfo fileInfo = new FileInfo(this.masterFile);
				this.Text = string.Concat(new string[]
				{
					this.Text,
					" [.master file last updated: ",
					fileInfo.LastWriteTime.ToLongTimeString(),
					" ",
					fileInfo.LastWriteTime.ToLongDateString(),
					"]"
				});
			}
			bool flag4 = this.listFTGI == null;
			if (flag4)
			{
				this.listFTGI = new List<FinalThesisGradingItem>();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EB8 File Offset: 0x000010B8
		private void dgvGradingItems_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			bool flag = rowIndex >= 0;
			if (flag)
			{
				List<FinalThesisGradingItem> list = (List<FinalThesisGradingItem>)this.dgvGradingItems.DataSource;
				this.current_ftgi = list[rowIndex];
				this.txtMajor.Text = this.current_ftgi.Major;
				this.txtMinor.Text = this.current_ftgi.Minor;
				this.txtItemGroup.Text = this.current_ftgi.ItemGroup;
				this.txtGradingItem.Text = this.current_ftgi.GradingItem;
				this.txtScale.Text = this.current_ftgi.Scale.ToString();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F78 File Offset: 0x00001178
		private void btnDelete_Click(object sender, EventArgs e)
		{
			bool flag = this.current_ftgi != null;
			if (flag)
			{
				this.listFTGI.Remove(this.current_ftgi);
				this.LoadGrid();
				this.SaveMasterFile();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FB8 File Offset: 0x000011B8
		private void lstAvailable_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.lstAvailable.Items.Count > 0;
			if (flag)
			{
				this.txtSubjectCode.Text = this.lstAvailable.Text;
				this.btnLoad.PerformClick();
			}
		}

		// Token: 0x04000024 RID: 36
		private List<FinalThesisGradingItem> listFTGI = null;

		// Token: 0x04000025 RID: 37
		private string masterFile = "FinalThesisGradingItems.master";

		// Token: 0x04000026 RID: 38
		private FinalThesisGradingItem current_ftgi = null;
	}
}
