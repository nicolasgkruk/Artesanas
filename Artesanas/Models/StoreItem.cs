using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Artesanas.Models
{
    public class StoreItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string HighlightedWords { get; set; }
        public string Description { get; set; }

        public string Pairing { get; set; }

        public int IBU { get; set; }
 
        public int Gravity { get; set; }

        public float Alcohol { get; set; }
        public enum EBitterness { NA = 0, Bajo = 1, Moderado = 2, MedioAmargor = 3, Alto = 4 }

        public float Amount { get; set; }

        public string Image { get; set; }

        [Display(Name = "Tipo")]
        public int TipoId { get; set; }
        [ForeignKey("TipoId")]
        public virtual Tipo Tipo { get; set; }

        [Display(Name = "Cervecería")]
        public int MakerId { get; set; }
        [ForeignKey("MakerId")]
        public virtual Maker Maker { get; set; }

        [Display(Name = "SubTipo")]
        public int SubTipoId { get; set; }
        [ForeignKey("SubTipoId")]
        public virtual SubTipo SubTipo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor a €{1}")]
        public float Price { get; set; }

    }
}