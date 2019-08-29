using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Models.ViewModels
{
    public class AssetViewModel
    {
        public IEnumerable<Asset.Asset> Assets { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
