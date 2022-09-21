using Microsoft.AspNetCore.Mvc;
using RSVP_tracker.Services;

namespace RSVP_tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userList = await _userService.GetAllUsers();
            return Ok(userList);
        }

     }
}
