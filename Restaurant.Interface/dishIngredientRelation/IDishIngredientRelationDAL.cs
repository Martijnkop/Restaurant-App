using Restaurant.Interface.dish;
using Restaurant.Interface.ingredient;

namespace Restaurant.Interface.dishIngredientRelation
{
    public interface IDishIngredientRelationDAL
    {
        bool Add(IngredientDTO ingredient, DishDTO dish);
        bool Remove(IngredientDTO ingredient, DishDTO dish);
        bool Remove(IngredientDTO ingredient);
        bool Remove(DishDTO dish);
    }
}
