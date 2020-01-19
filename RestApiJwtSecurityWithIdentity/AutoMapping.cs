using AutoMapper;
using EFDataAccess.IdentityModels;
using RestApiJwtSecurityWithIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiJwtSecurityWithIdentity
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RegistrationViewModel, AppUser>();
        }
    }
}
