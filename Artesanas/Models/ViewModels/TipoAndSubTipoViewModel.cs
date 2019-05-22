using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artesanas.Models.ViewModels
{
    public class TipoAndSubTipoViewModel
    {
        public IEnumerable<Tipo> TipoList { get; set; }
        public SubTipo SubTipo { get; set; }
        public List<string> SubTipoList { get; set; }
        public string StatusMessage { get; set; }
    }
}
