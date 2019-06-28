using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionFilters.ActionFilters;
using ChoMoi.Api.Services.Interface;
using ChoMoi.DTOs;
using ChoMoi.Helper;
using DemoAPI.Models;
using DemoAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChoMoi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _iBookService;

        public BooksController(IBookService iBookService)
        {
            _iBookService = iBookService;
        }

        /// <summary>
        /// Get All Book by condition
        /// </summary>
        /// <param name="requestPagination"></param>
        /// <returns></returns>
        [Route("getAllBookByCondition")]
        [HttpGet]
        public PaginationViewModel<BookViewModel> GetAllBookByCondition([FromQuery] RequestPagination requestPagination)
        {
            List<BookViewModel> bookViewModels = new List<BookViewModel>();
            //var bookList = _context.BookAuthors.Where(p => p.User.Id == authorID).ToList();
            var bookList = _iBookService.GetAll();
            foreach (var item in bookList)
            {
                BookViewModel bookViewModel = new BookViewModel(item);
                bookViewModels.Add(bookViewModel);
            }

            //get all
            if (requestPagination.Page == -1)
            {
                PaginationViewModel<BookViewModel> notPagi = new PaginationViewModel<BookViewModel>();
                notPagi.Amount = bookViewModels.Count();
                notPagi.Data = bookViewModels;
                notPagi.TotalCount = bookViewModels.Count();
                notPagi.TotalPage = 1;
                return notPagi;
            }
            //=============

            var entries = _iBookService.GetByCondition(requestPagination, bookViewModels);
    
            return _iBookService.GetPagination(requestPagination, entries);

        }

        /// <summary>
        /// Get all Book
        /// </summary>
        /// <returns></returns>
        // GET: api/Books
        [HttpGet]
        public List<BookViewModel> GetBook()
        {
            //List<Book> books = _context.Book.ToList<Book>();
            var books = _iBookService.GetAll();
            List<BookViewModel> booksViewModel =new List<BookViewModel>();
            foreach (var book in books)
            {
                BookViewModel tmp = new BookViewModel(book);
                booksViewModel.Add(tmp);
            }
            return booksViewModel;
        }
        /// <summary>
        /// Add new book
        /// </summary>
        /// <param name="insertBookViewModel"></param>
        /// <returns></returns>
        [Route("InsertBook")]
        [HttpPost]
        public ActionResult InsertBook(InsertBookViewModel insertBookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (insertBookViewModel.Title == null || insertBookViewModel.CategoryId <1 || insertBookViewModel.AuthorIds == null) return BadRequest("Add more information");
            if (insertBookViewModel.BookBuyOnlineId == null && insertBookViewModel.BookBuyOffileId == null) return BadRequest("Insert book buy Id");


            Book book = new Book();

            book.BookBuyOffileId = insertBookViewModel.BookBuyOffileId;
            book.BookBuyOnlineId = insertBookViewModel.BookBuyOnlineId;

            book.CategoryId = insertBookViewModel.CategoryId;
            book.PublisherId = insertBookViewModel.PublisherId;

            book.Deleted = false;

            book.Title = insertBookViewModel.Title;
            if (insertBookViewModel.AuthorIds != null)
            {
                book.BookAuthors = insertBookViewModel.AuthorIds.Select((authorId) =>
                {
                    BookAuthors bookAuthors = new BookAuthors();

                    bookAuthors.UserId = authorId;
                    return bookAuthors;
                }).ToList();

            }


            try
            {

                _iBookService.Create(book);
            }
            catch (Exception)
            {

                return BadRequest("Input Wrong ID");
            }

            return Ok();           
        }

        // GET: api/Books/5
        /// <summary>
        /// get book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Book>))]
        public IActionResult GetBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = HttpContext.Items["entity"] as Book;
            //var book = _iBookService.FindBy(T => T.Id == id).FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(new BookViewModel(book));
            //return Ok(book);
        }

        // PUT: api/Books/5
        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Book>))]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] string bookTitle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var book = _iBookService.FindBy(F => F.Id == id).FirstOrDefault();

            //_context.Entry(publisher).State = EntityState.Modified;

            var book = HttpContext.Items["entity"] as Book;

            if (book == null)
            {
                return BadRequest();
            }

            try
            {
                book.Title = bookTitle;
                _iBookService.Update(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        
        /// <summary>
        /// Update Deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Book>))]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var book =  _iBookService.FindBy(T => T.Id == id).FirstOrDefault();
            var book = HttpContext.Items["entity"] as Book;
            if (book == null)
            {
                return NotFound();
            }

            book.Deleted = true;
            _iBookService.Update(book);

            return Ok(new BookViewModel(book));
        }

        private bool BookExists(int id)
        {
            //return _context.Book.Any(e => e.Id == id);
            return _iBookService.Any(e => e.Id == id);
        }

        
    }
}