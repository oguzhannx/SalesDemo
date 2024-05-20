using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SalesDemo.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles ="seyhanlar")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "seyhanlar")]
        public IActionResult test()
        {
            return Ok("Admin Works....");
        }
    }
}
