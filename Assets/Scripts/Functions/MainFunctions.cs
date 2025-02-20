using Functions.AutoFunctions;
using Functions.HandlerFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
namespace Functions
{
   internal class MainFunctions : IChatable
    {
        static MainFunctions()
        {
            inputSpeed = new string[]{
                "Nhập Cheat:",
                "Cheat"
            };
            inputFPS = new string[] {"Nhập FPS:","FPS" };
            MainFunctions.listBosses = new List<BossFunctions>();
            isSanBoss = true;
        }
        public static void updateKey()
        {
            if (!Main.isPC)
            {
                return;
            }
            else
            {
                
                if (!ChatTextField.gI().isShow)
                {
                    switch (GameCanvas.keyAsciiPress)
                    {
                        case 'x':
                            MenuFunctions.ShowMenu();
                            break;
                        case 'a':
                            SkillFunctions.isAutoSendAttack = !SkillFunctions.isAutoSendAttack;
                            GameScr.info1.addInfo("|5|Tự Đánh: " + (SkillFunctions.isAutoSendAttack ? "ON" : "OFF"), 0);
                            break;
                        case 'f':
                            MenuFunctions.UseBongTai();
                            break;
                        case 'c':
                            MenuFunctions.UseCapsule();
                            break;
                        case 'm':
                            Service.gI().openUIZone();
                            break;
                        case 'e':
                            CharFunctions.getInstance().Handler();
                            GameScr.info1.addInfo("|5|Auto Hồi Sinh: " + (CharFunctions.getInstance().AutoRivive() ? "ON" : "OFF"), 0);
                            break;
                    }
                }
            }
        }
        

        public static void ResetTF()
        {
            ChatTextField.gI().strChat = "Chat";
            ChatTextField.gI().tfChat.name = "chat";
            ChatTextField.gI().isShow = false;
        }
        
        public static void Chat(string text)
        {
            ChatableHandler.chat(text);
            switch (text)
            {
                case "ak":
                    SkillFunctions.isAutoSendAttack = !SkillFunctions.isAutoSendAttack;
                    GameScr.info1.addInfo("|5|Tự Đánh: " + (SkillFunctions.isAutoSendAttack ? "ON" : "OFF"), 0);
                    break;
                case "dapdo":
                    AutoUpgradeFunctions.isDapDo = !AutoUpgradeFunctions.isDapDo;
                    new Thread(new ThreadStart(AutoUpgradeFunctions.AutoDapDo)).Start();
                    GameScr.info1.addInfo("|5|Đập Đồ: " + (AutoUpgradeFunctions.isDapDo ? "ON" : "OFF"), 0);
                    break;
                case "tdlt":
                    AutoPickMobHandler.IsTanSat = !AutoPickMobHandler.IsTanSat;
                    GameScr.info1.addInfo("|5|Tự Động Luyện Tập:\n" + (AutoPickMobHandler.IsTanSat ? "ON" : "OFF"), 0);
                    break;
                case "alogin":
                    CharFunctions.isAutoLogin = !CharFunctions.isAutoLogin;
                    GameScr.info1.addInfo("|5|Auto Login: " + (CharFunctions.isAutoLogin ? "ON" : "OFF"), 0);
                    break;
                case "ahs":
                    CharFunctions.getInstance().Handler();
                    GameScr.info1.addInfo("|5|Auto Hồi Sinh: " + (CharFunctions.getInstance().AutoRivive() ? "ON" : "OFF"), 0);
                    break;
                case "anhat":
                    AutoPickFunctions.isAutoPick = !AutoPickFunctions.isAutoPick;
                    GameScr.info1.addInfo("|5|Auto Nhặt: " + (AutoPickFunctions.isAutoPick ? "OFF" : "ON"), 0);
                    break;
                case "vq":
                    AutoCrackBallFunctions.startmenu = !AutoCrackBallFunctions.startmenu;
                    new Thread(new ThreadStart(AutoCrackBallFunctions.startMenu)).Start();
                    GameScr.info1.addInfo("Auto Quay Thượng Đế: " + (AutoCrackBallFunctions.startmenu ? "ON" : "OFF"), 0);
                    break;
                default:
                    if(text.StartsWith("k "))
                    {
                        int zoneId = int.Parse(text.Split(new char[]
            {
                ' '
            })[1]);
                        Service.gI().requestChangeZone(zoneId, -1);
                    }
                    break;
            }

        }
        private static bool isBuffPet()
        {
            return (Char.myPetz().cStamina < 50 || Char.myPetz().cMP < Char.myPetz().myskill.manaUse || Char.myPetz().cMP < 100 || Char.myPetz().cHP < 500) && !Char.myPetz().meDead;
        }
        public static void updateFunctions()
        {
            if (Char.isPetHandler && GameCanvas.gameTick % 50 == 0)
            {
                Service.gI().petInfo();
                if (isBuffPet())
                {
                    GameScr.gI().doUseHP(); 
                }
            }
            CharFunctions.getInstance().update();
            AutoUpgradeFunctions.Update();
            SkillFunctions.Update();
            PeanFunctions.Update();
            AutoPickMobHandler.Update();
            ItemHandler.Update();
            ChatableHandler.Update();
            ListCharFunctions.Update();
            AutoBuyFunctions.Update();
            AutoPickFunctions.update();
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
                bool flag2 = ChatTextField.gI().strChat.Equals("Nhập mã màu nền");
                if (flag2)
                {
                    GraphicsHandler.ColorRGB = int.Parse(ChatTextField.gI().tfChat.getText());
                    MainFunctions.ResetTF();
                }
               
            }

            MainFunctions.ResetTF();
        }

        public void onCancelChat()
        {
          
        }

        /// <summary>
        ///variable
        public static List<BossFunctions> listBosses;
        public static string[] inputSpeed;
        public static string[] inputFPS;
        public static bool isSanBoss;
        /// </summary>

    }
}
