using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;

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
        public ActionResult Index()
        {
            var data = db.Menu.ToList();
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
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu _menu)
        {
            try
            {
                var menu = _menu;
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            var _edit = db.Menu.Find(id);
            return View(_edit);
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Menu edit)
        {
            try
            {
                var model = db.Menu.Find(id);
                model.Menu_Name = edit.Menu_Name;
                model.Price = edit.Price;
                model.Description = edit.Description;
                db.Menu.Update(model);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
