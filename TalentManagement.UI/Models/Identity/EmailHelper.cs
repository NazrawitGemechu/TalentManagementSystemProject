//using System.Net.Mail;

//namespace TalentManagement.UI.Models.Identity
//{
//    public class EmailHelper
//    {
//        public bool SendEmail(string userEmail, string confirmationLink)
//        {
//            MailMessage mailMessage = new MailMessage();
//            mailMessage.From = new MailAddress("nazrawitgemechu@protonmail.com");
//            mailMessage.To.Add(new MailAddress(userEmail));

//            mailMessage.Subject = "Confirm your email";
//            mailMessage.IsBodyHtml = true;
//            mailMessage.Body = confirmationLink;

//            SmtpClient client = new SmtpClient();
//            client.Credentials = new System.Net.NetworkCredential("nazrawitgemechu@protonmail.com", "0924339706nG!!!");
//            client.Host = "smtpout.secureserver.net";
//            client.Port = 80;

//            try
//            {
//                client.Send(mailMessage);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                // log exception
//            }
//            return false;
//        }
//    }
//}
