using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuGrade.Properties
{
	// Token: 0x02000017 RID: 23
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600012D RID: 301 RVA: 0x0001181D File Offset: 0x0000FA1D
		internal Resources()
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00011828 File Offset: 0x0000FA28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("FuGrade.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00011870 File Offset: 0x0000FA70
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00011887 File Offset: 0x0000FA87
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000120 RID: 288
		private static ResourceManager resourceMan;

		// Token: 0x04000121 RID: 289
		private static CultureInfo resourceCulture;
	}
}
