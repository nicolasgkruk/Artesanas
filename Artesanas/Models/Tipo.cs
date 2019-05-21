using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Artesanas.Models
{
    public class Tipo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Tipo de cerveza")]
        [Required]
        public string Name { get; set; }
    }
}
