using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractrions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Products
{
    public interface IProductService
    {
        GenericData<ProductViewModel> GetProducts(ProductPage model);
		Task<ProductDetailViewModel> GetProductDetail(Guid productId);
	}
    public class ProductService : IProductService
    {
		private readonly IRepository1<Product, Guid> _productRepository;
		private readonly IRepository1<Category, Guid> _categoryRepository;
		private readonly IRepository1<Review, Guid> _reviewRepository;
		private readonly IRepository1<ProductImage, Guid> _imageRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(
			IRepository1<Product, Guid> productRepository,
			IRepository1<Category, Guid> categoryRepository,
			IRepository1<Review, Guid> reviewRepository,
			IRepository1<ProductImage, Guid> imageRepository,
			IUnitOfWork unitOfWork)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_reviewRepository = reviewRepository;
			_imageRepository = imageRepository;
			_unitOfWork = unitOfWork;
		}
		public GenericData<ProductViewModel> GetProducts(ProductPage filter)
		{
			var data = new GenericData<ProductViewModel>();
			var products = _productRepository.FindAll();
			var categories = _categoryRepository.FindAll();

			var result = (from p in products
						  join c in categories
						  on p.CategoryId equals c.Id
						  select new ProductViewModel
						  {
							  ProductId = p.Id,
							  ProductName = p.Name,
							  Price = p.Price,
							  DiscountPrice = p.DiscountPrice,
							  CategoryName = c.Name,
							  CategoryId = c.Id,
						  }).AsEnumerable();
			
			if (!string.IsNullOrEmpty(filter.CategoryId) && Guid.TryParse(filter.CategoryId, out Guid categoryId))
			{
				result = result.Where(s => s.CategoryId == categoryId);
			}

			if (filter.FromPrice.HasValue && filter.ToPrice.HasValue)
			{
				result = result.Where(s => s.Price >= filter.FromPrice.Value && s.Price <= filter.ToPrice);
			}

			if (filter.ToPrice.HasValue && !filter.FromPrice.HasValue)
			{
				result = result.Where(s => s.Price <= filter.ToPrice.Value);
			}
			if (filter.FromPrice.HasValue && !filter.ToPrice.HasValue)
			{
				result = result.Where(s => s.Price >= filter.FromPrice.Value);
			}

			if (!string.IsNullOrEmpty(filter.KeyWord))
			{
				result = result.Where(s => s.ProductName.Contains(filter.KeyWord, StringComparison.OrdinalIgnoreCase) || s.CategoryName.Contains(filter.KeyWord, StringComparison.OrdinalIgnoreCase));
			}
			if (filter.SortBy.Equals(SortEnum.Price))
			{
				result = result.OrderBy(s => s.Price);
			}
			else
			{
				result = result.OrderBy(s => s.ProductName);
			}
			// lấy ra số lượng product để tính số trang
			data.Count = result.Count();

			// lấy ra danh sách product ứng với PageIndex truyền vào (lúc đầu là 1)
			var productViewModels = result.Skip(filter.SkipNumber).Take(filter.PageSize).ToList();

			// lấy ra imageurl và rating
			var images = _imageRepository.FindAll();
			var reviews = _reviewRepository.FindAll();


			foreach (var item in productViewModels)
			{
				var image = images.FirstOrDefault(s => s.ProductId == item.ProductId)?.ImageLink;
				item.ImageUrl = string.IsNullOrEmpty(image) ? string.Empty : image;

				var productReviews = reviews.Where(s => s.ProductId == item.ProductId);
				if (productReviews!=null && productReviews.Any())
				{
					item.Rating = productReviews.Max(s => s.Rating);
				}
			}

			//gán danh sách product vào data
			data.Data = productViewModels;
			return data;
		}

		public async Task<ProductDetailViewModel> GetProductDetail(Guid productId)
		{
			var product = await _productRepository.FindById(productId);
			if (product == null)
			{
				return null;
			}
			var result = new ProductDetailViewModel();
			result.Id = product.Id;
			result.Name = product.Name;
			result.Description = product.Description;
			result.Quantity = product.Quantity;
			result.Price = product.Price;
			result.DiscountPrice = product.DiscountPrice;
			result.CategoryId = product.CategoryId;
			result.CategoryName = await GetCategory(product.CategoryId);
			result.Reviews = await GetReviewModels(productId);
			result.Images = await GetImageModels(productId);
			return result;
		}

		private async Task<string> GetCategory(Guid categoryId)
		{
			var category = await _categoryRepository.FindById(categoryId);
			return category != null ? category.Name : string.Empty;
		}
		private async Task<List<ReviewModel>> GetReviewModels(Guid productId)
		{
			var result = await _reviewRepository.FindAllAsync()
				.Where(s => s.ProductId == productId)
				.Select(x => new ReviewModel
				{
					Id = x.Id,
					Content = x.Content,
					ReviewerName = x.ReviewerName,
					Email = x.Email,
					Rating = x.Rating

				}).ToListAsync();
		
			return result;
		}

		private async Task<List<ImageViewModel>> GetImageModels(Guid productId)
		{

			var result = await _imageRepository.FindAllAsync()
				.Where(s => s.ProductId == productId)
				.Select(x => new ImageViewModel
				{
					Id = x.Id,
					ImageLink = x.ImageLink,
					Alt = x.Alt,
				}).ToListAsync();
			return result.ToList();
		}
	}
}
