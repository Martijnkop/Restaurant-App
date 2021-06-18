using Restaurant.Data;
using Restaurant.Interface.bill;

namespace Restaurant.Factory
{
    public class BillFactory
    {
        public IBillContainerDAL CreateIBillContainerDAL()
        {
            return new BillDAL();
        }

        public IBillDAL CreateIBillDAL()
        {
            return new BillDAL();
        }
    }
}
