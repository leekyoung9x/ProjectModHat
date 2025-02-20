using Functions.AutoFunctions;
using Functions.HandlerFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Functions
{
  internal class ScreenFunctions
{
		/// <summary>
		private static Image imgSettings = mSystem.loadImage("/pc/myTexture2dSettings.png");
		private static Image imgLogo = mSystem.loadImage("/mainImage/logo1.png");
		public static Image imgtich = mSystem.loadImage("/mainImage/tich.png");
		/// </summary>
		/// <param ></param>
		public static void Paint(mGraphics g) {
			paintListBosses(g);
			paintLineBoss(g);
			paintListChar(g);
			paintUpgrade(g);
			drawString(g);
			paintModInfo(g);
            if (MenuFunctions.isPetSPInfo)
            {
				paintInfo(g);
            }
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				g.drawImage(imgSettings, 160, 3); 
			}
			//g.drawImage(imgLogo, GameCanvas.w / 2, 25, 3);
			if (global::Char.myCharz().mobFocus != null && global::Char.myCharz().charFocus == null)
			{
				paintMobFocusInfo(g, Char.myCharz().mobFocus);
			}
		}
		private static void paintInfo(mGraphics g) {
			int num = 39; //x
			mFont.tahoma_7_red.drawStringBd(g, "Sư Phụ: [" + Char.myCharz().cName + "]", num, GameCanvas.h - 160, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBd(g, "Sức Mạnh: " + NinjaUtil.getMoneys(Char.myCharz().cPower), num, GameCanvas.h - 150, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBd(g, "Tiềm Năng: " + NinjaUtil.getMoneys(Char.myCharz().cTiemNang), num, GameCanvas.h - 140, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBd(g, string.Concat(new object[2]
			{
				"Sức Đánh: ",
				NinjaUtil.getMoneys(Char.myCharz().cDamFull)
			}), num, GameCanvas.h - 130, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBd(g, "HP: " + NinjaUtil.getMoneys(Char.myCharz().cHPFull), num, GameCanvas.h - 120, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBd(g, "MP: " + NinjaUtil.getMoneys(Char.myCharz().cMPFull), num, GameCanvas.h - 110, mFont.LEFT, mFont.tahoma_7_grey);
			num += GameCanvas.w / 4;
			if (Char.myCharz().havePet)
            {
				Service.gI().petInfo();
				mFont.tahoma_7_red.drawStringBd(g, "Đệ Tử: [" + Char.myPetz().cName + "]", num, GameCanvas.h - 160, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_white.drawStringBd(g, "Sức Mạnh: " + NinjaUtil.getMoneys(Char.myPetz().cPower), num, GameCanvas.h - 150, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_white.drawStringBd(g, "Tiềm Năng: " + NinjaUtil.getMoneys(Char.myPetz().cTiemNang), num, GameCanvas.h - 140, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_white.drawStringBd(g, string.Concat(new object[2]
				{
				"Sức Đánh: ",
				NinjaUtil.getMoneys(Char.myPetz().cDamFull)
				}), num, GameCanvas.h - 130, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_white.drawStringBd(g, "HP: " + NinjaUtil.getMoneys(Char.myPetz().cHP), num, GameCanvas.h - 120, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_white.drawStringBd(g, "MP: " + NinjaUtil.getMoneys(Char.myPetz().cMP), num, GameCanvas.h - 110, mFont.LEFT, mFont.tahoma_7_grey);
				num += GameCanvas.w / 4;
			}
            else
            {
				GameScr.info1.addInfo("|5|Chưa Có Đệ Tử!",0);
            }
		}
		private static void paintModInfo(mGraphics g)
        {
			int num = 140;
			mFont mfont = mFont.tahoma_7b_unfocus;
			int anchor = mFont.LEFT;
            if (SkillFunctions.isAutoSendAttack)
            {
				mfont.drawString(g, "Tự Đánh: Bật", 550, num, anchor);
				num += 10;
            }
            if (Char.isPetHandler)
            {
				mfont.drawString(g, "Auto Up Đệ: Bật" , 550, num, anchor);
				num += 10;
            }
            if (AutoPickMobHandler.IsTanSat)
            {
				mfont.drawString(g, "Tàn Sát: Bật", 550, num, anchor);
				num += 10;
            }
            if (MainFunctions.isSanBoss)
            {
				mfont.drawString(g, "Thông Báo Boss: Bật", 550, num, anchor);
				num += 10;
			}
            if (CharFunctions.getInstance().AutoRivive())
            {
				mfont.drawString(g, "Auto Hồi Sinh: Bật", 550, num, anchor);
				num += 10;
			}
            if (CharFunctions.isAutoLogin)
            {
				mfont.drawString(g, "Auto Login: Bật", 550, num, anchor);
				num += 10;
			}
            if (AutoPickFunctions.isAutoPick)
            {
				mfont.drawString(g, "Auto Nhặt: Bật", 550, num, anchor);
				num += 10;
			}
            if (GraphicsHandler.xoamap)
            {
				mfont.drawString(g, "Xóa Map: Bật", 550, num, anchor);
				num += 10;
			}
            if (GraphicsHandler.listchar)
            {
				mfont.drawString(g, "D.Sách Nhân Vật: Bật", 550, num, anchor);
				num += 10;
			}
            if (GraphicsHandler.enablePaintColor_Wallpaper)
            {
				mfont.drawString(g, "Mã Màu Nền: " + GraphicsHandler.ColorRGB, 550, num, anchor);
				num += 10;
			}
        }
		private static void paintMobFocusInfo(mGraphics g, Mob mobTemplate)
		{
			mFont.tahoma_7b_red.drawString(g, string.Concat(new string[]
			{
		mobTemplate.getTemplate().name,
		" [",
		NinjaUtil.getMoneys((long)mobTemplate.hp),
		"/",
		NinjaUtil.getMoneys((long)mobTemplate.maxHp),
		"]"
			}), GameCanvas.w / 2, 62, 2);
			int num = 72;
			int num2 = 10;
			if (mobTemplate.sleepEff)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị Thôi Miên", GameCanvas.w / 2, num, 2);
				num += num2;
			}
			if (mobTemplate.blindEff)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị Choáng", GameCanvas.w / 2, num, 2);
				num += num2;
			}
			if (mobTemplate.isFreez)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị TDHS", GameCanvas.w / 2, num, 2);
				num += num2;
			}
			if (mobTemplate.holdEffID != 0)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị Trói", GameCanvas.w / 2, num, 2);
				num += num2;
			}
		}

		private static void drawString(mGraphics g)
        {

			mFont.tahoma_7_white.drawStringBd(g, string.Format("| {0} | ID {1} - Khu {2}", TileMap.mapName, TileMap.mapID, TileMap.zoneID), 7, 80, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7b_blue.drawStringBd(g, "X: " + Char.myCharz().cx + " - Y:" + Char.myCharz().cy, 10, 105, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.bigNumber_blue.drawString(g,"Cheat: " + Time.timeScale.ToString(), 85, 30, mFont.LEFT);
			//mFont.tahoma_7b_white.drawStringBd(g, "Mod 231 By -  PQM", GameCanvas.w - 210, 5, 1, mFont.tahoma_7_grey);//demopqm
			mFont.tahoma_7_white.drawStringBd(g, DateTime.Now.ToString(), 7, 90, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.bigNumber_While.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().cHP), 90, 5, mFont.LEFT);
			mFont.bigNumber_While.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().cMP), 90, 17, mFont.LEFT);

		}
		
		private static void paintUpgrade(mGraphics g)
        {
			if (AutoUpgradeFunctions.isDapDo)
	{
		mFont.tahoma_7b_red.drawString(g, (AutoUpgradeFunctions.doDeDap != null) ? AutoUpgradeFunctions.doDeDap.template.name : "Chưa Có", GameCanvas.w / 2, 72, mFont.CENTER);
		mFont.tahoma_7b_red.drawString(g, (AutoUpgradeFunctions.doDeDap != null) ? ("Số Sao : " + AutoUpgradeFunctions.saoHienTai.ToString()) : "Số Sao : -1", GameCanvas.w / 2, 82, mFont.CENTER);
		mFont.tahoma_7b_red.drawString(g, "Số Sao Cần Đập : " + AutoUpgradeFunctions.soSaoCanDap + " Sao", GameCanvas.w / 2, 92, mFont.CENTER);
	}
	if (AutoUpgradeFunctions.isDapDo /*|| AutoUpgradeFunctions.isThuongDeThuong || AutoUpgradeFunctions.isThuongDeVip*/)
	{
		mFont.tahoma_7b_red.drawString(g, "Ngọc Xanh : " + NinjaUtil.getMoneys((long)global::Char.myCharz().luong) + " Ngọc Hồng : " + NinjaUtil.getMoneys((long)global::Char.myCharz().luongKhoa), GameCanvas.w / 2, 102, mFont.CENTER);
		mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
		{
			"Vàng : ",
			NinjaUtil.getMoneys(global::Char.myCharz().xu),
			" Thỏi Vàng : ",
			AutoUpgradeFunctions.thoiVang()
		}), GameCanvas.w / 2, 112, mFont.CENTER);
	}
        }

		private static void paintLineBoss(mGraphics g) {
			if (BossFunctions.LineBoss)
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (@char != null && @char != null && @char.cTypePk == 5)
					{
						if (global::Char.myCharz().charFocus == @char)
						{
							g.setColor(Color.green);
							g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
						}
						else
						{
							g.setColor(Color.red);
							g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
						}
					}
				}
			}
		}
		private static void paintListChar(mGraphics g)
        {
			bool flag5 = GraphicsHandler.listchar;
			if (flag5)
			{
				ListCharFunctions.Paint(g);
			}
		}
		private static void paintListBosses(mGraphics a)
		{
			if (MainFunctions.isSanBoss)
			{
				int num = 37;
				for (int i = 0; i < MainFunctions.listBosses.Count; i++)
				{
					
					MainFunctions.listBosses[i].paint(a, GameCanvas.w - 2, num, mFont.RIGHT);
					num += 10;
				}
			}
		}
	}
}
