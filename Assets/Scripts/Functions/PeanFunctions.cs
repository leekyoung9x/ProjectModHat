using System;

namespace Functions
{
	// Token: 0x020000C4 RID: 196
	public class PeanFunctions : IActionListener, IChatable
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x000A6A54 File Offset: 0x000A4C54
		public static PeanFunctions getInstance()
		{
			bool flag = PeanFunctions._Instance == null;
			if (flag)
			{
				PeanFunctions._Instance = new PeanFunctions();
			}
			return PeanFunctions._Instance;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000A6A84 File Offset: 0x000A4C84
		public static void Update()
		{
			bool flag = PeanFunctions.isAutoRequestPean;
			if (flag)
			{
				PeanFunctions.RequestPean();
			}
			bool flag2 = PeanFunctions.isAutoDonatePean && GameCanvas.gameTick % 20 == 0;
			if (flag2)
			{
				PeanFunctions.DonatePean();
			}
			bool flag3 = PeanFunctions.isAutoHarvestPean;
			if (flag3)
			{
				PeanFunctions.HarvestPean();
			}
			bool flag4 = !global::Char.myCharz().meDead;
			if (flag4)
			{
				PeanFunctions.AutoUsePean();
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000A6AF0 File Offset: 0x000A4CF0
		public void onChatFromMe(string text, string to)
		{
			bool flag = ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null;
			if (flag)
			{
				bool flag2 = ChatTextField.gI().strChat.Equals(PeanFunctions.inputHPPercent[0]);
				if (flag2)
				{
					try
					{
						int num = int.Parse(ChatTextField.gI().tfChat.getText());
						bool flag3 = num >= 100;
						if (flag3)
						{
							num = 99;
						}
						bool flag4 = num <= 0;
						if (flag4)
						{
							num = 1;
						}
						PeanFunctions.minimumHPPercent = num;
						GameScr.info1.addInfo("Auto pean khi HP dưới: " + num.ToString() + "%", 0);
						bool flag5 = PeanFunctions.isSaveData;
						if (flag5)
						{
							Rms.saveRMSInt("AutoPeanPercentHP", PeanFunctions.minimumHPPercent);
						}
					}
					catch
					{
						GameScr.info1.addInfo("%HP không hợp lệ, vui lòng nhập lại", 0);
					}
					PeanFunctions.ResetChatTextField();
				}
				else
				{
					bool flag6 = ChatTextField.gI().strChat.Equals(PeanFunctions.inputMPPercent[0]);
					if (flag6)
					{
						try
						{
							int num2 = int.Parse(ChatTextField.gI().tfChat.getText());
							bool flag7 = num2 >= 100;
							if (flag7)
							{
								num2 = 99;
							}
							bool flag8 = num2 <= 0;
							if (flag8)
							{
								num2 = 1;
							}
							PeanFunctions.minimumMPPercent = num2;
							GameScr.info1.addInfo("Auto pean khi MP dưới: " + num2.ToString() + "%", 0);
							bool flag9 = PeanFunctions.isSaveData;
							if (flag9)
							{
								Rms.saveRMSInt("AutoPeanPercentMP", PeanFunctions.minimumMPPercent);
							}
						}
						catch
						{
							GameScr.info1.addInfo("%MP không hợp lệ, vui lòng nhập lại", 0);
						}
						PeanFunctions.ResetChatTextField();
					}
					else
					{
						bool flag10 = ChatTextField.gI().strChat.Equals(PeanFunctions.inputHP[0]);
						if (flag10)
						{
							try
							{
								int num3 = PeanFunctions.minimumHP = int.Parse(ChatTextField.gI().tfChat.getText());
								bool flag11 = PeanFunctions.isSaveData;
								if (flag11)
								{
									Rms.saveRMSString("AutoPeanHP", PeanFunctions.minimumHP.ToString());
								}
								GameScr.info1.addInfo("Auto pean khi HP dưới: " + NinjaUtil.getMoneys((long)num3) + "HP", 0);
							}
							catch
							{
								GameScr.info1.addInfo("HP không hợp lệ, vui lòng nhập lại", 0);
							}
							PeanFunctions.ResetChatTextField();
						}
						else
						{
							bool flag12 = ChatTextField.gI().strChat.Equals(PeanFunctions.inputMP[0]);
							if (flag12)
							{
								try
								{
									int num4 = PeanFunctions.minimumMP = int.Parse(ChatTextField.gI().tfChat.getText());
									bool flag13 = PeanFunctions.isSaveData;
									if (flag13)
									{
										Rms.saveRMSString("AutoPeanMP", PeanFunctions.minimumMP.ToString());
									}
									GameScr.info1.addInfo("Auto pean khi MP dưới: " + NinjaUtil.getMoneys((long)num4) + "MP", 0);
								}
								catch
								{
									GameScr.info1.addInfo("MP không hợp lệ, vui lòng nhập lại", 0);
								}
								PeanFunctions.ResetChatTextField();
							}
						}
					}
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
				PeanFunctions.ResetChatTextField();
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00003FF5 File Offset: 0x000021F5
		public void onCancelChat()
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000A6E64 File Offset: 0x000A5064
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
				case 1:
					{
						PeanFunctions.isAutoRequestPean = !PeanFunctions.isAutoRequestPean;
						GameScr.info1.addInfo("Xin Đậu\n" + (PeanFunctions.isAutoRequestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
						bool flag = PeanFunctions.isSaveData;
						if (flag)
						{
							Rms.saveRMSInt("AutoPeanIsAutoRequestPean", PeanFunctions.isAutoRequestPean ? 1 : 0);
						}
						break;
					}
				case 2:
					{
						PeanFunctions.isAutoDonatePean = !PeanFunctions.isAutoDonatePean;
						GameScr.info1.addInfo("Cho Đậu\n" + (PeanFunctions.isAutoDonatePean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
						bool flag2 = PeanFunctions.isSaveData;
						if (flag2)
						{
							Rms.saveRMSInt("AutoPeanIsAutoSendPean", PeanFunctions.isAutoDonatePean ? 1 : 0);
						}
						break;
					}
				case 3:
					{
						PeanFunctions.isAutoHarvestPean = !PeanFunctions.isAutoHarvestPean;
						GameScr.info1.addInfo("Thu Đậu\n" + (PeanFunctions.isAutoHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
						bool flag3 = PeanFunctions.isSaveData;
						if (flag3)
						{
							Rms.saveRMSInt("AutoPeanIsAutoHarvestPean", PeanFunctions.isAutoHarvestPean ? 1 : 0);
						}
						break;
					}
				case 4:
					{
						bool flag4 = PeanFunctions.minimumHP != 0;
						if (flag4)
						{
							PeanFunctions.minimumHP = 0;
							GameScr.info1.addInfo("Auto đậu: 0HP", 0);
							bool flag5 = PeanFunctions.isSaveData;
							if (flag5)
							{
								Rms.saveRMSString("AutoPeanHP", PeanFunctions.minimumHP.ToString());
							}
						}
						else
						{
							bool flag6 = PeanFunctions.minimumHP == 0;
							if (flag6)
							{
								ChatTextField.gI().strChat = PeanFunctions.inputHP[0];
								ChatTextField.gI().tfChat.name = PeanFunctions.inputHP[1];
								ChatTextField.gI().startChat2(PeanFunctions.getInstance(), string.Empty);
							}
						}
						break;
					}
				case 5:
					{
						bool flag7 = PeanFunctions.minimumHPPercent != 0;
						if (flag7)
						{
							PeanFunctions.minimumHPPercent = 0;
							GameScr.info1.addInfo("Auto đậu: 0% HP", 0);
							bool flag8 = PeanFunctions.isSaveData;
							if (flag8)
							{
								Rms.saveRMSInt("AutoPeanPercentHP", PeanFunctions.minimumHPPercent);
							}
						}
						else
						{
							bool flag9 = PeanFunctions.minimumHPPercent == 0;
							if (flag9)
							{
								ChatTextField.gI().strChat = PeanFunctions.inputHPPercent[0];
								ChatTextField.gI().tfChat.name = PeanFunctions.inputHPPercent[1];
								ChatTextField.gI().startChat2(PeanFunctions.getInstance(), string.Empty);
							}
						}
						break;
					}
				case 6:
					{
						bool flag10 = PeanFunctions.minimumMP != 0;
						if (flag10)
						{
							PeanFunctions.minimumMP = 0;
							GameScr.info1.addInfo("Auto đậu: 0MP", 0);
							bool flag11 = PeanFunctions.isSaveData;
							if (flag11)
							{
								Rms.saveRMSString("AutoPeanMP", PeanFunctions.minimumMP.ToString());
							}
						}
						else
						{
							bool flag12 = PeanFunctions.minimumMP == 0;
							if (flag12)
							{
								ChatTextField.gI().strChat = PeanFunctions.inputMP[0];
								ChatTextField.gI().tfChat.name = PeanFunctions.inputMP[1];
								ChatTextField.gI().startChat2(PeanFunctions.getInstance(), string.Empty);
							}
						}
						break;
					}
				case 7:
					{
						bool flag13 = PeanFunctions.minimumMPPercent != 0;
						if (flag13)
						{
							PeanFunctions.minimumMPPercent = 0;
							GameScr.info1.addInfo("Auto đậu: 0% MP", 0);
							bool flag14 = PeanFunctions.isSaveData;
							if (flag14)
							{
								Rms.saveRMSInt("AutoPeanPercentMP", PeanFunctions.minimumMPPercent);
							}
						}
						else
						{
							bool flag15 = PeanFunctions.minimumMPPercent == 0;
							if (flag15)
							{
								ChatTextField.gI().strChat = PeanFunctions.inputMPPercent[0];
								ChatTextField.gI().tfChat.name = PeanFunctions.inputMPPercent[1];
								ChatTextField.gI().startChat2(PeanFunctions.getInstance(), string.Empty);
							}
						}
						break;
					}
				case 8:
					{
						PeanFunctions.isSaveData = !PeanFunctions.isSaveData;
						GameScr.info1.addInfo("Lưu Cài Đặt\n" + (PeanFunctions.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
						Rms.saveRMSInt("AutoPeanIsSaveRms", PeanFunctions.isSaveData ? 1 : 0);
						bool flag16 = PeanFunctions.isSaveData;
						if (flag16)
						{
							PeanFunctions.SaveData();
						}
						break;
					}
				case 9:
					Char.isPetHandler = !Char.isPetHandler;
					GameScr.info1.addInfo("Auto Buff Đậu Cho Đệ:\n"+(Char.isPetHandler ? "ON":"OFF"),0);
					break;
			}
		}
		public static bool isbuffpet = true;
		// Token: 0x06000A3F RID: 2623 RVA: 0x000A72AC File Offset: 0x000A54AC
		public static void ShowMenu()
		{
			PeanFunctions.LoadData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Xin Đậu\n" + (PeanFunctions.isAutoRequestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), PeanFunctions.getInstance(), 1, null));
			myVector.addElement(new Command("Cho Đậu\n" + (PeanFunctions.isAutoDonatePean ? "[STATUS: ON]" : "[STATUS: OFF]"), PeanFunctions.getInstance(), 2, null));
			myVector.addElement(new Command("Thu Đậu\n" + (PeanFunctions.isAutoHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), PeanFunctions.getInstance(), 3, null));
			myVector.addElement(new Command("Ăn Đậu Khi HP Dưới: " + NinjaUtil.getMoneys((long)PeanFunctions.minimumHP) + "HP", PeanFunctions.getInstance(), 4, null));
			myVector.addElement(new Command("Ăn Đậu Khi HP Dưới: " + PeanFunctions.minimumHPPercent.ToString() + "%", PeanFunctions.getInstance(), 5, null));
			myVector.addElement(new Command("Ăn Đậu Khi MP Dưới: " + NinjaUtil.getMoneys((long)PeanFunctions.minimumMP) + "MP", PeanFunctions.getInstance(), 6, null));
			myVector.addElement(new Command("Ăn Đậu Khi MP Dưới: " + PeanFunctions.minimumMPPercent.ToString() + "%", PeanFunctions.getInstance(), 7, null));
			myVector.addElement(new Command("Auto Buff Đậu Cho Đệ:\n" +(Char.isPetHandler ? "[ON]":"[OFF]"), PeanFunctions.getInstance(), 9, null));
			myVector.addElement(new Command("Lưu Cài Đặt\n" + (PeanFunctions.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), PeanFunctions.getInstance(), 8, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000A7448 File Offset: 0x000A5648
		private static void LoadData()
		{
			PeanFunctions.isSaveData = (Rms.loadRMSInt("AutoPeanIsSaveRms") == 1);
			bool flag = PeanFunctions.isSaveData;
			if (flag)
			{
				PeanFunctions.isAutoRequestPean = (Rms.loadRMSInt("AutoPeanIsAutoRequestPean") == 1);
				PeanFunctions.isAutoDonatePean = (Rms.loadRMSInt("AutoPeanIsAutoSendPean") == 1);
				PeanFunctions.isAutoHarvestPean = (Rms.loadRMSInt("AutoPeanIsAutoHarvestPean") == 1);
				bool flag2 = Rms.loadRMSInt("AutoPeanPercentHP") == -1;
				if (flag2)
				{
					PeanFunctions.minimumHPPercent = 0;
				}
				else
				{
					PeanFunctions.minimumHPPercent = Rms.loadRMSInt("AutoPeanPercentHP");
				}
				bool flag3 = Rms.loadRMSInt("AutoPeanPercentMP") == -1;
				if (flag3)
				{
					PeanFunctions.minimumMPPercent = 0;
				}
				else
				{
					PeanFunctions.minimumMPPercent = Rms.loadRMSInt("AutoPeanPercentMP");
				}
				bool flag4 = Rms.loadRMSString("AutoPeanHP") == null;
				if (flag4)
				{
					PeanFunctions.minimumHP = 0;
				}
				else
				{
					PeanFunctions.minimumHP = int.Parse(Rms.loadRMSString("AutoPeanHP"));
				}
				bool flag5 = Rms.loadRMSString("AutoPeanMP") == null;
				if (flag5)
				{
					PeanFunctions.minimumMP = 0;
				}
				else
				{
					PeanFunctions.minimumMP = int.Parse(Rms.loadRMSString("AutoPeanMP"));
				}
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000A7568 File Offset: 0x000A5768
		private static void SaveData()
		{
			Rms.saveRMSInt("AutoPeanIsAutoRequestPean", PeanFunctions.isAutoRequestPean ? 1 : 0);
			Rms.saveRMSInt("AutoPeanIsAutoSendPean", PeanFunctions.isAutoDonatePean ? 1 : 0);
			Rms.saveRMSInt("AutoPeanIsAutoHarvestPean", PeanFunctions.isAutoHarvestPean ? 1 : 0);
			Rms.saveRMSString("AutoPeanHP", PeanFunctions.minimumHP.ToString());
			Rms.saveRMSInt("AutoPeanPercentHP", PeanFunctions.minimumHPPercent);
			Rms.saveRMSString("AutoPeanMP", PeanFunctions.minimumMP.ToString());
			Rms.saveRMSInt("AutoPeanPercentMP", PeanFunctions.minimumMPPercent);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00006812 File Offset: 0x00004A12
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000A7604 File Offset: 0x000A5804
		private static void RequestPean()
		{
			bool flag = mSystem.currentTimeMillis() - PeanFunctions.lastTimeRequestedPean >= 301000L;
			if (flag)
			{
				PeanFunctions.lastTimeRequestedPean = mSystem.currentTimeMillis();
				Service.gI().clanMessage(1, "", -1);
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000A764C File Offset: 0x000A584C
		private static void DonatePean()
		{
			for (int i = 0; i < ClanMessage.vMessage.size(); i++)
			{
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
				bool flag = clanMessage.maxCap != 0 && clanMessage.playerName != global::Char.myCharz().cName && clanMessage.recieve != clanMessage.maxCap;
				if (flag)
				{
					Service.gI().clanDonate(clanMessage.id);
					break;
				}
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000A76D4 File Offset: 0x000A58D4
		private static void HarvestPean()
		{
			bool flag = TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23;
			if (flag)
			{
				int num = 0;
				for (int i = 0; i < global::Char.myCharz().arrItemBox.Length; i++)
				{
					bool flag2 = global::Char.myCharz().arrItemBox[i] != null && global::Char.myCharz().arrItemBox[i].template.type == 6;
					if (flag2)
					{
						num += global::Char.myCharz().arrItemBox[i].quantity;
					}
				}
				bool flag3 = num < 20 && GameCanvas.gameTick % 100 == 0;
				if (flag3)
				{
					for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
					{
						bool flag4 = global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.type == 6;
						if (flag4)
						{
							Service.gI().getItem(1, (sbyte)j);
							break;
						}
					}
				}
				bool flag5 = GameScr.gI().magicTree.currPeas > 0 && (GameScr.hpPotion < 10 || num < 20) && GameCanvas.gameTick % 200 == 0;
				if (flag5)
				{
					Service.gI().openMenu(4);
					Service.gI().menu(4, 0, 0);
				}
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000A7844 File Offset: 0x000A5A44
		private static void AutoUsePean()
		{
			bool flag = GameScr.hpPotion > 0;
			if (flag)
			{
				bool flag2 = PeanFunctions.minimumHPPercent != 0 && PeanFunctions.isMyHPLowerThan(PeanFunctions.minimumHP, PeanFunctions.minimumHPPercent) && GameCanvas.gameTick % 20 == 0;
				if (flag2)
				{
					GameScr.gI().doUseHP();
				}
				else
				{
					bool flag3 = PeanFunctions.minimumMPPercent != 0 && PeanFunctions.isMyMPLowerThan(PeanFunctions.minimumMP, PeanFunctions.minimumMPPercent) && GameCanvas.gameTick % 20 == 0;
					if (flag3)
					{
						GameScr.gI().doUseHP();
					}
				}
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000A78D0 File Offset: 0x000A5AD0
		private static int MyHPPercent()
		{
			return (int)((long)global::Char.myCharz().cHP * 100L / (long)global::Char.myCharz().cHPFull);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000A7900 File Offset: 0x000A5B00
		private static int MyMPPercent()
		{
			return (int)((long)global::Char.myCharz().cMP * 100L / (long)global::Char.myCharz().cMPFull);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000A7930 File Offset: 0x000A5B30
		private static bool isMyHPLowerThan(int minHP, int minHPPercent)
		{
			return global::Char.myCharz().cHP > 0 && (PeanFunctions.MyHPPercent() <= minHPPercent || global::Char.myCharz().cHP < minHP);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000A796C File Offset: 0x000A5B6C
		private static bool isMyMPLowerThan(int minMP, int minMPPercent)
		{
			return global::Char.myCharz().cHP > 0 && (PeanFunctions.MyMPPercent() <= minMPPercent || global::Char.myCharz().cMP < minMP);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000A79A8 File Offset: 0x000A5BA8
		static PeanFunctions()
		{
			PeanFunctions.LoadData();
		}

		// Token: 0x0400132C RID: 4908
		public static PeanFunctions _Instance;

		// Token: 0x0400132D RID: 4909
		private static bool isAutoRequestPean;

		// Token: 0x0400132E RID: 4910
		private static long lastTimeRequestedPean;

		// Token: 0x0400132F RID: 4911
		private static bool isAutoDonatePean;

		// Token: 0x04001330 RID: 4912
		private static bool isAutoHarvestPean;

		// Token: 0x04001331 RID: 4913
		private static int minimumHPPercent;

		// Token: 0x04001332 RID: 4914
		private static int minimumMPPercent;

		// Token: 0x04001333 RID: 4915
		private static int minimumHP;

		// Token: 0x04001334 RID: 4916
		private static int minimumMP;

		// Token: 0x04001335 RID: 4917
		private static bool isSaveData;

		// Token: 0x04001336 RID: 4918
		private static string[] inputHPPercent = new string[]
		{
			"Nhập %HP Pean",
			"%HP"
		};

		// Token: 0x04001337 RID: 4919
		private static string[] inputMPPercent = new string[]
		{
			"Nhập %MP Pean",
			"%MP"
		};

		// Token: 0x04001338 RID: 4920
		private static string[] inputHP = new string[]
		{
			"Nhập HP Pean",
			"HP"
		};

		// Token: 0x04001339 RID: 4921
		private static string[] inputMP = new string[]
		{
			"Nhập MP Pean",
			"MP"
		};
	}
}
