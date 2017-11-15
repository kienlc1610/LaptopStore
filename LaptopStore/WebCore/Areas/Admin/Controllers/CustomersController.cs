using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebCore.Controllers
{
    [Area("Admin")]

    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(string id)
        {
            ViewBag.CustomerId = id;
            return View();
        }

        public IActionResult Edit(string id)
        {
            ViewBag.CustomerId = id;
            return View();
        }

    }
}