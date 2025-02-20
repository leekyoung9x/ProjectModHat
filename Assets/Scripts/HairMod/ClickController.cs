using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace HairMod
{
   public class ClickController
    {
        public static Image btnClick1 = GameCanvas.loadImage("/mainImage/setclick1.png");
        public static Image btnClick2 = GameCanvas.loadImage("/mainImage/setclick1.png");
        public static int selected;
        public static bool isClick = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) ? true : false;
        public static void paintClick(mGraphics g, int x, int y, string clickCaption)
        {
            if (isClick)
            {
                if (!GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null)
                {

                    g.drawImage(btnClick1, x, y);
                    mFont.tahoma_7b_dark.drawString(g, clickCaption, x + (int)9.5, y + 4, 0);
                }
            }
        }
      
    }
}
