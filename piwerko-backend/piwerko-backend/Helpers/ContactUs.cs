using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Piwerko.Api.Helpers
{
    public class ContactUs
    {
        public string username { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public bool sendEmail()
        {
            try
            {

                using (SmtpClient client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "piwerkobuissnes@gmail.com",
                        Password = "zaq1@WSX"
                    };
                    client.Credentials = credential;

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;

                    var message = new MailMessage();

                    message.To.Add(new MailAddress("piwerko.business@gmail.com"));
                    message.From = new MailAddress("piwerkobuissnes@gmail.com");
                    message.Subject = this.username + ": " + this.subject;
                    message.Body = this.username + "napisal:<br>"+this.subject+"<br>"+this.body+"<br> <br>";
                    message.Body += "Odpisz mu na: " + this.email;
                    message.IsBodyHtml = true;
                    /*
                    if (FileUpload1.HasFile)
                    {
                        string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        message.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, FileName));
                    }
                    */

                    client.Send(message);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
