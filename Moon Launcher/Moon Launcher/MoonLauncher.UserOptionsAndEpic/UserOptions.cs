using System;
using Newtonsoft.Json;

namespace Moon_Launcher.Models
{
	// Token: 0x02000008 RID: 8
	public class UserSettings
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000248B File Offset: 0x0000068B
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002493 File Offset: 0x00000693
		[JsonProperty("secret")]
		public string Secret { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000249C File Offset: 0x0000069C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000024A4 File Offset: 0x000006A4
		[JsonProperty("currentInstall")]
		public string CurrentInstall { get; set; }
	}
}
