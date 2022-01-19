using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;
using Restaurant_Management_System_CRUD.ViewModel;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext db;
        public CustomerController(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        // GET: CustomerController
        public ActionResult Index(string Sorting_Order, string Search_Data)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

            var dataa = from _name in db.RestuarantCustomer select _name;
            
                if (!String.IsNullOrEmpty(Search_Data))
                {
                    dataa = dataa.Where(_name => _name.Name.ToLower().Contains(Search_Data.ToLower()));
                }

       
            switch (Sorting_Order)
            {
                case "Name_Description":
                    dataa = dataa.OrderByDescending(_name => _name.Name);
                    break;
                default:
                    dataa = dataa.OrderBy(_menu => _menu.Name);
                    break;
            }

            return View(dataa.ToList());



           /* var customer = from m in db.RestuarantCustomer select m;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                customer = customer.Where(s => s.Name.ToLower().Contains(Search_Data.ToLower()));
            }
            return View(customer);

            var data = db.RestuarantCustomer.ToList();
            return View(data);*/
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var _detail = db.RestuarantCustomer.Find(id);
            return View(_detail);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVm Vm)
        {
            var customer= new Customer()
            {
                Name = Vm.Name,
                Email = Vm.Email,
                Address = Vm.Address,
                Phone = Vm.Phone
            };          
              
                  
                    db.RestuarantCustomer.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
              
                    /*return View();*/
  
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var _edit = db.RestuarantCustomer.Find(id);
            var vm = new CustomerVm()
            {
                Name = _edit.Name,
                Email = _edit.Email,
                Phone = _edit.Phone,
                Address = _edit.Address,
            };
            return View(vm);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerVm Vm)
        {

                var model = db.RestuarantCustomer.Find(id);
                model.Name = Vm.Name;
                model.Email = Vm.Email;
                model.Address = Vm.Address;
                model.Phone = Vm.Phone;

                db.RestuarantCustomer.Update(model);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

                //return View();
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var _delete = db.RestuarantCustomer.Find(id);
            return View(_delete);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Customer _delete)
        {
            try
            {
                var delete = db.RestuarantCustomer.Find(id);
                db.RestuarantCustomer.Remove(delete);
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
