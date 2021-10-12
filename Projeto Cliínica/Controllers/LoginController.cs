using Microsoft.AspNetCore.Mvc;
using Projeto_Cliínica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Controllers
{
    public class LoginController : Controller
    {
        private DataContext dataContext;
        public LoginController(DataContext dc)
        {
            dataContext = dc;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
