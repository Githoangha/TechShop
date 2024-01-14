using System;
using System.Collections.Generic;
using System.Text;
using Domain.Abstractrions;
using Domain.Entities;

namespace Application.Products
{
    public interface IProductService
    {
        GenericData<ProductViewModel> GetProducts();
    }
    public class ProductService : IProductService
    {
        public readonly IRepository1<Product, Guid> _productRepository;
        public readonly IRepository1<Category, Guid> _categoryRepository;
        public readonly IRepository1<Review, Guid> _reviewRepository;
        public readonly IRepository1<ProductImage, Guid> _imageRepository;
        public ProductService(
            IRepository1<Product, Guid> productRepository,
            IRepository1<Category, Guid> categoryRepository,
            IRepository1<Review, Guid> reviewRepository,
            IRepository1<ProductImage, Guid> imageRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _imageRepository = imageRepository;
        }
        public GenericData<ProductViewModel> GetProducts()
        {
            var result = new GenericData<ProductViewModel>();
            var Product = _productRepository.FindAll();
            var Categories = _categoryRepository.FindAll();
            return result;
        }
    }
}
