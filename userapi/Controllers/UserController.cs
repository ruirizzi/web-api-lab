using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using UserApi.Models;


namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Defines the context to be used throughout the API.
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            // Initializes the context.
            _context = context;
        }

        /// <summary>
        /// Retrieves all User entries from the Database.
        /// </summary>
        /// <returns>An User IEnumerable.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Request a list of User from the context.
            return await _context.User.ToListAsync();
        }

        /// <summary>
        ///  Retrieves a specific User from the Database.
        /// </summary>
        /// <param name="id">The User ID to be retrieved from the Database.</param>
        /// <returns>An User object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Int64 id)
        {
            // Retrieves the User with a given ID from the Database.
            User us = await _context.User.FindAsync(id);

            // Returns a NotFound status if the user object is null.
            if(us == null) return NotFound();

            // Returns the user object retrieved from the Database.
            return us;
        }
        /// <summary>
        /// Creates an User entry on the Database.
        /// </summary>
        /// <param name="us">An User object.</param>
        /// <returns>The inserted entry.</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User us)
        {
            // Adds the User object to the context.
            _context.User.Add(us);

            // Tries to commit the changes to the Database.
            await _context.SaveChangesAsync();

            // Returns a Created message if successful.
            return CreatedAtAction(nameof(GetUser), new { id = us.id }, us);
        }
        /// <summary>
        /// Updates an User entry on the Database.
        /// </summary>
        /// <param name="id">The User ID that will be updated.</param>
        /// <param name="us">The User object that will be used to update the existing entry on the Database.</param>
        /// <returns>An OK status message.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Int64 id , User us)
        {
            // If the ID passed to this method is differente from the ID of the User object, returns a BadRequest status message.
            if (id != us.id) return BadRequest();

            // Sets the context's entry status to Modified.
            _context.Entry(us).State = EntityState.Modified;
            // Tries to commit the changes to the Database.
            await _context.SaveChangesAsync();

            // Returns an OK status message if successful.
            return Ok();
        }
        /// <summary>
        /// Deletes an User entry from the Database.
        /// </summary>
        /// <param name="id">The User ID that will be used to delete the desired entry.</param>
        /// <returns>An OK status message.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Int64 id)
        {
            // Tries to retrieve the desired user entry from the database.
            User us = await _context.User.FindAsync(id);


            // Returns a NotFound status if the user object is null.
            if (us == null) return NotFound();

            // Remove the User object from the context.
            _context.User.Remove(us);

            // Tries to commit the changes to the Database.
            await _context.SaveChangesAsync();

            // Returns an OK status message if successful.
            return Ok();
        }
    }
}