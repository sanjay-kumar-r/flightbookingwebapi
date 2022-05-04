using DAL.Interface;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace FlightAirLine.Controllers
{    
    [Route("api/v1.0/flight/airline/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUsers _Users;
        public RegisterController(IUsers Users)
        {
            _Users = Users;
        }

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] TblUserDetail tblUserDetail)
        {
            using (var scope = new TransactionScope())
            {
                _Users.InsertUsers(tblUserDetail);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = tblUserDetail.UserId }, tblUserDetail);
            }
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _Users.GetUsers();
            return new OkObjectResult(users);
        }
        [HttpGet]
        [Route("GetUserByID")]
        public IActionResult Get(int id)
        {
            var user = _Users.GetUserByID(id);
            return new OkObjectResult(user);
        }
    }
}
