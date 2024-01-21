using System;
using System.Collections.Generic;
using System.Text;
using Application;

namespace Application.Products
{
	public class ProductPage : Page
	{
		public string KeyWord { get; set; } = string.Empty;
		public string CategoryId { get; set; }
		public decimal? FromPrice { get; set; }
		public decimal? ToPrice { get; set; }
		public SortEnum SortBy { get; set; }
	}
}
