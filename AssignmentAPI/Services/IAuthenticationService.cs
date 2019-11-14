using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAPI.Models;

namespace TOS.API.Services
{
    public interface IAuthenticationService
    {
        Response<String> Login(User item);
    }
}
