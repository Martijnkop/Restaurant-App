using Restaurant.Interface.dish;
using Restaurant.Interface.dishIngredientRelation;
using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    internal class DishIngredientRelationDAL : IDishIngredientRelationDAL
    {
        public bool Add(IngredientDTO ingredient, DishDTO dish)
        {
            int ingredientID = new IngredientDAL().FindByName(ingredient.Name).Id;
            int dishID = new DishDAL().FindByName(dish.Name).Id;

            DBConnection conn = new();
            if (conn.Open())
            {
                string cmd =
                    "INSERT INTO ingredientdishrelation" +
                        "(ingredientID, dishID)" +
                    "VALUES" +
                        $"('{ingredientID}', '{dishID}'); ";
                conn.RunCommand(cmd);
                conn.Close();
                return true;
            }
            return false;
        }

        public bool Remove(IngredientDTO ingredient)
        {
            DBConnection conn = new();
            if (conn.Open())
            {
                string cmd =
                    "DELETE " +
                        "ingredientdishrelation.* " +
                    "FROM " +
                        "ingredientdishrelation " +
                    "JOIN " +
                        "ingredient " +
                    "ON " +
                        "ingredient.id = ingredientdishrelation.ingredientID " +
                    "WHERE " +
                       $"ingredient.name = '{ingredient.Name}'";
                conn.RunCommand(cmd);
                conn.Close();
                return true;
            }
            return false;
        }

        public bool Remove(DishDTO dish)
        {
            DBConnection conn = new();
            if (conn.Open())
            {
                string cmd =
                    "DELETE " +
                        "ingredientdishrelation.* " +
                    "FROM " +
                        "ingredientdishrelation " +
                    "JOIN " +
                        "dish " +
                    "ON " +
                        "dish.id = ingredientdishrelation.dishID " +
                    "WHERE " +
                       $"dish.name = '{dish.Name}'";
                conn.RunCommand(cmd);
                conn.Close();
                return true;
            }
            return false;
        }

        public bool Remove(IngredientDTO ingredient, DishDTO dish)
        {
            DBConnection conn = new();
            if (conn.Open())
            {
                string cmd =
                    "DELETE " +
                        "ingredientdishrelation.* " +
                    "FROM " +
                        "ingredientdishrelation " +
                    "JOIN " +
                        "dish " +
                    "ON " +
                        "dish.id = ingredientdishrelation.dishID " +
                    "JOIN " +
                        "ingredient " +
                    "ON " +
                        "ingredient.id = ingredientdishrelation.ingredientID " +
                    "WHERE " +
                       $"ingredient.name = '{ingredient.Name}' " +
                    "AND " +
                       $"dish.name = '{dish.Name}'";
                conn.RunCommand(cmd);
                conn.Close();
                return true;
            }
            return false;
        }
    }
}
