using Hgtrg.Ecommerce.BusinessLayer.DTO.RequestModels;
using Hgtrg.Ecommerce.BusinessLayer.DTO.ResponseModels;
using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
using Hgtrg.Ecommerce.BusinessLayer.Services;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hgtrg.Ecommerce.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IUserServices _userService;
        private readonly ISellerServices _sellerService;
        private IUnitOfWork<HgtrgEcommerceContext> _unitOfWork;

        public AuthenticateController(IJwtService jwtService, IUserServices userService,
            ISellerServices sellerService, IUnitOfWork<HgtrgEcommerceContext> unitOfWork)
        {
            _jwtService = jwtService;
            _userService = userService;
            _sellerService = sellerService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Create new user object
            var user = _userService.RegisterUser(model);

            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            await _unitOfWork.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "User created successfully!", Data = user });
        }

        [HttpPost]
        [Route("register-seller")]
        public async Task<IActionResult> RegisterSeller([FromBody] RegisterModel model)
        {
            // Set seller role to user if existed
            var user = _userService.RegisterSeller(model);
            
            var seller = _sellerService.AddSeller(user);
            if (seller == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Seller creation failed! Please check user details and try again." });

            await _unitOfWork.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "Seller created successfully!", Data = seller });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Create new user object
            var user = _userService.SigninUser(model);

            if (user != null)
            {
                var token = _jwtService.GenerateToken(user);
                return Ok(new Response { Status = "Success", Message = "User logged in successfully!", Data = token });
            }
            return Unauthorized();
        }
    }
}
