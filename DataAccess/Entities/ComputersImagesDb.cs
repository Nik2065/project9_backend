using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("computers_images")]
    public class ComputersImagesDb
    {
        [Key]
        public int Id { get; set; }
        public int ComputerId { get; set; }
        public string ImagePath { get; set; }
        public int ImageUseTo { get; set; } //назначение: основные фото/миниатюры
        public bool MainPhoto { get; set; } //признак "главного" фото

    }
}
