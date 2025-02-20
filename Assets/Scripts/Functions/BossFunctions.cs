using System;

namespace Functions
{
	public class BossFunctions
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00003CC4 File Offset: +0x00001EC4
		public BossFunctions()
		{
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00091DB4 File Offset: 0x0008FFB4
		public BossFunctions(string a)
		{
			a = a.Replace("BOSS ", "").Replace(" vừa xuất hiện tại ", "|").Replace(" appear at ", "|");
			string[] array = a.Split(new char[]
			{
			'|'
			});
			this.NameBoss = array[0].Trim();
			this.MapName = array[1].Trim();
			this.MapId = this.GetMapID(this.MapName);
			this.AppearTime = DateTime.Now;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00091E44 File Offset: 0x00090044
		public int GetMapID(string a)
		{
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				if (TileMap.mapNames[i].Equals(a))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00091E78 File Offset: 0x00090078
		public void paint(mGraphics a, int b, int c, int d)
		{
			TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
			int num = (int)timeSpan.TotalSeconds;
			mFont mFont = mFont.tahoma_7_yellow;
			if (TileMap.mapID == this.MapId)
			{
				mFont = mFont.tahoma_7_red;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					if (((global::Char)GameScr.vCharInMap.elementAt(i)).cName.Equals(this.NameBoss))
					{
						mFont = mFont.tahoma_7b_red;
						break;
					}
				}
			}
			mFont.drawString(a, string.Concat(new string[]
			{
			this.NameBoss,
			" - ",
			this.MapName,
			" - ",
			(num < 60) ? (num.ToString() + "s") : (timeSpan.Minutes.ToString() + "p"),
			" trước"
			}), b, c, d);
		}

		// Token: 0x0400147C RID: 5244
		public string NameBoss;

		// Token: 0x0400147D RID: 5245
		public string MapName;

		// Token: 0x0400147E RID: 5246
		public int MapId;

		// Token: 0x0400147F RID: 5247
		public DateTime AppearTime;

		public static bool LineBoss;
	}
}