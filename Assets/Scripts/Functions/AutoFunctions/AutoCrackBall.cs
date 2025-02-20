using System;
using System.Threading;
using UnityEngine;

namespace Functions.AutoFunctions
{
	internal class AutoCrackBall : IActionListener
	{
		public static bool isauto;

		public static bool khongdu;

		private static int type;

		public int typenhando;

		private static AutoCrackBall instance;

		public static bool dangNhanAll;

		public static bool NhanAll;

		public static AutoCrackBall gI()
		{
			if (instance != null)
			{
				return instance;
			}
			return instance = new AutoCrackBall();
		}

		public static void startMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Mở thường", gI(), 1, 1));
			//myVector.addElement(new Command("Mở đặc\nbiệt", gI(), 1, 2));
			myVector.addElement(new Command("Nhận đồ.", gI(), 2, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public void update()
		{
			//if (AutoCrackBall.isauto && Input.GetKey(";"))
			//{
			//    AutoCrackBall.isauto = false;
			//    GameScr.info1.addInfo("đã tắt quay thượng đê!", 0);
			//}
			if (isauto)
			{
				Service.gI().openMenu(19);
				Thread.Sleep(500);
				Service.gI().confirmMenu(19, (sbyte)(GameCanvas.menu.menuItems.size() - 1));
				Thread.Sleep(500);
				Service.gI().confirmMenu(19, (sbyte)type);
				Thread.Sleep(1000);
			}
			while (isauto && !dangNhanAll)
			{
				try
				{
					Service.gI().SendCrackBall(2, 7);
					Thread.Sleep(500);
					if (khongdu)
					{
						isauto = false;
						khongdu = false;
						GameScr.info1.addInfo("Xong.", 0);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public static void infoMe(string s)
		{
			//AutoGoiRong.infoMe(s);
			if (s.Contains(mResources.not_enough_money_1) && isauto)
			{
				khongdu = true;
			}
			if (NhanAll && s.ToLower().Contains("rương phụ đã đầy") && isauto)
			{
				if (GameScr.gI().isBagFull())
				{
					isauto = false;
					ChatPopup.currChatPopup = null;
					Effect2.vEffect2.removeAllElements();
					Effect2.vEffect2Outside.removeAllElements();
					InfoDlg.hide();
					GameCanvas.menu.doCloseMenu();
					GameCanvas.panel.cp = null;
					GameCanvas.startOKDlg("Hành trang đã đầy hãy dọn để quay tiếp !");
					return;
				}
				new Thread(NhanAllThuongDe).Start();
				isauto = false;
				GameScr.info1.addInfo("Xong.", 0);
			}
		}

		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				type = (int)p;
				isauto = true;
				new Thread(gI().update).Start();
			}
			if (idAction == 2)
			{
				MyVector myVector = new MyVector();
				myVector.addElement(new Command("Nhận vàng", gI(), 3, 9));
				myVector.addElement(new Command("Nhận bùa", gI(), 3, 13));
				myVector.addElement(new Command("Nhận đồ", gI(), 3, -1));
				myVector.addElement(new Command("Nhận cải trang", gI(), 3, 5));
				myVector.addElement(new Command("Nhận Tất Cả", gI(), 4, 0));
				GameCanvas.menu.startAt(myVector, 3);
			}
			if (idAction == 3)
			{
				typenhando = (int)p;
				new Thread(runnhando).Start();
			}
			if (idAction == 4)
			{
				NhanAll = !NhanAll;
				GameScr.info1.addInfo("Nhận tất cả:" + (NhanAll ? "ON" : "OFF"), 0);
			}
		}

		public void runnhando()
		{
			Service.gI().openMenu(19);
			Thread.Sleep(700);
			Service.gI().confirmMenu(19, (sbyte)(GameCanvas.menu.menuItems.size() - 1));
			ChatPopup.currChatPopup = null;
			Effect2.vEffect2.removeAllElements();
			Effect2.vEffect2Outside.removeAllElements();
			InfoDlg.hide();
			GameCanvas.menu.doCloseMenu();
			GameCanvas.panel.cp = null;
			Thread.Sleep(700);
			if (GameCanvas.menu.menuItems.size() != 5)
			{
				GameScr.info1.addInfo("Không có đồ.", 0);
				return;
			}
			Service.gI().confirmMenu(19, 3);
			Thread.Sleep(500);
			ChatPopup.currChatPopup = null;
			Effect2.vEffect2.removeAllElements();
			Effect2.vEffect2Outside.removeAllElements();
			InfoDlg.hide();
			GameCanvas.menu.doCloseMenu();
			GameCanvas.panel.cp = null;
			if (typenhando == -1)
			{
				for (int i = 0; i < Char.myCharz().arrItemShop[0].Length; i++)
				{
					Item item = Char.myCharz().arrItemShop[0][i];
					if (item != null && (item.template.type == 0 || item.template.type == 1 || item.template.type == 2 || item.template.type == 3 || item.template.type == 4))
					{
						Service.gI().buyItem(0, i, 0);
					}
				}
				return;
			}
			int num = 0;
			while (num < Char.myCharz().arrItemShop[0].Length)
			{
				Item item2 = Char.myCharz().arrItemShop[0][num];
				if (item2 != null && item2.template.type == typenhando)
				{
					Service.gI().buyItem(0, num, 0);
					Thread.Sleep(500);
				}
				else
				{
					num++;
				}
			}
		}

		static AutoCrackBall()
		{
			isauto = false;
			khongdu = false;
			type = 1;
		}

		public static void NhanAllThuongDe()
		{
			dangNhanAll = true;
			Service.gI().openMenu(19);
			Thread.Sleep(1000);
			Service.gI().confirmMenu(19, 1);
			Service.gI().confirmMenu(19, 3);
			Service.gI().buyItem(2, 0, 0);
			Thread.Sleep(500);
			dangNhanAll = false;
			isauto = true;
			new Thread(gI().update).Start();
		}
	}
}
