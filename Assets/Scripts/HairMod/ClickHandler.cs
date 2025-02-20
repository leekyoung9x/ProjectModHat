using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMod
{
    public class ClickHandler
    {
        private ArrayList a;
        public static int MenuSelected;
        public static int cmdX;
        public static int cmdY;
        public int size()
        {
            if (a == null)
            {
                return 0;
            }
            return a.Count;
        }
        public static void paint(mGraphics g)
        {
            if (ClickController.isClick)
            {
                if (!GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null)
                {
                    cmdX = GameCanvas.w;
                    cmdY = (mGraphics.zoomLevel > 2) ? -10 : 0;
                    ClickController.paintClick(g, cmdX - 30, cmdY + 115, "M");
                    if (GameCanvas.isPointerHoldIn(cmdX - 30, cmdY + 115, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 90;
                        g.drawImage((mScreen.keyTouch != 90) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX - 30, cmdY + 115);
                        Service.gI().openUIZone();
                        GameCanvas.clearAllPointerEvent();
                    }
                    cmdY += 30;
                    ClickController.paintClick(g, cmdX - 30, cmdY + 115, "C");

                    if (GameCanvas.isPointerHoldIn(cmdX - 30, cmdY + 115, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 91;
                        g.drawImage((mScreen.keyTouch != 91) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX - 30, cmdY + 115);
                        ZamasuMain.UseItem(193);
                        ZamasuMain.UseItem(194);
                        GameCanvas.clearAllPointerEvent();
                    }
                    cmdX -= 30;
                    ClickController.paintClick(g, cmdX - 30, cmdY + 115, "A");
                    if (GameCanvas.isPointerHoldIn(cmdX - 30, cmdY + 115, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 92;
                        g.drawImage((mScreen.keyTouch != 92) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX - 30, cmdY + 115);
                        ZamasuMain.aaMod.isAk = !ZamasuMain.aaMod.isAk;
                        GameScr.info1.addInfo("Đã " + (ZamasuMain.aaMod.isAk ? "bật" : "tắt") + " tự động đánh.", 0);
                        GameCanvas.clearAllPointerEvent();
                    }
                    cmdY -= 30;
                    ClickController.paintClick(g, cmdX - 30, cmdY + 115, "E");
                    if (GameCanvas.isPointerHoldIn(cmdX - 30, cmdY + 115, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 92;
                        g.drawImage((mScreen.keyTouch != 92) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX - 30, cmdY + 115);
                        ZamasuMain.aaMod.isAutoHoiSinh = !ZamasuMain.aaMod.isAutoHoiSinh;
                        GameScr.info1.addInfo("Đã " + (ZamasuMain.aaMod.isAutoHoiSinh ? "bật" : "tắt") + " tự động hồi sinh.", 0);

                        GameCanvas.clearAllPointerEvent();
                    }
                    cmdX = GameCanvas.w;
                    cmdY = 0;
                    ClickController.paintClick(g, (cmdX - 150), cmdY + 190, "F");
                    if (GameCanvas.isPointerHoldIn(cmdX - 150, cmdY + 190, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 93;
                        g.drawImage((mScreen.keyTouch != 93) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX - 150, cmdY + 190);
                        ZamasuMain.UseItem(454);
                        ZamasuMain.UseItem(921);
                        GameCanvas.clearAllPointerEvent();
                    }
                    cmdX += 30;
                    ClickController.paintClick(g, (cmdX - 150), cmdY + 190, "G");
                    if (GameCanvas.isPointerHoldIn(cmdX - 150, cmdY + 190, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 93;
                        g.drawImage((mScreen.keyTouch != 93) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX - 150, cmdY + 190);
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
                        GameCanvas.clearAllPointerEvent();
                    }
                    cmdX = 0;
                    cmdY = (mGraphics.zoomLevel > 2) ? -20 : 0;
                    ClickController.paintClick(g, cmdX + 20, cmdY + 170, "J");
                    if (GameCanvas.isPointerHoldIn(cmdX + 20, cmdY + 170, 25, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 93;
                        g.drawImage((mScreen.keyTouch != 93) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX + 20, cmdY + 150);
                        LoadMap.LoadMapLeft();
                        GameCanvas.clearAllPointerEvent();
                    }
                    ClickController.paintClick(g, cmdX + 50, cmdY + 170, "K");
                    if (GameCanvas.isPointerHoldIn(cmdX + 50, cmdY + 170, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 95;
                        g.drawImage((mScreen.keyTouch != 95) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX + 50, cmdY + 150);
                        LoadMap.LoadMapCenter();
                        GameCanvas.clearAllPointerEvent();
                    }

                    ClickController.paintClick(g, cmdX + 80, cmdY + 170, "L");
                    if (GameCanvas.isPointerHoldIn(cmdX + 80, cmdY + 170, 18, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        mScreen.keyTouch = 94;
                        g.drawImage((mScreen.keyTouch != 94) ? ClickController.btnClick1 : ClickController.btnClick2, cmdX + 80, cmdY + 150);
                        LoadMap.LoadMapRight();
                        GameCanvas.clearAllPointerEvent();
                    }
                }
            }
        }
    }
}
