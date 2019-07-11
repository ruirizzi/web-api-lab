using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using userwebapi.Models;
using userwebapi.Repositories;

namespace userwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await userRepository.GetUsers();

            if (users == null || users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Int64? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                User user = await userRepository.GetUser(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                Int64 userId = await userRepository.AddUser(user);

                if (userId > 0)
                {
                    return Ok(userId);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userRepository.UpdateUser(user);
                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Int64? id)
        {
            Int64 result = 0;

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await userRepository.DeleteUser(id);

                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
