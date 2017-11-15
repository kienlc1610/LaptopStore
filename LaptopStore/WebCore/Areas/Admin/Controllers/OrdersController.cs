using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebCore.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

    }
}