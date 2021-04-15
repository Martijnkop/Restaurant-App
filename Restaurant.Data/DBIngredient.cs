using MySql.Data.MySqlClient;
using Restaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class DBIngredient
    {
        public static bool Add(string name, int diet)
        {
            DBConnection conn = new();
            if (conn.Open())
            {
                string addIngredient =
                    "INSERT INTO ingredient (" +
                        "name," +
                        "diet" +
                    ") values(" +
                        $"'{name}'," +
                        $"{diet}" +
                    ");";

                conn.RunCommand(addIngredient);

                conn.Close();
                return true;
            }

            return false;
        }

        public static bool Remove(string name)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string removeIngredient = 
                    $"DELETE FROM ingredient WHERE NAME='{name}'";

                conn.RunCommand(removeIngredient);
                conn.Close();
                return true;
            }

            return false;
        }

        public static DatabaseIngredient Get(string name)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string getIngredient =
                    $"SELECT * FROM ingredient WHERE name={name}";

                MySqlCommand cmd = new(getIngredient, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        byte diet = byte.Parse(reader.GetString(2));

                        return new DatabaseIngredient { Id = id, Name = name, Diet = diet };
                    }
                }
            }

            return null;
        }

        public static List<DatabaseIngredient> GetAll()
        {
            DBConnection conn = new();

            List<DatabaseIngredient> ingredients = new();

            if (conn.Open())
            {
                string getAll =
                    "SELECT * FROM ingredient ORDER BY name;";

                MySqlCommand cmd = new(getAll, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        string name = reader.GetString(1);
                        byte diet = byte.Parse(reader.GetString(2));

                        ingredients.Add(new DatabaseIngredient { Id = id, Name = name, Diet = diet });
                    }
                }
            }

            return ingredients;
        }
    }
}
