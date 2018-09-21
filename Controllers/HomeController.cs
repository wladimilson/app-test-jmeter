using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app_test_jmeter.Models;
using app_test_jmeter.Models.ViewModels;

namespace app_test_jmeter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Formulario(){
            return View(new PessoaViewModel());
        }

        [HttpPost]
        public IActionResult Formulario(PessoaViewModel pesssoa){
            return View("Resultado", pesssoa);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
