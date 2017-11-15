using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebCore.Controllers
{
    
    public class CategoriesController : Controller
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
            ViewBag.CateId = id;
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CateId = id;
            return View();
        }
    }
}