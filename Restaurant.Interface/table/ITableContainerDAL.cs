using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.table
{
    public interface ITableContainerDAL
    {
        List<TableDTO> GetAll();
        TableDTO FindByTableNumber(int tableNumber);

    }
}
