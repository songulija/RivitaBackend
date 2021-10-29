using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Controllers
{
    public class WagonsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
