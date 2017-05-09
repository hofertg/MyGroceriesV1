using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGroceries.Data;
using MyGroceries.ViewModels.ItemViewModels;
using MyGroceries.Models;


namespace MyGroceries.Controllers
{
    public class ItemController : Controller
    {
        private MyGroceriesDbContext context;

        public ItemController(MyGroceriesDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IndexItemViewModel indexItemViewModel =
                new IndexItemViewModel(context.Items.OrderBy(i => i.Name).ToList(), 
                context.Units.OrderBy(u => u.Name).ToList());
            return View(indexItemViewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexItemViewModel indexItemViewModel)
        {
            if (ModelState.IsValid)
            {
                Unit unit = context.Units.Single(u => u.ID == indexItemViewModel.UnitID);
                Item item = new Item(indexItemViewModel.Name, unit);
                item.AddItemToContext(context);

                return Redirect($"/Item/Index");
            }

            indexItemViewModel.AddItems(context.Items.OrderBy(i => i.Name).ToList(), 
                context.Units.OrderBy(u => u.Name).ToList());

            return View(indexItemViewModel);
        }

        public IActionResult Delete()
        {
            DeleteItemViewModel deleteItemViewModel =
                new DeleteItemViewModel(context.Items.ToList());
            return View(deleteItemViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int[] items)
        {
            foreach (int itemId in items)
            {
                Item theItem = context.Items.Single(sl => sl.ID == itemId);
                context.Items.Remove(theItem);
            }

            context.SaveChanges();

            return Redirect("/Item");
        }
    }
}
