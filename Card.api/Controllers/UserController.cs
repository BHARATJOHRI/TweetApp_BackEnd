using Card.api.Data;
using Card.api.Models;
using Card.api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Card.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        
        public readonly UserContext _contextdata;
        public UserController(IConfiguration config, UserContext context)
        {
            _config = config;
            _contextdata = context;
        }
        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            if(_contextdata.Users.Where(u=>u.Email==user.Email).FirstOrDefault() != null)
            {
                return Ok("Already Register");
            }
            user.MemberSince = DateTime.Now;
            _contextdata.Users.Add(user);
            _contextdata.SaveChanges();
            return Ok("Success");
        }
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {
            var userAvailable = _contextdata.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if(userAvailable != null)
            {
                return Ok(new JwtService(_config).GenerateToken(
                    userAvailable.UserId.ToString(),
                    userAvailable.Name,
                    userAvailable.Email
                    ));
            }
            return Ok("Failure");
        }
       
        
    }
}
