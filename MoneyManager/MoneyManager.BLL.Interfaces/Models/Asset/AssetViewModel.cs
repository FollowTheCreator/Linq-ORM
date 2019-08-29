using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.Asset
{
    public class AssetViewModel
    {
        public IEnumerable<Asset> Assets { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
