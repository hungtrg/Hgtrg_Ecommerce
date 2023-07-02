using Hgtrg.Ecommerce.BusinessLayer.DTO.RequestModels;
using Hgtrg.Ecommerce.BusinessLayer.DTO.ResponseModels;
using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
using Hgtrg.Ecommerce.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hgtrg.Ecommerce.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserServices _service;
        private readonly IJwtService _jwtService;

        public AuthenticateController(IUserServices service, IJwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Create new user object
            var user = _service.RegisterUser(model);

            // Add user to repository and Save changes to data store
            var result = await _service.AddAsync(user);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Create new user object
            var user = _service.SigninUser(model);

            if (user != null)
            {
                var token = _jwtService.GenerateToken(user);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
