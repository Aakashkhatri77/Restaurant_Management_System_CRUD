using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;
using Restaurant_Management_System_CRUD.ViewModel;
using System.Dynamic;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class ViewCustomerController : Controller
    {
        private readonly ApplicationDbContext db;

        public ViewCustomerController(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        // GET: ViewCustomerController
        public ActionResult Index()
        {

            var data = db.RestuarantCustomer.ToList();
            return View(data);
        }

        // GET: ViewCustomerController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: ViewCustomerController/Create
        public ActionResult Create()
        {

                   return View();
        }

        // POST: ViewCustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewCustomer viewCustomer)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
          
        }

        // GET: ViewCustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ViewCustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ViewCustomer viewCustomer)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewCustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViewCustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
