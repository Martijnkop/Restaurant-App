namespace Restaurant.Interface.ingredient
{
    public interface IIngredientDAL
    {
        bool Add(string name, int diet);
        bool Remove(string name);
        bool Update(string oldName, string newName, int newDiet);
    }
}
