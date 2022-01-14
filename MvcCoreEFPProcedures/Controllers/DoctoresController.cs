using Microsoft.AspNetCore.Mvc;
using MvcCoreEFPProcedures.Models;
using MvcCoreEFPProcedures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFPProcedures.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryDoctores repo;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<String> especialidades = this.repo.GetEspecialidades();

            ViewBag.Especialidades = especialidades;

            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        [HttpPost]
        public IActionResult Index(int incremento, string especialidad)
        {
            List<String> especialidades = this.repo.GetEspecialidades();
            ViewBag.Especialidades = especialidades;
            this.repo.IncrementarSalarioEspecialidad(incremento, especialidad);
            List<Doctor> doctores = this.repo.GetDoctoresEspecialidad(especialidad);
            return View(doctores);

        }
    }
}
