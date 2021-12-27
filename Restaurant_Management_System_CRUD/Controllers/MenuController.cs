using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;
using Restaurant_Management_System_CRUD.ViewModel;
using System.Diagnostics;

namespace Restaurant_Management_System_CRUD.Controllers
{

    public class MenuController : Controller
    {
        private readonly ApplicationDbContext db;

        public MenuController(ApplicationDbContext _db)
        {
            db = _db;
        }
        // GET: MenuController
        public async Task<IActionResult> Index()
        {   
            var data = await db.Menu.Include("Category").ToListAsync();
            return View(data);
        }

       

        // GET: MenuController/Details/5
        public ActionResult Details(int id)
        {
            var _details = db.Menu.Find(id);
            return View(_details);
        }

        // GET: MenuController/Create
        public ActionResult Create()
        {
            List<Category> li = new List<Category>();
            li = db.Categories.ToList();
            ViewBag.listofitems = li;
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuVm _menu)
        {

            try
            {
                var menu = new Menu()
                {
                    Name = _menu.Name,
                    Category = _menu.Category,
                    Price = _menu.Price,
                    Description = _menu.Description,
                    CategoryId= _menu.CategoryId,
                };
                //var menu = _Menu;
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            List<Category> li = new List<Category>();
            li = db.Categories.ToList();
            ViewBag.listofitems = li;
                return View();
        }

        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            List<Category> li = new List<Category>();
            li = db.Categories.ToList();
            ViewBag.listofitems = li;

            var _edit = db.Menu.Find(id);
            var vm = new MenuVm()
            {
                Name = _edit.Name,
                Category = _edit.Category,
                Price = _edit.Price,
                Description = _edit.Description,
                CategoryId = _edit.CategoryId,
            };
            return View(vm);
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MenuVm edit)
        {
            try
            {
                var model = db.Menu.Find(id);
                model.Name = edit.Name;
                model.Category = edit.Category;
                model.Price = edit.Price;
                model.Description = edit.Description;
                model.CategoryId = edit.CategoryId;
                db.Menu.Update(model);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            List<Category> li = new List<Category>();
            li = db.Categories.ToList();
            ViewBag.listofitems = li;
        }

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            var _delete = db.Menu.Find(id);
            return View(_delete);
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Menu _delete)
        {
            try
            {
                var MenuDelete = db.Menu.Find(id);
                db.Menu.Remove(MenuDelete);
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
