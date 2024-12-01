using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("computers")]
    public class ComputerDb
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description{ get; set; }
        public int CpuId { get; set; }
        public int GpuId { get; set; }

        //офисный / игровой / пр.
        public int ComputerTypeId { get; set; }
    }
}
