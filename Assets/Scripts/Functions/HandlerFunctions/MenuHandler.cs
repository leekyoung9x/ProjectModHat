using Functions.AutoFunctions;
using MapObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Functions.HandlerFunctions
{
    internal class MenuHandler
{
		public static string[][] MenuOption = new string[][]
	{
			new string[]
			{
				"D.sách",
				"Item"
			},
			new string[]
			{
				"Chức",
				"Năng"
			},
			new string[]
            {
				"H.Dẫn","Sử Dụng"
            }
	};
		public static int PANEL_TYPE { get; set; }
		/// <summary>
		public static int[] LIST_ITEM_ICONID = new int[]
		{
			2755,
			2756,
			2754,
			2757,
			2760,
			6324,
			6325,
			6326,
			6327,
			6328,
			2758,
			7149,
			8060,
			8061,
			8062,
			10714,
			10715,
			10716,
			10712,
			10717
		};

		// Token: 0x04001395 RID: 5013
		public static string[] LIST_ITEM_NAME = new string[]
		{
			"Bổ huyết",
			"Bổ khí",
			"Cuồng nộ",
			"Giáp Xên bọ hung",
			"Ẩn danh",
			"Bánh Pudding",
			"Xúc xích",
			"Kem dâu",
			"Mì ly",
			"Sushi",
			"Máy dò Capsule kì bí",
			"Khẩu trang",
			"Cua rang me",
			"Bạch tuộc nướng",
			"Tôm tẩm bột chiên xù",
			"Bổ huyết 2",
			"Bổ khí 2",
			"Cuồng nộ 2",
			"Giáp xên bọ hung 2",
			"Ẩn danh 2"
		};

		// Token: 0x04001396 RID: 5014
		public static string[] TUTORIAL_SETTINGS = new string[]
		{
			"1.Lệnh Chat:",
			"ak: Tự Đánh",
			"dapdo: Auto Đập Đồ",
			"tdlt: Tàn Sát",
			"k X: Đổi Sang Khu X",
			//"vq: Auto Quay Thượng Đế",
			"alogin: Auto Login",
			"ahs: Auto Hồi Sinh",
			"anhat: Auto Nhặt",
			"2.Phím Tắt:",
		"A: Tự Đánh",
		"X: Menu Mod",
		"C: Capsule Nhanh",
		"F: Bông Tai Nhanh",
		"M: Mở Bảng Đổi Khu",
		"M: Giao Dịch",
		"E: Auto Hồi Sinh"
		};
		
		public static string[] GRAPHIC_SETTING_LIST_NAME = new string[]
		{
			"Xóa Map",
			"Bật Nền Màu RGB",
			"Danh Sách Người Chơi",
			"Thông Báo Boss",
			"Đường Kẻ Tới Boss",
			"Tàn Sát",
			"Auto Nhặt",
			"Auto Login",
			"Auto Hồi Sinh",
			"Auto Up Đệ"
			
		};
		public static string Setting_SubNames(int index)
		{
			string result;
			switch (index)
			{
				case 0:
					result = "Trạng thái: " + InfoHandler.StatusMenu(GraphicsHandler.xoamap);
					break;
				case 1:
					result = "Trạng thái: " + InfoHandler.StatusMenu(GraphicsHandler.enablePaintColor_Wallpaper);
					break;
				case 2:
					result = "Trạng thái: " + InfoHandler.StatusMenu(GraphicsHandler.listchar);
					break;
				case 3:
					result = "Trạng thái: " + InfoHandler.StatusMenu(MainFunctions.isSanBoss);
					break;
				case 4:
					result = "Trạng thái: " + InfoHandler.StatusMenu(BossFunctions.LineBoss);
					break;
				case 5:
					result = "Trạng thái: " + InfoHandler.StatusMenu(AutoPickMobHandler.IsTanSat);
					break;
				case 6:
					result = "Trạng thái: " + InfoHandler.StatusMenu(AutoPickFunctions.isAutoPick);
					break;
				case 7:
					result = "Trạng thái: " + InfoHandler.StatusMenu(CharFunctions.isAutoLogin);
					break;
				case 8:
					result = "Trạng thái: " + InfoHandler.StatusMenu(CharFunctions.getInstance().AutoRivive());
					break;
				case 9:
					result = "Trạng thái: " + InfoHandler.StatusMenu(Char.isPetHandler);
					break;
				default:
					result = string.Empty;
					break;
			}
			return result;
		}
		public static void doFireGraphicSetting(int selected)
		{
			bool flag = selected == -1;
			if (!flag)
			{
				switch (selected)
				{
					case 0:
						GraphicsHandler.xoamap = !GraphicsHandler.xoamap;
						GameScr.info1.addInfo("Xóa Map: " + InfoHandler.Status(GraphicsHandler.xoamap), 0);
						break;
					case 1:
						{
							bool flag2 = !GraphicsHandler.enablePaintColor_Wallpaper;
							if (flag2)
							{
								ChatableHandler.gI().OpenChat("Nhập mã màu nền");
								GraphicsHandler.enablePaintColor_Wallpaper = true;
							}
							else
							{
								GraphicsHandler.enablePaintColor_Wallpaper = false;
							}
							GameScr.info1.addInfo("Bật hình nền Color RGB: " + InfoHandler.Status(GraphicsHandler.enablePaintColor_Wallpaper), 0);
							break;
						}
					case 2:
						GraphicsHandler.listchar = !GraphicsHandler.listchar;
						GameScr.info1.addInfo("D.sách Nhân Vật: " + InfoHandler.Status(GraphicsHandler.listchar), 0);
						break;
					case 3:
						MainFunctions.isSanBoss = !MainFunctions.isSanBoss;
						GameScr.info1.addInfo("Thông Báo Boss: " + InfoHandler.Status(MainFunctions.isSanBoss), 0);
						break;
					case 4:
						BossFunctions.LineBoss = !BossFunctions.LineBoss;
						GameScr.info1.addInfo("Đường Kẻ Tới Boss: " + InfoHandler.Status(BossFunctions.LineBoss), 0);
						break;
					case 5:
						AutoPickMobHandler.IsTanSat = !AutoPickMobHandler.IsTanSat;
						GameScr.info1.addInfo("Tàn Sát: " + InfoHandler.Status(AutoPickMobHandler.IsTanSat), 0);
						break;
					case 6:
						AutoPickFunctions.isAutoPick = !AutoPickFunctions.isAutoPick;
						GameScr.info1.addInfo("Auto Nhặt: " + InfoHandler.Status(AutoPickFunctions.isAutoPick), 0);
						break;
					case 7:
						CharFunctions.isAutoLogin = !CharFunctions.isAutoLogin;
						GameScr.info1.addInfo("Auto Login: " + InfoHandler.Status(CharFunctions.isAutoLogin), 0);
						break;
					case 8:
						CharFunctions.getInstance().Handler();
						GameScr.info1.addInfo("Auto Hồi Sinh: " + InfoHandler.Status(CharFunctions.getInstance().AutoRivive()), 0);
						break;
					case 9:
						Char.isPetHandler = !Char.isPetHandler;
						GameScr.info1.addInfo("Auto Up Đệ: " + InfoHandler.Status(Char.isPetHandler), 0);
						break;
					
				}
			}
		}
		/// </summary>
		/// <param name="panelType"></var>

		// Token: 0x06000A8E RID: 2702 RVA: 0x000AE8E1 File Offset: 0x000ACAE1
		public static void paintItemList(mGraphics g)
		{
			g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
			g.translate(0, -GameCanvas.panel.cmy);
			for (int i = 0; i < MenuHandler.LIST_ITEM_NAME.Length; i++)
			{
				int xScroll = GameCanvas.panel.xScroll;
				int num = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
				int w = GameCanvas.panel.wScroll - 1;
				int h = GameCanvas.panel.ITEM_HEIGHT - 1;
				bool flag = num - GameCanvas.panel.cmy <= GameCanvas.panel.yScroll + GameCanvas.panel.hScroll && num - GameCanvas.panel.cmy >= GameCanvas.panel.yScroll - GameCanvas.panel.ITEM_HEIGHT;
				mFont mFont = (ItemHandler.ItemQuantity(MenuHandler.LIST_ITEM_ICONID[i], "iconID") > 0) ? mFont.tahoma_7_yellow : mFont.tahoma_7b_dark;
				if (flag)
				{
					g.setColor((i != GameCanvas.panel.selected) ? 0 : 0, 0.5f);
					g.fillRect(xScroll, num, w, h);
					bool flag2 = mGraphics.zoomLevel == 2 || mGraphics.zoomLevel == 3|| mGraphics.zoomLevel == 4;
					if (flag2)
					{
						mFont.tahoma_7_white.drawStringBd(g, MenuHandler.LIST_ITEM_NAME[i], xScroll + 30, num, 0, mFont.tahoma_7b_dark);
					}
					else
					{
						bool flag3 = mGraphics.zoomLevel == 1;
						if (flag3)
						{
							mFont.tahoma_7b_green.drawString(g, MenuHandler.LIST_ITEM_NAME[i], xScroll + 30, num, 0);
						}
					}
					SmallImage.drawSmallImage(g, MenuHandler.LIST_ITEM_ICONID[i], xScroll + 2, num + 2, 0, 0);
					string st = (ItemHandler.ItemQuantity(MenuHandler.LIST_ITEM_ICONID[i], "iconID") > 0) ? ("Số lượng: x" + ItemHandler.ItemQuantity(MenuHandler.LIST_ITEM_ICONID[i], "iconID").ToString()) : "Không có ITEM";
					
					foreach (ItemHandler.Items items in ItemHandler.ListItemAuto)
					{
						bool flag4 = items.iconID == MenuHandler.LIST_ITEM_ICONID[i];
						if (flag4)
						{
							mFont = mFont.tahoma_7b_red;
							g.setColor((i != GameCanvas.panel.selected) ? 0 : 0, 0.5f);
							g.fillRect(xScroll, num, w, h);
							st = "ẤN ĐỂ XÓA KHỎI DANH SÁCH ! ! !";
						}
					}
					mFont.drawString(g, st, xScroll + 30, num + 11, 0);
				}
			}
			GameCanvas.panel.paintScrollArrow(g);
		}
		

		// Token: 0x06000A88 RID: 2696 RVA: 0x000AE15C File Offset: 0x000AC35C
		public static void paintGraphicSetting(mGraphics g)
		{
			g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
			g.translate(0, -GameCanvas.panel.cmy);
			for (int i = 0; i < MenuHandler.GRAPHIC_SETTING_LIST_NAME.Length; i++)
			{
				int xScroll = GameCanvas.panel.xScroll;
				int num = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
				int w = GameCanvas.panel.wScroll - 1;
				int h = GameCanvas.panel.ITEM_HEIGHT - 1;
				bool flag = num - GameCanvas.panel.cmy <= GameCanvas.panel.yScroll + GameCanvas.panel.hScroll && num - GameCanvas.panel.cmy >= GameCanvas.panel.yScroll - GameCanvas.panel.ITEM_HEIGHT;
				if (flag)
				{
					g.setColor((i != GameCanvas.panel.selected) ? 0 : 0, 0.5f);
					g.fillRect(xScroll, num, w, h);
					mFont.tahoma_7_white.drawString(g, (i + 1).ToString() + ". " + MenuHandler.GRAPHIC_SETTING_LIST_NAME[i], xScroll + 5, num, 0);
					bool flag2 = MenuHandler.Setting_SubNames(i).Contains("Trạng thái: Đang Bật");
					if (flag2)
					{
						g.setColor((i != GameCanvas.panel.selected) ? 0 : 0, 0.5f);
						g.fillRect(xScroll, num, w, h);
					}
					((MenuHandler.Setting_SubNames(i) == "Trạng thái: Đang Bật") ? mFont.tahoma_7_yellow : mFont.tahoma_7).drawString(g, MenuHandler.Setting_SubNames(i), xScroll + 5, num + 11, 0);
				}
			}
		}
		public static void paintTutorialSettings(mGraphics g)
		{
			g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
			g.translate(0, -GameCanvas.panel.cmy);
				for (int i = 0; i < TUTORIAL_SETTINGS.Length; i++)
				{
					int xScroll = GameCanvas.panel.xScroll;
					int num = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
					int w = GameCanvas.panel.wScroll - 1;
					int h = GameCanvas.panel.ITEM_HEIGHT - 1;
					bool flag = num - GameCanvas.panel.cmy <= GameCanvas.panel.yScroll + GameCanvas.panel.hScroll && num - GameCanvas.panel.cmy >= GameCanvas.panel.yScroll - GameCanvas.panel.ITEM_HEIGHT;
					if (flag)
					{
						g.setColor((i != GameCanvas.panel.selected) ? 0 : 0, 0.5f);
						g.fillRect(xScroll, num, w, h);
						mFont.tahoma_7b_white.drawStringBd(g, MenuHandler.TUTORIAL_SETTINGS[i], xScroll + 5, num, 0,mFont.tahoma_7b_green);
					}
				}
			
          
		}
		// Token: 0x06000A89 RID: 2697 RVA: 0x000AE32C File Offset: 0x000AC52C
		
		public static void doFireMenu()
		{
			switch (GameCanvas.panel.currentTabIndex)
			{
				case 0:
					MenuHandler.doFireItem(GameCanvas.panel.selected);
					break;
				case 1:
					MenuHandler.doFireGraphicSetting(GameCanvas.panel.selected);
					break;
				
			}
		}
		public static void paintMenuMod(mGraphics g)
		{
			int panel_TYPE = MenuHandler.PANEL_TYPE;
			int num = panel_TYPE;
			if (num == 0)
			{
				MenuHandler.paintModMenu(g);
			}
		}
		public static void paintModMenu(mGraphics g)
		{
			switch (GameCanvas.panel.currentTabIndex)
			{
				case 0:
					MenuHandler.paintItemList(g);
					break;
				case 1:
					MenuHandler.paintGraphicSetting(g);
					break;
				case 2:
					MenuHandler.paintTutorialSettings(g);
					break;
			
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000ADE94 File Offset: 0x000AC094
		public static void doFireItem(int selected)
		{
			bool flag = selected == -1;
			if (!flag)
			{
				for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
				{
					Item item = global::Char.myCharz().arrItemBag[i];
					bool flag2 = item == null;
					if (flag2)
					{
						break;
					}
					bool flag3 = ItemHandler.ItemQuantity(MenuHandler.LIST_ITEM_ICONID[selected], "iconID").Equals(0);
					if (flag3)
					{
						GameScr.info1.addInfo("Bạn Không Có Item!", 0);
						break;
					}
					bool flag4 = ItemHandler.ItemQuantity(MenuHandler.LIST_ITEM_ICONID[selected], "iconID") > 0;
					if (flag4)
					{
						bool flag5 = item.template.name == MenuHandler.LIST_ITEM_NAME[selected];
						if (flag5)
						{
							ItemHandler.AddItemstoList(item);
						}
					}
				}
			}
		}
		public static void setTypeMenuMod(int panelType)
		{
			GameCanvas.panel.type = 23;
			MenuHandler.PANEL_TYPE = panelType;
			MenuHandler.setTypeModMenu();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000AE900 File Offset: 0x000ACB00
		public static void setTypeModMenu()
		{
			SoundMn.gI().getSoundOption();
			GameCanvas.panel.setType(0);
			bool flag = MenuHandler.PANEL_TYPE == 0;
			if (flag)
			{
				GameCanvas.panel.tabName[23] = MenuHandler.MenuOption;
				GameCanvas.panel.setType(0);
				MenuHandler.setTabMenuMod();
			}
		}
		public static void setTabMenuMod()
		{
			int panel_TYPE = MenuHandler.PANEL_TYPE;
			int num = panel_TYPE;
			if (num == 0)
			{
				MenuHandler.setTabModMenu();
			}
		}
		public static void setTabModMenu()
		{
			switch (GameCanvas.panel.currentTabIndex)
			{
				case 0:
					GameCanvas.panel.currentListLength = MenuHandler.LIST_ITEM_NAME.Length;
					break;
				case 1:
					GameCanvas.panel.currentListLength = MenuHandler.GRAPHIC_SETTING_LIST_NAME.Length;
					break;
			
			}
			GameCanvas.panel.ITEM_HEIGHT = 24;
			GameCanvas.panel.selected = (GameCanvas.isTouch ? -1 : 0);
			GameCanvas.panel.cmyLim = GameCanvas.panel.currentListLength * GameCanvas.panel.ITEM_HEIGHT - GameCanvas.panel.hScroll;
			bool flag = GameCanvas.panel.cmyLim < 0;
			if (flag)
			{
				GameCanvas.panel.cmyLim = 0;
			}
			GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = GameCanvas.panel.cmyLast[GameCanvas.panel.currentTabIndex]);
			bool flag2 = GameCanvas.panel.cmy < 0;
			if (flag2)
			{
				GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
			}
			bool flag3 = GameCanvas.panel.cmy > GameCanvas.panel.cmyLim;
			if (flag3)
			{
				GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = GameCanvas.panel.cmyLim);
			}
		}
	}
}
