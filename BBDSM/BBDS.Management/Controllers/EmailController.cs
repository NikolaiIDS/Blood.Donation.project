using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mail;

namespace BBDS.Management.Controllers
{
    public class EmailController
    {
        //Global variables
        string mailBody = "<!DOCTYPE html>" +
                                 "<html>" +
                                     "<body>" +
                                     "<h1>That's just a testing email.</h1>" +
                                     "</body>" +
                                 "</html>";
        string fromEmail = "bbdsmanagement@gmail.com";
        string mailTitle = "Emergency";
        string mailSubject = "Blood request";
        string mailPassword = "Aa!123456";

        [HttpPost]
        public IActionResult SendEmail(string toEnail)//,IFormFile attachment) ---> in case we want to add attachment
        {
            //Attachment
            //message.Attachments.Add(new Attachment());

            //Mail message
            MailMessage message = new MailMessage(new MailAddress(fromEmail,mailTitle),new MailAddress(toEnail));

            //Mail content
            message.Subject = mailSubject;
            message.Body= mailBody;
            message.IsBodyHtml= true;

            //Server details
            SmtpClient smtp = new SmtpClient(); 
            smtp.Host= "smtp.gmail.com";
            smtp.Port = 578;
            smtp.EnableSsl= true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //Credentials
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            credential.UserName= fromEmail;
            credential.Password= mailPassword;
            smtp.UseDefaultCredentials= false;   
            smtp.Credentials = credential;

            //Send email
            smtp.Send(message);

            return View();
        }
    }
}
