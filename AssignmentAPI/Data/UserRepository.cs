using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAPI.Data;
using AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAPI.Data
{
    public class UserRepository:ApplicationRepository<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {

        }


    }
}
