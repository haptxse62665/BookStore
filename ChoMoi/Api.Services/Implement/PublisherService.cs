using Api.Repositories;
using Api.Services;
using ChoMoi.Api.Repositories.Interface;
using ChoMoi.Api.Services.Interface;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.Api.Services.Implement
{

    public class PublisherService : EntityService<Publisher>, IPublisherService
    {
        
        public PublisherService(IUnitOfWork unitOfWork, IPublisherRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
