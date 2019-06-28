using Api.Models;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace ActionFilters.ActionFilters
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter where T: class, IEntity<int>
    {
        private readonly BookStoreContext _context;

        public ValidateEntityExistsAttribute(BookStoreContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id ;

            if (context.ActionArguments.ContainsKey("Id"))
            {
                id = (int)context.ActionArguments["Id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            var entity = _context.Set<T>().SingleOrDefault(x => x.Id.Equals(id));     
            if(entity == null)
            {
                context.Result = new BadRequestObjectResult("ID is not found");
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
