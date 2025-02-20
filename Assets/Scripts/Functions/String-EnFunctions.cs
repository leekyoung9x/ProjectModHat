using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assets.Script.Functions
{
	
	internal class String_EnFunctions : IStringProtect
    {
		public static String_EnFunctions getInstance()
		{
			bool flag = String_EnFunctions._Instance == null;
			if (flag)
			{
				String_EnFunctions._Instance = new String_EnFunctions();
			}
			return String_EnFunctions._Instance;
		}
		public static String_EnFunctions _Instance = new String_EnFunctions();
		public string Hex2String(string A_0, object A_1)
        {
			A_1 = 0;
			A_0 = Regex.Replace(A_0, "[^0-9A-Fa-f]", "");
			if (A_0.Length % 2 != Convert.ToInt32(A_1))
			{
				A_0 = A_0.Remove(A_0.Length - 1, 1);
			}
			if (A_0.Length <= 0)
			{
				return "";
			}
			byte[] array = new byte[A_0.Length / 2];
			for (int i = 0; i < A_0.Length; i += 2)
			{
				if (!byte.TryParse(A_0.Substring(i, 2), NumberStyles.HexNumber, null, out array[i / 2]))
				{
					array[i / 2] = 0;
				}
			}
			return (Encoding.Default.GetString(array));
		}

        public string ReverseString(string input)
        {
			char[] charArray = input.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
    }
}
