using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Artesanas.Models
{
    public class Maker
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cervecería")]
        [Required]
        public string Name { get; set; }
    }
}
