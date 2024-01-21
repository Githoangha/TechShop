using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Categories
{
	public class CategoryViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public int ProductCount { get; set; }
	}
}
