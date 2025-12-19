using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PoliceProject.Data;
using PoliceProject.Models;

namespace PoliceProject.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly PoliceDbContext _context;

        public ReportController(PoliceDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime data, int type)
        {
            var ListaReports = _context.Reports.Include(r => r.IdPersonNavigation).ToList();
            var oggetto = new ObjectType();
            if (type != 0) { 
                oggetto = _context.ObjectTypes.Where(x => x.IdObjectType == type).FirstOrDefault();
                if (!oggetto.ObjectType1.Equals(" "))
                {
                    ListaReports = _context.Reports.Include(x => x.IdPersonNavigation).Where(x => x.IdObjectType == type).ToList();
                }
            }
            if (data != default(DateTime))
            {
                DateOnly data2 = DateOnly.FromDateTime(data);
                ListaReports = _context.Reports.Include(r => r.IdPersonNavigation).Where(x => x.ReportDate == data2).ToList();
            }

            double numR = _context.RecoveredItems.Count();
            double numD = _context.Reports.Count();
            double x = (numR / numD) * 100d;
            int r = (int)x;
            ViewData["Percentuale"] = r;
            ViewBag.IdObjectType = new SelectList(_context.ObjectTypes.ToList(), "IdObjectType", "ObjectType1");
            return View(ListaReports);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.IdObjectType = new SelectList(_context.ObjectTypes.ToList(), "IdObjectType", "ObjectType1");
            ViewBag.IdPerson = new SelectList(_context.People.ToList(), "IdPerson", "NomeCompleto");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Report report)
        {

            if (report != null)
            {
                report.ReportDate = DateOnly.FromDateTime(DateTime.UtcNow);
                report.Solved = false;
                if (report.FileUpload.Length > 0)
                {
                    string rootPath = Directory.GetCurrentDirectory();
                    string UpdateFolder = Path.Combine(rootPath, "wwwroot", "img");

                    long ticks = DateTime.Now.Ticks;
                    string ext = Path.GetExtension(report.FileUpload.FileName);

                    string FileName = $"{ticks}{ext}";

                    string FilePath = Path.Combine(UpdateFolder, FileName); 
                    var stream = new FileStream(FilePath, FileMode.Create);
                    report.FileUpload.CopyTo(stream);
                    stream.Close();
                    report.ImageFileUrl = $"/img/{FileName}";
                }
                _context.Reports.Add(report);
                _context.SaveChanges();
                return RedirectToAction("Index", "Report");
            }
            int a = 0;
            return View();
        }

        [HttpGet]
        public IActionResult CreatePerson()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {

            if (person != null)
            {
                _context.People.Add(person);
                _context.SaveChanges();
                return RedirectToAction("Index", "Report");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            var ListaReport = _context.Reports.ToList();
            Report reportFound = new Report();
            foreach (Report report in ListaReport)
            {
                if (report.IdReport == id)
                {
                    reportFound = report;
                    break;
                }
            }
            ViewBag.IdObjectType = new SelectList(_context.ObjectTypes.ToList(), "IdObjectType", "ObjectType1");
            ViewBag.IdPerson = new SelectList(_context.People.ToList(), "IdPerson", "NomeCompleto");
            return View(reportFound);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Modify(Report report)
        {
            if(report != null)
            {
                _context.Reports.Update(report);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Report");
        }
    }
}
