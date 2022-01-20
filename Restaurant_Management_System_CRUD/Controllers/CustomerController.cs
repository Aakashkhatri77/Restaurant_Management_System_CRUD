using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;
using Restaurant_Management_System_CRUD.ViewModel;
using Restaurant_Management_System_CRUD.Views.Shared.Components.SearchBar;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext db;
        public CustomerController(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        private List<SelectListItem> GetPageSizes(int selectedPageSize = 10)
        {
            var pageSizes = new List<SelectListItem>();
            if (selectedPageSize == 5)
                pageSizes.Add(new SelectListItem("5", "5", true));
            else
                pageSizes.Add(new SelectListItem("5", "5"));

            for (int lp = 10; lp <= 100; lp+=10)
            {
                if (lp == selectedPageSize)
                {
                    pageSizes.Add(new SelectListItem(lp.ToString(), lp.ToString(), true));
                }
                else
                {
                    pageSizes.Add(new SelectListItem(lp.ToString(), lp.ToString()));
                }
            }
                return pageSizes;
        }

        // GET: CustomerController
        public ActionResult Index(string Sorting_Order, string Search_Data, int pg = 1, int pageSize = 5)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

            var dataa = from _name in db.RestuarantCustomer select _name;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                dataa = dataa.Where(_name => _name.Name.ToLower().Contains(Search_Data.ToLower()));
            }

            //Sorting
            switch (Sorting_Order)
            {
                case "Name_Description":
                    dataa = dataa.OrderByDescending(_name => _name.Name);
                    break;
                default:
                    dataa = dataa.OrderBy(_menu => _menu.Name);
                    break;
            }
            //pagination
            //const int pageSize = 4;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = dataa.Count();
            int recSkip = (pg - 1) * pageSize;
            List<Customer> recCustomers = dataa.Skip(recSkip).Take(pageSize).ToList();
            SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "Index", Controller = "Customer", SearchData = "SearchData" };
            ViewBag.SearchPager = SearchPager;
            this.ViewBag.pageSizes = GetPageSizes(pageSize);

            return View(recCustomers); 

          
            /*var menuRecords = dataa.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;*/



            //return View(menuRecords.ToList());
            /*            return View(dataa.ToList());*/


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
            var customer = new Customer()
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
