using Microsoft.AspNetCore.Mvc;
using MvcCoreEFPProcedures.Models;
using MvcCoreEFPProcedures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFPProcedures.Controllers
{
    public class EnfermosController : Controller
    {

        private RepositoryEnfermos repo;

        public EnfermosController(RepositoryEnfermos repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Details(int inscripcion)
        {
            Enfermo enfermo = this.repo.FindEnfermo(inscripcion);
            return View(enfermo);
        }

        public IActionResult Delete(int inscripcion)
        {
            this.repo.DeleteEnfermo(inscripcion);
            return RedirectToAction("Index");
        }
    }
}
