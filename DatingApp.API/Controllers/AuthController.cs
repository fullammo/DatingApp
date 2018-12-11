using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // API CONTOLLER GIVES US THIS HELP FROM VALIDATION ATTRIBUTES
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await repo.UserExists(userForRegisterDto.UserName))
            {
                return BadRequest("Username already exists");
            }

            User userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };

            User createdUser = await repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }


        private readonly IAuthRepository repo;
    }
}