using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.RecipeViewModels
{
    public class DeleteRecipeViewModel
    {
        public List<Recipe> Recipes;

        public DeleteRecipeViewModel() { }

        public DeleteRecipeViewModel(IEnumerable<Recipe> recipes)
        {
            Recipes = (List<Recipe>)recipes;
        }
        
    }
}
