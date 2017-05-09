using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGroceries.Data;
using MyGroceries.ViewModels.ShoppingListViewModels;
using MyGroceries.Models;
using Microsoft.EntityFrameworkCore;


namespace MyGroceries.Controllers
{
    public class ShoppingListController : Controller
    {

        private MyGroceriesDbContext context;

        public ShoppingListController(MyGroceriesDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IndexShoppingListViewModel indexShoppingListViewModel = 
                new IndexShoppingListViewModel(context.ShoppingLists.
                OrderByDescending(sl => sl.DateUpdated).ToList());
            return View(indexShoppingListViewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexShoppingListViewModel indexShoppingListViewModel)
        {
            if (ModelState.IsValid)
            {

                ShoppingList newShoppingList = new ShoppingList
                {
                    Name = indexShoppingListViewModel.Name,
                    CreatedBy = "admin",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                context.ShoppingLists.Add(newShoppingList);
                context.SaveChanges();

                return Redirect($"/ShoppingList/View/{newShoppingList.ID}");
            }
            indexShoppingListViewModel.AddShoppingLists(context.ShoppingLists
                .OrderByDescending(sl => sl.DateUpdated)
                .ToList());

            return View(indexShoppingListViewModel);
        }
       

        public IActionResult Delete()
        {
            DeleteShoppingListViewModel deleteShoppingListViewModel = 
                new DeleteShoppingListViewModel(context.ShoppingLists
                .OrderByDescending(sl => sl.DateUpdated)
                .ToList());
            return View(deleteShoppingListViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int[] shoppingLists)
        {
            foreach (int shoppingListId in shoppingLists)
            {
                ShoppingList theShoppingList = context.ShoppingLists.Single(sl => sl.ID == shoppingListId);
                context.ShoppingLists.Remove(theShoppingList);
            }

            context.SaveChanges();

            return Redirect("/ShoppingList");
        }

        public IActionResult View(int id)
        {
            ShoppingList shoppingList = context.ShoppingLists.Single(sl => sl.ID == id);
            List<ShoppingListItem> items = context
                .ShoppingListItems
                .Include(item => item.Item)
                .Where(sl => sl.ShoppingListID == id)
                .OrderByDescending(sli => sli.DateUpdated)
                .ToList();
            //List<Item> selectItems = context.Items.OrderBy(i => i.Name).ToList();
            List<Unit> units = context.Units.OrderBy(u => u.Name).ToList();

            ViewShoppingListViewModel viewShoppingListViewModel = new ViewShoppingListViewModel
            {
                ShoppingList = shoppingList,
            };
            viewShoppingListViewModel.AddItems(items, units);

            return View(viewShoppingListViewModel);
        }

        
        [HttpPost]
        public IActionResult View(int id, ViewShoppingListViewModel viewShoppingListViewModel)
        {

            if (ModelState.IsValid)
            {
                Unit unit = context.Units.Single(u => u.ID == viewShoppingListViewModel.UnitID);
                Item item = new Item(viewShoppingListViewModel.Name, unit);
                item.AddItemToContext(context);

                item = context.Items.Single(i => i.Name == viewShoppingListViewModel.Name && i.UnitID == unit.ID);
                ShoppingList shoppingList = context.ShoppingLists.Single(sl => sl.ID == id);
                double quantity = viewShoppingListViewModel.Quantity;

                ShoppingListItem newShoppingListItem = new ShoppingListItem(item, shoppingList, quantity);
                newShoppingListItem.AddShoppingListItemToContext(context);

                return Redirect($"/ShoppingList/View/{id}");
            }

            viewShoppingListViewModel.ShoppingList = context.
                ShoppingLists.
                Single(sl => sl.ID == id);

            List<ShoppingListItem> items = context
                .ShoppingListItems
                .Include(item => item.Item)
                .Where(sl => sl.ShoppingListID == id)
                .OrderByDescending(sli => sli.DateUpdated)
                .ToList();

            List<Unit> units = context.Units.OrderBy(u => u.Name).ToList();

            viewShoppingListViewModel.AddItems(items, units);

            return View(viewShoppingListViewModel);
        }
        

        public IActionResult Manage(int id)
        {
            ShoppingList shoppingList = context.ShoppingLists.Single(sl => sl.ID == id);

            List<ShoppingListItem> items = context
                .ShoppingListItems
                .Include(item => item.Item)
                .Include(item => item.Item.Unit)
                .Where(sl => sl.ShoppingListID == id)
                .OrderByDescending(sli => sli.DateUpdated)
                .ToList();

            ManageShoppingListViewModel manageShoppingListViewModel = new ManageShoppingListViewModel
            {
                ShoppingList = shoppingList,
                Items = items
            };

            return View(manageShoppingListViewModel);
        }

        [HttpPost]
        public IActionResult Manage(int id, int[] itemIds, string action)
        {

            foreach (int itemId in itemIds)
            {
                ShoppingListItem theShoppingListItem = context.ShoppingListItems
                        .Where(sli => sli.ShoppingListID == id)
                        .Single(sli => sli.ItemID == itemId);

                if (action == "delete")
                {
                    context.ShoppingListItems.Remove(theShoppingListItem);
                }
                else if (action == "strikethrough")
                {
                    if (theShoppingListItem.StrikeThrough == false)
                        theShoppingListItem.StrikeThrough = true;
                    else
                        theShoppingListItem.StrikeThrough = false;
                }
            }
            context.ShoppingLists.Single(sl => sl.ID == id).DateUpdated = DateTime.UtcNow;
            context.SaveChanges();

            return Redirect($"/ShoppingList/View/{id}");

        }
        

    }
}
