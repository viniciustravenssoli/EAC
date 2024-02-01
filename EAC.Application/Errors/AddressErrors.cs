using EAC.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Errors
{
    public class AddressErrors
    {
        public static ResultError NotFoundAddress => new ResultError("not_found_user", "This User was not found");
        public static ResultError AddressNotBelongToUser => new ResultError("address_is_not_from_user", "This Address Not Belong to user");
    }
}

