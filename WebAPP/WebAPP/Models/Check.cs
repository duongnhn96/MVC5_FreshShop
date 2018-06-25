using System;
using System.Text.RegularExpressions;
namespace WebAPP.Models
{
    public class Check
    {
        public static bool isEmail(string inputEmail)

        {
            //kiểm tra email dạng user@hots.com
            //inputEmail = inputEmail ?? string.Empty;//

            string strRegex = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@+[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";

            Regex re = new Regex(strRegex);

            if (re.IsMatch(inputEmail))

                return (true);

            else

                return (false);

        }
        public static bool isMatKhau(string MatKhau)
        {
            string strRegex = "^[\\w]{6,32}$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(MatKhau)) return true;
            else return false;
        }
        public static bool isSdt(string Sdt)
        {
            string strRegex = "^[0-9]{10,11}$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(Sdt)) return true;
            else return false;
        }
        
    }
}