using System;

namespace MapObject
{
	// Token: 0x020000CB RID: 203
	public struct NextMapObject
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x0000660A File Offset: 0x0000480A
		public NextMapObject(int mapID, MapEnum type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x0400133D RID: 4925
		public int MapID;

		// Token: 0x0400133E RID: 4926
		public MapEnum Type;

		// Token: 0x0400133F RID: 4927
		public int[] Info;
	}
}
