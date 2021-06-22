using Restaurant.Interface.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.TestDAL
{
    public class TableTestDAL : ITableDAL, ITableContainerDAL
    {
        public List<TableDTO> tables = new();

        public bool Add(int tableNumber)
        {
            tables.Add(new TableDTO { Id = tables.Count, TableNumber = tableNumber, Status = TableStatus.Free });
            return true;
        }

        public TableDTO FindByTableNumber(int tableNumber)
        {
            return tables.Where(t => t.TableNumber == tableNumber).FirstOrDefault();
        }

        public List<TableDTO> GetAll()
        {
            return tables;
        }

        public bool Update(int oldTableNumber, int tableNumber, int status)
        {
            int index = tables.IndexOf(FindByTableNumber(oldTableNumber));
            if (index == -1) return false;
            tables[index] = new TableDTO { Id = tables.ElementAt(index).Id, TableNumber = tableNumber, Status = (TableStatus)status };
            return true;
        }
    }
}
