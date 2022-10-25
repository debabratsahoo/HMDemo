using HM.Product.API;
using HM.Product.API.Controllers;
using HM.Product.Data;
using HM.Product.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace HM.Product.UnitTest
{
    public class DataControllerTest
    {
        Mock<ILogger<DataController>> MockLogger = new Mock<ILogger<DataController>>();
        Mock<IProductRepository<ProductDB>> MockProductDB = new Mock<IProductRepository<ProductDB>>();
        Mock<IProductRepository<ArticleDB>> MockArticleDB = new Mock<IProductRepository<ArticleDB>>();
        Mock<IProductService> MockIProductService = new Mock<IProductService>();

        [Fact]
        public async void AddProduct()
        {
            MockIProductService.Setup(g => g.AddProduct(It.IsAny<ProductDB>(), It.IsAny<string>())).ReturnsAsync(true);
            var product = JsonSerializer.Deserialize<ProductDB>("{\"id\":\"P5\",\"productName\":\"TEST\",\"productYear\":2022,\"channelId\":2,\"sizeScaleId\":\"1XXL\",\"articles\":[{\"colorId\":\"TESTCOLOR1\",\"id\":\"P5\"},{\"colorId\":\"TESTCOLOR2\",\"id\":\"P5\"},{\"colorId\":\"TESTCOLOR3\",\"id\":\"P5\"}]}");
            var dataController = new DataController(MockLogger.Object, MockProductDB.Object, MockArticleDB.Object, MockIProductService.Object);
            var result = await dataController.AddProduct(product, "Test");
            Assert.NotNull(result);
        }
        [Fact]
        public async void GetProductByIdTest()
        {
            MockIProductService.Setup(g => g.GetProductDetails(It.IsAny<string>())).ReturnsAsync(new ProductDetail { Id = "P5", ProductName = "TEST", Articles = new List<Article> { new Article { ArticleId = "P5", ArticleName = "TEST", ColourCode= "TESTCOLOR1", ColourId = "TESTCOLOR1", ColourName = "ArticWhite" } }, ChannelId= 2, CreatedBy = "TESTNAME", CreatedDate = DateTime.Now, ProductCode= "ACB234", ProductYear= 2022, Sizes = new List<SizeScale> { new SizeScale {SizeId = "1XXL", SizeName = "XXL" } }, SizeScaleId = "1XXL" });          
            var dataController = new DataController(MockLogger.Object, MockProductDB.Object, MockArticleDB.Object, MockIProductService.Object);
            var result = await dataController.GetProductById("Test");
            Assert.NotNull(result);
        }
    }
}