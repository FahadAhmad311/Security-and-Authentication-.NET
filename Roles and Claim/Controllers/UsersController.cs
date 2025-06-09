using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        [HttpGet("role-based")]
        public IActionResult GetUserByRole()
        {
        
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.Role, "Admin") // Simulating an Admin role
            }, "mock"));

            HttpContext.User = user;

        
            if (user.IsInRole("Admin"))
            {
                return Ok(new { Message = "Access granted for Admin role." });
            }
            else
            {
                return Forbid();
            }
        }

        
        [HttpGet("claims-based")]
        public IActionResult GetUserByClaim()
        {
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim("Department", "IT") // Simulating an IT Department claim
            }, "mock"));

            HttpContext.User = user;

            
            var hasClaim = user.HasClaim(c => c.Type == "Department" && c.Value == "IT");

            if (hasClaim)
            {
                return Ok(new { Message = "Access granted for IT department." });
            }
            else
            {
                return Forbid();
            }
        }
    }
}
