using System;
using System.Collections.Generic;
using System.Text;

namespace Piwerko.Data.Interfaces
{
    public interface IConfigurationManager
    {
        string GetValue(string key);
    }
}
