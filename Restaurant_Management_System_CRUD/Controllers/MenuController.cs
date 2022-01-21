using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Restaurant_Management_System_CRUD.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace Restaurant_Management_System_CRUD.Controllers
{

    public class MenuController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment _hostEnvironment;


        public MenuController(ApplicationDbContext _db, IWebHostEnvironment webHostEnvironment)
        {
            db = _db;
            _hostEnvironment = webHostEnvironment;

        }
        // GET: MenuController
        public IActionResult Index(string Sorting_Order, string Filter_Value, string Search_Data , int pg=1 )
        {
            ViewBag.CurrentSort = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
            ViewBag.SortingCategory = String.IsNullOrEmpty(Sorting_Order) ? "Category_Description" : "";
            ViewBag.SortingPrice = String.IsNullOrEmpty(Sorting_Order) ? "Price" : "";
            //var data = await db.Menu.Include("Category").ToListAsync();

            if (Search_Data != null)
            {
                pg = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;

            var dataa = from _menu in db.Menu.Include("Category") select _menu;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                dataa =  dataa.Where(_menu => _menu.Name.ToLower().Contains(Search_Data.ToLower()));
            }
            switch (Sorting_Order)
            {
                case "Name_Description":
                    dataa = dataa.OrderByDescending(_menu => _menu.Name);
                    break;
                case "Category_Description":
                    dataa = dataa.OrderBy(_menu => _menu.Category);
                    break;
                case "Price":
                    dataa = dataa.OrderBy(_menu => _menu.Price);
                    break;
                default:
                    dataa = dataa.OrderBy(_menu => _menu.Name);
                    break;
            }

            /*            return View(dataa.ToList());*/

            //pagination
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int menuCount = dataa.Count();
            var pager = new Pager(menuCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var menuRecords = dataa.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(menuRecords.ToList());

        }

        // GET: MenuController/Details/5
        public IActionResult Details(int id)
        {
            var _details = db.Menu.Find(id);
            ViewData["categories"] = db.Categories.ToList();
            return View(_details);
        }

        List<Cart> li = new List<Cart>();
        [HttpPost]
        public IActionResult Details(Menu m, int id, int qty)
        {
            var sessionStr = HttpContext.Session.GetString(StrConsts.SessionString.Cart);
            sessionStr = sessionStr ?? "";
            var sessionObj = JsonConvert.DeserializeObject<List<CartViewModel>>(sessionStr);
            if (sessionObj==null || sessionObj.Count==0)
            {
                sessionObj = new List<CartViewModel>();
                //add the item to list and save it to session
                var menu = db.Menu.Find(id);
                if (menu!=null)
                {
                    sessionObj.Add(new CartViewModel()
                    {
                        MenuId = menu.Id,
                        MenuName = menu.Name,
                        Price = menu.Price,
                        Quantity = qty
                    });
                }
            }
            else
            {
                var existingCart = sessionObj.FirstOrDefault(p => p.MenuId == id);
                if (existingCart==null)
                {
                    var menu = db.Menu.Find(id);
                    if (menu != null)
                    {
                        sessionObj.Add(new CartViewModel()
                        {
                            MenuId = menu.Id,
                            MenuName = menu.Name,
                            Price = menu.Price,
                            Quantity = qty
                        });
                    }
                }
                else
                {
                    existingCart.Quantity += qty;
                }
            }
            HttpContext.Session.SetString(StrConsts.SessionString.Cart, JsonConvert.SerializeObject(sessionObj));

            //return View("Checkout");
            return RedirectToAction("Checkout");


            //            Menu menu = db.Menu.Find(id);
            //            Cart cart = new Cart();
            //            cart.menuId = menu.Id;
            //            cart.menuName = menu.Name;
            //            cart.price = Convert.ToInt32(menu.Price);
            //            cart.qty = Convert.ToInt32(qty);
            //            cart.bill = cart.price * cart.qty;

            //            if (HttpContext.Session.GetString("cart") == null)
            //            {
            //                li.Add(cart);
            ///*                TempData["CartData"] = li;
            //*/               


            //                HttpContext.Session.SetString("cart", li.ToString());
            //                HttpContext.Session.GetString("cart");

            //            }
            //            /*      else
            //                  {
            //                      List<Cart> li2 = TempData["cart"] as List<Cart>;
            //                      li2.Add(cart);
            //                      TempData["cart"] = li2;
            //                  }*/



            //            TempData.Keep();

            //            return View("Checkout");

            //            /*            return RedirectToAction("Checkout");
            //            */
        }

        public IActionResult Checkout()
        {
            
            return View();
        }

        // GET: MenuController/Create
        public async Task<IActionResult> Create()
        {

            var model = new Menu();
            ViewData["categories"] = await db.Categories.ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (ModelState.IsValid == true)
            {
                // Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string file = Path.GetFileNameWithoutExtension(menu.ImageFile.FileName);
                string extension = Path.GetExtension(menu.ImageFile.FileName);
                menu.Image = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                IFormFile postedFile = menu.ImageFile;
                long length = postedFile.Length;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (length <= 1000000)
                    {
                        string path = Path.Combine(wwwRootPath + "/Image/", file);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await menu.ImageFile.CopyToAsync(fileStream);
                        }
                        db.Menu.Add(menu);
                        int Student = await db.SaveChangesAsync();
                        if (Student > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Inserted Successfully.')</script>";
                            ModelState.Clear();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Not Inserted.')</script>";

                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image file should be less than 1mb')</script>";
                    }
                }
                else
                {
                    TempData["ExtentionMessage"] = "<script>alert('Format Not Supported')</script>";
                }

            }
            return View();
        }

        // GET: MenuController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewData["categories"] = db.Categories.ToList();
            var _edit = await db.Menu.FindAsync(id);
            TempData["imgPath"] = _edit.Image;
            return View(_edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Menu menu)
        {
            if (ModelState.IsValid == true)
            {
                if (menu.ImageFile != null)
                {
                    // Update image to wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(menu.ImageFile.FileName);
                    string extension = Path.GetExtension(menu.ImageFile.FileName);
                    IFormFile postedFile = menu.ImageFile;
                    long length = postedFile.Length;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        if (length <= 1000000)
                        {
                            menu.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/Image/" + fileName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await menu.ImageFile.CopyToAsync(fileStream);
                            }

                            db.Entry(menu).State = EntityState.Modified;
                            string imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image/", TempData["imgPath"].ToString());
                            var _menu = await db.SaveChangesAsync();

                            if (_menu > 0)
                            {
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                                TempData["UpdateMessage"] = "<script>alert('Data Updatxed Successfully.')</script>";
                                ModelState.Clear();
                               
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data Not Updated.')</script>";
                                return View();
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size should be less than 1 MB')</script>";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                        return View();
                    }
                }
                else
                {
                    menu.Image = TempData["imgPath"].ToString();
                    db.Entry(menu).State = EntityState.Modified;
                    int _Menu = await db.SaveChangesAsync();
                    if (_Menu > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully.')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Not Updated.')</script>";
                    }

                }
            }
            return View();
        }

        // GET: MenuController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menu = await db.Menu.FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var menu = await db.Menu.FindAsync(id);
                var imagePath = menu.Image.ToString();
                imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image/", imagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                db.Menu.Remove(menu);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
