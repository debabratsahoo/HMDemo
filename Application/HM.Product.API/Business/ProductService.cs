using HM.Product.Data;
using HM.Product.Models;
using System;

namespace HM.Product.API
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository<ProductDB> _productRepository;
        private readonly IProductRepository<ArticleDB> _articleRepository;
        private readonly IHttpServiceProvider _httpServiceProvider;
        public ProductService(IProductRepository<ProductDB> productRepository, IHttpServiceProvider httpServiceProvider, IProductRepository<ArticleDB> articleRepository)
        {
            _productRepository = productRepository;
            _httpServiceProvider = httpServiceProvider;
            _articleRepository = articleRepository;
        }
        public async Task<bool> AddProduct(ProductDB product, string userName)
        {
            product.ProductCode = await GetProductCode(product.ChannelId);
            product.CreatedDate = DateTime.Now;
            product.CreatedBy = userName;
            var result = await _productRepository.Save(product);
            //call other apis to add event for new product
            //_httpServiceProvider.InvokeService<bool>("")
            return result;
        }
        public async Task<ProductDetail> GetProductDetails(string productId)
        {
            var data = await _productRepository.Get(g => g.Id == productId);
            if(data == null)
            {
                return null;
            }
            var articles = await _articleRepository.GetAll(g => g.Id == productId);
            var colors = new List<Colour>();
            var throttler = new SemaphoreSlim(10);
            var sizeAPIcall = _httpServiceProvider.InvokeService<List<SizeScale>>($"https://localhost:7088/api/sizeScale/{data.SizeScaleId}");
            var allTasks = articles.DistinctBy(g => g.ColorId).Select(async batch =>
            {
                await throttler.WaitAsync().ConfigureAwait(false);
                try
                {
                    var reqColorAPIResponse = await _httpServiceProvider.InvokeService<Colour>($"https://localhost:7088/api/colours/{batch.ColorId}");
                    if (reqColorAPIResponse != null)
                    {
                        colors.Add(reqColorAPIResponse);
                    }
                }
                finally
                {
                    throttler.Release();
                }
            });
            await Task.WhenAll(allTasks);
            var sizeAPIresponse = await sizeAPIcall;
            var result = new ProductDetail {
            Id = productId, ProductCode = data.ProductCode, ChannelId = data.ChannelId, CreatedBy = data.CreatedBy, CreatedDate = data.CreatedDate, Articles = articles.Join(colors, a => a.ColorId, c => c.ColourId, (a, c) => new Article { ArticleId = a.Id, ArticleName = $"{data.ProductName}-{c.ColourCode}", ColourId = c.ColourId, ColourCode = c.ColourCode, ColourName = c.ColourName }).ToList(), ProductName = data.ProductName, ProductYear = data.ProductYear , SizeScaleId = data.SizeScaleId, Sizes = sizeAPIresponse.Any() ? sizeAPIresponse : new List<SizeScale>()
            };

            return result;
        }
        private async Task<string> GetProductCode(int channelId)
        {
            var random = new Random();
            switch (channelId)
            {
                case 1:
                    return $"{DateTime.Now.Year}{DateTime.Now.Ticks.ToString().Substring(11, 3)}";
                case 2:
                    return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 6).ToUpper();
                case 3:
                    var data = await _productRepository.Get(g => g.ChannelId == 3, y => y.ProductCode);
                    return data != null ? $"{Convert.ToInt32(data.ProductCode) + 1}" : "100000";
                default:
                    throw new Exception("Invalid channelId");
            }
        }
    }
}
