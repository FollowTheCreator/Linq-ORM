using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IConfigService
    {
        int GetPageSize();

        int GetPopularPostsCount();
    }
}
