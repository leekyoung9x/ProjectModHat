using Functions.HandlerFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Functions.AutoFunctions	
{
	// Token: 0x020000C7 RID: 199
	public class AutoPickMobFunctions
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x000A2324 File Offset: 0x000A0524
		public static void Update()
		{
			bool flag = AutoPickMobFunctions.IsWaiting();
			if (!flag)
			{
				global::Char @char = global::Char.myCharz();
				bool flag2 = @char.statusMe == 14 || @char.cHP <= 0;
				if (!flag2)
				{
					bool flag3 = GameScr.hpPotion >= 1 && (@char.cHP <= @char.cHPFull * AutoPickMobHandler.HpBuff / 100 || @char.cMP <= @char.cMPFull * AutoPickMobHandler.MpBuff / 100);
					if (flag3)
					{
						GameScr.gI().doUseHP();
					}
					bool flag4 = ItemTime.isExistItem(4387);
					bool flag5 = AutoPickMobHandler.IsTanSat && flag4;
					bool flag6 = AutoPickMobHandler.IsAutoPickItems && !flag5;
					if (flag6)
					{
						bool isPickingItems = AutoPickMobFunctions.IsPickingItems;
						if (isPickingItems)
						{
							bool flag7 = AutoPickMobFunctions.IndexItemPick >= AutoPickMobFunctions.ItemPicks.Count;
							if (flag7)
							{
								AutoPickMobFunctions.IsPickingItems = false;
								return;
							}
							ItemMap itemMap = AutoPickMobFunctions.ItemPicks[AutoPickMobFunctions.IndexItemPick];
							switch (AutoPickMobFunctions.GetTpyePickItem(itemMap))
							{
							case AutoPickMobFunctions.TpyePickItem.CanNotPickItem:
								AutoPickMobFunctions.IndexItemPick++;
								return;
							case AutoPickMobFunctions.TpyePickItem.PickItemNormal:
							{
								Service.gI().pickItem(itemMap.itemMapID);
								bool isTanSat = AutoPickMobHandler.IsTanSat;
								if (isTanSat)
								{
									itemMap.countAutoPick++;
									AutoPickMobFunctions.IndexItemPick++;
								}
								AutoPickMobFunctions.Wait((AutoPickMobHandler.TimesAutoPickItemMax <= 1) ? 1 : 500);
								return;
							}
							case AutoPickMobFunctions.TpyePickItem.PickItemTDLT:
							{
								Service.gI().pickItem(itemMap.itemMapID);
								bool isTanSat2 = AutoPickMobHandler.IsTanSat;
								if (isTanSat2)
								{
									itemMap.countAutoPick++;
									AutoPickMobFunctions.IndexItemPick++;
								}
								AutoPickMobFunctions.Wait((AutoPickMobHandler.TimesAutoPickItemMax <= 1) ? 1 : 500);
								return;
							}
							case AutoPickMobFunctions.TpyePickItem.PickItemTanSat:
								Service.gI().pickItem(itemMap.itemMapID);
								AutoPickMobFunctions.Wait((AutoPickMobHandler.TimesAutoPickItemMax <= 1) ? 1 : 500);
								return;
							}
						}
						AutoPickMobFunctions.ItemPicks.Clear();
						AutoPickMobFunctions.IndexItemPick = 0;
						for (int i = 0; i < GameScr.vItemMap.size(); i++)
						{
							ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(i);
							bool flag8 = AutoPickMobFunctions.GetTpyePickItem(itemMap2) > AutoPickMobFunctions.TpyePickItem.CanNotPickItem;
							if (flag8)
							{
								AutoPickMobFunctions.ItemPicks.Add(itemMap2);
							}
						}
						bool flag9 = AutoPickMobFunctions.ItemPicks.Count > 0;
						if (flag9)
						{
							AutoPickMobFunctions.IsPickingItems = true;
							return;
						}
					}
					bool isTanSat3 = AutoPickMobHandler.IsTanSat;
					if (isTanSat3)
					{
						bool flag10 = global::Char.myCharz().cHP <= global::Char.myCharz().cHPFull * 10 / 100 || global::Char.myCharz().cMP <= global::Char.myCharz().cMPFull * 10 / 100;
						if (flag10)
						{
							GameScr.gI().doUseHP();
						}
						bool isCharge = @char.isCharge;
						if (isCharge)
						{
							AutoPickMobFunctions.Wait(AutoPickMobHandler.IsTanSat ? 1 : 500);
						}
						else
						{
							@char.clearFocus(0);
							bool flag11 = @char.mobFocus != null && !AutoPickMobFunctions.IsMobTanSat(@char.mobFocus);
							if (flag11)
							{
								@char.mobFocus = null;
							}
							bool flag12 = @char.mobFocus == null;
							if (flag12)
							{
								@char.mobFocus = AutoPickMobFunctions.GetMobTanSat();
								bool flag13 = flag4 && @char.mobFocus != null;
								if (flag13)
								{
									bool dichChuyenPem  = SkillFunctions.dichChuyenPem;
									if (dichChuyenPem)
									{
										bool flag14 = global::Math.abs(@char.mobFocus.xFirst - @char.cx) >= 20 || global::Math.abs(@char.mobFocus.yFirst - @char.cy) >= 20;
										if (flag14)
										{
											SkillFunctions.GotoXY(@char.mobFocus.xFirst, @char.mobFocus.yFirst);
										}
										return;
									}
									@char.cx = @char.mobFocus.xFirst;
									@char.cy = @char.mobFocus.yFirst;
									Service.gI().charMove();
								}
							}
							bool flag15 = @char.mobFocus != null;
							if (flag15)
							{
								bool flag16 = @char.skillInfoPaint() == null;
								if (flag16)
								{
									Skill skillAttack = AutoPickMobFunctions.GetSkillAttack();
									bool flag17 = skillAttack != null && !skillAttack.paintCanNotUseSkill;
									if (flag17)
									{
										Mob mobFocus = @char.mobFocus;
										mobFocus.x = mobFocus.xFirst;
										mobFocus.y = mobFocus.yFirst;
										bool flag18 = global::Char.myCharz().myskill != skillAttack;
										if (flag18)
										{
											GameScr.gI().doSelectSkill(skillAttack, true);
										}
										bool flag19 = Res.distance(mobFocus.xFirst, mobFocus.yFirst, @char.cx, @char.cy) <= 48;
										if (flag19)
										{
											bool flag20 = GameCanvas.gameTick % 50 == 0 && Mob.arrMobTemplate[global::Char.myCharz().mobFocus.templateId].type == 4;
											if (flag20)
											{
												SkillFunctions.GotoXY(mobFocus.xFirst, mobFocus.yFirst + 1);
											}
											bool flag21 = global::Char.myCharz().myskill.template.iconId == 539 || global::Char.myCharz().myskill.template.name.ToLower().Contains("liên hoàn") || global::Char.myCharz().myskill.template.iconId == 540;
											if (flag21)
											{
												SkillFunctions.Ak();
												return;
											}
											GameScr.gI().doDoubleClickToObj(mobFocus);
										}
										else
										{
											bool dichChuyenPem2 = SkillFunctions.dichChuyenPem;
											if (dichChuyenPem2)
											{
												bool flag22 = global::Math.abs(mobFocus.xFirst - @char.cx) >= 20 || global::Math.abs(mobFocus.yFirst - @char.cy) >= 20;
												if (flag22)
												{
													SkillFunctions.GotoXY(mobFocus.xFirst, mobFocus.yFirst);
												}
												return;
											}
											AutoPickMobFunctions.Move(mobFocus.xFirst, mobFocus.yFirst);
										}
									}
								}
							}
							else
							{
								bool flag23 = !flag4;
								if (flag23)
								{
									Mob mobNext = AutoPickMobFunctions.GetMobNext();
									bool flag24 = mobNext != null;
									if (flag24)
									{
										global::Char.myCharz().focusManualTo(mobNext);
										bool dichChuyenPem3 = SkillFunctions.dichChuyenPem;
										if (dichChuyenPem3)
										{
											bool flag25 = global::Math.abs(mobNext.xFirst - @char.cx) >= 20 || global::Math.abs(mobNext.yFirst - @char.cy) >= 20;
											if (flag25)
											{
												SkillFunctions.GotoXY(mobNext.xFirst, mobNext.yFirst);
											}
											return;
										}
										AutoPickMobFunctions.Move(mobNext.xFirst, mobNext.yFirst);
									}
								}
							}
							AutoPickMobFunctions.Wait(AutoPickMobHandler.IsTanSat ? 1 : 500);
						}
					}
				}
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000A2A14 File Offset: 0x000A0C14
		public static void Move(int x, int y)
		{
			global::Char @char = global::Char.myCharz();
			bool flag = !AutoPickMobHandler.IsVuotDiaHinh;
			if (flag)
			{
				@char.currentMovePoint = new MovePoint(x, y);
			}
			else
			{
				int[] pointYsdMax = AutoPickMobFunctions.GetPointYsdMax(@char.cx, x);
				bool flag2 = pointYsdMax[1] >= y || (pointYsdMax[1] >= @char.cy && (@char.statusMe == 2 || @char.statusMe == 1));
				if (flag2)
				{
					pointYsdMax[0] = x;
					pointYsdMax[1] = y;
				}
				@char.currentMovePoint = new MovePoint(pointYsdMax[0], pointYsdMax[1]);
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000A2A9C File Offset: 0x000A0C9C
		private static AutoPickMobFunctions.TpyePickItem GetTpyePickItem(ItemMap itemMap)
		{
			global::Char @char = global::Char.myCharz();
			bool flag = itemMap.playerId == @char.charID || itemMap.playerId == -1;
			bool flag2 = AutoPickMobHandler.IsItemMe && !flag;
			AutoPickMobFunctions.TpyePickItem result;
			if (flag2)
			{
				result = AutoPickMobFunctions.TpyePickItem.CanNotPickItem;
			}
			else
			{
				bool flag3 = AutoPickMobHandler.IsLimitTimesPickItem && itemMap.countAutoPick > AutoPickMobHandler.TimesAutoPickItemMax;
				if (flag3)
				{
					result = AutoPickMobFunctions.TpyePickItem.CanNotPickItem;
				}
				else
				{
					bool flag4 = !AutoPickMobFunctions.FilterItemPick(itemMap);
					if (flag4)
					{
						result = AutoPickMobFunctions.TpyePickItem.CanNotPickItem;
					}
					else
					{
						bool flag5 = Res.abs(@char.cx - itemMap.xEnd) < 60 && Res.abs(@char.cy - itemMap.yEnd) < 60;
						if (flag5)
						{
							result = AutoPickMobFunctions.TpyePickItem.PickItemNormal;
						}
						else
						{
							bool flag6 = ItemTime.isExistItem(4387);
							if (flag6)
							{
								result = AutoPickMobFunctions.TpyePickItem.PickItemTDLT;
							}
							else
							{
								bool isTanSat = AutoPickMobHandler.IsTanSat;
								if (isTanSat)
								{
									result = AutoPickMobFunctions.TpyePickItem.PickItemTanSat;
								}
								else
								{
									result = AutoPickMobFunctions.TpyePickItem.CanNotPickItem;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000A2B80 File Offset: 0x000A0D80
		private static bool FilterItemPick(ItemMap itemMap)
		{
			return (AutoPickMobHandler.IdItemPicks.Count == 0 || AutoPickMobHandler.IdItemPicks.Contains(itemMap.template.id)) && (AutoPickMobHandler.IdItemBlocks.Count == 0 || !AutoPickMobHandler.IdItemBlocks.Contains(itemMap.template.id)) && (AutoPickMobHandler.TypeItemPicks.Count == 0 || AutoPickMobHandler.TypeItemPicks.Contains(itemMap.template.type)) && (AutoPickMobHandler.TypeItemBlock.Count == 0 || !AutoPickMobHandler.TypeItemBlock.Contains(itemMap.template.type));
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000A2C28 File Offset: 0x000A0E28
		private static Mob GetMobTanSat()
		{
			Mob result = null;
			int num = int.MaxValue;
			global::Char @char = global::Char.myCharz();
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				int num2 = (mob.xFirst - @char.cx) * (mob.xFirst - @char.cx) + (mob.yFirst - @char.cy) * (mob.yFirst - @char.cy);
				bool flag = AutoPickMobFunctions.IsMobTanSat(mob) && num2 < num;
				if (flag)
				{
					result = mob;
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000A2CDC File Offset: 0x000A0EDC
		private static Mob GetMobNext()
		{
			Mob result = null;
			long num = mSystem.currentTimeMillis();
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				bool flag = AutoPickMobFunctions.IsMobNext(mob) && mob.timeLastDie < num;
				if (flag)
				{
					result = mob;
					num = mob.timeLastDie;
				}
			}
			return result;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000A2D4C File Offset: 0x000A0F4C
		private static bool IsMobTanSat(Mob mob)
		{
			bool flag = mob.status == 0 || mob.status == 1 || mob.hp <= 0 || mob.isMobMe;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = AutoPickMobHandler.IsNeSieuQuai && !ItemTime.isExistItem(4387);
				result = ((mob.levelBoss == 0 || !flag2) && AutoPickMobFunctions.FilterMobTanSat(mob));
			}
			return result;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
		private static bool IsMobNext(Mob mob)
		{
			bool isMobMe = mob.isMobMe;
			bool result;
			if (isMobMe)
			{
				result = false;
			}
			else
			{
				bool flag = !AutoPickMobFunctions.FilterMobTanSat(mob);
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = AutoPickMobHandler.IsNeSieuQuai && !ItemTime.isExistItem(4387) && mob.getTemplate().hp >= 3000;
					if (flag2)
					{
						bool flag3 = mob.levelBoss != 0;
						if (flag3)
						{
							Mob mob2 = null;
							bool flag4 = false;
							for (int i = 0; i < GameScr.vMob.size(); i++)
							{
								mob2 = (Mob)GameScr.vMob.elementAt(i);
								bool flag5 = mob2.countDie == 10 && (mob2.status == 0 || mob2.status == 1);
								if (flag5)
								{
									flag4 = true;
									break;
								}
							}
							bool flag6 = !flag4;
							if (flag6)
							{
								return false;
							}
							mob.timeLastDie = mob2.timeLastDie;
						}
						else
						{
							bool flag7 = mob.countDie == 10 && (mob.status == 0 || mob.status == 1);
							if (flag7)
							{
								return false;
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000A2EF0 File Offset: 0x000A10F0
		private static bool FilterMobTanSat(Mob mob)
		{
			return (AutoPickMobHandler.IdMobsTanSat.Count == 0 || AutoPickMobHandler.IdMobsTanSat.Contains(mob.mobId)) && (AutoPickMobHandler.TypeMobsTanSat.Count == 0 || AutoPickMobHandler.TypeMobsTanSat.Contains(mob.templateId));
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000A2F44 File Offset: 0x000A1144
		private static Skill GetSkillAttack()
		{
			Skill skill = null;
			SkillTemplate skillTemplate = new SkillTemplate();
			foreach (sbyte id in AutoPickMobHandler.IdSkillsTanSat)
			{
				skillTemplate.id = id;
				Skill skill2 = global::Char.myCharz().getSkill(skillTemplate);
				bool flag = AutoPickMobFunctions.IsSkillBetter(skill2, skill);
				if (flag)
				{
					skill = skill2;
				}
			}
			return skill;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000A2FCC File Offset: 0x000A11CC
		private static bool IsSkillBetter(Skill SkillBetter, Skill skill)
		{
			bool flag = SkillBetter == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !AutoPickMobFunctions.CanUseSkill(SkillBetter);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = (SkillBetter.template.id == 17 && skill.template.id == 2) || (SkillBetter.template.id == 9 && skill.template.id == 0);
					result = (skill == null || skill.coolDown < SkillBetter.coolDown || flag3);
				}
			}
			return result;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000A3054 File Offset: 0x000A1254
		private static bool CanUseSkill(Skill skill)
		{
			bool flag = mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown + 25L;
			if (flag)
			{
				skill.paintCanNotUseSkill = false;
			}
			return (!skill.paintCanNotUseSkill || AutoPickMobFunctions.IdSkillsMelee.Contains(skill.template.id)) && !AutoPickMobFunctions.IdSkillsCanNotAttack.Contains(skill.template.id) && global::Char.myCharz().cMP >= AutoPickMobFunctions.GetManaUseSkill(skill);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000A30DC File Offset: 0x000A12DC
		private static long GetManaUseSkill(Skill skill)
		{
			bool flag = skill.template.manaUseType == 2;
			long result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = skill.template.manaUseType == 1;
				if (flag2)
				{
					result = skill.manaUse * global::Char.myCharz().cMPFull / 100;
				}
				else
				{
					result = skill.manaUse;
				}
			}
			return result;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000A3138 File Offset: 0x000A1338
		public static int GetYsd(int xsd)
		{
			global::Char @char = global::Char.myCharz();
			int num = TileMap.pxh;
			int result = -1;
			for (int i = 24; i < TileMap.pxh; i += 24)
			{
				bool flag = TileMap.tileTypeAt(xsd, i, 2);
				if (flag)
				{
					int num2 = Res.abs(i - @char.cy);
					bool flag2 = num2 < num;
					if (flag2)
					{
						num = num2;
						result = i;
					}
				}
			}
			return result;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000A31A8 File Offset: 0x000A13A8
		private static int[] GetPointYsdMax(int xStart, int xEnd)
		{
			int num = TileMap.pxh;
			int num2 = -1;
			bool flag = xStart > xEnd;
			if (flag)
			{
				for (int i = xEnd; i < xStart; i += 24)
				{
					int ysd = AutoPickMobFunctions.GetYsd(i);
					bool flag2 = ysd < num;
					if (flag2)
					{
						num = ysd;
						num2 = i;
					}
				}
			}
			else
			{
				for (int j = xEnd; j > xStart; j -= 24)
				{
					int ysd2 = AutoPickMobFunctions.GetYsd(j);
					bool flag3 = ysd2 < num;
					if (flag3)
					{
						num = ysd2;
						num2 = j;
					}
				}
			}
			return new int[]
			{
				num2,
				num
			};
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000065D6 File Offset: 0x000047D6
		public static void Wait(int time)
		{
			AutoPickMobFunctions.IsWait = true;
			AutoPickMobFunctions.TimeStartWait = mSystem.currentTimeMillis();
			AutoPickMobFunctions.TimeWait = (long)time;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000A3244 File Offset: 0x000A1444
		public static bool IsWaiting()
		{
			bool flag = AutoPickMobFunctions.IsWait && mSystem.currentTimeMillis() - AutoPickMobFunctions.TimeStartWait >= AutoPickMobFunctions.TimeWait;
			if (flag)
			{
				AutoPickMobFunctions.IsWait = false;
			}
			return AutoPickMobFunctions.IsWait;
		}

		// Token: 0x04001314 RID: 4884
		private const int TIME_REPICKITEM = 500;

		// Token: 0x04001315 RID: 4885
		private const int TIME_DELAY_TANSAT = 0;

		// Token: 0x04001316 RID: 4886
		private const int ID_ICON_ITEM_TDLT = 4387;

		// Token: 0x04001317 RID: 4887
		private static readonly sbyte[] IdSkillsMelee = new sbyte[]
		{
			0,
			9,
			2,
			17,
			4
		};

		// Token: 0x04001318 RID: 4888
		private static readonly sbyte[] IdSkillsCanNotAttack = new sbyte[]
		{
			10,
			11,
			14,
			23,
			7
		};

		// Token: 0x04001319 RID: 4889
		private static readonly AutoPickMobFunctions _Instance = new AutoPickMobFunctions();

		// Token: 0x0400131A RID: 4890
		public static bool IsPickingItems;

		// Token: 0x0400131B RID: 4891
		private static bool IsWait;

		// Token: 0x0400131C RID: 4892
		private static long TimeStartWait;

		// Token: 0x0400131D RID: 4893
		private static long TimeWait;

		// Token: 0x0400131E RID: 4894
		public static List<ItemMap> ItemPicks = new List<ItemMap>();

		// Token: 0x0400131F RID: 4895
		private static int IndexItemPick = 0;

		// Token: 0x020000C8 RID: 200
		private enum TpyePickItem
		{
			// Token: 0x04001321 RID: 4897
			CanNotPickItem,
			// Token: 0x04001322 RID: 4898
			PickItemNormal,
			// Token: 0x04001323 RID: 4899
			PickItemTDLT,
			// Token: 0x04001324 RID: 4900
			PickItemTanSat
		}
	}
}
