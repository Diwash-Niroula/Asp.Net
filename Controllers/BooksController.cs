using BookExchange.Context;
using BookExchange.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookExchange.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public BooksController(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,Bookname,phoneNo,Email,Date")] Books book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Bookname,phoneNo,Email,Date")] Books book)
        {
            if (id != book.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
