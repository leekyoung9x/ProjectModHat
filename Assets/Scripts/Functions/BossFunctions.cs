using System;

namespace Functions
{
	public class BossFunctions
	{
		public BossFunctions()
		{
		}

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

        public static void FocusBoss()
        {
            if (BossFunctions.focusBoss && mSystem.currentTimeMillis() - BossFunctions.currFocusBoss >= 500L)
            {
                BossFunctions.currFocusBoss = mSystem.currentTimeMillis();
                for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                {
                    global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
                    if (@char != null && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
                    {
                        global::Char.myCharz().focusManualTo(@char);
                        return;
                    }
                }
            }
        }

        public string NameBoss;

		public string MapName;

		public int MapId;

		public DateTime AppearTime;

		public static bool LineBoss;

        public static bool focusBoss;

        private static long currFocusBoss;
    }
}