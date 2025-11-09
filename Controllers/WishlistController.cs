using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using story_brook_api.Data;
using story_brook_api.Dtos;
using story_brook_api.Models;

namespace story_brook_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WishListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Wishlist
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<WishBook>>> GetWishListByUser(string userId)
        {
            return await _context.WishList
            .Include(w => w.User)
            .Where(w => w.UserId == userId).ToListAsync();
        }

        // GET: api/Wishlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishBook>> GetWishList(string id)
        {
            var wishList = await _context.WishList.FindAsync(id);

            if (wishList == null)
            {
                return NotFound();
            }

            return wishList;
        }

        // POST: api/Wishlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WishBook>> PostWishlist(WishBookDto wishbookDto)
        {
            WishBook wishBook = new WishBook
            {
                Id = Guid.NewGuid().ToString(),
                BookId = wishbookDto.BookId,
                User = await _context.Users.FindAsync(wishbookDto.UserId)
            };
            _context.WishList.Add(wishBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WishListExists(wishBook.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetWishList), new { id = wishBook.Id }, wishBook);
        }

        // DELETE: api/Wishlist/5
        [HttpDelete("{userId}/{bookId}")]
        public async Task<IActionResult> DeleteWishlist(string userId, string bookId)
        {
            var wishList = await _context.WishList
                .Include(w => w.User)
                .Where(w => w.UserId == userId && w.BookId == bookId).FirstOrDefaultAsync();
            if (wishList == null)
            {
                return NotFound();
            }

            _context.WishList.Remove(wishList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishListExists(string id)
        {
            return _context.WishList.Any(e => e.Id == id);
        }
    }
}
