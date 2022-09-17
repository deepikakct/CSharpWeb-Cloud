using Microsoft.AspNetCore.Mvc;
using UWRESTProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UWRESTProject.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    [Authenticator]
    public class UserObjectController : ControllerBase
    {
        private static List<UserModel> users = new List<UserModel>();
        private static int currentId = 1;    

        
        /// <summary>
        /// This gets all the contacts in our array
        /// </summary>
        /// <returns></returns>
        // GET: api/<ContactsController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(users);
        }

        // POST api/<UserObjectController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel value)
        {
            if (string.IsNullOrEmpty(value.UserName))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Name"
                    });
            }

            if (string.IsNullOrEmpty(value.Email))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Email"
                    });
            }
            if (string.IsNullOrEmpty(value.Password))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Password"
                    });
            }
            value.UserID = currentId++;            
            value.DateAdded = DateTime.Now;
            users.Add(value);

            return CreatedAtAction(
                nameof(GetSpecific), // which method
                new { id = value.UserID }, // route id
                value // response body
                );
        }

        // GET api/<ContactsController>/101
        [HttpGet("{id}")]
        public IActionResult GetSpecific(int id)
        {
            var user = users.FirstOrDefault(t => t.UserID == id);

            if (user == null)
            {
                return NotFound(null);
            }

            return Ok(user);
        }

        // PUT api/<UserObjectController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel value)
        {
            var user = users.FirstOrDefault(t => t.UserID == id);

            if (user == null)
            {
                return NotFound(null);
            }

            if (string.IsNullOrEmpty(value.UserName))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Name"
                    });
            }

            if (string.IsNullOrEmpty(value.Email))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Email"
                    });
            }
            if (string.IsNullOrEmpty(value.Password))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Password"
                    });
            }
            user.Email = value.Email;
            user.UserName = value.UserName;
            user.Password = value.Password;
            return Ok(user);
        }

        // DELETE api/<UserObjectController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedRows = users.RemoveAll(t => t.UserID == id);

            if(deletedRows == 0)
            {
                return NotFound(null);
            }

            return Ok();
        }
    }
}
