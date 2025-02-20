using System;
using System.Collections.Generic;
using UnityEngine;

namespace Functions
{
	// Token: 0x020000C7 RID: 199
	public class SkillFunctions : IActionListener, IChatable
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x000A91AC File Offset: 0x000A73AC
		public static SkillFunctions getInstance()
		{
			bool flag = SkillFunctions._Instance == null;
			if (flag)
			{
				SkillFunctions._Instance = new SkillFunctions();
			}
			return SkillFunctions._Instance;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000A91DC File Offset: 0x000A73DC
		public static void TeleportTo(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y + 1;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}
		public static void FocusTo(int charId)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				bool flag = !@char.isMiniPet && !@char.isPet && @char.charID == charId;
				if (flag)
				{
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().npcFocus = null;
					global::Char.myCharz().itemFocus = null;
					global::Char.myCharz().charFocus = @char;
					break;
				}
			}
		}
		public static void Update()
		{
			UseSkillAuto();
			bool flag = SkillFunctions.isAutoSendAttack;
			if (flag)
			{
				SkillFunctions.AutoSendAttack();
			}
			bool flag2 = SkillFunctions.isTrainPet;
			if (flag2)
			{
				SkillFunctions.AutoSkillForPet();
			}
			bool flag3 = !global::Char.myCharz().meDead;
			if (flag3)
			{
				for (int i = 0; i < GameScr.keySkill.Length; i++)
				{
					bool flag4 = SkillFunctions.isAutoUseSkills[i];
					if (flag4)
					{
						SkillFunctions.AutoUseSkill(i);
					}
				}
			}
			bool flag5 = SkillFunctions.isLoadKeySkill && GameCanvas.gameTick % 20 == 0;
			if (flag5)
			{
				SkillFunctions.isLoadKeySkill = false;
				SkillFunctions.LoadKeySkills();
			}
			bool flag6 = SkillFunctions.isAutoChangeFocus;
			if (flag6)
			{
				SkillFunctions.AutoChangeFocus();
			}
		}
		public static void GotoXY(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}
		public static long getTimeSkill(Skill s)
		{
			long result;
			if (s.template.id == 29 || s.template.id == 22 || s.template.id == 7 || s.template.id == 18 || s.template.id == 23)
			{
				result = (long)s.coolDown + 500L;
			}
			else
			{
				long num = (long)((double)s.coolDown * 1.2);
				if (num < 406L)
				{
					result = 406L;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}
		public static void Ak()
		{
			if (!global::Char.myCharz().stone && !global::Char.isLoadingMap && !global::Char.myCharz().meDead && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && !global::Char.myCharz().myskill.paintCanNotUseSkill)
			{
				int skill = SkillFunctions.getSkill();
				if (mSystem.currentTimeMillis() - SkillFunctions.currTimeAK[skill] > SkillFunctions.getTimeSkill(global::Char.myCharz().myskill))
				{
					if (GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus) && (double)Res.abs(global::Char.myCharz().mobFocus.xFirst - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						SkillFunctions.AkMob();
						SkillFunctions.currTimeAK[skill] = mSystem.currentTimeMillis();
						return;
					}
					if (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) && (double)Res.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						SkillFunctions.AkChar();
						SkillFunctions.currTimeAK[skill] = mSystem.currentTimeMillis();
					}
				}
			}
		}
		private static int getSkill()
		{
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] == global::Char.myCharz().myskill)
				{
					return i;
				}
			}
			return 0;
		}
		public static void AkMob()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().mobFocus);
				Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
				global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
			}
			catch
			{
			}
		}
		public static void AkChar()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().charFocus);
				Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
				global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
			}
			catch
			{
			}
		}
		// Token: 0x06000A6F RID: 2671 RVA: 0x000A928C File Offset: 0x000A748C
		public void onChatFromMe(string text, string to)
		{
			bool flag = ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null;
			if (flag)
			{
				bool flag2 = ChatTextField.gI().strChat.Equals(SkillFunctions.inputDelay[0]);
				if (flag2)
				{
					try
					{
						long num = long.Parse(ChatTextField.gI().tfChat.getText());
						SkillFunctions.timeAutoSkills[SkillFunctions.indexSkillAuto] = num;
						SkillFunctions.isAutoUseSkills[SkillFunctions.indexSkillAuto] = true;
						GameScr.info1.addInfo(string.Concat(new string[]
						{
							"Auto ",
							GameScr.keySkill[SkillFunctions.indexSkillAuto].template.name,
							": ",
							NinjaUtil.getMoneys(num),
							" mili giây"
						}), 0);
					}
					catch
					{
						GameScr.info1.addInfo("Vui Lòng Nhập Lại Delay!", 0);
					}
					SkillFunctions.ResetChatTextField();
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00003FF5 File Offset: 0x000021F5
		public void onCancelChat()
		{
		}
	
		// Token: 0x06000A71 RID: 2673 RVA: 0x000A93C0 File Offset: 0x000A75C0
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
				case 1:
					{
						SkillFunctions.isAutoSendAttack = !SkillFunctions.isAutoSendAttack;
						GameScr.info1.addInfo("Tự Đánh\n" + (SkillFunctions.isAutoSendAttack ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
						bool flag = SkillFunctions.isAutoSendAttack;
						if (flag)
						{
							SkillFunctions.isAutoChangeFocus = false;
						}
						break;
					}
				case 2:
					SkillFunctions.isTrainPet = !SkillFunctions.isTrainPet;
					GameScr.info1.addInfo("Đánh Khi Đệ Cần\n" + (SkillFunctions.isTrainPet ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
					break;
				case 3:
					SkillFunctions.ShowOKyNang();

					break;
				case 4:
					SkillFunctions.isAutoShield = !SkillFunctions.isAutoShield;
					GameScr.info1.addInfo("Auto Khiên Pro\n" + (SkillFunctions.isAutoShield ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
					break;
				case 5:
					GameScr.info1.addInfo("Không Hỗ Trợ Lưu Cài Đặt Ở Mục Này!", 0);
					break;
				case 6:
					{
						SkillFunctions.isAutoChangeFocus = !SkillFunctions.isAutoChangeFocus;
						GameScr.info1.addInfo("Đánh Chuyển Mục Tiêu\n" + (SkillFunctions.isAutoChangeFocus ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
						bool flag2 = SkillFunctions.isAutoChangeFocus;
						if (flag2)
						{
							SkillFunctions.isAutoSendAttack = false;
						}
						break;
					}
				case 7:
					SkillFunctions.listTargetAutoChangeFocus.Clear();
					GameScr.info1.addInfo("Đã Xóa Danh Sách Đánh Chuyển Mục Tiêu!", 0);
					break;
				case 8:
					{
						bool flag3 = global::Char.myCharz().charFocus != null;
						if (flag3)
						{
							SkillFunctions.listTargetAutoChangeFocus.Remove(global::Char.myCharz().charFocus);
							GameScr.info1.addInfo(string.Concat(new string[]
							{
						"Đã Xóa ",
						global::Char.myCharz().charFocus.cName,
						" [",
						global::Char.myCharz().charFocus.charID.ToString(),
						"]"
							}), 0);
						}
						break;
					}
				case 9:
					{
						bool flag4 = global::Char.myCharz().charFocus != null;
						if (flag4)
						{
							SkillFunctions.listTargetAutoChangeFocus.Add(global::Char.myCharz().charFocus);
							GameScr.info1.addInfo(string.Concat(new string[]
							{
						"Đã Thêm ",
						global::Char.myCharz().charFocus.cName,
						" [",
						global::Char.myCharz().charFocus.charID.ToString(),
						"]"
							}), 0);
						}
						break;
					}
				case 10:
					SkillFunctions.ShowMenuAutoSkill((int)p);
					break;
				case 11:
					{
						int num = (int)p;
						SkillFunctions.isAutoUseSkills[num] = !SkillFunctions.isAutoUseSkills[num];
						bool flag5 = SkillFunctions.isAutoUseSkills[num];
						if (flag5)
						{
							SkillFunctions.timeAutoSkills[num] = -1L;
						}
						GameScr.info1.addInfo("Auto " + GameScr.keySkill[num].template.name + (SkillFunctions.isAutoUseSkills[num] ? (": " + NinjaUtil.getMoneys(SkillFunctions.timeAutoSkills[num]) + " mili giây") : "\n[STATUS: OFF]"), 0);
						break;
					}
				case 12:
					ChatTextField.gI().strChat = SkillFunctions.inputDelay[0];
					ChatTextField.gI().tfChat.name = SkillFunctions.inputDelay[1];
					ChatTextField.gI().startChat2(SkillFunctions.getInstance(), string.Empty);
					SkillFunctions.indexSkillAuto = (int)p;
					break;
				case 13:
					{
						int num2 = (int)p;
						GameScr.keySkill[num2].coolDown = 0;
						GameScr.keySkill[num2].manaUse = 0;
						GameScr.info1.addInfo("Đóng Băng " + GameScr.keySkill[num2].template.name, 0);
						break;
					}
				case 14:
					{
						MyVector myVector = new MyVector();
						for (int i = 0; i < GameScr.keySkill.Length; i++)
						{
							myVector.addElement(new Command(((GameScr.keySkill[i] != null) ? GameScr.keySkill[i].template.name : "Không Có") + "\n[" + (i + 1).ToString() + "]\n", SkillFunctions.getInstance(), 10, i));
						}
						break;
					}
				case 15:
					{
						MyVector myVector7 = new MyVector();
						for (int i = 0; i < GameScr.keySkill.Length; i++)
						{
							myVector7.addElement(new Command((GameScr.keySkill[i] == null) ? "Chưa Gán Skill" : (GameScr.keySkill[i].template.name + " [" + (SkillFunctions.listSkillsAuto.Contains(GameScr.keySkill[i]) ? "Xóa" : "Thêm") + "]"), 16));
						}
						GameCanvas.menu.startAt(myVector7, 4);
						break;
					}
				case 16:
					SkillFunctions.AddRemoveSkill(GameCanvas.menu.menuSelectedItem);
					break;
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000A990C File Offset: 0x000A7B0C
		public static void ShowMenu()
		{
			SkillFunctions.LoadData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Tự Đánh\n" + (SkillFunctions.isAutoSendAttack ? "[STATUS: ON]" : "[STATUS: OFF]"), SkillFunctions.getInstance(), 1, null));
			myVector.addElement(new Command("Đánh Khi Đệ Cần\n" + (SkillFunctions.isTrainPet ? "[STATUS: ON]" : "[STATUS: OFF]"), SkillFunctions.getInstance(), 2, null));
			myVector.addElement(new Command("Lưu Cài Đặt\n" + (SkillFunctions.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), SkillFunctions.getInstance(), 5, null));
			myVector.addElement(new Command("Đánh Chuyển Mục Tiêu\n" + (SkillFunctions.isAutoChangeFocus ? "[STATUS: ON]" : "[STATUS: OFF]"), SkillFunctions.getInstance(), 6, null));
			bool flag = SkillFunctions.listTargetAutoChangeFocus.Count > 0;
			if (flag)
			{
				myVector.addElement(new Command("Clear Danh Sách Chuyển Mục Tiêu", SkillFunctions.getInstance(), 7, null));
			}
			bool flag2 = global::Char.myCharz().charFocus != null;
			if (flag2)
			{
				bool flag3 = SkillFunctions.listTargetAutoChangeFocus.Contains(global::Char.myCharz().charFocus);
				if (flag3)
				{
					myVector.addElement(new Command("Xóa Khỏi Danh Sách Chuyển Mục Tiêu", SkillFunctions.getInstance(), 8, null));
				}
				else
				{
					myVector.addElement(new Command("Thêm Vào Danh Sách Chuyển Mục Tiêu", SkillFunctions.getInstance(), 9, null));
				}
			}
			//myVector.addElement(new Command("Auto Dùng Skill", SkillFunctions.getInstance(), 15, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000A9A80 File Offset: 0x000A7C80
		private static void ShowMenuAutoSkill(int skillIndex)
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Sử Dụng\n" + (SkillFunctions.isAutoUseSkills[skillIndex] ? ("[" + NinjaUtil.getMoneys(SkillFunctions.timeAutoSkills[skillIndex]) + " mili giây]") : "[STATUS: OFF]"), SkillFunctions.getInstance(), 11, skillIndex));
			myVector.addElement(new Command("Nhập Delay\n[mili giây]", SkillFunctions.getInstance(), 12, skillIndex));
			myVector.addElement(new Command("Đóng Băng\n" + GameScr.keySkill[skillIndex].template.name, SkillFunctions.getInstance(), 13, skillIndex));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00006812 File Offset: 0x00004A12
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00003FF5 File Offset: 0x000021F5
		private static void LoadData()
		{
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00003FF5 File Offset: 0x000021F5
		private static void smethod_6()
		{
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000A9B44 File Offset: 0x000A7D44
		private static void LoadKeySkills()
		{
			for (int i = 0; i < global::Char.myCharz().nClass.skillTemplates.Length; i++)
			{
				SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
				Skill skill = global::Char.myCharz().getSkill(skillTemplate);
				bool flag = skill != null;
				if (flag)
				{
					GameScr.keySkill[i] = skill;
				}
				GameScr.gI().saveKeySkillToRMS();
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000A9BB4 File Offset: 0x000A7DB4
		public static void AutoSendAttack()
		{
			bool flag = !global::Char.myCharz().meDead && global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && (!global::Char.myCharz().myskill.paintCanNotUseSkill || GameCanvas.panel.isShow);
			if (flag)
			{
				int mySkillIndex = SkillFunctions.GetMySkillIndex();
				bool flag2 = mSystem.currentTimeMillis() - SkillFunctions.lastTimeSendAttack[mySkillIndex] > SkillFunctions.GetCoolDown(global::Char.myCharz().myskill);
				if (flag2)
				{
					bool flag3 = GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus);
					if (flag3)
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						SkillFunctions.SendAttackToMobFocus();
						SkillFunctions.lastTimeSendAttack[mySkillIndex] = mSystem.currentTimeMillis();
					}
					else
					{
						bool flag4 = global::Char.myCharz().charFocus != null && SkillFunctions.isMeCanAttackChar(global::Char.myCharz().charFocus) && (double)global::Math.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.7;
						if (flag4)
						{
							global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
							SkillFunctions.SendAttackToCharFocus();
							SkillFunctions.lastTimeSendAttack[mySkillIndex] = mSystem.currentTimeMillis();
						}
					}
				}
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000A9D68 File Offset: 0x000A7F68
		private static void AutoSkillForPet()
		{
			bool flag = SkillFunctions.isPetAskedForUseSkill && GameScr.vMob.size() != 0 && global::Char.myCharz().myskill.template.type != 3;
			if (flag)
			{
				Mob mobFocus = (Mob)GameScr.vMob.elementAt(0);
				int num = 0;
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(i);
					int num2 = global::Math.abs(global::Char.myCharz().cx - mob.x);
					int num3 = global::Math.abs(global::Char.myCharz().cy - mob.y);
					int num4 = num2 * num2 + num3 * num3;
					bool flag2 = num < num4;
					if (flag2)
					{
						num = num4;
						mobFocus = mob;
					}
				}
				global::Char.myCharz().mobFocus = mobFocus;
				int mySkillIndex = SkillFunctions.GetMySkillIndex();
				bool flag3 = mSystem.currentTimeMillis() - SkillFunctions.lastTimeSendAttack[mySkillIndex] > (long)(global::Char.myCharz().myskill.coolDown + 100) && GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus);
				if (flag3)
				{
					SkillFunctions.SendAttackToMobFocus();
					SkillFunctions.lastTimeSendAttack[mySkillIndex] = mSystem.currentTimeMillis();
					SkillFunctions.isPetAskedForUseSkill = false;
				}
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000A9EB8 File Offset: 0x000A80B8
		private static void AutoUseSkill(int skillIndex)
		{
			bool flag = TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23;
			if (flag)
			{
				bool flag2 = skillIndex >= GameScr.keySkill.Length;
				if (flag2)
				{
					skillIndex = GameScr.keySkill.Length - 1;
				}
				bool flag3 = skillIndex < 0;
				if (flag3)
				{
					skillIndex = 0;
				}
				bool flag4 = GameScr.keySkill[skillIndex] != null && !GameScr.keySkill[skillIndex].paintCanNotUseSkill;
				if (flag4)
				{
					bool flag5 = GameScr.keySkill[skillIndex].coolDown == 0;
					if (flag5)
					{
						SkillFunctions.timeAutoSkills[skillIndex] = 500L;
					}
					else
					{
						bool flag6 = SkillFunctions.isMeHasEnoughMP(GameScr.keySkill[skillIndex]) && !GameScr.gI().isCharging() && mSystem.currentTimeMillis() - SkillFunctions.lastTimeAutoUseSkill > 150L;
						if (flag6)
						{
							bool flag7 = SkillFunctions.timeAutoSkills[skillIndex] == -1L && GameCanvas.gameTick % 20 == 0;
							if (flag7)
							{
								SkillFunctions.lastTimeUseSkill[skillIndex] = mSystem.currentTimeMillis();
								SkillFunctions.lastTimeAutoUseSkill = mSystem.currentTimeMillis();
								GameScr.gI().doSelectSkill(GameScr.keySkill[skillIndex], true);
							}
							bool flag8 = mSystem.currentTimeMillis() - SkillFunctions.lastTimeUseSkill[skillIndex] > SkillFunctions.timeAutoSkills[skillIndex];
							if (flag8)
							{
								SkillFunctions.lastTimeUseSkill[skillIndex] = mSystem.currentTimeMillis();
								SkillFunctions.lastTimeAutoUseSkill = mSystem.currentTimeMillis();
								GameScr.gI().doSelectSkill(GameScr.keySkill[skillIndex], true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000AA034 File Offset: 0x000A8234
		public static bool isMeCanAttackChar(global::Char ch)
		{
			bool flag = TileMap.mapID == 113;
			bool result;
			if (flag)
			{
				result = (ch != null && global::Char.myCharz().myskill != null && (ch.cTypePk == 5 || ch.cTypePk == 3));
			}
			else
			{
				result = (ch != null && global::Char.myCharz().myskill != null && ((ch.statusMe != 14 && ch.statusMe != 5 && global::Char.myCharz().myskill.template.type != 2 && ((global::Char.myCharz().cFlag == 8 && ch.cFlag != 0) || (global::Char.myCharz().cFlag != 0 && ch.cFlag == 8) || (global::Char.myCharz().cFlag != ch.cFlag && global::Char.myCharz().cFlag != 0 && ch.cFlag != 0) || (ch.cTypePk == 3 && global::Char.myCharz().cTypePk == 3) || global::Char.myCharz().cTypePk == 5 || ch.cTypePk == 5 || (global::Char.myCharz().cTypePk == 1 && ch.cTypePk == 1) || (global::Char.myCharz().cTypePk == 4 && ch.cTypePk == 4))) || (global::Char.myCharz().myskill.template.type == 2 && ch.cTypePk != 5)));
			}
			return result;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000AA1AC File Offset: 0x000A83AC
		private static bool isMeHasEnoughMP(Skill skillToUse)
		{
			bool flag = skillToUse.template.manaUseType == 2;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = skillToUse.template.manaUseType != 1;
				if (flag2)
				{
					result = (global::Char.myCharz().cMP >= skillToUse.manaUse);
				}
				else
				{
					result = (global::Char.myCharz().cMP >= skillToUse.manaUse * global::Char.myCharz().cMPFull / 100);
				}
			}
			return result;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000AA22C File Offset: 0x000A842C
		private static void SendAttackToCharFocus()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().charFocus);
				Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
			}
			catch
			{
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000AA27C File Offset: 0x000A847C
		private static void SendAttackToMobFocus()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().mobFocus);
				Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
			}
			catch
			{
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000AA2CC File Offset: 0x000A84CC
		private static long GetCoolDown(Skill skill)
		{
			bool flag = skill.template.id != 20 && skill.template.id != 22 && skill.template.id != 7 && skill.template.id != 18 && skill.template.id != 23;
			long result;
			if (flag)
			{
				result = (long)(skill.coolDown + 100);
			}
			else
			{
				result = (long)skill.coolDown + 500L;
			}
			return result;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000AA354 File Offset: 0x000A8554
		private static int GetMySkillIndex()
		{
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				bool flag = GameScr.keySkill[i] == global::Char.myCharz().myskill;
				if (flag)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000AA39C File Offset: 0x000A859C
		private static void AutoChangeFocus()
		{
			bool flag = SkillFunctions.listTargetAutoChangeFocus.Count == 0;
			if (flag)
			{
				GameScr.info1.addInfo("Danh sách chuyển mục tiêu trống!", 0);
				SkillFunctions.isAutoChangeFocus = false;
			}
			else
			{
				bool flag2 = !global::Char.myCharz().meDead && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && !global::Char.myCharz().myskill.paintCanNotUseSkill;
				if (flag2)
				{
					SkillFunctions.cooldownAutoChangeFocus = SkillFunctions.GetCooldownAutoChangeFocus(global::Char.myCharz().myskill);
					bool flag3 = SkillFunctions.targetIndex >= SkillFunctions.listTargetAutoChangeFocus.Count;
					if (flag3)
					{
						SkillFunctions.targetIndex = 0;
					}
					bool flag4 = mSystem.currentTimeMillis() - SkillFunctions.lastTimeChangeFocus >= SkillFunctions.cooldownAutoChangeFocus;
					if (flag4)
					{
						SkillFunctions.lastTimeChangeFocus = mSystem.currentTimeMillis();
						global::Char.myCharz().charFocus = GameScr.findCharInMap(SkillFunctions.listTargetAutoChangeFocus[SkillFunctions.targetIndex].charID);
						SkillFunctions.targetIndex++;
						bool flag5 = SkillFunctions.targetIndex >= SkillFunctions.listTargetAutoChangeFocus.Count;
						if (flag5)
						{
							SkillFunctions.targetIndex = 0;
						}
						bool flag6 = global::Char.myCharz().charFocus != null && SkillFunctions.isMeCanAttackChar(global::Char.myCharz().charFocus) && (double)global::Math.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5;
						if (flag6)
						{
							global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
							SkillFunctions.SendAttackToCharFocus();
						}
					}
				}
			}
		}
		public static void AddRemoveSkill(int indexSkill)
		{
			if (SkillFunctions.listSkillsAuto.Contains(GameScr.keySkill[indexSkill]))
			{
				SkillFunctions.listSkillsAuto.Remove(GameScr.keySkill[indexSkill]);
				GameScr.info1.addInfo("Đã xóa " + GameScr.keySkill[indexSkill].template.name + " khỏi list skill auto", 0);
				MenuFunctions.ShowMenu();
				return;
			}
			SkillFunctions.listSkillsAuto.Add(GameScr.keySkill[indexSkill]);
			GameScr.info1.addInfo("Đã thêm " + GameScr.keySkill[indexSkill].template.name + " vào list skill auto", 0);
			MenuFunctions.ShowMenu();
		}
		private static void UseSkillAuto()
		{
			if (Input.GetKey("q") && SkillFunctions.listSkillsAuto.Count > 0)
			{
				SkillFunctions.listSkillsAuto.Clear();
				GameScr.info1.addInfo("Đã reset skill auto", 0);
				return;
			}
			foreach (Skill skill in SkillFunctions.listSkillsAuto)
			{
				if (global::Char.myCharz().isStandAndCharge || global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().stone)
				{
					break;
				}
				if (global::Char.myCharz().holdEffID != 0)
				{
					break;
				}
				if (global::Char.myCharz().blindEff)
				{
					break;
				}
				if (global::Char.myCharz().sleepEff)
				{
					break;
				}
				if (global::Char.myCharz().cHP <= 0)
				{
					break;
				}
				if (global::Char.myCharz().statusMe == 14)
				{
					break;
				}
				if (TileMap.mapID == global::Char.myCharz().cgender + 21)
				{
					break;
				}
				if ((SkillFunctions.GetCoolDownSkill(skill) <= 0 && skill.template.type == 3) || skill.lastTimeUseThisSkill == 0L)
				{
					SkillFunctions.UseSkill(skill);
				}
			}
		}
		public static int GetCoolDownSkill(Skill skill)
		{
			return (int)((long)skill.coolDown - mSystem.currentTimeMillis() + skill.lastTimeUseThisSkill);
		}
		public static void UseSkill(Skill sk)
		{
			if (global::Char.myCharz().myskill != sk)
			{
				GameScr.gI().doSelectSkill(sk, true);
				GameScr.gI().doSelectSkill(sk, true);
				return;
			}
			GameScr.gI().doSelectSkill(sk, true);
		}
		// Token: 0x06000A82 RID: 2690 RVA: 0x000AA590 File Offset: 0x000A8790
		private static long GetCooldownAutoChangeFocus(Skill skill)
		{
			bool flag = skill.coolDown <= 500;
			long result;
			if (flag)
			{
				result = 1000L;
			}
			else
			{
				result = (long)((double)skill.coolDown * 1.2 + 200.0);
			}
			return result;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00003FF5 File Offset: 0x000021F5
		private static void smethod_0()
		{
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000AA5E4 File Offset: 0x000A87E4
		static SkillFunctions()
		{
			SkillFunctions.LoadData();
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000AA654 File Offset: 0x000A8854
		public static void FreezeSelectedSkill()
		{
			int mySkillIndex = SkillFunctions.GetMySkillIndex();
			GameScr.keySkill[mySkillIndex].coolDown = 0;
			GameScr.keySkill[mySkillIndex].manaUse = 0;
			GameScr.info1.addInfo("Đóng Băng\n" + GameScr.keySkill[mySkillIndex].template.name, 0);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000AA6AC File Offset: 0x000A88AC
		public static void ShowOKyNang()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(GameScr.keySkill.Length.ToString() + " Ô Kỹ Năng", SkillFunctions.getInstance(), 14, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x0400134F RID: 4943
		private static SkillFunctions _Instance;

		// Token: 0x04001350 RID: 4944
		public static bool isLoadKeySkill = true;

		// Token: 0x04001351 RID: 4945
		public static bool isAutoSendAttack;

		// Token: 0x04001352 RID: 4946
		private static long[] lastTimeSendAttack = new long[10];

		// Token: 0x04001353 RID: 4947
		public static bool isTrainPet;

		// Token: 0x04001354 RID: 4948
		public static bool isPetAskedForUseSkill;

		// Token: 0x04001355 RID: 4949
		public static bool[] isAutoUseSkills = new bool[10];

		// Token: 0x04001356 RID: 4950
		private static long[] lastTimeUseSkill = new long[10];

		// Token: 0x04001357 RID: 4951
		private static long[] timeAutoSkills = new long[10];

		// Token: 0x04001358 RID: 4952
		private static int indexSkillAuto;

		// Token: 0x04001359 RID: 4953
		private static bool isAutoChangeFocus;

		// Token: 0x0400135A RID: 4954
		private static long cooldownAutoChangeFocus;

		// Token: 0x0400135B RID: 4955
		private static long lastTimeChangeFocus;

		// Token: 0x0400135C RID: 4956
		private static List<global::Char> listTargetAutoChangeFocus = new List<global::Char>();

		// Token: 0x0400135D RID: 4957
		private static int targetIndex;

		// Token: 0x0400135E RID: 4958
		private static bool isAutoShield;

		// Token: 0x0400135F RID: 4959
		private static string[] inputDelay = new string[]
		{
			"Nhập delay",
			"mili giây"
		};

		// Token: 0x04001360 RID: 4960
		private static bool isSaveData;
		public static long[] currTimeAK = new long[8];
		// Token: 0x04001361 RID: 4961
		private static long lastTimeAutoUseSkill;
		public static List<Skill> listSkillsAuto = new List<Skill>();
		public static bool dichChuyenPem = true;
	}
}
