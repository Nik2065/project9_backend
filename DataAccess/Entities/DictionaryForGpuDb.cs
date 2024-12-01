using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("dictionary_for_gpu")]
    public class DictionaryForGpuDb
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
