using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class TableController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TableController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;       
        }
      
        public IActionResult Index()
        {
            var model = _context.Tables.ToList();
            return View(model);
        }

        public IActionResult Create(int id)
        {
            var tables = new Table();
            if (id == 0)
            {
                return View(tables);
            }
            else
            {
                var data = _context.Tables.Find(id);
                return View(data);
            }
        }

        [HttpPost]
        public IActionResult Create(int id, Table table)
        {
            if (table.Id == 0)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string file = Path.GetFileNameWithoutExtension(table.ImageFile.FileName);
                string extension = Path.GetExtension(table.ImageFile.FileName);
                table.Image = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                IFormFile postedFile = table.ImageFile;
                long length = postedFile.Length;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (length <= 1000000)
                    {
                        string path = Path.Combine(wwwRootPath + "/TableImage/", file);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            table.ImageFile.CopyToAsync(fileStream);
                        }
                        _context.Tables.Add(table);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        public IActionResult Edit (int id)
        {
            var table = _context.Tables.Find(id);
            TempData["imgPath"] = table.Image;
            return View(table);
        }

        [HttpPost]
        public IActionResult Edit(int id, Table table)
        {
            if (table.ImageFile != null)
            {
                // Update image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(table.ImageFile.FileName);
                string extension = Path.GetExtension(table.ImageFile.FileName);
                IFormFile postedFile = table.ImageFile;
                long length = postedFile.Length;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (length <= 1000000)
                    {
                        table.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/TableImage/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            table.ImageFile.CopyToAsync(fileStream);
                        }

                        _context.Entry(table).State = EntityState.Modified;
                        string imagePath = Path.Combine(_hostEnvironment.WebRootPath, "TableImage/", TempData["imgPath"].ToString());
                        int _table = _context.SaveChanges();
                        if (_table > 0)
                        {
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            else
            {
                table.Image = TempData["imgPath"].ToString();
                _context.Entry(table).State = EntityState.Modified;
                _context.SaveChanges();
                    return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var table = _context.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var table = _context.Tables.Find(id);
                var imagePath = table.Image.ToString();
                imagePath = Path.Combine(_hostEnvironment.WebRootPath, "TableImage/", imagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.Tables.Remove(table);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
