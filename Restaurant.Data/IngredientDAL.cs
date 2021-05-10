using MySql.Data.MySqlClient;
using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class IngredientDAL : IIngredientDAL, IIngredientContainerDAL
    {
        public bool Add(string name, int diet)
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

        public bool Remove(string name)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                new DishIngredientRelationDAL().Remove(new IngredientDTO { Name = name });

                string removeIngredient = 
                    $"DELETE FROM ingredient WHERE NAME='{name}'";

                conn.RunCommand(removeIngredient);
                conn.Close();
                return true;
            }

            return false;
        }

        public bool Update(string oldName, string newName, int newDiet)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string updateIngredient =
                    $"UPDATE ingredient SET NAME='{newName}', DIET='{newDiet}' WHERE NAME='{oldName}'";

                conn.RunCommand(updateIngredient);
                conn.Close();
                return true;
            }

            return false;
        }

        public IngredientDTO FindByName(string name)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string getIngredient =
                    $"SELECT * FROM ingredient WHERE name='{name}'";

                MySqlCommand cmd = new(getIngredient, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        byte diet = byte.Parse(reader.GetString(2));

                        conn.Close();
                        return new IngredientDTO { Id = id, Name = name, Diet = diet };
                    }
                }
            }

            return new IngredientDTO();
        }

        public List<IngredientDTO> GetAll()
        {
            DBConnection conn = new();

            List<IngredientDTO> ingredients = new();

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

                        ingredients.Add(new IngredientDTO { Id = id, Name = name, Diet = diet });
                    }
                }

                conn.Close();
            }

            return ingredients;
        }
    }
}
