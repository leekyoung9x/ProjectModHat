using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Functions.HandlerFunctions
{
   internal class InfoHandler
{
		public static string Status(bool Bool)
		{
			return Bool ? "Bật" : "Tắt";
		}
		public static void paint(mFont ForwardFont, mGraphics g, string Text, int x, int y, int align, mFont BackgroundFont, string type, int ZoomLevel)
		{
			if (ZoomLevel != 1)
			{
				if (ZoomLevel == 2)
				{
					if (!(type == "border"))
					{
						if (!(type == "noborder"))
						{
							if (type == "underline")
							{
								ForwardFont.drawString(g, Text, x, y, align, BackgroundFont);
							}
						}
						else
						{
							ForwardFont.drawString(g, Text, x, y, align);
						}
					}
					else
					{
						ForwardFont.drawStringBd(g, Text, x, y, align, BackgroundFont);
					}
				}
			}
			else if (!(type == "border"))
			{
				if (!(type == "noborder"))
				{
					if (type == "underline")
					{
						ForwardFont.drawString(g, Text, x, y, align, BackgroundFont);
					}
				}
				else
				{
					ForwardFont.drawString(g, Text, x, y, align);
				}
			}
			else
			{
				InfoHandler.drawStringBd(ForwardFont, g, Text, x, y, align, BackgroundFont);
			}
		}
		public static void drawStringBd(mFont font1, mGraphics g, string text, int x, int y, int align, mFont font2)
		{
			font2.drawString(g, text, x - 1, y - 1, align);
			font2.drawString(g, text, x - 1, y + 1, align);
			font2.drawString(g, text, x + 1, y - 1, align);
			font2.drawString(g, text, x + 1, y + 1, align);
			font2.drawString(g, text, x, y - 1, align);
			font2.drawString(g, text, x, y + 1, align);
			font2.drawString(g, text, x - 1, y, align);
			font2.drawString(g, text, x + 1, y, align);
			font1.drawString(g, text, x, y, align);
		}
		// Token: 0x06000AD1 RID: 2769 RVA: 0x000B1688 File Offset: 0x000AF888
		public static string StatusMenu(bool Bool)
		{
			return Bool ? "Đang Bật" : "Đang Tắt";
		}
		public static bool IsGetInfoChat<T>(string text, string s)
		{
			bool flag = !text.StartsWith(s);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					Convert.ChangeType(text.Substring(s.Length), typeof(T));
				}
				catch
				{
					return false;
				}
				result = true;
			}
			return result;
		}
		public static T GetInfoChat<T>(string text, string s)
		{
			return (T)((object)Convert.ChangeType(text.Substring(s.Length), typeof(T)));
		}
	}
}
