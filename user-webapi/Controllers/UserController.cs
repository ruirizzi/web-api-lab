using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using userwebapi.Models;
using userwebapi.Repositories;

namespace userwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository _userRepository;
        private readonly IDistributedCache _memoryCache;

        public UserController(IUserRepository userRepository, IDistributedCache memoryCache = null)
        {
            _userRepository = userRepository;

            if(memoryCache != null) _memoryCache = memoryCache;
        }

        [HttpGet("SetUsersCacheData")]
        public IActionResult SetUsersCacheData()
        {
            try
            {
                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddYears(1)
                };

                List<User> users = _userRepository.GetUsers().Result;

                if (users == null || users.Count == 0)
                {
                    return NotFound();
                }

                _memoryCache.Set("users", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(users)), cacheOptions);

                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex = ex });
            }
        }

        [HttpGet("GetUsersCacheData")]
        public string GetUsersCacheData()
        {
            try
            {
                String users = Encoding.UTF8.GetString(_memoryCache.Get("users"));

                return users;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        [HttpGet("GetUserCacheDataById/{id}")]
        public string GetUserCacheDataById(Int64 id)
        {
            try
            {
                User user = JsonConvert.DeserializeObject<List<User>>(Encoding.UTF8.GetString(_memoryCache.Get("users"))).Where(x => x.Id == id).FirstOrDefault();

                return user.ToString();
            }
            catch (Exception ex)
            {
                if(ex.GetType().Name == "NullReferenceException")
                {
                    return new KeyNotFoundException(String.Format("The User with key \"{0}\" was not found.", id.ToString())).Message;
                }
                return ex.GetBaseException().Message;
            }
        }

        [HttpGet("RemoveUsersCacheData")]
        public bool RemoveCacheData()
        {
            _memoryCache.Remove("users");
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await _userRepository.GetUsers();

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
                User user = await _userRepository.GetUser(id);

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
                try
                {
                    Int64 userId = await _userRepository.AddUser(user);

                    if (userId > 0)
                    {
                        return Ok(userId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.UpdateUser(user);
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
                result = await _userRepository.DeleteUser(id);

                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
