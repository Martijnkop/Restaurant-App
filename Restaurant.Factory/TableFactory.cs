using Restaurant.Data;
using Restaurant.Interface.table;

namespace Restaurant.Factory
{
    public class TableFactory
    {
        public ITableContainerDAL CreateITableContainerDAL()
        {
            return new TableDAL();
        }

        public ITableDAL CreateITableDAL()
        {
            return new TableDAL();
        }
    }
}
