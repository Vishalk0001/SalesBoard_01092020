using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesBoard.Data;
using SalesBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesBoard
{

    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _session;

        public ItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor session)
        {
            _userManager = userManager;
            _context = context;
            _session = session;
        }

        // GET: All Items User & Admin can access
        
        public async Task<IActionResult> Index()
        {
            // ALl Items 
            _session.HttpContext.Session.SetString("IsMyItem", "NO");
            _session.HttpContext.Session.SetString("IsMyPage", "false");
            var result = View(await _context.Items.ToListAsync());
            return result; 
        }

        // GET: Items/myItems   
        public ActionResult MyItems()
        {
            _session.HttpContext.Session.SetString("IsMyItem", "YES");
            var seller = _userManager.GetUserName(User);
            var items = _context.Items
                .Where(m => m.Seller == seller);
            return View("Index",items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Items/Create 
       // [Authorize(Policy = "AdminPolicy")]
        public IActionResult Create()
        { 
                return View(); 
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Quantity")] Items items)
        {
            if (ModelState.IsValid)
            {
                // get the seller
                var seller = _userManager.GetUserName(User);
                items.Seller = seller;
                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Quantity")] Items items)
        {
            if (id != items.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(items);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(items.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var items = await _context.Items.FindAsync(id);
            _context.Items.Remove(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Items/Purchase/5
       
        public async Task<IActionResult> Purchase(int? id)
        {
            if (!_session.HttpContext.Session.IsAvailable || !_session.HttpContext.Session.Keys.Contains("isAdmin"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var items = await _context.Items
                    .FirstOrDefaultAsync(m => m.Id == id);
                ViewData["maxQuantity"] = items.Quantity;
                items.Quantity = 1; // Default

                if (items == null)
                {
                    return NotFound();
                }

                return View(items);
            }
            else
            {
                var res = await _context.Items.ToListAsync();
                return View();
            }
        }

        // POST: Items/Purchase/5
        [HttpPost, ActionName("Purchase")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseConfirmed([Bind("Item,Quantity")] Cart cart)
        {
            // get or create a cart id
            string cartId = _session.HttpContext.Session.GetString("cartId");

            if (string.IsNullOrEmpty(cartId) == true) cartId = Guid.NewGuid().ToString();

            // use the cart id
            cart.CartId = cartId.ToString();
            //cart.Name =
          //  cart.ItemName =
            // make the sale
            _context.Add(cart);

            // Save the changes
            await _context.SaveChangesAsync();

            // add to cart
            var checkCount = _session.HttpContext.Session.GetInt32("cartCount");
            int cartCount = checkCount == null ? 0 : (int) checkCount;
            _session.HttpContext.Session.SetString("cartId", cartId.ToString());
            _session.HttpContext.Session.SetInt32("cartCount", ++cartCount);

            return RedirectToAction(nameof(Index));
        }

        private bool ItemsExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        //MS : Overload to searching only 
        [HttpPost]
        public async Task<IActionResult> Index(string searchString, bool notUsed)
        {
            var itm = from m in _context.Items
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                itm = itm.Where(s => s.Name.Contains(searchString));
            }

            return View(await itm.ToListAsync());
        }
        
    }
}
