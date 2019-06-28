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
        }

        public List<BookViewModel> GetAllBookBuyOnlineAndBuyOffline()
        {
            List<BookViewModel> result = new List<BookViewModel>();
            var books = context.Book.Where(b => b.BookBuyOnlineId != null && b.BookBuyOffileId != null).ToList();
            foreach(var item in books)
            {
                BookViewModel book = new BookViewModel(item);
            }
            return result;
        }
    }
}
