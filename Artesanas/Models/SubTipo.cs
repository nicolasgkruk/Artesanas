using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Artesanas.Models
{
    public class SubTipo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Subtipo de cerveza")]
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Tipo de cerveza")]
        public int TipoId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Tipo Tipo { get; set; }
    }
}
