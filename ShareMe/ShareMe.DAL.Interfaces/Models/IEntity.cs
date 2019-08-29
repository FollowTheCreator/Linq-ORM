using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DAL.Interfaces.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
