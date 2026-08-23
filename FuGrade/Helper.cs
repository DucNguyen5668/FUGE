using System;
using System.Security.Cryptography;
using System.Text;

namespace FuGrade
{
	// Token: 0x02000013 RID: 19
	public class Helper
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00011618 File Offset: 0x0000F818
		public static string GetMd5Hash(MD5 md5Hash, string input)
		{
			byte[] array = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00011678 File Offset: 0x0000F878
		public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
		{
			string md5Hash2 = Helper.GetMd5Hash(md5Hash, input);
			StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
			return ordinalIgnoreCase.Compare(md5Hash2, hash) == 0;
		}
	}
}
