using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
using Hgtrg.Ecommerce.BusinessLayer.Services;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using Hgtrg.Ecommerce.DataLayer.Repositories;
using Hgtrg.Ecommerce.PresentationLayer.ViewModels.RequestModels;
using Hgtrg.Ecommerce.PresentationLayer.ViewModels.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Hgtrg.Ecommerce.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        //private readonly IUserRepository _userRepository;
        ////private readonly IJwtService _jwtService;
        //private IUnitOfWork<HgtrgEcommerceContext> _unitOfWork;
        //private IGenericRepository<User> repository;
        private IUserServices _service;

        //public AuthenticateController(IUnitOfWork<HgtrgEcommerceContext> unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //    //If you want to use Generic Repository with Unit of work
        //    repository = new GenericRepository<User>(_unitOfWork);
        //    //If you want to use a Specific Repository with Unit of work
        //    _userRepository = new UserRepository(_unitOfWork);
        //}
        public AuthenticateController(IUserServices services)
        {
            _service = services;
        }


        //[HttpGet]
        //[Route("get")]
        //public async Task<IActionResult> Login(string name)
        //{
        //    var user = _userRepository.GetUserByUsername(name);

        //    if (user != null)
        //    {
        //        return Ok(user);
        //    }
        //    return Unauthorized();
        //}

        //[HttpGet]
        //[Route("get-id")]
        //public async Task<IActionResult> Lala(int id)
        //{
        //    var user = repository.GetById(id);

        //    if (user != null)
        //    {
        //        return Ok(user);
        //    }
        //    return Unauthorized();
        //}

        [HttpGet]
        [Route("get-id")]
        public async Task<IActionResult> Lala(int id)
        {
            var user = _service.GetById(id);

            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }
    }
}
//using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
//using Hgtrg.Ecommerce.BusinessLayer.Services;
//using Hgtrg.Ecommerce.DataLayer.Models;
//using Hgtrg.Ecommerce.PresentationLayer.ViewModels.RequestModels;
//using Hgtrg.Ecommerce.PresentationLayer.ViewModels.ResponseModels;
//using Microsoft.AspNetCore.Mvc;

//namespace Hgtrg.Ecommerce.PresentationLayer.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthenticateController : ControllerBase
//    {
//        private readonly IUserServices _userServices;
//        private readonly IJwtService _jwtService;

//        public AuthenticateController(IUserServices userServices, IJwtService jwtService)
//        {
//            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
//            _jwtService = jwtService;
//        }

//        [HttpPost]
//        [Route("login")]
//        public async Task<IActionResult> Login([FromBody] LoginModel model)
//        {
//            var user = await _userServices.SigninUser(model.Username, model.Password);

//            if (user != null)
//            {
//                var token = _jwtService.GenerateToken(user);
//                return Ok(token);
//            }
//            return Unauthorized();
//        }

//        [HttpPost]
//        [Route("register")]
//        public IActionResult Register([FromBody] RegisterModel model)
//        {
//            var user = _userServices.CreateUser(model.Username, model.Password);
//            var test = _userServices.GetUserById(user.UserId);
//            if (user == null)
//                return StatusCode(StatusCodes.Status500InternalServerError,
//                    new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

//            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
//        }
//    }
//}
