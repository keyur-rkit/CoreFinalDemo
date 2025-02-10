using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFinalDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLLogin : ControllerBase
    {
        private Response _objResponse;
        private ILogin _objBLLogin;
        private USR01 _objUSR01;

        public CLLogin(Response objResponse,ILogin objBLLogin)
        {
            _objResponse = objResponse;
            _objBLLogin = objBLLogin;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(DTOLogin objDTO)
        {
            _objUSR01 = _objBLLogin.AuthenticatUser(objDTO);

            if(_objUSR01 == null) 
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
