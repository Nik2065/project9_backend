using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{

    [Table("dictionary_for_cpu")]
    public class DictionaryForCpuDb
    {
        [Key]
        [Column("id")]
        public int Id {  get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("performance_points")]
        public int PerformancePoints {  get; set; }


        [Column("performance_class")]
        public int PerformanceClass {  get; set; }


        [Column("inserted")]
        public DateTime Inserted { get; set; }
        //
        //
        //добавить:
        //проиводитель/Дата релиза
        //

    }
}
