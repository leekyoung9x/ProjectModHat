using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HairMod
{

    class AutoPoint : IActionListener, IChatable
	{
		private static AutoPoint _Instance;

		public static int typePotential;

		public static bool isAutoPoint;

		public static int damageToAuto;

		public static int hpToAuto;

		public static int mpToAuto;

		public static string[] inputDamageAuto = new string[2] { "Nhập Sức Đánh Mà Bạn Muốn Auto", "Sức Đánh" };

		public static string[] inputHPAuto = new string[2] { "Nhập HP Mà Bạn Muốn Auto", "HP" };

		public static string[] inputMPAuto = new string[2] { "Nhập MP Mà Bạn Muốn Auto", "MP" };

		public static string[] inputPointToAdd = new string[2] { "Nhập Chỉ Số Mà Bạn Muốn Cộng Thêm", "Chỉ Số" };

		public static string[] inputPointAddTo = new string[2] { "Nhập Chỉ Số Mà Bạn Muốn Cộng Tới", "Chỉ Số" };


		public static AutoPoint gI()
		{
			if (_Instance == null)
				_Instance = new AutoPoint();
			return _Instance;
		}

        public static void DoIt()
        {
            // Tăng Damage (Sức đánh)
            if (Char.myCharz().cDamGoc < damageToAuto && Char.myCharz().cTiemNang > Char.myCharz().cDamGoc * 100 && GameCanvas.gameTick % 10 == 0)
            {
                long diemTiemNang = Char.myCharz().cTiemNang;
                long damageHienTai = Char.myCharz().cDamGoc;
                int damageMucTieu = damageToAuto;

                int soLanCong = 0;

                // Tính số lần có thể cộng mà không vượt damageToAuto
                while (diemTiemNang >= (damageHienTai * 100) && damageHienTai < damageMucTieu)
                {
                    diemTiemNang -= (damageHienTai * 100);
                    damageHienTai++;
                    soLanCong++;
                }

                // Điều chỉnh số lần cộng theo quy tắc 1, 10, 100
                if (soLanCong >= 100)
                    soLanCong = 100;
                else if (soLanCong >= 10)
                    soLanCong = 10;
                else if (soLanCong > 0)
                    soLanCong = 1;

                if (soLanCong > 0)
                {
                    Service.gI().upPotential(2, soLanCong);
                }
            }
            // Tăng HP
            else if (Char.myCharz().cHPGoc < hpToAuto && Char.myCharz().cTiemNang > Char.myCharz().cHPGoc + 1000 && GameCanvas.gameTick % 10 == 0)
            {
                (int soLanCoTheCong, long mauSauCong, long diemConLai) = TinhSoLanCong(Char.myCharz().cHPGoc, Char.myCharz().cTiemNang, false);
                (int soLanMuonCong, long diemCan) = TinhDiemTiemNangCan(Char.myCharz().cHPGoc, hpToAuto, false);

                // Chọn số lần cộng tối đa có thể nhưng không vượt quá hpToAuto
                long soLanCong = (soLanCoTheCong < soLanMuonCong) ? soLanCoTheCong : soLanMuonCong;

                // Giới hạn số lần cộng để không vượt quá hpToAuto
                if (Char.myCharz().cHPGoc + (soLanCong * 20) > hpToAuto)
                {
                    soLanCong = (hpToAuto - Char.myCharz().cHPGoc) / 20;
                }

                // Điều chỉnh số lần cộng theo quy tắc 1, 10, 100
                if (soLanCong >= 100)
                    soLanCong = 100;
                else if (soLanCong >= 10)
                    soLanCong = 10;
                else
                    soLanCong = 1;

                if (soLanCong > 0)
                {
                    Service.gI().upPotential(0, (int)soLanCong);
                }
            }

            // Tăng MP
            else if (Char.myCharz().cMPGoc < mpToAuto)
            {
                if (Char.myCharz().cTiemNang > Char.myCharz().cMPGoc + 1100 && GameCanvas.gameTick % 10 == 0)
                {
                    (int soLanCoTheCong, long mpSauCong, long diemConLai) = TinhSoLanCong(Char.myCharz().cMPGoc, Char.myCharz().cTiemNang, true);
                    (int soLanMuonCong, long diemCan) = TinhDiemTiemNangCan(Char.myCharz().cMPGoc, mpToAuto, true);

                    long soLanCong = (soLanCoTheCong < soLanMuonCong) ? soLanCoTheCong : soLanMuonCong;

                    // Giới hạn số lần cộng để không vượt quá hpToAuto
                    if (Char.myCharz().cMPGoc + (soLanCong * 20) > mpToAuto)
                    {
                        soLanCong = (mpToAuto - Char.myCharz().cMPGoc) / 20;
                    }

                    // Điều chỉnh số lần cộng theo quy tắc 1, 10, 100
                    if (soLanCong >= 100)
                        soLanCong = 100;
                    else if (soLanCong >= 10)
                        soLanCong = 10;
                    else
                        soLanCong = 1;

                    if (soLanCong > 0)
                    {
                        // Giới hạn số lần cộng: 1 -> 10 -> 100
                        soLanCong = (soLanCong > 100) ? 100 : (soLanCong > 10 ? 10 : 1);

                        Service.gI().upPotential(1, (int)soLanCong);
                    }
                }
            }
        }

        /// <summary>
        /// Tính số lần có thể cộng dựa trên điểm tiềm năng hiện tại.
        /// </summary>
        private static (int soLanCong, long mauSauCong, long diemConLai) TinhSoLanCong(long mauHienTai, long diemTiemNang, bool isMP)
        {
            int tangMau = 20; // Mỗi lần cộng thêm 20 máu hoặc MP
            int soLanCong = 0;

            while (diemTiemNang >= ((isMP ? 1100 : 1000) + mauHienTai))
            {
                long diemCan = (isMP ? 1100 : 1000) + mauHienTai;
                diemTiemNang -= diemCan;
                mauHienTai += tangMau;
                soLanCong++;
            }

            return (soLanCong, mauHienTai, diemTiemNang);
        }

        /// <summary>
        /// Tính số lần cần cộng và điểm tiềm năng cần có để đạt được mức máu/MP mục tiêu.
        /// </summary>
        static (int soLanCong, long diemCan) TinhDiemTiemNangCan(long mauHienTai, long mauMucTieu, bool isMP)
        {
            int tangMau = 20; // Mỗi lần cộng thêm 20 máu hoặc MP
            int soLanCong = 0;
            long diemCan = 0;

            while (mauHienTai < mauMucTieu)
            {
                long diemCanChoLanNay = (isMP ? 1100 : 1000) + mauHienTai;
                diemCan += diemCanChoLanNay;
                mauHienTai += tangMau;
                soLanCong++;
            }

            return (soLanCong, diemCan);
        }

        public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(inputPointToAdd[0]))
				{
					try
					{
						int num = int.Parse(ChatTextField.gI().tfChat.getText());
						if ((typePotential == 0 || typePotential == 1) && num % 20 != 0)
						{
							GameScr.info1.addInfo("Chỉ Số HP, MP Phải chia hết cho 20. Vui Lòng Nhập Lại!", 0);
							return;
						}
						if (typePotential == 0 || typePotential == 1)
							num /= 20;
						Service.gI().upPotential(typePotential, num);
						GameScr.info1.addInfo("Đã Cộng Xong!", 0);
					}
					catch
					{
						GameScr.info1.addInfo("Chỉ Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					ResetTF();
				}
				else if (ChatTextField.gI().strChat.Equals(inputPointAddTo[0]))
				{
					try
					{
						long num2 = int.Parse(ChatTextField.gI().tfChat.getText());
						if ((typePotential == 0 || typePotential == 1) && num2 % 20 != 0)
						{
							GameScr.info1.addInfo("Chỉ Số HP, MP Phải chia hết cho 20. Vui Lòng Nhập Lại!", 0);
							return;
						}
						if (typePotential == 0 || typePotential == 1)
							num2 /= 20;
						long num3 = Char.myCharz().cHPGoc / 20;
						if (typePotential == 1)
							num3 = Char.myCharz().cMPGoc / 20;
						if (typePotential == 2)
							num3 = Char.myCharz().cDamGoc;
						if (typePotential == 3)
							num3 = Char.myCharz().cDefGoc;
						if (typePotential == 4)
							num3 = Char.myCharz().cCriticalGoc;
						if (num2 <= num3)
						{
							GameScr.info1.addInfo("Chỉ Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
							return;
						}
						Service.gI().upPotential(typePotential, (int)(num2 - num3));
						GameScr.info1.addInfo("Đã Cộng Xong!", 0);
					}
					catch
					{
						GameScr.info1.addInfo("Chỉ Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					ResetTF();
				}
				else if (ChatTextField.gI().strChat.Equals(inputDamageAuto[0]))
				{
					try
					{
						int num4 = (damageToAuto = int.Parse(ChatTextField.gI().tfChat.getText()));
						GameScr.info1.addInfo("Auto Cộng Sức Đánh: " + NinjaUtil.getMoneys(num4), 0);
					}
					catch
					{
						GameScr.info1.addInfo("Sức Đánh Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					ResetTF();
				}
				else if (ChatTextField.gI().strChat.Equals(inputHPAuto[0]))
				{
					try
					{
						int num5 = (hpToAuto = int.Parse(ChatTextField.gI().tfChat.getText()));
						GameScr.info1.addInfo("Auto Cộng HP: " + NinjaUtil.getMoneys(num5), 0);
					}
					catch
					{
						GameScr.info1.addInfo("HP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					ResetTF();
				}
				else if (ChatTextField.gI().strChat.Equals(inputMPAuto[0]))
				{
					try
					{
						int num6 = (mpToAuto = int.Parse(ChatTextField.gI().tfChat.getText()));
						GameScr.info1.addInfo("Auto Cộng MP: " + NinjaUtil.getMoneys(num6), 0);
					}
					catch
					{
						GameScr.info1.addInfo("MP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					ResetTF();
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
				ResetTF();
			}
		}
		private static void ResetTF()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}
		public void onCancelChat()
		{
		}

		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
				case 1:
					ShowMenuAutoPoint();
					break;
				case 2:
					ShowMenuAutoPointFast();
					break;
				case 3:
					isAutoPoint = !isAutoPoint;
					GameScr.info1.addInfo("Auto\n" + (isAutoPoint ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
					break;
				case 4:
					ChatTextField.gI().strChat = inputDamageAuto[0];
					ChatTextField.gI().tfChat.name = inputDamageAuto[1];
					ChatTextField.gI().startChat2(gI(), string.Empty);
					break;
				case 5:
					ChatTextField.gI().strChat = inputHPAuto[0];
					ChatTextField.gI().tfChat.name = inputHPAuto[1];
					ChatTextField.gI().startChat2(gI(), string.Empty);
					break;
				case 6:
					ChatTextField.gI().strChat = inputMPAuto[0];
					ChatTextField.gI().tfChat.name = inputMPAuto[1];
					ChatTextField.gI().startChat2(gI(), string.Empty);
					break;
				case 7:
					ShowMenuAddPoint(0);
					break;
				case 8:
					ShowMenuAddPoint(1);
					break;
				case 9:
					ShowMenuAddPoint(2);
					break;
				case 10:
					ShowMenuAddPoint(3);
					break;
				case 11:
					ShowMenuAddPoint(4);
					break;
				case 12:
					typePotential = (int)p;
					GameCanvas.panel.isShow = false;
					ChatTextField.gI().strChat = inputPointToAdd[0];
					ChatTextField.gI().tfChat.name = inputPointToAdd[1];
					ChatTextField.gI().startChat2(gI(), string.Empty);
					break;
				case 13:
					typePotential = (int)p;
					GameCanvas.panel.isShow = false;
					ChatTextField.gI().strChat = inputPointAddTo[0];
					ChatTextField.gI().tfChat.name = inputPointAddTo[1];
					ChatTextField.gI().startChat2(gI(), string.Empty);
					break;
			}
		}

		public static void ShowMenu()
		{
			
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto\nCộng\nChỉ Số", gI(), 1, null));
			myVector.addElement(new Command("Cộng\nChỉ Số\nNhanh", gI(), 2, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuAutoPoint()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto\n" + (isAutoPoint ? "[STATUS: ON]" : "[STATUS: OFF]"), gI(), 3, null));
			myVector.addElement(new Command("Sức Đánh\n[" + NinjaUtil.getMoneys(damageToAuto) + "]", gI(), 4, null));
			myVector.addElement(new Command("HP\n[" + NinjaUtil.getMoneys(hpToAuto) + "]", gI(), 5, null));
			myVector.addElement(new Command("MP\n[" + NinjaUtil.getMoneys(mpToAuto) + "]", gI(), 6, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuAutoPointFast()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("HP", gI(), 7, null));
			myVector.addElement(new Command("MP", gI(), 8, null));
			myVector.addElement(new Command("Sức Đánh", gI(), 9, null));
			myVector.addElement(new Command("Giáp", gI(), 10, null));
			myVector.addElement(new Command("Chí Mạng", gI(), 11, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuAddPoint(int typePotential)
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Cộng", gI(), 12, typePotential));
			myVector.addElement(new Command("Cộng\nTới Mức", gI(), 13, typePotential));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void update()
		{
			if (isAutoPoint)
				DoIt();
		}
	}
}
