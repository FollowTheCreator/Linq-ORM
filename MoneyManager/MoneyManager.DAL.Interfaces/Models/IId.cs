using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.DAL.Interfaces.Models
{
    public interface IId<TId>
    {
        TId Id { get; set; }
    }
}
