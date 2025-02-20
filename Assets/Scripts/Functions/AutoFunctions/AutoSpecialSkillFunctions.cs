using System;
using System.Threading;

namespace CilentModify
{
	// Token: 0x020000CF RID: 207
	internal class AutoSpecialSkillFunctions : IActionListener, IChatable
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x000ADC0C File Offset: 0x000ABE0C
		public static AutoSpecialSkillFunctions gI()
		{
			bool flag = AutoSpecialSkillFunctions.instance != null;
			AutoSpecialSkillFunctions result;
			if (flag)
			{
				result = AutoSpecialSkillFunctions.instance;
			}
			else
			{
				result = (AutoSpecialSkillFunctions.instance = new AutoSpecialSkillFunctions());
			}
			return result;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000ADC44 File Offset: 0x000ABE44
		public void open()
		{
			bool flag = global::Char.myCharz().cPower < 10000000000L;
			if (flag)
			{
				GameScr.info1.addInfo("Cần 10 tỉ sức mạnh để mở.", 0);
			}
			else
			{
				bool flag2 = AutoSpecialSkillFunctions.openMax && AutoSpecialSkillFunctions.max == -1;
				if (flag2)
				{
					this.isnoitai = false;
					AutoSpecialSkillFunctions.openMax = false;
				}
				else
				{
					for (; ; )
					{
						bool flag3 = global::Char.myCharz().luong < 1000;
						if (flag3)
						{
							bool flag4 = this.cgender == 0;
							if (flag4)
							{
								this.isnoitai = false;
								Service.gI().openMenu(0);
								Service.gI().confirmMenu(0, 2);
								this.isnoitai = true;
							}
							bool flag5 = this.cgender == 1;
							if (flag5)
							{
								this.isnoitai = false;
								Service.gI().openMenu(2);
								Service.gI().confirmMenu(2, 2);
								this.isnoitai = true;
							}
							bool flag6 = this.cgender == 3;
							if (flag6)
							{
								this.isnoitai = false;
								Service.gI().openMenu(1);
								Service.gI().confirmMenu(1, 2);
								this.isnoitai = true;
							}
						}
						Service.gI().speacialSkill(0);
						bool flag7 = Panel.specialInfo.Contains(AutoSpecialSkillFunctions.tennoitaicanmo);
						if (flag7)
						{
							bool flag8 = !AutoSpecialSkillFunctions.openMax;
							if (flag8)
							{
								break;
							}
							int num = Panel.specialInfo.IndexOf("%");
							int num2 = Panel.specialInfo.Substring(0, num).LastIndexOf(' ');
							bool flag9 = int.Parse(this.CutString(num2 + 1, num - 1, Panel.specialInfo)) == AutoSpecialSkillFunctions.max;
							if (flag9)
							{
								goto Block_10;
							}
						}
						Thread.Sleep(200);
						Service.gI().confirmMenu(5, AutoSpecialSkillFunctions.type);
						Thread.Sleep(200);
						Service.gI().confirmMenu(5, 0);
						Thread.Sleep(400);
						if (!this.isnoitai)
						{
							goto Block_11;
						}
					}
					this.isnoitai = false;
					GameScr.info1.addInfo("Xong", 0);
					return;
					Block_10:
					this.isnoitai = false;
					AutoSpecialSkillFunctions.openMax = false;
					GameScr.info1.addInfo("Xong", 0);
					Block_11:;
				}
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000ADE80 File Offset: 0x000AC080
		public string CutString(int start, int end, string s)
		{
			string text = "";
			for (int i = start; i <= end; i++)
			{
				text += s[i].ToString();
			}
			return text;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000ADEC8 File Offset: 0x000AC0C8
		public void perform(int idAction, object p)
		{
			bool flag = idAction != 1;
			if (flag)
			{
				bool flag2 = idAction == 2;
				if (flag2)
				{
					string text = (string)p;
					int length = text.Substring(0, text.IndexOf('%')).LastIndexOf(' ');
					AutoSpecialSkillFunctions.tennoitaicanmo = text.Substring(0, length);
					this.isnoitai = true;
					AutoSpecialSkillFunctions.type = (sbyte)idAction;
					GameCanvas.panel.hide();
					new Thread(new ThreadStart(this.open)).Start();
				}
			}
			else
			{
				string text2 = (string)p;
				int length2 = text2.Substring(0, text2.IndexOf('%')).LastIndexOf(' ');
				AutoSpecialSkillFunctions.tennoitaicanmo = text2.Substring(0, length2);
				this.isnoitai = true;
				AutoSpecialSkillFunctions.type = (sbyte)idAction;
				GameCanvas.panel.hide();
				new Thread(new ThreadStart(this.open)).Start();
			}
			bool flag3 = idAction == 3;
			if (flag3)
			{
				AutoSpecialSkillFunctions.openMax = false;
				MyVector myVector = new MyVector();
				myVector.addElement(new Command("Mở Vip", AutoSpecialSkillFunctions.gI(), 2, p));
				myVector.addElement(new Command("Mở Thường", AutoSpecialSkillFunctions.gI(), 1, p));
				GameCanvas.menu.startAt(myVector, 3);
			}
			bool flag4 = idAction == 4;
			if (flag4)
			{
				string text3 = (string)p;
				AutoSpecialSkillFunctions.openMax = true;
				int num = text3.IndexOf("đến ");
				int length3 = text3.Substring(num + 4).IndexOf("%");
				AutoSpecialSkillFunctions.max = int.Parse(text3.Substring(num + 4, length3));
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command("Mở Vip", AutoSpecialSkillFunctions.gI(), 2, p));
				myVector2.addElement(new Command("Mở Thường", AutoSpecialSkillFunctions.gI(), 1, p));
				GameCanvas.menu.startAt(myVector2, 3);
			}
			bool flag5 = idAction == 5;
			if (flag5)
			{
				MyVector myVector3 = new MyVector();
				for (int i = 1; i <= 8; i++)
				{
					myVector3.addElement(new Command(i.ToString() + " sao", this, 7, i));
				}
				GameCanvas.menu.startAt(myVector3, 3);
			}
			bool flag6 = idAction == 6;
			if (flag6)
			{
				Service.gI().combine(1, GameCanvas.panel.vItemCombine);
			}
			bool flag7 = idAction == 8;
			if (flag7)
			{
				string text4 = (string)p;
				int length4 = text4.Substring(0, text4.IndexOf('%')).LastIndexOf(' ');
				AutoSpecialSkillFunctions.tennoitaicanmo = text4.Substring(0, length4);
				int num2 = text4.IndexOf("%");
				int num3 = text4.IndexOf("đến ");
				int start = text4.Substring(0, num2).LastIndexOf(' ');
				int num4 = text4.LastIndexOf('%');
				AutoSpecialSkillFunctions.chiso[0] = int.Parse(this.CutString(start, num2 - 1, text4));
				AutoSpecialSkillFunctions.chiso[1] = int.Parse(this.CutString(num3 + 4, num4 - 1, text4));
				string str = this.CutString(start, num4, text4);
				AutoSpecialSkillFunctions.noitai = "Nhập chỉ số bạn muốn chọn trong khoảng " + str;
				MyVector myVector4 = new MyVector();
				myVector4.addElement(new Command("Mở Vip", AutoSpecialSkillFunctions.gI(), 9, 2));
				myVector4.addElement(new Command("Mở Thường", AutoSpecialSkillFunctions.gI(), 9, 1));
				GameCanvas.menu.startAt(myVector4, 3);
			}
			bool flag8 = idAction == 9;
			if (flag8)
			{
				AutoSpecialSkillFunctions.type = (sbyte)((int)p);
				AutoSpecialSkillFunctions.startChat(1, AutoSpecialSkillFunctions.noitai);
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000AE274 File Offset: 0x000AC474
		public void onChatFromMe(string text, string to)
		{
			bool flag = GameCanvas.panel.chatTField.strChat == AutoSpecialSkillFunctions.noitai;
			if (flag)
			{
				int num = -1;
				try
				{
					num = int.Parse(GameCanvas.panel.chatTField.tfChat.getText());
				}
				catch (Exception)
				{
					GameCanvas.startOKDlg(mResources.input_quantity_wrong);
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					return;
				}
				bool flag2 = num != -1 && num >= AutoSpecialSkillFunctions.chiso[0] && num <= AutoSpecialSkillFunctions.chiso[1];
				if (flag2)
				{
					AutoSpecialSkillFunctions.max = num;
					AutoSpecialSkillFunctions.openMax = true;
					AutoSpecialSkillFunctions.gI().isnoitai = true;
					GameCanvas.panel.hide();
					new Thread(new ThreadStart(AutoSpecialSkillFunctions.gI().open)).Start();
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				}
				else
				{
					GameCanvas.startOKDlg(AutoSpecialSkillFunctions.noitai);
				}
			}
			GameCanvas.panel.chatTField.parentScreen = GameCanvas.panel;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00006975 File Offset: 0x00004B75
		public void onCancelChat()
		{
			GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000AE3C0 File Offset: 0x000AC5C0
		public static void startChat(int type, string caption)
		{
			bool flag = GameCanvas.panel.chatTField == null;
			if (flag)
			{
				GameCanvas.panel.chatTField = new ChatTextField();
				GameCanvas.panel.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				GameCanvas.panel.chatTField.initChatTextField();
				GameCanvas.panel.chatTField.parentScreen = AutoSpecialSkillFunctions.gI();
			}
			GameCanvas.panel.chatTField.parentScreen = AutoSpecialSkillFunctions.gI();
			ChatTextField chatTField = GameCanvas.panel.chatTField;
			chatTField.strChat = caption;
			chatTField.tfChat.name = mResources.input_quantity;
			chatTField.to = string.Empty;
			chatTField.isShow = true;
			chatTField.tfChat.isFocus = true;
			chatTField.tfChat.setIputType(type);
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				GameCanvas.panel.chatTField.tfChat.doChangeToTextBox();
			}
			bool flag2 = !Main.isPC;
			if (flag2)
			{
				chatTField.startChat2(AutoSpecialSkillFunctions.gI(), string.Empty);
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000AE4E0 File Offset: 0x000AC6E0
		public void In4Me(string s)
		{
			bool flag = s.ToLower().Contains("bạn không đủ vàng");
			if (flag)
			{
				AutoSpecialSkillFunctions.gI().isnoitai = false;
				AutoSpecialSkillFunctions.openMax = false;
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000AE518 File Offset: 0x000AC718
		public static void AddChatPopup(string[] s)
		{
			bool flag = s.Length != 0 && s[s.Length - 1] != string.Empty;
			if (flag)
			{
				string text = s[s.Length - 1].ToLower();
				int num = text.IndexOf("cần ");
				int num2 = text.IndexOf("tr");
				bool flag2 = num != -1 && num2 != -1;
				if (flag2)
				{
					int.Parse(AutoSpecialSkillFunctions.gI().CutString(num + 3, num2 - 1, text).Trim());
				}
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000AE59C File Offset: 0x000AC79C
		public static void startMenu()
		{
			MyVector myVector = new MyVector();
		//	myVector.addElement(new Command("Thường", AutoSpecialSkillFunctions.gI(), 6, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000AE274 File Offset: 0x000AC474
		public void onChatFromMee(string text, string to)
		{
			bool flag = GameCanvas.panel.chatTField.strChat == AutoSpecialSkillFunctions.noitai;
			if (flag)
			{
				int num = -1;
				try
				{
					num = int.Parse(GameCanvas.panel.chatTField.tfChat.getText());
				}
				catch (Exception)
				{
					GameCanvas.startOKDlg(mResources.input_quantity_wrong);
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					return;
				}
				bool flag2 = num != -1 && num >= AutoSpecialSkillFunctions.chiso[0] && num <= AutoSpecialSkillFunctions.chiso[1];
				if (flag2)
				{
					AutoSpecialSkillFunctions.max = num;
					AutoSpecialSkillFunctions.openMax = true;
					AutoSpecialSkillFunctions.gI().isnoitai = true;
					GameCanvas.panel.hide();
					new Thread(new ThreadStart(AutoSpecialSkillFunctions.gI().open)).Start();
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				}
				else
				{
					GameCanvas.startOKDlg(AutoSpecialSkillFunctions.noitai);
				}
			}
			GameCanvas.panel.chatTField.parentScreen = GameCanvas.panel;
		}

		// Token: 0x040013B6 RID: 5046
		private int cgender = global::Char.myCharz().cgender;

		// Token: 0x040013B7 RID: 5047
		public static string noitai;

		// Token: 0x040013B8 RID: 5048
		public static string tennoitaicanmo;

		// Token: 0x040013B9 RID: 5049
		public static sbyte type = 1;

		// Token: 0x040013BA RID: 5050
		public static bool openMax = false;

		// Token: 0x040013BB RID: 5051
		public static int max = -1;

		// Token: 0x040013BC RID: 5052
		public static int[] chiso = new int[2];

		// Token: 0x040013BD RID: 5053
		private static AutoSpecialSkillFunctions instance;

		// Token: 0x040013BE RID: 5054
		public bool isnoitai;
	}
}
