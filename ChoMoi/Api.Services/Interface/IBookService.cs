using Api.Services;
using ChoMoi.DTOs;
using DemoAPI.Models;
using DemoAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Services.Interface
{
    public interface IBookService : IEntityService<Book>
    {
        PaginationViewModel<BookViewModel> GetPagination(RequestPagination requestPagination, List<BookViewModel> entries);
        List<BookViewModel> GetByCondition(RequestPagination requestPagination, List<BookViewModel> entries);

    }
}
