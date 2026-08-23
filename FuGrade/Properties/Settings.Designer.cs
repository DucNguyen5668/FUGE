using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FuGrade.Properties
{
	// Token: 0x02000018 RID: 24
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00011890 File Offset: 0x0000FA90
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000122 RID: 290
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
