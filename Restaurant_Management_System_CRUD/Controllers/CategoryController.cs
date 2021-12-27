using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public string CategoryName { get; private set; }

        public CategoryController(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var Category = db.Categories.ToList();
            return View(Category);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = db.Categories.Find(id);
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            var _category = new Category()
            {
                Name = category.Name,
                Description = category.Description,
            };
            try
            {
                db.Categories.Add(_category);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var _category = db.Categories.Find(id);
            return View(_category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
          
      
            try
            {
                var _category = db.Categories.Find(id);
                _category.Name = category.Name;
                _category.Description = category.Description;
                db.Categories.Update(_category);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var _category = db.Categories.Find(id);
            return View(_category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                var CategoryDelete = db.Categories.Find(id);
                db.Categories.Remove(CategoryDelete);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
