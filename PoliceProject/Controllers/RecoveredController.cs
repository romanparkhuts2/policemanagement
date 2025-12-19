
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PoliceProject.Data;
using PoliceProject.Models;

namespace PoliceProject.Controllers
{
    [Authorize]
    public class RecoveredController : Controller
    {
        private readonly PoliceDbContext _context;

        public RecoveredController (PoliceDbContext context)
        {
            _context = context;
        }

        //QUANDO UN POLIZIOTTO INSERIRA UN RECOVERED OGGETTO, DEVO DARGLI UNA Viewbag di Report(where x=>x.Solved==false), quindi poi li passo un con get name=id)
        //li passo id e poi prendo il report Find con quel id e setto lo stato come solved (archiviato)

        //FILTRA DATA
        public IActionResult Index(DateTime data)
        {
            var ListaRecover = _context.RecoveredItems.Include(x => x.IdPersonNavigation).ToList();

            if (data != default(DateTime))
            {
                DateOnly data2 = DateOnly.FromDateTime(data);
                ListaRecover = _context.RecoveredItems.Include(r => r.IdPersonNavigation).Where(x => x.FoundDate == data2).ToList();
            }
            double numR = _context.RecoveredItems.Count();
            double numD = _context.Reports.Count();
            double x = (numR / numD) * 100d;
            int r = (int)x;
            ViewData["Percentuale"] = r;
            return View(ListaRecover);
        }

        public IActionResult Create()
        {
            ViewBag.IdReport = new SelectList(_context.Reports.Where(x=>x.Solved==false).ToList(), "IdReport", "ReportCompleto");
            ViewBag.IdPerson = new SelectList(_context.People.ToList(), "IdPerson", "NomeCompleto");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(RecoveredItem recover)
        {
            if (recover != null)
            {
                // Uso recover.IdReport come proprietà, non come metodo
                int reportId = recover.IdReport;
                _context.RecoveredItems.Add(recover);
                var Report = _context.Reports.Where(x => x.IdReport == reportId).FirstOrDefault();
                if (Report != null)
                {
                    Report.Solved = true;
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPerson = new SelectList(_context.People.ToList(), "IdPerson", "NomeCompleto");
            return View();
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            var ListaRitrov = _context.RecoveredItems.ToList();
            RecoveredItem recovFound = new RecoveredItem();
            foreach (RecoveredItem recover in ListaRitrov)
            {
                if (recover.IdRecoveredItem == id)
                {
                    recovFound = recover;
                    break;
                }
            }
            ViewBag.IdPerson = new SelectList(_context.People.ToList(), "IdPerson", "NomeCompleto");
            return View(recovFound);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Modify(RecoveredItem recover)
        {
            if (recover != null)
            {
                _context.RecoveredItems.Update(recover);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Recovered");
        }

        [HttpGet]
        public IActionResult Delete (int id)
        {
            var ListaRitrov = _context.RecoveredItems.Include(x=>x.IdReportNavigation).Include(x=>x.IdPersonNavigation).ToList();
            RecoveredItem recovFound = new RecoveredItem();
            foreach (RecoveredItem recover in ListaRitrov)
            {
                if (recover.IdRecoveredItem == id)
                {
                    recovFound = recover;
                    break;
                }
            }
            return View(recovFound);
        }

        [HttpPost]
        public IActionResult Delete(RecoveredItem recover)
        {
            if (recover != null)
            {
                var Report = _context.Reports.Where(x => x.IdReport == recover.IdReport).FirstOrDefault();
                Report.Solved =false;
                _context.RecoveredItems.Remove(recover);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Recovered");
        }
    }
}
