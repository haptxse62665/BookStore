using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChoMoi.Api.Models;
using DemoAPI.Models;
using ChoMoi.Api.Services.Interface;
using ChoMoi.DTOs;

namespace ChoMoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookBuysController : ControllerBase
    {
        private readonly IBookBuyService _iBookBuyService;

        public BookBuysController(IBookBuyService iBookBuyService)
        {
            _iBookBuyService = iBookBuyService;
        }

        // GET: api/BookBuys
        /// <summary>
        /// Get All Book Buy From
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<BookBuyViewModel> GetBookBuy()
        {
            var allBookBuy = _iBookBuyService.GetAll();
            List<BookBuyViewModel> bookBuyViewModels = new List<BookBuyViewModel>();
            foreach (var item in allBookBuy)
            {
                bookBuyViewModels.Add(new BookBuyViewModel(item));
            }
            return bookBuyViewModels;
        }

        // GET: api/BookBuys/5
        /// <summary>
        /// Ger book buy ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetBookBuy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookBuy = _iBookBuyService.FindBy(T => T.Id == id).FirstOrDefault();

            if (bookBuy == null)
            {
                return NotFound();
            }

            return Ok(new BookBuyViewModel(bookBuy));
        }

        // PUT: api/BookBuys/5
        /// <summary>
        /// Update Book Buy From
        /// </summary>
        /// <param name="bookBuy"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookBuy([FromBody] BookBuyViewModel bookBuy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Entry(bookBuy).State = EntityState.Modified;
            var bookBuyFrom = _iBookBuyService.FindBy(F => F.Id == bookBuy.Id).FirstOrDefault();
            if (bookBuyFrom == null)
            {
                return BadRequest();
            }
            try
            {
                bookBuyFrom.BuyFrom = bookBuy.BuyFrom;
                _iBookBuyService.Update(bookBuyFrom);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookBuyExists(bookBuy.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookBuys
        /// <summary>
        /// Create Book Buy From
        /// </summary>
        /// <param name="bookBuyFrom"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostBookBuy([FromQuery] string bookBuyFrom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookBuy bookBuy = new BookBuy();
            bookBuy.BuyFrom = bookBuyFrom;

            _iBookBuyService.Create(bookBuy);

            return CreatedAtAction("GetBookBuy", new { id = bookBuy.Id }, bookBuy);
        }

        // DELETE: api/BookBuys/5
        /// <summary>
        /// Delete Book buy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookBuy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookBuy = _iBookBuyService.FindBy(T => T.Id == id).FirstOrDefault();
            if (bookBuy == null)
            {
                return NotFound();
            }

            bookBuy.Deleted = true;
            _iBookBuyService.Update(bookBuy);

            return Ok(bookBuy);
        }

        private bool BookBuyExists(int id)
        {
            return _iBookBuyService.Any(e => e.Id == id);
        }
    }
}