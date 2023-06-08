using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        // we dont need to create db object from appdbcontext to deal with the db (because of dependency injection)
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            // retrieve all the data from the db to render them along with the page
            // no sql code required
            IEnumerable<Category> objCategoryList = _db.Categories;
            // now pass it
            return View(objCategoryList);
        }

        [HttpGet]
        public IActionResult Create() { return View();}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            // optional validation
            if (obj.Name.Equals(obj.DisplayOrder.ToString())) 
            {
                // key cannot be used two times
                ModelState.AddModelError("CustomError", "Name and DisplayOrder cannot be the same!");
                return View(obj);
            }
            // modelstate is a field that is used in valdation of the user's input
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }
        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            // the user may take out the id that's why we are checking
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // otherwise let's get the item to edit it
            // var objCategory = _db.Categories.FirstOrDefault(x => x.Id == id); // first item with the id or nothing (no exception)
            // var o = _db.Categories.SingleOrDefault(u => u.Id == id); // if more than one element exist returns exception
            var objCategory = _db.Categories.Find(id); // finds by the primary key
            if (objCategory == null)
            {
                return NotFound();
            }
            return View(objCategory);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid) 
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category is Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(Category obj) 
        {
            if (obj.Id == 0) 
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category is Deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}
