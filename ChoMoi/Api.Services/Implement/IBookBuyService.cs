using Api.Repositories;
using Api.Services;
using ChoMoi.Api.Models;
using ChoMoi.Api.Repositories.Interface;
using ChoMoi.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Services.Implement
{
    public class BookBuyService : EntityService<BookBuy>, IBookBuyService
    {

        public BookBuyService(IUnitOfWork unitOfWork, IBookBuyRepository repository) : base(unitOfWork, repository)
        {
        }

    }
}
