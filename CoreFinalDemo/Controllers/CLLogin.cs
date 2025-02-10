using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreFinalDemo.Controllers
{
    /// <summary>
    /// Login controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLLogin : ControllerBase
    {
        private Response _objResponse;
        private ILogin _objBLLogin;
        private USR01 _objUSR01;
        private readonly ILogger _logger;

        public CLLogin(Response objResponse, ILogin objBLLogin, ILogger<CLLogin> logger)
        {
            _objResponse = objResponse;
            _objBLLogin = objBLLogin;
            _logger = logger;
        }

        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="objDTO">Login DTO.</param>
        /// <returns>Result of login operation.</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(DTOLogin objDTO)
        {
            _objUSR01 = _objBLLogin.AuthenticatUser(objDTO);

            if (_objUSR01 == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Credentials are invalid";
            }
            else
            {
                _objResponse = _objBLLogin.GenerateJwt(_objUSR01);
            }

            _logger.LogInformation(_objResponse.Message);

            return Ok(_objResponse);
        }
    }
}