using Application;
using Application.Categories;
using Application.Helper;
using Application.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index(string categoryId, string keyWord)
        {
            var model = new ProductListingPageModel();
            model.Categories = _categoryService.GetCategories();
            model.SelectPageSize = new List<int> { 6, 9, 18, 27, 36 };
            model.OrderBys = EnumHelper.GetList(typeof(SortEnum));

            model.CategoryId = !string.IsNullOrEmpty(categoryId) ? categoryId : string.Empty;
            model.KeyWord = !string.IsNullOrEmpty(keyWord) ? keyWord : string.Empty;

            return View(model);
        }
        public IActionResult ProductListPartial([FromBody] ProductPage model)
        {
            var result = _productService.GetProducts(model);
            return PartialView(result);
        }
        public IActionResult Detail(Guid id)
        {
            return View();
        }
    }
}
