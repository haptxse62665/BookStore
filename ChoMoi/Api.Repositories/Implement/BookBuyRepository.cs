using Api.Repositories;
using ChoMoi.Api.Models;
using ChoMoi.Api.Repositories.Interface;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Repositories.Implement
{
    public class BookBuyRepository : GenericRepository<BookBuy>, IBookBuyRepository
    {
        public BookBuyRepository(BookStoreContext bookStoreContext)
          : base(bookStoreContext)
        {
        }
    }
}
