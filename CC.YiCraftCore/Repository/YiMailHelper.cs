using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using CC.YiCraftCore.Models;



namespace CC.YiCraftCore.Repository
{

   public static class YiMailHelper
    {
        public static bool Mail(string Mail_To, string Mail_Body)
        {
            string Mail_From = "454313500@qq.com";
            string Mail_Subject = "YiCraftCore意界白名单审核验证码";
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Mail_From);
                mailMessage.To.Add(new MailAddress(Mail_To));
                mailMessage.Subject = Mail_Subject;
                mailMessage.Body = Mail_Body;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.qq.com";
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Mail_From, "wwairzyjnfzgcbdi");
                client.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        public static string GetRandomString(int length = 4, string custom = "")
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            str += "0123456789";
            str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
    }
}
