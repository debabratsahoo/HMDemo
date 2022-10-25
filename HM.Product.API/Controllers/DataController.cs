using HM.Product.Data;
using HM.Product.Models;
using Microsoft.AspNetCore.Mvc;

namespace HM.Product.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly IProductRepository<ProductDB> _productRepository;
        private readonly IProductRepository<ArticleDB> _articleRepository;
        private readonly IProductService _productService;

        public DataController(ILogger<DataController> logger, IProductRepository<ProductDB> productRepository, IProductRepository<ArticleDB> articleRepository, IProductService productService)
        {
            _logger = logger;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _productService = productService;
        }

        [HttpGet]
        [Route("GetProductById/{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            var result = await _productService.GetProductDetails(productId);
            return result == null ? (IActionResult)NotFound() : Ok(result);
        }
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDB product, [FromHeader] string userName)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var result = await _productService.AddProduct(product, userName);
            return Ok(result);
        }

        [HttpGet]
        [Route("colours/{colourId}")]
        public IActionResult GetColourById(string colourId)
        {
            return Ok(new List<Colour> { new Colour { ColourId = "TESTCOLOR1", ColourCode = "TESTCOLOR1", ColourName = "ArticWhite" }, new Colour { ColourId = "TESTCOLOR2", ColourCode = "TESTCOLOR2", ColourName = "PearlBlue" }, new Colour { ColourId = "TESTCOLOR3", ColourCode = "TESTCOLOR3", ColourName = "PianoBlack" } }.Where(g => g.ColourId == colourId).FirstOrDefault());
        }

        [HttpGet]
        [Route("colours")]
        public IActionResult GetAllColours()
        {
            return Ok(new List<Colour> { new Colour { ColourId = "TESTCOLOR1", ColourCode = "TESTCOLOR1", ColourName = "ArticWhite" }, new Colour { ColourId = "TESTCOLOR2", ColourCode = "TESTCOLOR2", ColourName = "PearlBlue" }, new Colour { ColourId = "TESTCOLOR3", ColourCode = "TESTCOLOR3", ColourName = "PianoBlack" } });
        }

        [HttpGet]
        [Route("sizeScale/{sizeScaleId}")]
        public IActionResult GetSizeScaleById(string sizeScaleId)
        {
            return Ok(new List<SizeScale> { new SizeScale { SizeId = "1XL", SizeName = "XL" }, new SizeScale { SizeId = "1L", SizeName = "L" }, new SizeScale { SizeId = "1XXL", SizeName = "XXL" }, new SizeScale { SizeId = "1M", SizeName = "M" }, new SizeScale { SizeId = "1S", SizeName = "S" }, new SizeScale { SizeId = "1XS", SizeName = "XS" }, new SizeScale { SizeId = "1XXXL", SizeName = "XXXL" } }.Where(g => g.SizeId == sizeScaleId).AsEnumerable());
        }

        [HttpGet]
        [Route("Test")]
        public async Task<IEnumerable<ProductDB>> TestData()
        {
            var x = await _productRepository.GetAll(g => g.Id == "TEST1");
            var y = await _articleRepository.GetAll(g => 1 == 1);
            return x.Select(g => new ProductDB { Id = g.Id, ProductName = g.ProductName, Articles = g.Articles.ToList() }).ToList();
        }

        //[HttpPost]
        //[Route("LoadData")]
        //public async Task<bool> LoadTestData()
        //{
        //    var tempData = new List<ProductDB> {
        //    new ProductDB{ Id = "TEST1", ProductName = "TEST", ChannelId = 1, SizeScaleId = "SIZETEST1", ProductCode = "2022001", ProductYear = 2022, Articles = new List<ArticleDB> { new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST1", ColorId = "COLORTEST123" }, new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST2", ColorId = "COLORTEST1" }, new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST3", ColorId = "COLORTEST3" } } }
        //    };
        //    return await _productRepository.Save(tempData[0]);
        //}
    }
}