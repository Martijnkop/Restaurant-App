using Restaurant.Data;
using Restaurant.Factory;
using Restaurant.Interface.ingredient;
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
                ingredients.Add(new Ingredient(dbIngredient.Name, dbIngredient.Diet));
            }

            return ingredients;
        }

        public Ingredient FindByName(string name)
        {
            IngredientDTO dto = ingredientContainerDAL.FindByName(name);

            return new Ingredient(dto.Name, dto.Diet);
        }

        public List<Ingredient> FindByNames(List<string> names)
        {
            List<Ingredient> ingredients = new();
            foreach (string name in names)
            {
                ingredients.Add(FindByName(name));
            }

            return ingredients;
        }
    }
}

