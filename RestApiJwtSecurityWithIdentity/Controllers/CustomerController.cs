using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EFDataAccess;
using EFDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RestApiJwtSecurityWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ApiAuthorizeUser")]
    public class CustomerController : ControllerBase
    {

        private CustomerDbContext _customerDbContext;        
        private readonly ClaimsPrincipal _callerUser;
        public CustomerController(CustomerDbContext customerDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _customerDbContext = customerDbContext;
            _callerUser = httpContextAccessor.HttpContext.User;
        }

        [HttpGet]
        [Route("list")]
        public List<Customer> GetCustomers()
        {
            return _customerDbContext.Customers.ToList();
        }

        [HttpGet]
        [Route("currentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _callerUser.Claims.Single(c => c.Type == "id");
            var customer = await _customerDbContext.Customers.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                customer.Identity.FirstName,
                customer.Identity.LastName,
                customer.Identity.PictureUrl,
                customer.Identity.FacebookId,
                customer.Location,
                customer.Locale,
                customer.Gender
            });
        }

    }
}