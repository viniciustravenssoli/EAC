﻿using EAC.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Errors
{
    public class UserErrors
    {
        public static ResultError NotFoundUser => new ResultError("not_found_user", "This User was not found");
        public static ResultError UserAlredyExist => new ResultError("email_is_used", "This User already exists");
    }
}
