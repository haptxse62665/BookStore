using Api.Repositories;
using DemoAPI.Models;
using DemoAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Repositories.Interface
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        List<BookViewModel> GetAllBookBuyOnlineOrBuyOffline(bool isOnline, bool isOffline);
    }
}
