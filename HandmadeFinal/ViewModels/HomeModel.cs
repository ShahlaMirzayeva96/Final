using HandmadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.ViewModels
{
    public class HomeModel
    {
        public IEnumerable<HomeBanner>HomeBanners { get; set; }
        public IEnumerable<ShippingDetail> ShippingDetails { get; set; }
        public IEnumerable<BrandIcon>BrandIcons { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
