using System;
using System.Collections.Generic;
using System.Threading;

namespace Functions.HandlerFunctions
{
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

		public static ItemHandler gI()
		{
			bool flag = ItemHandler._Instance == null;
			if (flag)
			{
				ItemHandler._Instance = new ItemHandler();
			}
			return ItemHandler._Instance;
		}

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

		public void infoMe(string s)
		{
			bool flag = s.ToLower().StartsWith("mua thành công") || s.ToLower().StartsWith("buy successful");
			if (flag)
			{
				ItemHandler.itemAuto.Quantity--;
			}
		}

		public void onCancelChat()
		{
		}

		private static void ResetTF()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		private static void OpenTFAutoUseItem(ItemHandler.ItemAuto item)
		{
			ItemHandler.itemAuto = item;
			ChatTextField.gI().strChat = ItemHandler.inputDelay[0];
			ChatTextField.gI().tfChat.name = ItemHandler.inputDelay[1];
			GameCanvas.panel.isShow = false;
			ChatTextField.gI().startChat2(ItemHandler.gI(), string.Empty);
		}

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

		private void AutoBuy(ItemHandler.ItemAuto item)
		{
			while (item.Quantity > 0 && !GameScr.gI().isBagFull())
			{
				Service.gI().buyItem((!item.IsGold) ? (sbyte)1 : (sbyte)0, item.templateId, 0);
				Thread.Sleep(1000);
			}
			GameScr.info1.addInfo("Xong!", 0);
		}

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
							bool flag3 = item != null && (int)item.template.iconID == items.iconID && mSystem.currentTimeMillis() - ItemHandler.TIME_DELAY_USE_ITEM > 1000L;
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

		private static ItemHandler _Instance;

		private static List<ItemHandler.ItemAuto> listItemAuto;

		private static ItemHandler.ItemAuto itemAuto;

		public static List<string> set1;

		public static List<string> set2;

		private static bool isChangingClothes;

		private static string[] inputDelay;

		private static string[] inputSellQuantity;

		private static string[] inputBuyQuantity;

		public static List<ItemHandler.Equipment1> ListEquipment1 = new List<ItemHandler.Equipment1>();

		public static List<ItemHandler.Equipment2> ListEquipment2 = new List<ItemHandler.Equipment2>();

		public static int IndexList;

		public static string OBJECT;

		public static List<ItemHandler.Items> ListItemAuto = new List<ItemHandler.Items>();

		public static bool enableAutoUseItem = true;

		public static long TIME_DELAY_USE_ITEM;

		public static bool enableAutoSellItem;

		public class ItemAuto
		{
			public ItemAuto()
			{
			}

			public ItemAuto(int int_1, string string_0)
			{
				this.templateId = int_1;
				this.Name = string_0;
			}

			public ItemAuto(int int_1, short short_0, bool bool_0, bool bool_1)
			{
				this.templateId = int_1;
				this.IsGold = bool_0;
				this.Index = short_0;
				this.IsSell = bool_1;
			}

			public int templateId;

			public string Name;

			public int Quantity;

			public short Index;

			public bool IsGold;

			public bool IsSell;

			public int Delay;

			public long LastTimeUse;
		}

		public struct Equipment1
		{
			public Equipment1(int type, string info)
			{
				this.type = type;
				this.info = info;
			}

			public string info;

			public int type;
		}

		public struct Equipment2
		{
			public Equipment2(int type, string info)
			{
				this.type = type;
				this.info = info;
			}

			public string info;

			public int type;
		}

		public struct Items
		{
			public Items(int id, string name)
			{
				this.iconID = id;
				this.name = name;
			}

			public int iconID;

			public string name;
		}
	}
}
