using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HM.Product.Models
{
    public class ArticleDB
    {
        [Key]
        [Column(Order = 1)]
        public string Id { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ColorId { get; set; }
        public virtual ProductDB? Product { get; set; }
    }
}
