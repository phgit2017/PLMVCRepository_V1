﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IAuthenticationService
    {
        int ValidAuthentication(AuthenticationDto dto);
        IQueryable<AuthenticationDto> GetAll();
        AuthenticationDto FindByUserId(int userId);
    }
}
