using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Products
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? Rating { get; set; }
    }
}
