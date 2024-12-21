using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("products")]
    public class ProductDb
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        
        [Column("product_title")]
        public string ProductTitle { get; set; }

        
        [Column("product_description")]
        public string Description{ get; set; }

        
        [Column("current_price")]
        public decimal? CurrentPrice { get; set; }

        
        [Column("created")]
        public DateTime Created { get; set; }

        
        [Column("category_id")]
        public DateTime CategoryId { get; set; }

        [Column("cpu_id")]
        public int CpuId { get; set; }

        [Column("gpu_id")]
        public int? GpuId { get; set; }

    }
}
