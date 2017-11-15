using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Net.Http;

namespace WebCore.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }
    }
}