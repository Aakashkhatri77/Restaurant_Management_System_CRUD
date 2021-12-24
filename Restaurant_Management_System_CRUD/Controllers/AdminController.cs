using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;
using Restaurant_Management_System_CRUD.ViewModel;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;

        public AdminController(ApplicationDbContext _db)
        {
            db = _db;
        }
        // GET: AdminController
        public ActionResult Index()
        {
            var data = db.Admin.ToList();
            return View(data);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            var admin = db.Admin.Find(id);
            return View(admin);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdministratorVm _admin)
        {
            var admin = new Administrator()
            {
                FirstName = _admin.FirstName,
                LastName = _admin.LastName,
                Email = _admin.Email,
                Address = _admin.Address,
                Username = _admin.Username,
                Password = _admin.Password,
                Confirm_Password = _admin.Confirm_Password
            };
            try
            {
                var adminData = admin;
                db.Admin.Add(adminData);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            var _edit = db.Admin.Find(id);
            var vm = new AdministratorVm()
            {
                FirstName = 
            }
            return View(_edit);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Administrator edit)
        {
            try
            {
                var model = db.Admin.Find(id);
                model.FirstName = edit.FirstName;
                model.LastName = edit.LastName;
                model.Email = edit.Email;
                model.Address = edit.Address;
                model.Username = edit.Username;
                model.Password = edit.Password;
                model.Confirm_Password = edit.Confirm_Password;
                db.Admin.Update(model);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            var _delete = db.Admin.Find(id);
            return View(_delete);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Administrator _delete)
        {
            try
            {
                var delete = db.Admin.Find(id);
                db.Admin.Remove(delete);
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
