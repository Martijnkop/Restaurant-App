using MySql.Data.MySqlClient;
using Restaurant.Interface.bill;
using Restaurant.Interface.dish;
using Restaurant.Interface.dishBillRelation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    internal class DishBillRelationDAL : IDishBillRelationDAL
    {
        public bool AddRange(List<DishDTO> dishes, BillDTO bill)
        {
            throw new NotImplementedException();
        }

        public List<DishDTO> GetAllOnBill(BillDTO bill)
        {
            DBConnection conn = new();

            List<DishDTO> dishes = new();

            if (conn.Open())
            {
                string getDishes =
                    "SELECT " +
                        "`dish`.* " +
                    "FROM " +
                        "dish " +
                    "JOIN `dishBillRelation` " +
                        "ON `dish`.`id` = `dishBillRelation`.`dishID` " +
                    "JOIN `bill` " +
                        "ON `bill`.id = dishBillRelation.billID " +
                   $"WHERE `bill`.tableNumber='{bill.TableNumber}' " +
                   $"AND `bill`.archived='0'";

                MySqlCommand cmd = new(getDishes, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        string name = reader.GetString(1);
                        float price = float.Parse(reader.GetString(2));

                        dishes.Add(new DishDTO { Id = id, Name = name, Price = price});
                    }
                }

                conn.Close();
            }

            return dishes;
        }

        internal void AddDishes(BillDTO bill, List<DishDTO> dishDTOs)
        {
            int billId = new BillDAL().FindByTableNumber(bill.TableNumber).ID;

            DBConnection conn = new();

            if (conn.Open())
            {
                foreach (DishDTO dish in dishDTOs)
                {
                    int id = new DishDAL().FindByName(dish.Name).Id;

                    string cmd =
                        "INSERT INTO dishbillrelation" +
                            "(dishID, billID)" +
                        "VALUES" +
                            $"('{id}', '{billId}'); ";

                    conn.RunCommand(cmd);
                    
                }

                conn.Close();
            }
        }

    }
}
