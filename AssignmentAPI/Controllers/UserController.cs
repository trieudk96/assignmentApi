using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentAPI.Models;
using AssignmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TOS.API.Services;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Authorize]
        public async Task<ICollection<User>> Get()
        {
            var res = await _userServices.GetAllAsync();
            return res;
        }
        [HttpGet("{id}")]
        [Authorize]
        public User Get(int id)
        {
            var res = _userServices.GetById(id);
            res.Password = string.Empty;
            return res;
        }

        [HttpPost]
        public async Task<Response<User>> Post(User user)
        {
            var res = await _userServices.InsertAsync(user);
            return res;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<Response<User>> Put(User user, int id)
        {
            var item = _userServices.GetById(id);
            if(item != null)
            {
                user.Email = item.Email;
                user.Password = item.Password;
            }
            var res = await _userServices.UpdateAsync(user);
            return res;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<Response<User>> Delete(int id)
        {
            var res = await _userServices.DeleteAsync(id);
            return res;
        }
    }
}