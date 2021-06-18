using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.TestDAL
{
    public class IngredientTestDAL : IIngredientDAL, IIngredientContainerDAL
    {
        public List<IngredientDTO> TestList = new();

        public bool Add(string name, int diet)
        {
            TestList.Add(new IngredientDTO { Id = TestList.Count, Name = name, Diet = diet });
            return true;
        }

        public IngredientDTO FindByName(string name)
        {
            foreach (IngredientDTO i in TestList)
            {
                if (i.Name == name) return i;
            }
            return new IngredientDTO();
        }

        public List<IngredientDTO> GetAll()
        {
            return TestList;
        }

        public bool Remove(string name)
        {
            TestList.RemoveAll(i => i.Name == name);
            return true;
        }

        public bool Update(string oldName, string newName, int newDiet)
        {
            int index = TestList.IndexOf(FindByName(oldName));
            TestList[index] = new IngredientDTO { Id = TestList.ElementAt(index).Id, Name = newName, Diet = newDiet };
            return true;
        }
    }
}
