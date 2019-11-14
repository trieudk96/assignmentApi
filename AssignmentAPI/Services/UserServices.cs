using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAPI.Data;
using AssignmentAPI.Models;
using AssignmentAPI.Services;

namespace AssignmentAPI.Services
{
    public class UserServices : ApplicationService<User>,IUserServices
    {
        public UserServices(IRepository<User> repository) : base(repository)
        {
        }
    }
}
