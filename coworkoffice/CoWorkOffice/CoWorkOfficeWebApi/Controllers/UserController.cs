using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using CoWorkOfficeModel.Models.DTO;
using CoWorkOfficeWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorkOfficeWebApi.Controllers
{
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTokenDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiMsgDTO))]
        public async Task<ActionResult<UserTokenDTO>> Authenticate([FromBody] loginDTO model)
        {
            var user = await _context.Users.Where(f=>f.Email == model.Email && f.Password == model.Password).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new ApiMsgDTO { code = "KO", message = "User or password invalid" });

            var token = TokenService.CreateToken(user);
            user.Password = "";
            return Ok(new UserTokenDTO
                        {
                            user = user.Name,
                            role = user.Role,
                            token = token
                        });
        }

        //[HttpGet]
        //[Route("anonymous")]
        //[AllowAnonymous]
        //public string Anonymous()
        //{
        //    return "You are Anonymous";
        //}

        //[HttpGet]
        //[Route("authenticated")]
        //[Authorize]
        //public string Authenticated() => String.Format("Authenticated - {0}", User.Identity.Name);

    }
}
