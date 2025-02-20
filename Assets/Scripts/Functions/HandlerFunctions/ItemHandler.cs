using System;
using System.Collections.Generic;
using System.Threading;

namespace Functions.HandlerFunctions
{
	// Token: 0x020000C6 RID: 198
	internal class ItemHandler : IActionListener, IChatable
	{
		public void perform(int idAction, object p)
		{
			if (idAction <= 3)
			{
				if (idAction != 1)
				{
					if (idAction == 3)
					{
						ItemHandler.OpenTFAutoTradeItem((ItemHandler.ItemAuto)p);
					}
				}
				else
				{
					ItemHandler.OpenTFAutoUseItem((ItemHandler.ItemAuto)p);
				}
			}
			else if (idAction != 11821)
			{
				if (idAction == 11822)
				{
					new Thread(delegate ()
					{
						ItemHandler.EquipItems(2, 4);
					}).Start();
				}
			}
			else
			{
				new Thread(delegate ()
				{
					ItemHandler.EquipItems(1, 4);
				}).Start();
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000AF198 File Offset: 0x000AD398
		public static ItemHandler gI()
		{
			bool flag = ItemHandler._Instance == null;
			if (flag)
			{
				ItemHandler._Instance = new ItemHandler();
			}
			return ItemHandler._Instance;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000AF1C8 File Offset: 0x000AD3C8
		public static void update()
		{
			bool flag = ItemHandler.listItemAuto.Count <= 0;
			if (!flag)
			{
				int num = 0;
				ItemHandler.ItemAuto itemAuto;
				for (; ; )
				{
					bool flag2 = num < ItemHandler.listItemAuto.Count;
					if (!flag2)
					{
						goto IL_63;
					}
					itemAuto = ItemHandler.listItemAuto[num];
					bool flag3 = mSystem.currentTimeMillis() - itemAuto.LastTimeUse > (long)(itemAuto.Delay * 1000);
					if (flag3)
					{
						break;
					}
					num++;
				}
				itemAuto.LastTimeUse = mSystem.currentTimeMillis();
				Service.gI().useItem(0, 1, -1, (short)itemAuto.templateId);
				IL_63:;
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000AF260 File Offset: 0x000AD460
		public void onChatFromMe(string text, string to)
		{
			bool flag = ChatTextField.gI().tfChat.getText() == null || ChatTextField.gI().tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null;
			if (flag)
			{
				ChatTextField.gI().isShow = false;
			}
			else
			{
				bool flag2 = ChatTextField.gI().strChat.Equals(ItemHandler.inputDelay[0]);
				if (flag2)
				{
					try
					{
						int delay = int.Parse(ChatTextField.gI().tfChat.getText());
						ItemHandler.itemAuto.Delay = delay;
						GameScr.info1.addInfo(string.Concat(new string[]
						{
							"Auto ",
							ItemHandler.itemAuto.Name,
							": ",
							delay.ToString(),
							" giây"
						}), 0);
						ItemHandler.listItemAuto.Add(ItemHandler.itemAuto);
					}
					catch
					{
						GameScr.info1.addInfo("Delay Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					ItemHandler.ResetTF();
				}
				else
				{
					bool flag3 = ChatTextField.gI().strChat.Equals(ItemHandler.inputBuyQuantity[0]);
					if (flag3)
					{
						try
						{
							int quantity = int.Parse(ChatTextField.gI().tfChat.getText());
							ItemHandler.itemAuto.Quantity = quantity;
							new Thread(delegate ()
							{
								this.AutoBuy(ItemHandler.itemAuto);
							}).Start();
						}
						catch
						{
							GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
						}
						ItemHandler.ResetTF();
					}
					else
					{
						bool flag4 = !ChatTextField.gI().strChat.Equals(ItemHandler.inputSellQuantity[0]);
						if (!flag4)
						{
							try
							{
								int quantity2 = int.Parse(ChatTextField.gI().tfChat.getText());
								ItemHandler.itemAuto.Quantity = quantity2;
								new Thread(delegate ()
								{
									ItemHandler.AutoSell(ItemHandler.itemAuto);
								}).Start();
							}
							catch
							{
								GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
							}
							ItemHandler.ResetTF();
						}
					}
				}
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000AF4B0 File Offset: 0x000AD6B0
		public void infoMe(string s)
		{
			bool flag = s.ToLower().StartsWith("mua thành công") || s.ToLower().StartsWith("buy successful");
			if (flag)
			{
				ItemHandler.itemAuto.Quantity--;
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000AF4FB File Offset: 0x000AD6FB
		public void onCancelChat()
		{
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000AF4FE File Offset: 0x000AD6FE
		private static void ResetTF()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000AF530 File Offset: 0x000AD730
		private static void OpenTFAutoUseItem(ItemHandler.ItemAuto item)
		{
			ItemHandler.itemAuto = item;
			ChatTextField.gI().strChat = ItemHandler.inputDelay[0];
			ChatTextField.gI().tfChat.name = ItemHandler.inputDelay[1];
			GameCanvas.panel.isShow = false;
			ChatTextField.gI().startChat2(ItemHandler.gI(), string.Empty);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000AF58C File Offset: 0x000AD78C
		private static void OpenTFAutoTradeItem(ItemHandler.ItemAuto item)
		{
			ItemHandler.itemAuto = item;
			GameCanvas.panel.isShow = false;
			bool isSell = item.IsSell;
			if (isSell)
			{
				ChatTextField.gI().strChat = ItemHandler.inputSellQuantity[0];
				ChatTextField.gI().tfChat.name = ItemHandler.inputSellQuantity[1];
			}
			else
			{
				ChatTextField.gI().strChat = ItemHandler.inputBuyQuantity[0];
				ChatTextField.gI().tfChat.name = ItemHandler.inputBuyQuantity[1];
			}
			ChatTextField.gI().startChat2(ItemHandler.gI(), string.Empty);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000AF620 File Offset: 0x000AD820
		private static void AutoSell(ItemHandler.ItemAuto item)
		{
			Thread.Sleep(100);
			short index = item.Index;
			for (; ; )
			{
				bool flag = item.Quantity > 0;
				if (!flag)
				{
					goto IL_DD;
				}
				bool flag2 = global::Char.myCharz().arrItemBag[(int)index] == null || (global::Char.myCharz().arrItemBag[(int)index] != null && global::Char.myCharz().arrItemBag[(int)index].template.id != (short)item.templateId);
				if (flag2)
				{
					break;
				}
				Service.gI().saleItem(0, 1, index);
				Thread.Sleep(100);
				Service.gI().saleItem(1, 1, index);
				Thread.Sleep(1000);
				item.Quantity--;
				bool flag3 = global::Char.myCharz().xu > 1963100000L;
				if (flag3)
				{
					goto Block_5;
				}
			}
			GameScr.info1.addInfo("Không Tìm Thấy Item!", 0);
			return;
			Block_5:
			GameScr.info1.addInfo("Xong!", 0);
			return;
			IL_DD:
			GameScr.info1.addInfo("Xong!", 0);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000AF738 File Offset: 0x000AD938
		private void AutoBuy(ItemHandler.ItemAuto item)
		{
			while (item.Quantity > 0 && !GameScr.gI().isBagFull())
			{
				Service.gI().buyItem((!item.IsGold) ? (sbyte)1 : (sbyte)0, item.templateId, 0);
				Thread.Sleep(1000);
			}
			GameScr.info1.addInfo("Xong!", 0);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000AF7A4 File Offset: 0x000AD9A4
		static ItemHandler()
		{
			ItemHandler.listItemAuto = new List<ItemHandler.ItemAuto>();
			ItemHandler.set1 = new List<string>();
			ItemHandler.set2 = new List<string>();
			ItemHandler.inputDelay = new string[]
			{
				"Nhập delay",
				"giây"
			};
			ItemHandler.inputSellQuantity = new string[]
			{
				"Nhập số lượng bán",
				"số lượng"
			};
			ItemHandler.inputBuyQuantity = new string[]
			{
				"Nhập số lượng mua",
				"số lượng"
			};
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000AF840 File Offset: 0x000ADA40
		public static void AddEquipmentstoList(Item item, int ListIndex)
		{
			if (ListIndex != 1)
			{
				if (ListIndex == 2)
				{
					foreach (ItemHandler.Equipment2 equipment in ItemHandler.ListEquipment2)
					{
						bool flag = equipment.type == (int)item.template.type;
						if (flag)
						{
							ItemHandler.ListEquipment2.Remove(equipment);
						}
					}
					ItemHandler.ListEquipment2.Add(new ItemHandler.Equipment2((int)item.template.type, item.info));
					GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào set đồ 2", 0);
				}
			}
			else
			{
				foreach (ItemHandler.Equipment1 equipment2 in ItemHandler.ListEquipment1)
				{
					bool flag2 = equipment2.type == (int)item.template.type;
					if (flag2)
					{
						ItemHandler.ListEquipment1.Remove(equipment2);
					}
				}
				ItemHandler.ListEquipment1.Add(new ItemHandler.Equipment1((int)item.template.type, item.info));
				GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào set đồ 1", 0);
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000AF9CC File Offset: 0x000ADBCC
		public static void EquipItems(int type, sbyte get)
		{
			if (type != 1)
			{
				if (type == 2)
				{
					foreach (ItemHandler.Equipment2 equipment in ItemHandler.ListEquipment2)
					{
						Item[] arrItemBag = global::Char.myCharz().arrItemBag;
						try
						{
							for (int i = 0; i < arrItemBag.Length; i++)
							{
								bool flag = (int)arrItemBag[i].template.type == equipment.type && arrItemBag[i].info == equipment.info;
								if (flag)
								{
									Service.gI().getItem(get, (sbyte)i);
									Thread.Sleep(100);
								}
							}
						}
						catch
						{
						}
					}
				}
			}
			else
			{
				foreach (ItemHandler.Equipment1 equipment2 in ItemHandler.ListEquipment1)
				{
					Item[] arrItemBag2 = global::Char.myCharz().arrItemBag;
					try
					{
						for (int j = 0; j < arrItemBag2.Length; j++)
						{
							bool flag2 = (int)arrItemBag2[j].template.type == equipment2.type && arrItemBag2[j].info == equipment2.info;
							if (flag2)
							{
								Service.gI().getItem(get, (sbyte)j);
								Thread.Sleep(100);
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000AFBA0 File Offset: 0x000ADDA0
		public static void AddItemstoList(Item item)
		{
			foreach (ItemHandler.Items items in ItemHandler.ListItemAuto)
			{
				bool flag = items.iconID == (int)item.template.iconID;
				if (flag)
				{
					ItemHandler.ListItemAuto.Remove(items);
					GameScr.info1.addInfo("Đã xóa " + item.template.name + " khỏi d/s item", 0);
				}
			}
			ItemHandler.ListItemAuto.Add(new ItemHandler.Items((int)item.template.iconID, item.template.name));
			GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào d/s item", 0);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000AFC88 File Offset: 0x000ADE88
		public static void AutoUseItem()
		{
			bool flag = ItemHandler.enableAutoUseItem && GameCanvas.gameTick % 5 == 0;
			if (flag)
			{
				for (int i = 0; i < ItemHandler.ListItemAuto.Count; i++)
				{
					ItemHandler.Items items = ItemHandler.ListItemAuto[i];
					bool flag2 = !ItemTime.isExistItem(items.iconID);
					if (flag2)
					{
						for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
						{
							Item item = global::Char.myCharz().arrItemBag[j];
							bool flag3 = item != null && (int)item.template.iconID == items.iconID && mSystem.currentTimeMillis() - ItemHandler.TIME_DELAY_USE_ITEM > 10000L;
							if (flag3)
							{
								Service.gI().useItem(0, 1, (sbyte)j, -1);
								ItemHandler.TIME_DELAY_USE_ITEM = mSystem.currentTimeMillis();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000AFD88 File Offset: 0x000ADF88
		public static int ItemQuantity(int id, string type)
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				bool flag = type == "id";
				if (flag)
				{
					bool flag2 = item != null && (int)item.template.id == id && id != 590 && id != 933;
					if (flag2)
					{
						return item.quantity;
					}
					bool flag3 = item != null && (int)item.template.id == id && id == 933;
					if (flag3)
					{
						string[] array = item.itemOption[0].getOptionString().Split(new char[]
						{
							' '
						});
						return int.Parse(array[2]);
					}
					bool flag4 = item != null && (int)item.template.id == id && id == 590;
					if (flag4)
					{
						string[] array2 = item.itemOption[0].getOptionString().Split(new char[]
						{
							' '
						});
						return int.Parse(array2[2]);
					}
				}
				else
				{
					bool flag5 = type == "iconID";
					if (flag5)
					{
						bool flag6 = item != null && (int)item.template.iconID == id && id != 590 && id != 933;
						if (flag6)
						{
							return item.quantity;
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000AFF0C File Offset: 0x000AE10C
		public static void AutoUseGrape()
		{
			bool flag = global::Char.myCharz().cStamina <= 5 && GameCanvas.gameTick % 100 == 0;
			if (flag)
			{
				int num = 0;
				Item item;
				for (; ; )
				{
					bool flag2 = num < global::Char.myCharz().arrItemBag.Length;
					if (!flag2)
					{
						goto IL_75;
					}
					item = global::Char.myCharz().arrItemBag[num];
					bool flag3 = item != null && item.template.id == 212;
					if (flag3)
					{
						break;
					}
					num++;
				}
				Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
				return;
				IL_75:
				int num2 = 0;
				Item item2;
				for (; ; )
				{
					bool flag4 = num2 < global::Char.myCharz().arrItemBag.Length;
					if (!flag4)
					{
						goto IL_C6;
					}
					item2 = global::Char.myCharz().arrItemBag[num2];
					bool flag5 = item2 != null && item2.template.id == 211;
					if (flag5)
					{
						break;
					}
					num2++;
				}
				Service.gI().useItem(0, 1, (sbyte)item2.indexUI, -1);
				IL_C6:;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000B001C File Offset: 0x000AE21C
		public static void AutoSellTrashItem()
		{
			bool flag = !ItemHandler.enableAutoSellItem;
			if (!flag)
			{
				bool flag2 = !GameScr.gI().isBagFull();
				if (!flag2)
				{
					bool flag3 = TileMap.mapID != 24 + global::Char.myCharz().cgender;
					if (!flag3)
					{
						for (int i = 0; i < GameScr.vNpc.size(); i++)
						{
							Npc npc = GameScr.vNpc.elementAt(i) as Npc;
							int cx = npc.cx;
							int cy = npc.cy;
							int cx2 = global::Char.myCharz().cx;
							int cy2 = global::Char.myCharz().cy;
							bool flag4 = npc != null && npc.template.npcTemplateId == 16;
							if (flag4)
							{
								bool flag5 = Res.distance(cx2, cy2, cx, cy) > 10;
								if (flag5)
								{
									global::Char.myCharz().cx = cx;
									global::Char.myCharz().cy = cy - 3;
									Service.gI().charMove();
									global::Char.myCharz().cx = cx;
									global::Char.myCharz().cy = cy;
									Service.gI().charMove();
									global::Char.myCharz().cx = cx;
									global::Char.myCharz().cy = cy - 3;
									Service.gI().charMove();
									return;
								}
							}
						}
						for (int j = global::Char.myCharz().arrItemBag.Length; j > 0; j--)
						{
							Item item = global::Char.myCharz().arrItemBag[j];
							bool flag6 = item != null;
							if (flag6)
							{
								bool flag7 = !ItemHandler.isItemKichHoat(item) && !ItemHandler.isItemHaveStar(item);
								if (flag7)
								{
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000B01EC File Offset: 0x000AE3EC
		public static bool isItemKichHoat(Item item)
		{
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				bool flag = item.itemOption[i].optionTemplate.name.StartsWith("$");
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000B0240 File Offset: 0x000AE440
		public static bool isItemHaveStar(Item item)
		{
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				bool flag = item.itemOption[i].optionTemplate.id == 107;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000B028C File Offset: 0x000AE48C
		public static void Update()
		{
			try
			{
				ItemHandler.AutoUseGrape();
			}
			catch (Exception ex)
			{
				Cout.Log(ex.Message);
			}
			try
			{
				ItemHandler.AutoUseItem();
			}
			catch (Exception ex2)
			{
				Cout.Log(ex2.Message);
			}
			ItemHandler.update();
		}

		// Token: 0x0400139A RID: 5018
		private static ItemHandler _Instance;

		// Token: 0x0400139B RID: 5019
		private static List<ItemHandler.ItemAuto> listItemAuto;

		// Token: 0x0400139C RID: 5020
		private static ItemHandler.ItemAuto itemAuto;

		// Token: 0x0400139D RID: 5021
		public static List<string> set1;

		// Token: 0x0400139E RID: 5022
		public static List<string> set2;

		// Token: 0x0400139F RID: 5023
		private static bool isChangingClothes;

		// Token: 0x040013A0 RID: 5024
		private static string[] inputDelay;

		// Token: 0x040013A1 RID: 5025
		private static string[] inputSellQuantity;

		// Token: 0x040013A2 RID: 5026
		private static string[] inputBuyQuantity;

		// Token: 0x040013A3 RID: 5027
		public static List<ItemHandler.Equipment1> ListEquipment1 = new List<ItemHandler.Equipment1>();

		// Token: 0x040013A4 RID: 5028
		public static List<ItemHandler.Equipment2> ListEquipment2 = new List<ItemHandler.Equipment2>();

		// Token: 0x040013A5 RID: 5029
		public static int IndexList;

		// Token: 0x040013A6 RID: 5030
		public static string OBJECT;

		// Token: 0x040013A7 RID: 5031
		public static List<ItemHandler.Items> ListItemAuto = new List<ItemHandler.Items>();

		// Token: 0x040013A8 RID: 5032
		public static bool enableAutoUseItem = true;

		// Token: 0x040013A9 RID: 5033
		public static long TIME_DELAY_USE_ITEM;

		// Token: 0x040013AA RID: 5034
		public static bool enableAutoSellItem;

		// Token: 0x020000F1 RID: 241
		public class ItemAuto
		{
			// Token: 0x06000C16 RID: 3094 RVA: 0x000C3391 File Offset: 0x000C1591
			public ItemAuto()
			{
			}

			// Token: 0x06000C17 RID: 3095 RVA: 0x000C339B File Offset: 0x000C159B
			public ItemAuto(int int_1, string string_0)
			{
				this.templateId = int_1;
				this.Name = string_0;
			}

			// Token: 0x06000C18 RID: 3096 RVA: 0x000C33B3 File Offset: 0x000C15B3
			public ItemAuto(int int_1, short short_0, bool bool_0, bool bool_1)
			{
				this.templateId = int_1;
				this.IsGold = bool_0;
				this.Index = short_0;
				this.IsSell = bool_1;
			}

			// Token: 0x04001609 RID: 5641
			public int templateId;

			// Token: 0x0400160A RID: 5642
			public string Name;

			// Token: 0x0400160B RID: 5643
			public int Quantity;

			// Token: 0x0400160C RID: 5644
			public short Index;

			// Token: 0x0400160D RID: 5645
			public bool IsGold;

			// Token: 0x0400160E RID: 5646
			public bool IsSell;

			// Token: 0x0400160F RID: 5647
			public int Delay;

			// Token: 0x04001610 RID: 5648
			public long LastTimeUse;
		}

		// Token: 0x020000F2 RID: 242
		public struct Equipment1
		{
			// Token: 0x06000C19 RID: 3097 RVA: 0x000C33DA File Offset: 0x000C15DA
			public Equipment1(int type, string info)
			{
				this.type = type;
				this.info = info;
			}

			// Token: 0x04001611 RID: 5649
			public string info;

			// Token: 0x04001612 RID: 5650
			public int type;
		}

		// Token: 0x020000F3 RID: 243
		public struct Equipment2
		{
			// Token: 0x06000C1A RID: 3098 RVA: 0x000C33EB File Offset: 0x000C15EB
			public Equipment2(int type, string info)
			{
				this.type = type;
				this.info = info;
			}

			// Token: 0x04001613 RID: 5651
			public string info;

			// Token: 0x04001614 RID: 5652
			public int type;
		}

		// Token: 0x020000F4 RID: 244
		public struct Items
		{
			// Token: 0x06000C1B RID: 3099 RVA: 0x000C33FC File Offset: 0x000C15FC
			public Items(int id, string name)
			{
				this.iconID = id;
				this.name = name;
			}

			// Token: 0x04001615 RID: 5653
			public int iconID;

			// Token: 0x04001616 RID: 5654
			public string name;
		}
	}
}
