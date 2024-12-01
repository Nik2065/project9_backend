using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{

    [Table("dictionary_for_cpu")]
    public class DictionaryForCpuDb
    {
        [Key]
        public int Id {  get; set; }
        public string Name { get; set; }

        public decimal PerformancePoints {  get; set; }

        public int PerformanceClass {  get; set; }

        public DateTime Inserted { get; set; }
        //
        //
        //добавить:
        //проиводитель/Дата релиза
        //

    }
}
