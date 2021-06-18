using Restaurant.Factory;
using Restaurant.Interface.ingredient;

namespace Restaurant.Logic.ingredient
{
    public class Ingredient
    {
        public string Name { get; private set; }
        public int Diet { get; private set; }

        private IIngredientDAL ingredientDAL;

        public Ingredient(string name, int diet = 0, IIngredientDAL dal = null)
        {
            if (dal != null) this.ingredientDAL = dal;
            else this.ingredientDAL = IngredientFactory.CreateIIngredientDal();
            this.Name = name;
            this.Diet = diet;
        }

        public bool Update(string oldName, IIngredientContainerDAL dal = null)
        {
            if (ExistsInDatabase(this.Name, dal) && this.Name != oldName) return false;

            if (!ExistsInDatabase(oldName, dal)) return false;

            if (this.Diet != 0 && this.Diet != 1 && this.Diet != 2) return false;

            return ingredientDAL.Update(oldName, this.Name, this.Diet);
        }

        public bool Add(IIngredientContainerDAL containerDAL = null)
        {
            Ingredient i = new IngredientContainer(containerDAL).FindByName(this.Name);
            if (ExistsInDatabase(this.Name, containerDAL)) return false;

            if (this.Diet != 0 && this.Diet != 1 && this.Diet != 2) return false;

            return ingredientDAL.Add(this.Name, this.Diet);
        }

        public bool Remove(IIngredientContainerDAL containerDAL = null)
        {
            if (!ExistsInDatabase(this.Name, containerDAL)) return false;
            return ingredientDAL.Remove(this.Name);
        }

        private bool ExistsInDatabase(string name, IIngredientContainerDAL containerDAL = null)
        {
            return new IngredientContainer(containerDAL).FindByName(name).Name == name;
        }
    }
}
