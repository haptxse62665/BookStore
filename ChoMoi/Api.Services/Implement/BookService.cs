using Api.Repositories;
using Api.Services;
using ChoMoi.Api.Repositories.Interface;
using ChoMoi.Api.Services.Interface;
using ChoMoi.DTOs;
using ChoMoi.Helper;
using DemoAPI.Models;
using DemoAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Services.Implement
{
    public class BookService : EntityService<Book>, IBookService
    {
        IBookRepository bookRepository;
        public BookService(IUnitOfWork unitOfWork, IBookRepository repository) : base(unitOfWork, repository)
        {
        }

        public List<BookViewModel> GetAllBookBuyOnlineAndBuyOffline()
        {
            List<BookViewModel> bookViewModels = new List<BookViewModel>();
            bookViewModels = bookRepository.GetAllBookBuyOnlineAndBuyOffline();
            return bookViewModels;
        }

        public List<BookViewModel> GetByCondition(RequestPagination requestPagination, List<BookViewModel> entries)
        {
            //sort
            if (!string.IsNullOrEmpty(requestPagination.SortBy))
            {
                if (requestPagination.SortBy.Equals(ObjectFields.BOOK_NAME_INC))
                    entries = entries.OrderBy(o => o.Title).ToList();

                if (requestPagination.SortBy.Equals(ObjectFields.BOOK_NAME_DESC))
                    entries = entries.OrderByDescending(o => o.Title).ToList();

            }



            //search
            if (!string.IsNullOrEmpty(requestPagination.SearchKey))
            {
                var searchKey = requestPagination.SearchKey;
                entries = entries.Where(s => s.Title.ToUpper().Contains(searchKey.ToUpper()) || s.Authors.Contains(searchKey)
                                       || s.Catergory.ToUpper().Contains(searchKey) || s.Publisher.ToUpper().Contains(searchKey.ToUpper())).ToList();
            }

            //fileter   
            if (!string.IsNullOrEmpty(requestPagination.FileterKey))
            {
                var fileterKey = requestPagination.FileterKey;
                entries = entries.Where(s => s.Catergory.Equals(fileterKey) || s.Publisher.Equals(fileterKey)
                                        || s.Authors.Equals(fileterKey)).ToList();
            }

            return entries;
        }

        PaginationViewModel<BookViewModel> IBookService.GetPagination(RequestPagination requestPagination, List<BookViewModel> entries)
        {
            var count = entries.Count();


            var page = requestPagination.Page.GetValueOrDefault();
            var pageSize = requestPagination.PageSize.GetValueOrDefault();


            entries = entries.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PaginationViewModel<BookViewModel> pagi = new PaginationViewModel<BookViewModel>();

            pagi.Amount = pageSize;
            pagi.Data = entries;
            pagi.TotalCount = count;
            pagi.TotalPage = count % pageSize == 0 ? count / pageSize : count / pageSize + 1;

            return pagi;
        }
    }
}
