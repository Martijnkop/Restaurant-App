using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.table
{
    public interface ITableDAL
    {
        bool Add(int tableNumber);
        bool Update(int oldTableNumber, int tableNumber, int status);
    }
}
