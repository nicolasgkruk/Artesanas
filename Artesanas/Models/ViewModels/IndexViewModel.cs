using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artesanas.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<StoreItem> StoreItem { get; set; }
        public IEnumerable<Tipo> Tipo { get; set; }
        public IEnumerable<Coupon> Coupon { get; set; }

    }
}