﻿using System;

namespace MoneyManager.DAL.Interfaces.Models.QueriesModels
{
    public class UserTransaction
    {
        public string AssetName { get; set; }

        public string TransactionSubcategory { get; set; }

        public string TransactionParentCategory { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionComment { get; set; }
    }
}
