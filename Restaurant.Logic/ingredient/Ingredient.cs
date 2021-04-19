using Restaurant.Factory;
using Restaurant.Interface.ingredient;

namespace Restaurant.Logic.ingredient.models
{
    public class Ingredient
    {
        public string Name { get; private set; }
        public int Diet { get; private set; }

        private IIngredientDAL ingredientDAL;

        public Ingredient(string name, int diet)
        {
            ingredientDAL = IngredientFactory.CreateIIngredientDal();
            this.Name = name;
            this.Diet = diet;
        }

        public Ingredient(string name)
        {
            ingredientDAL = IngredientFactory.CreateIIngredientDal();
            this.Name = name;
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
