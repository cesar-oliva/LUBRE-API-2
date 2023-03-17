using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lubre.WebAPI
{
    public class PositionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}