using System.Collections.Generic;
using System.Threading;

namespace Functions.AutoFunctions
{
    public class AutoBuyFunctions : IActionListener, IChatable
    {
        public static AutoBuyFunctions getInstance()
        {
            bool flag = AutoBuyFunctions._Instance == null;
            if (flag)
            {
                AutoBuyFunctions._Instance = new AutoBuyFunctions();
            }
            return AutoBuyFunctions._Instance;
        }

        public static void Update()
        {
            bool flag = AutoBuyFunctions.listItemAuto.Count > 0;
            if (flag)
            {
                for (int i = 0; i < AutoBuyFunctions.listItemAuto.Count; i++)
                {
                    AutoBuyFunctions.Item item = AutoBuyFunctions.listItemAuto[i];
                    bool flag2 = mSystem.currentTimeMillis() - item.LastTimeUse > (long)(item.Delay * 1000);
                    if (flag2)
                    {
                        item.LastTimeUse = mSystem.currentTimeMillis();
                        Service.gI().useItem(0, 1, -1, (short)item.Id);
                        break;
                    }
                }
            }
        }

        public void onChatFromMe(string text, string to)
        {
            bool flag = ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null;
            if (flag)
            {
                bool flag2 = ChatTextField.gI().strChat.Equals(AutoBuyFunctions.inputDelay[0]);
                if (flag2)
                {
                    try
                    {
                        int delay = int.Parse(ChatTextField.gI().tfChat.getText());
                        AutoBuyFunctions.itemToAuto.Delay = delay;
                        GameScr.info1.addInfo(string.Concat(new string[]
                        {
                            "Auto ",
                            AutoBuyFunctions.itemToAuto.Name,
                            ": ",
                            delay.ToString(),
                            " giây"
                        }), 0);
                        AutoBuyFunctions.listItemAuto.Add(AutoBuyFunctions.itemToAuto);
                    }
                    catch
                    {
                        GameScr.info1.addInfo("Delay Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
                    }
                    AutoBuyFunctions.ResetChatTextField();
                }
                else
                {
                    bool flag3 = ChatTextField.gI().strChat.Equals(AutoBuyFunctions.inputBuyQuantity[0]);
                    if (flag3)
                    {
                        try
                        {
                            int quantity = int.Parse(ChatTextField.gI().tfChat.getText());
                            AutoBuyFunctions.itemToAuto.Quantity = quantity;
                            new Thread(delegate ()
                            {
                                this.AutoBuy(AutoBuyFunctions.itemToAuto);
                            }).Start();
                        }
                        catch
                        {
                            GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
                        }
                        AutoBuyFunctions.ResetChatTextField();
                    }
                    else
                    {
                        bool flag4 = ChatTextField.gI().strChat.Equals(AutoBuyFunctions.inputSellQuantity[0]);
                        if (flag4)
                        {
                            try
                            {
                                int quantity2 = int.Parse(ChatTextField.gI().tfChat.getText());
                                AutoBuyFunctions.itemToAuto.Quantity = quantity2;
                                new Thread(delegate ()
                                {
                                    AutoBuyFunctions.AutoSell(AutoBuyFunctions.itemToAuto);
                                }).Start();
                            }
                            catch
                            {
                                GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
                            }
                            AutoBuyFunctions.ResetChatTextField();
                        }
                    }
                }
            }
            else
            {
                ChatTextField.gI().isShow = false;
            }
        }

        public void onCancelChat()
        {
        }

        public void perform(int idAction, object p)
        {
            switch (idAction)
            {
                case 1:
                    AutoBuyFunctions.OpenTFAutoUseItem((AutoBuyFunctions.Item)p);
                    break;

                case 2:
                    AutoBuyFunctions.RemoveItemAuto((int)p);
                    break;

                case 3:
                    AutoBuyFunctions.OpenTFAutoTradeItem((AutoBuyFunctions.Item)p);
                    break;

                case 4:
                    AutoBuyFunctions.set1.Add(((global::Item)p).getFullName());
                    break;

                case 5:
                    AutoBuyFunctions.set2.Add(((global::Item)p).getFullName());
                    break;

                case 6:
                    AutoBuyFunctions.set1.Remove(((global::Item)p).getFullName());
                    break;

                case 7:
                    AutoBuyFunctions.set2.Remove(((global::Item)p).getFullName());
                    break;
            }
        }

        private static void ResetChatTextField()
        {
            ChatTextField.gI().strChat = "Chat";
            ChatTextField.gI().tfChat.name = "chat";
            ChatTextField.gI().isShow = false;
        }

        public static bool isAutoUse(int templateId)
        {
            for (int i = 0; i < AutoBuyFunctions.listItemAuto.Count; i++)
            {
                bool flag = AutoBuyFunctions.listItemAuto[i].Id == templateId;
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }

        private static void RemoveItemAuto(int templateId)
        {
            for (int i = 0; i < AutoBuyFunctions.listItemAuto.Count; i++)
            {
                bool flag = AutoBuyFunctions.listItemAuto[i].Id == templateId;
                if (flag)
                {
                    AutoBuyFunctions.listItemAuto.RemoveAt(i);
                    break;
                }
            }
        }

        private static void OpenTFAutoUseItem(AutoBuyFunctions.Item item)
        {
            AutoBuyFunctions.itemToAuto = item;
            ChatTextField.gI().strChat = AutoBuyFunctions.inputDelay[0];
            ChatTextField.gI().tfChat.name = AutoBuyFunctions.inputDelay[1];
            GameCanvas.panel.isShow = false;
            ChatTextField.gI().startChat2(AutoBuyFunctions.getInstance(), string.Empty);
        }

        private static void OpenTFAutoTradeItem(AutoBuyFunctions.Item item)
        {
            AutoBuyFunctions.itemToAuto = item;
            GameCanvas.panel.isShow = false;
            bool isSell = item.IsSell;
            if (isSell)
            {
                ChatTextField.gI().strChat = AutoBuyFunctions.inputSellQuantity[0];
                ChatTextField.gI().tfChat.name = AutoBuyFunctions.inputSellQuantity[1];
            }
            else
            {
                ChatTextField.gI().strChat = AutoBuyFunctions.inputBuyQuantity[0];
                ChatTextField.gI().tfChat.name = AutoBuyFunctions.inputBuyQuantity[1];
            }
            ChatTextField.gI().startChat2(AutoBuyFunctions.getInstance(), string.Empty);
        }

        private static void AutoSell(AutoBuyFunctions.Item item)
        {
            Thread.Sleep(100);
            short index = item.Index;
            while (item.Quantity > 0)
            {
                bool flag = global::Char.myCharz().arrItemBag[(int)index] == null || (global::Char.myCharz().arrItemBag[(int)index] != null && global::Char.myCharz().arrItemBag[(int)index].template.id != (short)item.Id);
                if (flag)
                {
                    GameScr.info1.addInfo("Không Tìm Thấy Item!", 0);
                }
                else
                {
                    Service.gI().saleItem(0, 1, (short)(index + 3));
                    Thread.Sleep(100);
                    Service.gI().saleItem(1, 1, index);
                    Thread.Sleep(1000);
                    item.Quantity--;
                    bool flag2 = global::Char.myCharz().xu > 1963100000L;
                    if (!flag2)
                    {
                        continue;
                    }
                    GameScr.info1.addInfo("Xong!", 0);
                }
                return;
            }
            GameScr.info1.addInfo("Xong!", 0);
        }

        private void AutoBuy(AutoBuyFunctions.Item item)
        {
            while (item.Quantity > 0 && !GameScr.gI().isBagFull())
            {
                Service.gI().buyItem((sbyte)item.BuyType, item.Id, 0);
                item.Quantity--;
                Thread.Sleep(500);
            }
            GameScr.info1.addInfo("Xong!", 0);
        }

        public static void useSet(int type)
        {
            bool flag = AutoBuyFunctions.isChangingSet;
            if (flag)
            {
                GameScr.info1.addInfo("Đang Mặc Đồ!", 0);
            }
            else
            {
                new Thread(delegate ()
                {
                    bool flag2 = type == 0;
                    if (flag2)
                    {
                        AutoBuyFunctions.ChangeSet(AutoBuyFunctions.set1);
                    }
                    bool flag3 = type == 1;
                    if (flag3)
                    {
                        AutoBuyFunctions.ChangeSet(AutoBuyFunctions.set2);
                    }
                }).Start();
            }
        }

        private static void ChangeSet(List<string> set)
        {
            bool flag = AutoBuyFunctions.isChangingSet;
            if (flag)
            {
                GameScr.info1.addInfo("Đang Mặc Đồ!", 0);
            }
            else
            {
                AutoBuyFunctions.isChangingSet = true;
                for (int i = global::Char.myCharz().arrItemBag.Length - 1; i >= 0; i--)
                {
                    global::Item item = global::Char.myCharz().arrItemBag[i];
                    bool flag2 = item != null && set.Contains(item.getFullName());
                    if (flag2)
                    {
                        Service.gI().getItem(4, (sbyte)i);
                        Thread.Sleep(100);
                    }
                }
                AutoBuyFunctions.isChangingSet = false;
            }
        }

        private static AutoBuyFunctions _Instance;

        private static List<AutoBuyFunctions.Item> listItemAuto = new List<AutoBuyFunctions.Item>();

        private static AutoBuyFunctions.Item itemToAuto;

        public static List<string> set1 = new List<string>();

        public static List<string> set2 = new List<string>();

        private static bool isChangingSet;

        private static string[] inputDelay = new string[]
        {
            "Nhập delay",
            "giây"
        };

        private static string[] inputSellQuantity = new string[]
        {
            "Nhập số lượng bán",
            "số lượng"
        };

        private static string[] inputBuyQuantity = new string[]
        {
            "Nhập số lượng mua",
            "số lượng"
        };

        public class Item
        {
            public Item(int itemId, string itemName)
            {
                this.Id = itemId;
                this.Name = itemName;
            }

            public Item(int itemId, short index, bool isSell, int type)
            {
                this.Id = itemId;
                this.Index = index;
                this.IsSell = isSell;
                this.BuyType = type;
            }

            public int Id;

            public string Name;

            public int Quantity;

            public short Index;

            public bool IsSell;

            public int BuyType;

            public int Delay;

            public long LastTimeUse;
        }
    }
}