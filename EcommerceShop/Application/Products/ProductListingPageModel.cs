﻿using Application.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Products
{
	public class ProductListingPageModel
	{
		public List<CategoryViewModel> Categories { get; set; }
		public Dictionary<int, string> OrderBys { get; set; }
		public List<int> SelectPageSize { get; set; }
		public string CategoryId { get; set; }
		public string KeyWord { get; set; }
	}
}
