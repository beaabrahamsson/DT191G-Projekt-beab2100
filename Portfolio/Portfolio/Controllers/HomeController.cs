using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_context.Projects != null)
            {
                var projects = await _context.Projects.ToListAsync();
                ViewBag.Projects = projects;
                return View(projects);
            }

            return View();
        }

        //Get projects
        public async Task<IActionResult> Projects()
        {
            if (_context.Projects != null)
            {
                var projects = await _context.Projects.ToListAsync();
                ViewBag.Projects = projects;
                return View(projects);
            }

            return View();
        }

        // Get project details
        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var projectModel = await _context.Projects
                .FirstOrDefaultAsync(m => m.ID == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            ViewBag.ProjectDetails = projectModel;
            return View(projectModel);
        }

        //Get resume
        public async Task<IActionResult> Resume()
        {
            if (_context.CV != null)
            {
                var resume = await _context.CV.OrderByDescending(x => x.YearStart).ToListAsync(); //Order by YearStart 
                var education = await _context.Education.ToListAsync();
                ViewBag.Edu = education;
                ViewBag.Resume = resume;
                return View();
            }

            return View();
        }

        //Get education
        public async Task<IActionResult> Education()
        {
            if (_context.Education != null)
            {
                var education = await _context.Education.ToListAsync();
                ViewBag.Edu = education;
                return View(education);
            }

            return View();
        }

        //Accessibility page
        public IActionResult Accessibility()
        {
            return View();
        }

        //Contact page
        public IActionResult Contact()
        {
            return View();
        }

        //Send contact email
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Contact(ContactModel contactModel)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                //Set email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(contactModel.Email);

                mail.To.Add("beab2100@gmail.com");

                mail.Subject = contactModel.Subject;

                mail.IsBodyHtml = true;

                string content = "Namn : " + contactModel.Name;
                content += "<br/> Från : " + contactModel.Email;
                content += "<br/> Meddelande : " + contactModel.Message;

                mail.Body = content;


                // SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                //Network credential
                NetworkCredential networkCredential = new NetworkCredential("beab2100@gmail.com", "votwkygiomrfqigl");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                ViewBag.Message = "Meddelande skickat!";

                //Clear form
                ModelState.Clear();

            }
            catch (Exception ex)
            {
                //Set error
                ViewBag.Message = ex.Message.ToString();
            }
            return View();
        }
    


[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}