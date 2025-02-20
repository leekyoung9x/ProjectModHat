using System;

namespace Functions.HandlerFunctions
{
	// Token: 0x020000C8 RID: 200
	internal class ChatableHandler : IActionListener, IChatable
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x000B0434 File Offset: 0x000AE634
		public static ChatableHandler gI()
		{
			bool flag = ChatableHandler._Instance == null;
			if (flag)
			{
				ChatableHandler._Instance = new ChatableHandler();
			}
			return ChatableHandler._Instance;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000B0461 File Offset: 0x000AE661
		private static void ResetTF()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000B0492 File Offset: 0x000AE692
		public void onCancelChat()
		{
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000B0498 File Offset: 0x000AE698
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
					ChatableHandler.ResetTF();
				}
				
			}
			ChatableHandler.ResetTF();
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000B0574 File Offset: 0x000AE774
		public void OpenChat(string text)
		{
			ChatTextField.gI().strChat = text;
			ChatTextField.gI().tfChat.name = mResources.CHAT;
			GameCanvas.panel.isShow = false;
			ChatTextField.gI().startChat2(ChatableHandler.gI(), string.Empty);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000B05C1 File Offset: 0x000AE7C1
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000B05C4 File Offset: 0x000AE7C4
		public static void Update()
		{
			try
			{
				//HintCommand.gI.update();
			}
			catch (Exception ex)
			{
				Cout.Log(ex.Message);
			}
			try
			{
				ChatableHandler.AutoChat();
			}
			catch (Exception ex2)
			{
				Cout.Log(ex2.Message);
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000B0634 File Offset: 0x000AE834
		public static bool chat(string text)
		{
			bool flag = InfoHandler.IsGetInfoChat<string>(text, "/atc|");
			bool result;
			if (flag)
			{
				ChatableHandler.stringAutoChat = InfoHandler.GetInfoChat<string>(text, "/atc|");
				bool flag2 = ChatableHandler.stringAutoChat == "";
				if (flag2)
				{
					GameScr.info1.addInfo("Chưa nhập nội dung chat", 0);
				}
				else
				{
					ChatableHandler.enableAutoChat = !ChatableHandler.enableAutoChat;
					GameScr.info1.addInfo("Tự động chat: " + InfoHandler.Status(ChatableHandler.enableAutoChat), 0);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000B06C8 File Offset: 0x000AE8C8
		public static void AutoChat()
		{
			bool flag = !ChatableHandler.enableAutoChat;
			if (!flag)
			{
				bool flag2 = mSystem.currentTimeMillis() - ChatableHandler.TIME_DELAY_AUTO_CHAT > 5000L;
				if (flag2)
				{
					Service.gI().chat(ChatableHandler.stringAutoChat);
					ChatableHandler.TIME_DELAY_AUTO_CHAT = mSystem.currentTimeMillis();
				}
			}
		}

		// Token: 0x040013AC RID: 5036
		private static ChatableHandler _Instance;

		// Token: 0x040013AD RID: 5037
		public static bool enableAutoChat;

		// Token: 0x040013AE RID: 5038
		public static string stringAutoChat;

		// Token: 0x040013AF RID: 5039
		public static long TIME_DELAY_AUTO_CHAT;
	}
}
