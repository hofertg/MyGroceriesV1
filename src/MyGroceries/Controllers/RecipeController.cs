using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGroceries.Data;
using MyGroceries.ViewModels.RecipeViewModels;
using MyGroceries.Models;
using Microsoft.EntityFrameworkCore;

namespace MyGroceries.Controllers
{
    public class RecipeController : Controller
    {
        private MyGroceriesDbContext context;

        public RecipeController(MyGroceriesDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IndexRecipeViewModel indexRecipeViewModel =
                new IndexRecipeViewModel(context.Recipes.
                OrderByDescending(sl => sl.DateUpdated).ToList());
            return View(indexRecipeViewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexRecipeViewModel indexRecipeViewModel)
        {
            if (ModelState.IsValid)
            {

                Recipe newRecipe = new Recipe
                {
                    Name = indexRecipeViewModel.Name,
                    CreatedBy = "admin",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                context.Recipes.Add(newRecipe);
                context.SaveChanges();

                return Redirect($"/Recipe/AddEdit/{newRecipe.ID}");
            }
            indexRecipeViewModel.AddRecipes(context.Recipes
                .OrderByDescending(sl => sl.DateUpdated)
                .ToList());

            return View(indexRecipeViewModel);
        }


        public IActionResult Delete()
        {
            DeleteRecipeViewModel deleteRecipeViewModel =
                new DeleteRecipeViewModel(context.Recipes
                .OrderByDescending(sl => sl.DateUpdated)
                .ToList());
            return View(deleteRecipeViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int[] recipes)
        {
            foreach (int recipeId in recipes)
            {
                Recipe theRecipe = context.Recipes.Single(sl => sl.ID == recipeId);
                context.Recipes.Remove(theRecipe);
            }

            context.SaveChanges();

            return Redirect("/Recipe");
        }

        public IActionResult View(int id)
        {
            Recipe recipe = context.Recipes.Single(m => m.ID == id);

            List<RecipeItem> ingredients = context
                .RecipeItems
                .Include(item => item.Item)
                .Include(item => item.Item.Unit)
                .Where(cm => cm.RecipeID == id)
                .OrderBy(i => i.Item.Name)
                .ToList();

            ViewRecipeViewModel viewRecipeViewModel = new ViewRecipeViewModel
            {
                Recipe = recipe,
                Ingredients = ingredients
            };

            return View(viewRecipeViewModel);

        }

        public IActionResult AddEdit(int id)
        {
            Recipe recipe = context.Recipes.Single(m => m.ID == id);

            List<RecipeItem> ingredients = context
                .RecipeItems
                .Include(item => item.Item)
                .Where(cm => cm.RecipeID == id)
                .OrderBy(i => i.Item.Name)
                .ToList();
            List<Unit> units = context.Units.OrderBy(u => u.Name).ToList();

            AddEditRecipeViewModel addEditRecipeViewModel = new AddEditRecipeViewModel
            {
                RecipeID = id,
                Name = recipe.Name,
                Directions = recipe.Directions,
            };

            addEditRecipeViewModel.AddItems(ingredients);

            return View(addEditRecipeViewModel);

        }

        [HttpPost]
        public IActionResult AddEdit(int id, AddEditRecipeViewModel addEditRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                Recipe recipe = context.Recipes.Single(m => m.ID == id);
                recipe.Name = addEditRecipeViewModel.Name;
                recipe.Directions = addEditRecipeViewModel.Directions;
                recipe.DateUpdated = DateTime.UtcNow;

                context.SaveChanges();

                return Redirect($"/Recipe/View/{id}");
            }
            
            List<RecipeItem> ingredients = context
            .RecipeItems
            .Include(item => item.Item)
            .Where(cm => cm.RecipeID == id)
            .OrderBy(i => i.Item.Name)
            .ToList();
            List<Unit> units = context.Units.OrderBy(u => u.Name).ToList();

            addEditRecipeViewModel.AddItems(ingredients);

            return View(addEditRecipeViewModel);

        }

        public IActionResult AddIngredient(int id)
        {
            Recipe recipe = context.Recipes.Single(r => r.ID == id);

            List<RecipeItem> ingredients = context
            .RecipeItems
            .Include(item => item.Item)
            .Where(cm => cm.RecipeID == id)
            .OrderBy(i => i.Item.Name)
            .ToList();

            List<Unit> units = context.Units.OrderBy(u => u.Name).ToList();

            AddIngredientRecipeViewModel addIngredientRecipeViewModel = new AddIngredientRecipeViewModel
            {
                Recipe = recipe
            };

            addIngredientRecipeViewModel.AddItems(ingredients, units);

            return View(addIngredientRecipeViewModel);
        }

        [HttpPost]
        public IActionResult AddIngredient(int id, AddIngredientRecipeViewModel addIngredientRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                Unit unit = context.Units.Single(u => u.ID == addIngredientRecipeViewModel.UnitID);
                Item item = new Item(addIngredientRecipeViewModel.Name, unit);
                item.AddItemToContext(context);

                item = context.Items.Single(i => i.Name == addIngredientRecipeViewModel.Name && i.UnitID == unit.ID);
                Recipe recipe = context.Recipes.Single(r => r.ID == id);
                double quantity = addIngredientRecipeViewModel.Quantity;

                RecipeItem newRecipeItem = new RecipeItem(item, recipe, quantity);
                newRecipeItem.AddRecipeItemToContext(context);

                return Redirect($"/Recipe/AddEdit/{id}");
            }

            addIngredientRecipeViewModel.Recipe = context.Recipes.Single(r => r.ID == id);
            List<RecipeItem> ingredients = context
            .RecipeItems
            .Include(item => item.Item)
            .Where(cm => cm.RecipeID == id)
            .OrderBy(i => i.Item.Name)
            .ToList();
            List<Unit> units = context.Units.OrderBy(u => u.Name).ToList();

            addIngredientRecipeViewModel.AddItems(ingredients, units);

            return View(addIngredientRecipeViewModel);

        }

        public IActionResult RemoveIngredient(int id)
        {
            Recipe recipe = context.Recipes.Single(r => r.ID == id);

            List<RecipeItem> ingredients = context
                .RecipeItems
                .Include(item => item.Item)
                .Where(ri => ri.RecipeID == id)
                .Include(item => item.Item.Unit)
                .OrderBy(i => i.Item.Name)
                .ToList();

            RemoveIngredientRecipeViewModel removeIngredientRecipeViewModel = new RemoveIngredientRecipeViewModel
            {
                Recipe = recipe,
                Items = ingredients
            };

            return View(removeIngredientRecipeViewModel);
        }

        [HttpPost]
        public IActionResult RemoveIngredient(int id, int[] ingredientIds)
        {
            foreach (int ingredientId in ingredientIds)
            {

                RecipeItem theRecipeItem = context.RecipeItems
                    .Where(cm => cm.RecipeID == id).Single(cm => cm.ItemID == ingredientId);
                context.RecipeItems.Remove(theRecipeItem);

            }
            context.Recipes.Single(r => r.ID == id).DateUpdated = DateTime.UtcNow;
            context.SaveChanges();

            return Redirect($"/Recipe/AddEdit/{id}");
        }

        public IActionResult AddToShoppingList(int id)
        {
            Recipe recipe = context.Recipes.Single(r => r.ID == id);

            List<RecipeItem> ingredients = context
                .RecipeItems
                .Include(item => item.Item)
                .Include(item => item.Item.Unit)
                .Where(ri => ri.RecipeID == id)
                .OrderBy(i => i.Item.Name)
                .ToList();
            List<ShoppingList> shoppingLists = context.ShoppingLists.ToList();

            AddToShoppingListRecipeViewModel addToShoppingListRecipeViewModel = new AddToShoppingListRecipeViewModel
            {
                Recipe = recipe,
            };
            addToShoppingListRecipeViewModel.AddItems(ingredients, shoppingLists);

            return View(addToShoppingListRecipeViewModel);
        }

        [HttpPost]
        public IActionResult AddToShoppingList(int[] ingredientIds, 
            AddToShoppingListRecipeViewModel addToShoppingListRecipeViewModel,
            int recipeId)
        {
            ShoppingList shoppingList = context.
                ShoppingLists.
                Single(sl => sl.ID == addToShoppingListRecipeViewModel.ShoppingListID);
            foreach (int ingredientId in ingredientIds)
            {
                RecipeItem theRecipeItem = context
                    .RecipeItems
                    .Include(ri => ri.Item)
                    .Where(ri => ri.RecipeID == recipeId)
                    .Single(ri => ri.ItemID == ingredientId);

                ShoppingListItem newShoppingListItem = new ShoppingListItem(
                    theRecipeItem.Item, 
                    shoppingList, 
                    theRecipeItem.Quantity);

                newShoppingListItem.AddShoppingListItemToContext(context);
            }

            return Redirect($"/ShoppingList/View/{shoppingList.ID}");
        }
    }
}
