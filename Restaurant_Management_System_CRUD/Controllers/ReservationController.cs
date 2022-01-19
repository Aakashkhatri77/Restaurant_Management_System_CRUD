using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }
        //
        public IActionResult Index()
        {
            var data = _context.Reservations.Include(x => x.Table).ToList();
            return View(data);
        }

        public IActionResult Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tableBooking = _context.Reservations.Include("Table").FirstOrDefault(m=>m.Id == id);
            if (tableBooking == null)
            {
                return NotFound();
            }
            return View(tableBooking);
        }

        public IActionResult Create(int id)
        {
            var reservation = new Reservation();
            if (id == 0)
            {
                ViewData["tableType"] = _context.Tables.ToList();
                return View(reservation);
            }
            else
            {
                ViewData["tableType"] = _context.Tables.ToList();
                var model = _context.Reservations.Find(id);
                return View(model);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create (int id, Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (reservation.Id == 0 )
                {
                    _context.Reservations.Add(reservation);
                }
                else
                {

                    _context.Update(reservation);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.Include("Table")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
