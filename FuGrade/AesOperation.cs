using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FuGrade
{
	// Token: 0x02000002 RID: 2
	public class AesOperation
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string EncryptString(string key, string plainText)
		{
			string result;
			try
			{
				bool flag = key == null;
				if (flag)
				{
					key = AesOperation.DEFAULT_KEY;
				}
				byte[] iv = new byte[16];
				byte[] inArray;
				using (Aes aes = Aes.Create())
				{
					aes.Key = Encoding.UTF8.GetBytes(key);
					aes.IV = iv;
					ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
						{
							using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
							{
								streamWriter.Write(plainText);
							}
							inArray = memoryStream.ToArray();
						}
					}
				}
				result = Convert.ToBase64String(inArray);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002170 File Offset: 0x00000370
		public static string DecryptString(string key, string cipherText)
		{
			bool flag = key == null;
			if (flag)
			{
				key = AesOperation.DEFAULT_KEY;
			}
			byte[] iv = new byte[16];
			byte[] buffer = Convert.FromBase64String(cipherText);
			string result;
			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(key);
				aes.IV = iv;
				ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private static readonly string DEFAULT_KEY = "l10ca968o8e4133tyne2ea2315g19377";
	}
}
