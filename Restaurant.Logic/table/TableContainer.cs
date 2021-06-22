using Restaurant.Factory;
using Restaurant.Interface.table;
using Restaurant.Logic.bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic.table
{
    public class TableContainer
    {
        private ITableContainerDAL DAL;
        public TableContainer(ITableContainerDAL dal = null)
        {
            if (dal == null) this.DAL = new TableFactory().CreateITableContainerDAL();
            else this.DAL = dal;
        }

        public Table FindByTableNumber(int tableNumber)
        {
            TableDTO table = DAL.FindByTableNumber(tableNumber);
            if (table.TableNumber == 0) return null;
            Table t = ConvertFromDTO(table);
            t.Bill = new BillContainer().FindByTableNumber(tableNumber);
            return t;
        }

        public List<Table> GetAll()
        {
            List<TableDTO> tableDTOs = this.DAL.GetAll();
            List<Table> tables = new();
            
            foreach (TableDTO table in tableDTOs)
            {
                if (table.Status == TableStatus.Taken) tables.Add(new Table(table.TableNumber, table.Status, new BillContainer().FindByTableNumber(table.TableNumber)));
                else tables.Add(new Table(table.TableNumber, table.Status));
            }

            return tables;
        }

        private Table ConvertFromDTO(TableDTO tableDTO)
        {
            Bill bill = new BillContainer().ConvertFromDTO(tableDTO.Bill);
            return new Table(tableDTO.TableNumber, tableDTO.Status, bill);
        }
    }
}
