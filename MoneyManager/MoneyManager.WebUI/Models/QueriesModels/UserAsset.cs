﻿using System;

namespace MoneyManager.WebUI.Models.QueriesModels
{
    public class UserAsset
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
