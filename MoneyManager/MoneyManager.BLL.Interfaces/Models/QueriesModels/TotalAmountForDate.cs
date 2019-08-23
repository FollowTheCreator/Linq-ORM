﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.QueriesModels
{
    public class TotalAmountForDate
    {
        public decimal TotalValue { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
