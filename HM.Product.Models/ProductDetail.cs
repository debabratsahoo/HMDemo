namespace HM.Product.Models
{
    public class ProductDetail : ProductDB
    {
        public List<Article> Articles { get; set; }
        public List<SizeScale> Sizes { get; set; }
    }
}
