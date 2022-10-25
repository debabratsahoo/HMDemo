using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
namespace HM.Product.Models
{
    public class ProductDB
    {
        [Key]
        [Column(Order = 1)]
        public string Id { get; set; }
        public string? ProductCode {  get; set; }
        [MaxLength(100, ErrorMessage = "Product name should be less than or equals 100 characters")]
        [Required]
        public string ProductName { get; set; }
        [Range(1900, 2999)]
        public int ProductYear { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Please enter valid channel id")]
        public int ChannelId { get; set; }
        public string SizeScaleId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<ArticleDB> Articles { get; set; }
    }
}