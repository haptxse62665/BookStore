using Api.Repositories;
using ChoMoi.Api.Repositories.Interface;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Repositories.Implement
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreContext bookStoreContext)
          : base(bookStoreContext)
        {
        }
    }
}
