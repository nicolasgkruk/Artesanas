using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Artesanas.Areas.Admin.Controllers
{
    public class TipoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}