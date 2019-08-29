using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Services
{
    public interface IConfigService
    {
        int GetPageSize();

        int GetCountOfUsers();

        int GetCountOfAssets();

        int GetCountOfCategories();

        int GetCountOfTransactions();
    }
}
