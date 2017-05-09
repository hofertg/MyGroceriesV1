using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGroceries.Data;
using MyGroceries.Models;
using MyGroceries.ViewModels.UnitViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyGroceries.Controllers
{
    public class UnitController : Controller
    {

        private MyGroceriesDbContext context;

        public UnitController(MyGroceriesDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IndexUnitViewModel indexUnitViewModel =
                new IndexUnitViewModel(context.Units.OrderBy(u => u.Name).ToList());
            return View(indexUnitViewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexUnitViewModel indexUnitViewModel)
        {
            if (ModelState.IsValid)
            {

                Unit newUnit = new Unit
                {
                    Name = indexUnitViewModel.Name,
                    BaseUnit = indexUnitViewModel.BaseUnit,
                    ConversionToBaseFactor = indexUnitViewModel.ConversionToBaseFactor
                };

                context.Units.Add(newUnit);
                context.SaveChanges();

                return Redirect($"/Unit/Index");
            }
            indexUnitViewModel.AddUnits(context.Units.OrderBy(u => u.Name).ToList());

            return View(indexUnitViewModel);
        }

        public IActionResult Delete()
        {
            DeleteUnitViewModel deleteUnitViewModel =
                new DeleteUnitViewModel(context.Units.OrderBy(u => u.Name).ToList());
            return View(deleteUnitViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int[] units)
        {
            foreach (int unitId in units)
            {
                Unit theUnit = context.Units.Single(sl => sl.ID == unitId);
                context.Units.Remove(theUnit);
            }

            context.SaveChanges();

            return Redirect("/Unit");
        }
    }
}
