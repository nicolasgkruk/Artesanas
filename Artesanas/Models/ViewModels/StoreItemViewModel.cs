using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artesanas.Models.ViewModels
{
    public class StoreItemViewModel
    {
        public StoreItem StoreItem { get; set; }
        public IEnumerable<Tipo> Tipo { get; set; }
        public IEnumerable<SubTipo> SubTipo { get; set; }
        public IEnumerable<Maker> Maker { get; set; }
    }
}