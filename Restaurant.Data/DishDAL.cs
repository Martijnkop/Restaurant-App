using MySql.Data.MySqlClient;
using Restaurant.Interface.dish;
using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class DishDAL : IDishDAL, IDishContainerDAL
    {
        public bool Add(string name, double price, List<IngredientDTO> ingredients)
        {
            DBConnection conn = new();
            if (conn.Open())
            {
                string addDish =
                    "INSERT INTO dish (" +
                        "name," +
                        "price" +
                    ") values(" +
                        $"'{name}'," +
                        $"'{price}'" +
                    ");";

                conn.RunCommand(addDish);

                conn.Close();

                DishIngredientRelationDAL dirDAL = new();
                foreach (IngredientDTO i in ingredients)
                {
                    dirDAL.Add(i, new DishDTO { Name = name });
                }

                return true;
            }

            return false;
        }
        public bool Remove(string name)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                new DishIngredientRelationDAL().Remove(new DishDTO { Name = name });

                string removeDish =
                    $"DELETE FROM dish WHERE name='{name}';";

                conn.RunCommand(removeDish);
                conn.Close();
                return true;
            }

            return false;
        }

        public bool Update(string oldName, string newName, double price, List<IngredientDTO> ingredients)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string updateDish =
                    $"UPDATE dish SET NAME='{newName}', PRICE='{price}' WHERE NAME='{oldName}';";

                conn.RunCommand(updateDish);
                conn.Close();

                List<IngredientDTO> oldIngredients = FindByName(oldName).Ingredients;

                // Remove all identical entries
                // Everything left in oldIngredients should be removed after,
                // Everything left in ingredients should be added
                List<IngredientDTO> temp = new();
                temp.AddRange(ingredients);
                foreach (IngredientDTO i in temp)
                {
                    if (oldIngredients == null || oldIngredients.Count == 0) break;
                    if (oldIngredients.Exists(oldIngredient => oldIngredient.Name == i.Name))
                    {
                        oldIngredients.RemoveAll(oldIngredient => oldIngredient.Name == i.Name);
                        ingredients.Remove(i);
                    }
                }

                DishDTO curr = FindByName(newName);
                if (ingredients != null && ingredients.Count > 0) foreach (IngredientDTO i in ingredients)
                    {
                        //add
                        new DishIngredientRelationDAL().Add(i, curr);
                    }

                if (oldIngredients != null && oldIngredients.Count > 0) foreach (IngredientDTO i in oldIngredients)
                    {
                        new DishIngredientRelationDAL().Remove(i, curr);
                    }
                return true;
            }

            return false;
        }

        public DishDTO FindByName(string name)
        {
            DBConnection conn = new();

            List<IngredientDTO> ingredients = new();

            if (conn.Open())
            {
                string getIngredients =
                    "SELECT " +
                        "`ingredient`.* " +
                    "FROM " +
                        "ingredient " +
                    "JOIN `ingredientDishRelation` " +
                        "ON `ingredient`.`id` = `ingredientDishRelation`.`ingredientID` " +
                    "JOIN `dish` " +
                        "ON `dish`.id = ingredientDishRelation.dishID " +
                   $"WHERE `dish`.name='{name}'";

                MySqlCommand cmd = new(getIngredients, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        string ingredientName = reader.GetString(1);
                        byte diet = byte.Parse(reader.GetString(2));

                        ingredients.Add(new IngredientDTO { Id = id, Name = ingredientName, Diet = diet });
                    }
                }

                string getDish =
                    $"SELECT * FROM dish WHERE name='{name}'";

                MySqlCommand cmd2 = new(getDish, conn.Connection);

                using (var reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        float price = float.Parse(reader.GetString(2));

                        conn.Close();
                        return new DishDTO { Id = id, Name = name, Price = price, Ingredients = ingredients };
                    }
                }
            }

            return new DishDTO();
        }

        // No need to add all ingredient data if you show all dishes
        public List<DishDTO> GetAll()
        {
            DBConnection conn = new();

            List<DishDTO> dishes = new();

            if (conn.Open())
            {
                string getAll =
                    "SELECT * FROM dish ORDER BY name;";

                MySqlCommand cmd = new(getAll, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        string name = reader.GetString(1);
                        float price = float.Parse(reader.GetString(2));

                        dishes.Add(new DishDTO { Id = id, Name = name, Price = price });
                    }
                }
                conn.Close();
            }

            return dishes;
        }
    }
}
