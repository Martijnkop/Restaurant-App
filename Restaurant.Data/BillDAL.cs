using MySql.Data.MySqlClient;
using Restaurant.Interface.bill;
using Restaurant.Interface.dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class BillDAL : IBillContainerDAL, IBillDAL
    {
        public bool Add(BillDTO bill)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                int temp = 0;
                if (bill.Archived) temp = 1;

                string cmd =
                    "INSERT INTO bill (" +
                        "tableNumber," +
                        "billStatus," +
                        "tip," +
                        "archived" +
                    ") values(" +
                        $"'{bill.TableNumber}'," +
                        $"'{(int)bill.Status}'," +
                        $"'{(float)bill.Tip}', " +
                        $"'{temp}'" +
                    ");";

                conn.RunCommand(cmd);

                conn.Close();
            }

            return false;
        }

        public bool Remove(BillDTO bill)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string cmd =
                    $"DELETE FROM bill WHERE tablenumber='{bill.TableNumber}' AND archived='0';";

                conn.RunCommand(cmd);

                conn.Close();
                return true;
            }
            return false;
        }

        public bool Archive(BillDTO bill)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string cmd =
                    $"UPDATE bill SET archived='{1}' WHERE tablenumber='{bill.TableNumber}';";

                conn.RunCommand(cmd);

                conn.Close();
                return true;
            }
            return false;
        }

        public BillDTO FindByTableNumber(int tableNumber)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string getIngredient =
                    $"SELECT * FROM bill WHERE tableNumber='{tableNumber}' AND archived='0';";

                MySqlCommand cmd = new(getIngredient, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        int status = int.Parse(reader.GetString(2));
                        float tip = float.Parse(reader.GetString(3));
                        bool archived = int.Parse(reader.GetString(4)) == 1;
                        List<DishDTO> dishes = new();

                        conn.Close();
                        return new BillDTO { ID = id, TableNumber = tableNumber, Status = (BillStatus)status, Tip = tip, Archived = archived, Dishes = dishes };
                    }
                }
            }

            return new BillDTO();
        }

        public void AddDishes(BillDTO bill, List<DishDTO> dishDTOs)
        {
            new DishBillRelationDAL().AddDishes(bill, dishDTOs);
        }

        public List<DishDTO> GetAllOnBill(BillDTO bill)
        {
            return new DishBillRelationDAL().GetAllOnBill(bill);
        }
    }
}
