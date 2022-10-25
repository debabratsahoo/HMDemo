using HM.Product.Models;

namespace HM.Product.API
{
    public interface IProductService
    {
        Task<bool> AddProduct(ProductDB product, string userName);
        Task<ProductDetail> GetProductDetails(string productId);
    }
}