
using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.HairMod.Object.ImageHandle;
namespace HairMod
{
   public class ZamasuMain : MonoBehaviour, IActionListener ,IChatable
    {
        public static bool isSP, isDT;
        public static void PaintTT(mGraphics g)
        {
            int num = 10;
            if (isSP)
            {
                mFont.tahoma_7_white.drawStringBd(g, "Sư Phụ :", num, GameCanvas.h - 170, mFont.LEFT,mFont.tahoma_7_grey);
                mFont.tahoma_7b_red.drawString(g, "Sức Mạnh : " + NinjaUtil.getMoneys(global::Char.myCharz().cPower), num, GameCanvas.h - 160, mFont.LEFT);
                mFont.tahoma_7b_red.drawString(g, "Tiềm Năng : " + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang), num, GameCanvas.h - 150, mFont.LEFT);
                mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
                {
                "Sức Đánh : ",
                NinjaUtil.getMoneys((long)global::Char.myCharz().cDamFull),
                "  Giáp : ",
                global::Char.myCharz().cDefull
                }), num, GameCanvas.h - 140, mFont.LEFT);
                num += GameCanvas.w / 4;
            }
            if (isDT)
            {
                if (Char.myCharz().havePet)
                {
                    Service.gI().petInfo();
                    mFont.tahoma_7_white.drawStringBd(g, "Đệ Tử :", num, GameCanvas.h - 170, mFont.LEFT, mFont.tahoma_7_grey);
                    mFont.tahoma_7b_red.drawString(g, "Sức Mạnh : " + NinjaUtil.getMoneys(global::Char.myPetz().cPower), num, GameCanvas.h - 160, mFont.LEFT);
                    mFont.tahoma_7b_red.drawString(g, "Tiềm Năng : " + NinjaUtil.getMoneys(global::Char.myPetz().cTiemNang), num, GameCanvas.h - 150, mFont.LEFT);
                    mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
                    {
                "Sức Đánh : ",
                NinjaUtil.getMoneys((long)global::Char.myPetz().cDamFull),
                "  Giáp : ",
                global::Char.myPetz().cDefull
                    }), num, GameCanvas.h - 140, mFont.LEFT);
                    mFont.tahoma_7b_red.drawString(g, "HP : " + NinjaUtil.getMoneys((long)global::Char.myPetz().cHP), num, GameCanvas.h - 130, mFont.LEFT);
                    mFont.tahoma_7b_red.drawString(g, "MP : " + NinjaUtil.getMoneys((long)global::Char.myPetz().cMP), num, GameCanvas.h - 120, mFont.LEFT);
              

                }
                else
                {
                    GameCanvas.startOKDlg("Bạn Chưa Có Đệ Tử");
                    isDT = false;
                }
            }
          
        }
        public void onCancelChat()
        {

        }
        public void onChatFromMe(string text, string to) {
            if (ChatTextField.gI().strChat.Equals(ImageObject.arrimageUrl[0]))
            {

                try
                {
                    string str = ChatTextField.gI().tfChat.getText();
                    //string input = GameTCP.readFileText(str);
                    //ImageObject.imageUrl = input;
                    ImageObject.isBG = !ImageObject.isBG;
                  
                       
                    
                    
                    ResetTF();
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    ResetTF();

                }

                return;
            }
        }
        private static void ResetTF()
        {
            ChatTextField.gI().strChat = "Chat";
            ChatTextField.gI().tfChat.name = "chat";
        }
            public void perform(int idAction, object p) {
            switch (idAction)
            {
                case 1:
                    break;
              
                case 2:
                    AutoNhat.isAutoPick = !AutoNhat.isAutoPick;
                    InfoAdd("Auto Nhặt: ", AutoNhat.isAutoPick);
                    break;
                case 3:
                    ClickController.isClick = !ClickController.isClick;
                    GameScr.info1.addInfo("|0|Phím Tắt: " + (ClickController.isClick ? "[Bật]" : "[Tắt]"), 0);
                    break;
                case 4:
                    
                    break;
                case 5:
                    aaMod.isPaintboss = !aaMod.isPaintboss;
                    InfoAdd("Thông Báo Boss: ",aaMod.isPaintboss);
                    break;
                case 6:
                    gdh = !gdh;
                    InfoAdd("Giảm Đồ Họa: ",gdh);
                    break;
                case 7:

                    isSP =! isSP;
                    InfoAdd("Thông Tin Sư Phụ: ", isSP);
                    break;
                case 8:
                    isDT = !isDT;
                    InfoAdd("Thông Tin Đệ Tử: ", isDT);
                    break;
            }
            Effect2.vEffect2Outside.removeElement(Char.chatPopup);
            Effect2.vEffect2.removeElement(Char.chatPopup);
        }
        private static void InfoAdd(string str, bool active) {
            GameScr.info1.addInfo(str + (active ? "Bật" : "Tắt"),0);
        }
        public static bool HiddenInfoItem = true;
        private static ZamasuMain i;
        public static bool gdh;
        public static void ShowCommand()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Auto Nhặt\n" + ((AutoNhat.isAutoPick || AutoNhat.pickByList != 0) ? "[Bật]":"[Tắt]"), gI(), 2, null));
            if ((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
            {
                myVector.addElement(new Command(("Phím Tắt\n" + (ClickController.isClick ? "[Bật]" : "[Tắt]")), gI(), 3, null));
            }
          
                myVector.addElement(new Command("Thông Báo Boss\n"+(aaMod.isPaintboss ? "[Bật]":"[Tắt]"), gI(), 5, null));
            myVector.addElement(new Command("Giảm Đồ Họa\n" + (gdh ? "[Bật]" : "[Tắt]"), gI(), 6, null));
            myVector.addElement(new Command("Thông Tin Sư Phụ" + (isSP ? "[Bật]" : "[Tắt]"), gI(), 7, null));
            myVector.addElement(new Command("Thông Tin Đệ Tử" + (isDT ? "[Bật]" : "[Tắt]"), gI(), 8, null));
            GameCanvas.menu.startAt(myVector, 3);
            ChatPopup.addChatPopup(string.Concat(new object[] { 
            "|0|Thông Tin Sao Pha Lê: " + (HiddenInfoItem ? "Bật" : "Tắt"),
            "\nTự Động Đánh: " + (aaMod.isAk ? "Bật" : "Tắt"),
            "\nTự Động Hồi Sinh: " + (aaMod.isAutoHoiSinh ? "Bật" : "Tắt"),
            "\nCho Đậu Đệ: " + (aaMod.iscdt ? "Bật" : "Tắt"),
            "\nThông Báo Boss: " + (aaMod.isPaintboss ? "Bật" : "Tắt"),
            "\nThông Tin Người Chơi: " + (aaMod.isPaintChar ? "Bật" : "Tắt")
            }), 100000, new Npc(-1, 1, -1, -1, -1, 7992));
        }
        public static void UpdateKey()
        {
            if (!ChatTextField.gI().isShow)
            {
                switch (GameCanvas.keyAsciiPress)
                {
                    case 'z':
                        ShowCommand();
                        break;
                    case 'c':
                        ZamasuMain.UseItem(193);
                        ZamasuMain.UseItem(194);
                        break;
                    case 'm':
                        Service.gI().openUIZone();
                        break;
                    case 'e':
                        ZamasuMain.aaMod.isAutoHoiSinh = !ZamasuMain.aaMod.isAutoHoiSinh;
                        GameScr.info1.addInfo("Đã " + (ZamasuMain.aaMod.isAutoHoiSinh ? "bật" : "tắt") + " tự động hồi sinh.", 0);

                        break;
                    case 'f':
                        ZamasuMain.UseItem(454);
                        ZamasuMain.UseItem(921);
                        break;
                    case 'g':
                        bool flag10 = global::Char.myCharz().charFocus == null;
                        if (flag10)
                        {
                            GameScr.info1.addInfo("Vui Lòng Chọn Mục Tiêu!", 0);
                        }
                        else
                        {
                            Service.gI().giaodich(0, global::Char.myCharz().charFocus.charID, -1, -1);
                            GameScr.info1.addInfo("Đã Gửi Lời Mời Giao Dịch Đến: " + global::Char.myCharz().charFocus.cName, 0);
                        }
                        break;
                    case 'j':
                        LoadMap.LoadMapLeft();
                        break;
                    case 'k':
                        LoadMap.LoadMapCenter();
                        break;
                    case 'l':
                        LoadMap.LoadMapRight();
                        break;
                    case 'a':
                        ZamasuMain.aaMod.isAk = !ZamasuMain.aaMod.isAk;
                        GameScr.info1.addInfo("Đã " + (ZamasuMain.aaMod.isAk ? "bật" : "tắt") + " tự động đánh.", 0);

              
                        break;
                }
            }
        }
        public static ZamasuMain gI()
        {
            if(i == null)
            {
                i = new ZamasuMain();
            }
            return i;
        }
        
        public static void Paint(mGraphics g)
        {
            var logo = GameCanvas.loadImage("/mainImage/logo2.png");
            g.drawImage(logo, GameCanvas.w / 2, 25, 3);
        }
        public static void UseItem(int templateId)
        {
            for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
            {
                Item item = global::Char.myCharz().arrItemBag[i];
                bool flag = item != null && (int)item.template.id == templateId;
                if (flag)
                {
                    Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
                    break;
                }
            }
        }
        public class BossChat
    {
        public string NameBoss { get; set; }
        public string MapBoss { get; set; }
        public long TimeCreate { get; set; }

    }

    public static class aaMod
    {
        static readonly List<string> strBossAppeared = new List<string>()
        {
            "BOSS ",
            " vừa xuất hiện tại ",
            " khu vực "
        };
        public static List<BossChat> bosschats = new List<BossChat>();


        public static bool isPaintboss = true;
        public static bool isPaintChar = true;
        public static bool iscdt;
        public static bool isAutoHoiSinh;

        public static bool isAk;

           
            public static void AutoAk()
        {
            var myChar = Char.myCharz();



            long currentTimeMillis = mSystem.currentTimeMillis();

            if (currentTimeMillis - myChar.myskill.lastTimeUseThisSkill > myChar.myskill.coolDown)
            {
                if (myChar.myskill.template.id != 0 && myChar.myskill.template.id != 1 && myChar.myskill.template.id != 0 && myChar.myskill.template.id != 9 && myChar.myskill.template.id != 2 && myChar.myskill.template.id != 3 && myChar.myskill.template.id != 17 && myChar.myskill.template.id != 4 && myChar.myskill.template.id != 5 && myChar.myskill.template.id != 0) return;

                if (myChar.mobFocus != null && myChar.mobFocus.hp > 0)
                {
                    int num = Math.abs(myChar.cx - myChar.mobFocus.getX());
                    int num2 = Math.abs(myChar.cy - myChar.mobFocus.getY());
                    if (num <= myChar.myskill.dx && num2 <= myChar.myskill.dy)
                    {
                        myChar.myskill.lastTimeUseThisSkill = currentTimeMillis;
                        var vMob = new MyVector();
                        vMob.addElement(myChar.mobFocus);
                        Service.gI().sendPlayerAttack(vMob, new MyVector(), -1); // type = -1 -> auto
                    }
                }
                else if (myChar.charFocus != null && (myChar.charFocus.cTypePk != 0 || myChar.cFlag != myChar.charFocus.cFlag))
                {
                    int num = Math.abs(myChar.cx - myChar.charFocus.cx);
                    int num2 = Math.abs(myChar.cy - myChar.charFocus.cy);
                    if (num <= myChar.myskill.dx && num2 <= myChar.myskill.dy)
                    {
                        myChar.myskill.lastTimeUseThisSkill = currentTimeMillis;
                        var vChar = new MyVector();
                        vChar.addElement(myChar.charFocus);
                        Service.gI().sendPlayerAttack(new MyVector(), vChar, -1); // type = -1 -> auto                   
                    }
                }
            }
        }




        public static void ChatVip(string text)
        {
            if (text.StartsWith(strBossAppeared[0]))
            {
                strBossAppeared.ForEach(s => text = text.Replace(s, "|"));
                string[] array = text.Split('|');
                BossChat botchat = new BossChat();
                botchat.NameBoss = array[1];
                botchat.MapBoss = array[2];
                botchat.TimeCreate = mSystem.currentTimeMillis();
                bosschats.Add(botchat);
                if (bosschats.Count > 7)
                {
                    bosschats.RemoveAt(0);
                }
            }
        }
            private static string ListMap = "Làng Aru,Đồi hoa cúc,Thung lũng tre,Rừng nấm,Rừng xương,Đảo Kamê,Đông Karin,Làng Mori,Đồi nấm tím,Thị trấn Moori,Thung lũng Namếc,Thung lũng Maima,Vực maima,Đảo Guru,Làng Kakarot,Đồi hoang,Làng Plant,Rừng nguyên sinh,Rừng thông Xayda,Thành phố Vegeta,Vách núi đen,Nhà Gôhan,Nhà Moori,Nhà Broly,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Rừng Bamboo,Rừng dương xỉ,Nam Kamê,Đảo Bulông,Núi hoa vàng,Núi hoa tím,Nam Guru,Đông Nam Guru,Rừng cọ,Rừng đá,Thung lũng đen,Bờ vực đen,Vách núi Aru,Vách núi Moori,Vực Plant,Vách núi Aru,Vách núi Moori,Vách núi Kakarot,Thần điện,Tháp Karin,Rừng Karin,Hành tinh Kaio,Phòng tập thời gian,Thánh địa Kaio,Đấu trường,Đại hội võ thuật,Tường thành 1,Tầng 3,Tầng 1,Tầng 2,Tầng 4,Tường thành 2,Tường thành 3,Trại độc nhãn 1,Trại độc nhãn 2,Trại độc nhãn 3,Trại lính Fide,Núi dây leo,Núi cây quỷ,Trại qủy già,Vực chết,Thung lũng Nappa,Vực cấm,Núi Appule,Căn cứ Raspberry,Thung lũng Raspberry,Thung lũng chết,Đồi cây Fide,Khe núi tử thần,Núi đá,Rừng đá,Lãnh  địa Fize,Núi khỉ đỏ,Núi khỉ vàng,Hang quỷ chim,Núi khỉ đen,Hang khỉ đen,Siêu Thị,Hành tinh M-2,Hành tinh Polaris,Hành tinh Cretaceous,Hành tinh Monmaasu,Hành tinh Rudeeze,Hành tinh Gelbo,Hành tinh Tigere,Thành phố phía đông,Thành phố phía nam,Đảo Balê,95,Cao nguyên,Thành phố phía bắc,Ngọn núi phía bắc,Thung lũng phía bắc,Thị trấn Ginder,101,Nhà Bunma,Võ đài Xên bọ hung,Sân sau siêu thị,Cánh đồng tuyết,Rừng tuyết,Núi tuyết,Dòng sông băng,Rừng băng,Hang băng,Đông Nam Karin,Võ đài Hạt Mít,Đại hội võ thuật,Cổng phi thuyền,Phòng chờ,Thánh địa Kaio,Cửa Ải 1,Cửa Ải 2,Cửa Ải 3,Phòng chỉ huy,Đấu trường,Ngũ Hành Sơn,Ngũ Hành Sơn,Ngũ Hành Sơn,Võ đài Bang,Thành phố Santa,Cổng phi thuyền,Bụng Mabư,Đại hội võ thuật,Đại hội võ thuật Vũ Trụ,Hành Tinh Yardart,Hành Tinh Yardart 2,Hành Tinh Yardart 3,Đại hội võ thuật Vũ Trụ 6-7,Động hải tặc,Hang Bạch Tuộc,Động kho báu,Cảng hải tặc,Hành tinh Potaufeu,Hang động Potaufeu,Con đường rắn độc,Con đường rắn độc,Con đường rắn độc,Hoang mạc,Võ Đài Siêu Cấp,Tây Karin,Sa mạc,Lâu đài Lychee,Thành phố Santa,Lôi Đài,Hành tinh bóng tối,Vùng đất băng giá,Lãnh địa bang hội,Hành tinh Bill,Hành tinh ngục tù,Tây thánh địa,Đông thánh Địa,Bắc thánh địa,Nam thánh Địa,Khu hang động,Bìa rừng nguyên thủy,Rừng nguyên thủy,Làng Plant nguyên thủy,Tranh ngọc Namếc";
            public static string[] MapNames = ListMap.Split(new char[]
    {
        ','
    });
            public static int GetIDMap(string mapName)
            {
                int result = -1;
                for (int i = 0; i < MapNames.Length; i++)
                {
                    if (MapNames[i].Trim().ToLower().Equals(mapName.Trim().ToLower()))
                    {
                        result = i;
                    }
                }
                return result;
            }
            public static void chatVip(string chatVip)
            {
                if (chatVip.Trim().ToLower().Contains("boss"))
                {
                    bossVip.addElement(new BossFunctions(chatVip));
                    if (bossVip.size() > 5)
                    {
                        bossVip.removeElementAt(0);
                    }
                }
            }
            public class BossFunctions
            {
                // Token: 0x060008CF RID: 2255 RVA: 0x00092074 File Offset: 0x00090274
                public BossFunctions(string a)
                {
                    a = a.Replace(a.Substring(0, 5), "|");
                    a = a.Replace(" vừa xuất hiện tại", "|");
                    a = a.Replace(" khu vực", "|");
                    string[] array = a.Split(new char[]
                    {
            '|'
                    });
                    this.nameBoss = array[1].Trim();
                    this.mapName = array[2].Trim();
                    this.mapID =GetIDMap(this.mapName);
                    this.time = DateTime.Now;
                }

                // Token: 0x060008D0 RID: 2256 RVA: 0x0009210C File Offset: 0x0009030C
                public void paintBoss(mGraphics a, int b, int c, int d)
                {
                    TimeSpan timeSpan = DateTime.Now.Subtract(this.time);
                    int num = (int)timeSpan.TotalSeconds;
                    mFont mFont = mFont.tahoma_7b_yellowSmall2;
                    bool flag = TileMap.mapName.Trim().ToLower() == this.mapName.Trim().ToLower();
                    if (flag)
                    {
                        mFont = mFont.tahoma_7_red;
                        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                        {
                            global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
                            bool flag2 = @char.cName == this.nameBoss;
                            if (flag2)
                            {
                                mFont = mFont.tahoma_7b_red;
                                break;
                            }
                        }
                    }
                    mFont.drawString(a, string.Concat(new object[]
                    {
            this.nameBoss,
            " - ",
            this.mapName,
            " [",
          GetIDMap(this.mapName),
            "] - ",
            (num < 60) ? (num + "s") : (timeSpan.Minutes + "p"),
            " trước"
                    }), b, c, d);
                }

                // Token: 0x04001071 RID: 4209
                public string nameBoss;

                // Token: 0x04001072 RID: 4210
                public string mapName;

                // Token: 0x04001073 RID: 4211
                public int mapID;

                // Token: 0x04001074 RID: 4212
                public DateTime time;
            }
            public static MyVector bossVip = new MyVector();
            public static void Paint(mGraphics g)
        {
                
                if (!isPaintboss) return;
                int num3 = 35;
                for (int j = 0; j < bossVip.size(); j++)
                {
                    ((BossFunctions)bossVip.elementAt(j)).paintBoss(g, GameCanvas.w - 2, num3, mFont.RIGHT);
                    num3 += 10;
                }



            }



        public static string getTimeAgo(int timeRemainS)
        {
            int num = 0;
            if (timeRemainS < 60)
            {
                return timeRemainS + "s";
            }


            if (timeRemainS > 60)
            {
                num = timeRemainS / 60;
                timeRemainS %= 60;
            }
            int num2 = 0;
            if (num > 60)
            {
                num2 = num / 60;
                num %= 60;
            }
            int num3 = 0;
            if (num2 > 24)
            {
                num3 = num2 / 24;
                num2 %= 24;
            }
            string empty = string.Empty;




            if (num3 > 0)
            {
                empty += num3;
                empty += "d";
                return empty + num2 + "h";
            }
            if (num2 > 0)
            {
                empty += num2;
                empty += "h";
                return empty + num + "'";
            }
            if (num == 0)
            {
                num = 1;
            }
            empty += num;
            return empty + "ph";
        }
        public static List<Char> listChars = new List<Char>();

        public static void PaintCharInMap(mGraphics g)
        {

            //  mFont.number_red.drawString(g, "MapId: " + TileMap.mapID, 50, 80, mFont.LEFT);
            // mFont.number_red.drawString(g, "X: " + Char.myCharz().cx + "  Y:" + Char.myCharz().cy + "  ymap: " + getYGround(Char.myCharz().cx), 50, 90, mFont.LEFT);



            if (isPaintChar)
            {


                var cim = GameScr.vCharInMap;
                listChars.Clear();
                string text = "";
                int width = 0;

                for (int i = 0; i < cim.size(); i++)
                {
                    Char player = (Char)cim.elementAt(i);
                    if (player != null)
                    {
                        if (!string.IsNullOrEmpty(player.cName))
                        {
                            if (player.cTypePk == 5)
                            {
                                listChars.Insert(0, player);
                            }
                            else
                            {
                                listChars.Add(player);
                            }
                            text = player.cName + " - " + NinjaUtil.numberTostring(player.cHP.ToString());
                            int zzz = mFont.number_red.getWidth(text);
                            if (zzz > width)
                            {
                                width = zzz;
                            }
                        }
                    }
                }

                int y = 85;
                int x = 25;

                g.setColor(0, 0.5f);
                g.fillRect(x - 3, y + 2, width + 5, (12 * listChars.Count) - 2);


                listChars.ForEach(item =>
                {
                    if (item.cTypePk == 5)
                    {
                        text = item.cName + " - " + NinjaUtil.numberTostring(item.cHP.ToString());

                        mFont.number_red.drawString(g, text, x, y, mFont.LEFT);
                    }
                    else if (item.clanID == Char.myCharz().clanID)
                    {
                        text = item.cName + " - " + NinjaUtil.numberTostring(item.cHP.ToString());
                        mFont.tahoma_7b_yellowSmall2.drawString(g, text, x, y, mFont.LEFT);
                    }
                    else
                    {
                        text = item.cName + " - " + NinjaUtil.numberTostring(item.cHP.ToString());
                        mFont.tahoma_7_whiteSmall.drawString(g, text, x, y, mFont.LEFT);
                    }

                    y += 12;

                });
                for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                {
                    Char c = GameScr.vCharInMap.elementAt(i) as Char;
                    if (c.cTypePk == 5)
                    {

                        if (GameCanvas.gameTick % 2 == 0) g.setColor(Color.red); else g.setColor(Color.yellow);


                        g.drawLine2(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy + Char.myCharz().ch / 2 - 10, c.cx - GameScr.cmx, c.cy - GameScr.cmy + Char.myCharz().ch / 2 - 10);

                    }
                }

            }


        }


      
        public static long timeFixMapError;
        public static void Update()
            {
                AutoItem.update();
                //AutoPoint.Update();
                //Char.updatePetPean();
                AutoNhat.update();
                LoadMap.update();
            if (isAk && GameCanvas.gameTick % 5 == 0)
            {
                AutoAk();
            }
            if (timeFixMapError < mSystem.currentTimeMillis())
            {
                if (Char.myCharz().cy < 0)
                {
                    Char.myCharz().cy = 1;
                    Service.gI().charMove();
                }
                bool check = checkMapError(Char.myCharz().cx);
                if (!check)
                {
                    int groundy = getYGround(Char.myCharz().cx);
                    if (groundy != -1 && Char.myCharz().cy > groundy)
                    {
                        Char.myCharz().cy = groundy;
                        Service.gI().charMove();
                    }
                }

                timeFixMapError = mSystem.currentTimeMillis() + 5000;
            }
            if (isAutoHoiSinh && Char.myCharz().meDead && GameCanvas.gameTick % 5 == 0)
            {
                Service.gI().wakeUpFromDead();
            }
        }
        public static bool checkMapError(int x)
        {
            int y = 50;
            bool isOk = false;
            for (int i = 0; i < 80; i++)
            {
                y += 24;
                if (TileMap.tileTypeAt(x, y, 2))
                {
                    if (y % 24 != 0) y -= y % 24;
                    if (y >= Char.myCharz().cy)
                    {
                        isOk = true;
                        break;
                    }

                }
            }
            return isOk;
        }
        public static int getYGround(int x)
        {
            int y = 50;
            for (int i = 0; i < 30; i++)
            {
                y += 24;
                if (TileMap.tileTypeAt(x, y, 2))
                {
                    if (y % 24 != 0) y -= y % 24;
                    return y;
                }
            }
            return -1;
        }
            private static void drawString(mGraphics g,string st, int x, int y)
            {
                mFont.tahoma_7_green2.drawString(g,st, x, y,mFont.LEFT);
            }
            private static void PaintInfoMod(mGraphics g)
            {
                int cmdX = 85;
                int cmdY = 35;
                if (ZamasuMain.HiddenInfoItem)
                {
                    drawString(g, "Thông Tin Sao Pha Lê: Bật", cmdX ,cmdY );
                }
               
                if (isAk)
                { cmdY += 10;
                    drawString(g, "Tự Động Đánh: Bật", cmdX, cmdY);
                }
              
                if (isAutoHoiSinh)
                {  cmdY += 10;
                    drawString(g, "Hồi Sinh: Bật", cmdX, cmdY);
                }
               
                if (iscdt)
                { cmdY += 10;
                    drawString(g, "Cho Đậu Đệ: Bật", cmdX, cmdY);
                }
              
                if (isPaintboss)
                {  cmdY += 10;
                    drawString(g, "Thông Báo Boss: Bật", cmdX, cmdY);
                }
           
                if (isPaintChar)
                {     cmdY += 10;
                    drawString(g, "Thông Tin Người Chơi: Bật", cmdX, cmdY);
                }
            }
        public static bool chat(string chat)
        {
                if(chat == "vspl")
                {
                    ZamasuMain.HiddenInfoItem = !HiddenInfoItem;
                    GameScr.info1.addInfo("Đã " + (ZamasuMain.HiddenInfoItem ? "bật" : "tắt") + " hiển thị info sao pha lê.", 0);
                }
            if (chat == "vak")
            {
                isAk = !isAk;
                GameScr.info1.addInfo("Đã " + (isAk ? "bật" : "tắt") + " tự động đánh.", 0);
                return true;
            }
            else
            if (chat == "vhs")
            {
                isAutoHoiSinh = !isAutoHoiSinh;
                GameScr.info1.addInfo("Đã " + (isAutoHoiSinh ? "bật" : "tắt") + " tự động hồi sinh.", 0);
                return true;
            }
            else
            if (chat == "vcd")
            {
                iscdt = !iscdt;
                GameScr.info1.addInfo("Đã " + (iscdt ? "bật" : "tắt") + " tự động cho đệ tử đậu thần.", 0);
                return true;
            }
            else if (chat == "vtbb")
            {
                isPaintboss = !isPaintboss;
                GameScr.info1.addInfo("Đã " + (isPaintboss ? "bật" : "tắt") + " hiển thị boss.", 0);
                return true;
            }

            else if (chat == "vinfo")
            {
                isPaintChar = !isPaintChar;
                GameScr.info1.addInfo("Đã " + (isPaintChar ? "bật" : "tắt") + " hiển thị thông tin ngươi chơi.", 0);
                return true;
            }
            else
                if (chat.Contains("vtocdo "))
            {
                bool isok = float.TryParse(chat.Replace("vtocdo ", ""), out float zzz);
                if (isok)
                {
                    if (zzz > 5 || zzz < 1)
                    {
                        GameScr.info1.addInfo("Tốc độ chỉ từ 1 đến 5", 0);
                        return true;
                    }
                    Time.timeScale = zzz;
                }
                return true;
            }
            else if (chat.Contains("vk "))
            {
                bool isok = int.TryParse(chat.Replace("vk ", ""), out int zzz);
                if (isok)
                {
                    if (TileMap.zoneID != zzz)
                    {
                        Service.gI().requestChangeZone(zzz, -1);
                        InfoDlg.showWait();
                    }
                    else
                    {
                        GameScr.info1.addInfo(mResources.ZONE_HERE, 0);
                    }
                }
                return true;
            }
          
            return false;
        }

    }
}
}
