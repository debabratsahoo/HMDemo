using HM.Product.Models;

namespace HM.Product.Data
{
    public class TempData : ITempData
    {
        private readonly HMProductContext context;
        public TempData(HMProductContext productContext)
        {
            context = productContext;
            //var tempData = new List<ProductDB> {
            //new ProductDB{ Id = "TEST1", ProductName = "TEST", ChannelId = 1, SizeScaleId = "SIZETEST1", ProductCode = "2022001", ProductYear = 2022, Articles = new List<ArticleDB> { new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST1", ColorId = "COLORTEST123" }, new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST2", ColorId = "COLORTEST1" }, new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST3", ColorId = "COLORTEST3" } } }
            //};
            //context.Product.AddRange(tempData);
            context.SaveChanges();
        }
        public void Check()
        {

        }
    }
}