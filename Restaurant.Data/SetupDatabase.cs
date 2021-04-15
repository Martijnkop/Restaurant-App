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
                        "id INT AUTO_INCREMENT PRIMARY KEY," +
                        "name VARCHAR(255) NOT NULL UNIQUE," +
                        "diet TINYINT NOT NULL" +
                    ");";

                conn.RunCommand(createIngredientTable);

                string createDishTable =
                    "CREATE TABLE IF NOT EXISTS dish (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY," +
                        "name VARCHAR(255) NOT NULL UNIQUE" +
                    ");";

                conn.RunCommand(createDishTable);

                string createBillTable =
                    "CREATE TABLE IF NOT EXISTS bill (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY" +
                    ");";

                conn.RunCommand(createBillTable);

                string createTableTable =
                    "CREATE TABLE IF NOT EXISTS restaurantTable (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY," +
                        "tablenumber INT NOT NULL UNIQUE" +
                    ");";
                conn.RunCommand(createTableTable);

                string createGuestTable =
                    "CREATE TABLE IF NOT EXISTS guest (" +
                        "id INT AUTO_INCREMENT PRIMARY KEY," +
                        "status TINYINT NOT NULL," +
                        "lastupdate LONG NOT NULL" +
                    ");";

                conn.RunCommand(createGuestTable);

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
                        "billID INT NOT NULL," +
                        "" +
                        "FOREIGN KEY (dishID) REFERENCES dish (id)," +
                        "FOREIGN KEY (billID) REFERENCES bill (id)," +
                        "UNIQUE (dishID, billID)" +
                    ");";

                conn.RunCommand(createLinkDishBillTable);

                conn.Close();
            }
        }
    }
}
