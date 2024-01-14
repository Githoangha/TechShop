using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(Guid id)
        {
            return View();
        }
    }
}
