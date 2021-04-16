using Restaurant.Data;
using Restaurant.Factory;
using Restaurant.Interface.ingredient;
using Restaurant.Logic.ingredient.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic.ingredient
{
    public class IngredientContainer
    {
        private IIngredientContainerDAL ingredientContainerDAL;
        public IngredientContainer()
        {
            this.ingredientContainerDAL = IngredientFactory.CreateIIngredientContainerDal();
        }

        public List<Ingredient> GetAll()
        {
            List<Ingredient> ingredients = new();
            List<IngredientDTO> dbIngredients = ingredientContainerDAL.GetAll();

            foreach (IngredientDTO dbIngredient in dbIngredients)
            {
                ingredients.Add(new Ingredient { Name = dbIngredient.Name, Diet = dbIngredient.Diet });
            }

            return ingredients;
        }
    }
}

