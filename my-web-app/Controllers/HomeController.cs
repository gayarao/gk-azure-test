using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using my_web_app.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace my_web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> SendEmail()
        {
            ViewData["Message"] = "Your send mail page.";
            var apiKey = _configuration.GetSection("gkAPI").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("g.rao@reply.de", "Main user");
            List<EmailAddress> tos = new List<EmailAddress>
          {
              new EmailAddress("g.raogk@gmail.com", "To1"),
              new EmailAddress("gayathrirao.80@gmail.com", "To2"),
          };

            var subject = "Hello world email from Gayathri Rao.";
            var htmlContent = "<strong>Hello world email from Gayathri Rao.</strong>";
            var displayRecipients = true; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, displayRecipients);
            var response = await client.SendEmailAsync(msg);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
