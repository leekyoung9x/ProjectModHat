using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Functions
{
   internal class CharFunctions
{
		private bool isAutoRivive;
		public bool AutoRivive()
        {
			return isAutoRivive;
        }
		/// <summary>
		public static CharFunctions _Instance = new CharFunctions();
		public static bool dangLogin;
		public static bool isAutoLogin;
		public static bool isAutoNhatXa;
		public static int xNhatXa;
		public static int yNhatXa;
		private static long currNhatXa;
		/// </summary>
		/// <returns></returns>
		public static CharFunctions getInstance()
		{
			bool flag = CharFunctions._Instance == null;
			if (flag)
			{
				CharFunctions._Instance = new CharFunctions();
			}
			return CharFunctions._Instance;
		}
		public static void AutoLogin()
		{
			CharFunctions.dangLogin = true;
			Thread.Sleep(1000);
			GameCanvas.startOKDlg("Vui Lòng Đợi 25 Giây...");
			Thread.Sleep(23000);
			while (ServerListScreen.testConnect != 2)
			{
				GameCanvas.serverScreen.switchToMe();
				Thread.Sleep(1000);
			}
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			Thread.Sleep(1000);
			GameCanvas.loginScr.switchToMe();
			GameCanvas.loginScr.doLogin();
			CharFunctions.dangLogin = false;
		}
		public static void AutoNhatXa()
		{
			if (CharFunctions.isAutoNhatXa && mSystem.currentTimeMillis() - CharFunctions.currNhatXa >= 2000L)
			{
				CharFunctions.currNhatXa = mSystem.currentTimeMillis();
				for (int i = 0; i < GameScr.vItemMap.size(); i++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
					if (itemMap != null && itemMap.itemMapID == global::Char.myCharz().charID)
					{
						CharFunctions.GotoXY(itemMap.x, itemMap.y);
						Service.gI().pickItem(itemMap.itemMapID);
						CharFunctions.GotoXY(CharFunctions.xNhatXa, CharFunctions.yNhatXa);
						return;
					}
					if (itemMap != null)
					{
						CharFunctions.GotoXY(itemMap.x, itemMap.y);
						Service.gI().pickItem(itemMap.itemMapID);
						CharFunctions.GotoXY(CharFunctions.xNhatXa, CharFunctions.yNhatXa);
						return;
					}
				}
			}
		}
		public static void GotoXY(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}
		public void update()
        {
            if (this.isAutoRivive)
            {
				Revive();
            }
			CharFunctions.AutoNhatXa();
		}
		public void Handler()
        {
			this.isAutoRivive = !this.isAutoRivive;
        }
		public static void Revive()
		{
			if (global::Char.myCharz().luong + global::Char.myCharz().luongKhoa > 0 && global::Char.myCharz().meDead && global::Char.myCharz().cHP <= 0 && GameCanvas.gameTick % 20 == 0)
			{
				Service.gI().wakeUpFromDead();
				global::Char.myCharz().meDead = false;
				global::Char.myCharz().statusMe = 1;
				global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
				global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
				global::Char @char = global::Char.myCharz();
				global::Char char2 = global::Char.myCharz();
				global::Char.myCharz().cp3 = 0;
				char2.cp2 = 0;
				@char.cp1 = 0;
				ServerEffect.addServerEffect(109, global::Char.myCharz(), 2);
				GameScr.gI().center = null;
				GameScr.isHaveSelectSkill = true;
			}
		}
	}
}
