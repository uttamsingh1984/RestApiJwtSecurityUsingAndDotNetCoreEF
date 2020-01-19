using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFDataAccess;
using EFDataAccess.IdentityModels;
using EFDataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestApiJwtSecurityWithIdentity.Helpers;
using RestApiJwtSecurityWithIdentity.Models;

namespace RestApiJwtSecurityWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly CustomerDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, IMapper mapper, CustomerDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _mapper = mapper;
        }


        //POST api/account
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            userIdentity.UserName = model.Email;

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location, Gender = model.Gender });
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

    }
}