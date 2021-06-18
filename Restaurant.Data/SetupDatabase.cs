using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class SetupDatabase
    {
        public static void Setup()
        {
            DBConnection conn = new();
            if (conn.Open())
            {
                string createIngredientTable =
                    "CREATE TABLE IF NOT EXISTS ingredient (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY, " +
                        "name VARCHAR(255) NOT NULL UNIQUE," +
                        "diet TINYINT NOT NULL" +
                    ");";

                conn.RunCommand(createIngredientTable);

                string createDishTable =
                    "CREATE TABLE IF NOT EXISTS dish (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY, " +
                        "name VARCHAR(255) NOT NULL UNIQUE, " +
                        "price FLOAT NOT NULL" +
                    ");";

                conn.RunCommand(createDishTable);

                string createTableTable =
                    "CREATE TABLE IF NOT EXISTS restaurantTable (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY, " +
                        "tablenumber INT NOT NULL UNIQUE," +
                        "tableStatus INT" +
                    ");";

                conn.RunCommand(createTableTable);

                string createBillTable =
                    "CREATE TABLE IF NOT EXISTS bill (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY," +
                        "tableNumber INT," +
                        "billStatus INT," +
                        "tip FLOAT," +
                        "archived TINYINT(1)" +
                    ");";

                conn.RunCommand(createBillTable);


                string createLinkIngredientDishTable =
                    "CREATE TABLE IF NOT EXISTS ingredientDishRelation (" +
                        "ingredientID INT NOT NULL," +
                        "dishID INT NOT NULL," +
                        "" +
                        "FOREIGN KEY (ingredientID) REFERENCES ingredient (id)," +
                        "FOREIGN KEY (dishID) REFERENCES dish (id)," +
                        "UNIQUE (ingredientID, dishID)" +
                    ");";

                conn.RunCommand(createLinkIngredientDishTable);

                string createLinkDishBillTable =
                    "CREATE TABLE IF NOT EXISTS dishBillRelation (" +
                        "dishID INT NOT NULL," +
                        "billID INT NOT NULL " +
                        "" +
                    ");";

                conn.RunCommand(createLinkDishBillTable);

                conn.Close();
            }
        }
    }
}
