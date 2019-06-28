using Api.Repositories;
using ChoMoi.Api.Repositories.Interface;
using DemoAPI.Models;
using DemoAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Repositories.Implement
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private BookStoreContext context;
        public BookRepository(BookStoreContext bookStoreContext)
          : base(bookStoreContext)
        {
            context = bookStoreContext;
        }

        public List<BookViewModel> GetAllBookBuyOnlineOrBuyOffline(bool isOnline, bool isOffline)
        {
            List<BookViewModel> result = new List<BookViewModel>();
            List<Book> books = new List<Book>();
            if (!isOffline && !isOnline)
            {
                return result;
            }
            else if (!isOffline)
            {
                books = context.Book.Where(b => b.BookBuyOnlineId != null && b.BookBuyOffileId == null).ToList();
            }
            else if (!isOnline)
            {
                books = context.Book.Where(b => b.BookBuyOnlineId == null && b.BookBuyOffileId != null).ToList();
            }
            else {
                books = context.Book.Where(b => b.BookBuyOnlineId != null && b.BookBuyOffileId != null).ToList();
            }

            foreach(var item in books)
            {
                result.Add( new BookViewModel(item));
            }
            return result;
        }
    }
}
