using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using BBDS.Management.Models;

namespace BBDS.Management.Controllers
{
    public class ContactUsController : Controller
    {
        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(EmailDTO emailDTO)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("bbdsmanagement@gmail.com");
                mail.To.Add("b.b.d.s.email.reciever@gmail.com");
                mail.Subject = emailDTO.Subject;
                mail.IsBodyHtml = true;

                string content = "Name : " + emailDTO.Name;
                content += "<br/> Message : " + emailDTO.Message;
                mail.Body = content;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                NetworkCredential networkCredential = new NetworkCredential("bbdsmanagement@gmail.com", "qqizxtlzwhhgxnjb");

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                ViewBag.Message = "Mail Send";

                ModelState.Clear();

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }
            TempData["success"] = "Свързахте се с нас успешно!";
            return View();
        }
    }
}
