using Restaurant.Factory;
using Restaurant.Interface.ingredient;

namespace Restaurant.Logic.ingredient.models
{
    public class Ingredient
    {
        public string Name { get; internal set; }
        public int Diet { get; internal set; }

        private IIngredientDAL ingredientDAL;

        public Ingredient()
        {
            ingredientDAL = IngredientFactory.CreateIIngredientDal();
        }

        public void Update(string oldName)
        {
            ingredientDAL.Update(oldName, this.Name, this.Diet);
        }

        public void Add()
        {
            ingredientDAL.Add(this.Name, this.Diet);
        }

        public void Remove() => ingredientDAL.Remove(this.Name);
    }
}
