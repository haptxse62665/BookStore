using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoMoi.Api.Repositories.Interface;
using ChoMoi.Api.Services.Interface;
using ChoMoi.DTOs;
using DemoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChoMoi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _iPublisherService;

        public PublishersController(IPublisherService iPublisherService)
        {
            _iPublisherService = iPublisherService;
        }

        // GET: api/Publishers
        /// <summary>
        /// Get All Publisher
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPublisher()
        {
            var publshers = _iPublisherService.GetAll();
            List<PublisherViewModel> lPvm = new List<PublisherViewModel>();
            foreach (var item in publshers)
            {
                PublisherViewModel pvm = new PublisherViewModel();
                pvm.Id = item.Id;
                pvm.Name = item.Name;
                lPvm.Add(pvm);
            }
             return Ok(lPvm);
        }

        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                var publisher = _iPublisherService.FindBy(F => F.Id == id).FirstOrDefault();
            
                PublisherViewModel tmp = new PublisherViewModel();
                tmp.Name = publisher.Name;
                tmp.Id = publisher.Id;

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(tmp);
        }

        // PUT: api/Publishers/5
        /// <summary>
        /// Update Publisher
        /// </summary>
        /// <param name="publisherVM"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult PutPublisher([FromBody] PublisherViewModel publisherVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var publisher = _iPublisherService.FindBy(F => F.Id == publisherVM.Id).FirstOrDefault();

            if (publisher == null)
            {
                return BadRequest();
            }

            //_context.Entry(publisher).State = EntityState.Modified;
            

            try
            {
                publisher.Name = publisherVM.Name;
                _iPublisherService.Update(publisher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(publisherVM.Id))
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
        /// Add Publisher
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        // POST: api/Publishers
        [HttpPost]
        public IActionResult PostPublisher(string publisherName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Publisher publisher = new Publisher();
            publisher.CreatedDate = DateTime.UtcNow;               
            publisher.Name = publisherName;
            _iPublisherService.Create(publisher);

            return CreatedAtAction("GetPublisher", new { id = publisher.Id }, publisher);
        }

        /// <summary>
        /// Delete Publisher by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = _iPublisherService.FindBy(F => F.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            var pub= publisher.SingleOrDefault();
            pub.Deleted = true;
            _iPublisherService.Update(pub);

            PublisherViewModel pvm = new PublisherViewModel();
            pvm.Id = pub.Id;
            pvm.Name = pvm.Name;

            return Ok(pvm);
        }

        private bool PublisherExists(int id)
        {
            return _iPublisherService.Any(e => e.Id == id);
        }
    }
}