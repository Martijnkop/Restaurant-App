using Restaurant.Data;
using Restaurant.Data.Models;
using Restaurant.Logic.ingredient.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic.ingredient
{
    public class IngredientLogic
    {
        public void Add(string name, int diet)
        {
            DBIngredient.Add(name, diet);
        }

        public void Remove(string name) => DBIngredient.Remove(name);


        public List<IngredientModel> GetAll()
        {
            List<IngredientModel> ingredients = new();
            List<DatabaseIngredient> dbIngredients = DBIngredient.GetAll();

            foreach (DatabaseIngredient dbIngredient in dbIngredients)
            {
                ingredients.Add(new IngredientModel { Name = dbIngredient.Name, Diet = dbIngredient.Diet });
            }

            return ingredients;
        }
    }
}

