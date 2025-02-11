using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Filters;
using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFinalDemo.Controllers
{
    /// <summary>
    /// Login controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggingFilter))]
    public class CLLogin : ControllerBase
    {
        private Response _objResponse;
        private ILogin _objBLLogin;
        private USR01 _objUSR01;

        public CLLogin(Response objResponse, ILogin objBLLogin)
        {
            _objResponse = objResponse;
            _objBLLogin = objBLLogin;
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


            return Ok(_objResponse);
        }
    }
}