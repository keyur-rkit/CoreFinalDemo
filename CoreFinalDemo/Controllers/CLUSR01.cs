using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.ENUM;
using CoreFinalDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreFinalDemo.Filters;

namespace CoreFinalDemo.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [JWTAuthorizationFilter("Admin")]
    [ServiceFilter(typeof(LoggingFilter))]
    public class CLUSR01 : ControllerBase
    {
        private Response _objResponse;
        private IUSR01 _objBLUSR01;

        public CLUSR01(IUSR01 objBLUSR01, Response objResponce)
        {
            _objResponse = objResponce;
            _objBLUSR01 = objBLUSR01;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of users.</returns>
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            _objResponse = _objBLUSR01.GetAll();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>User details.</returns>
        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            _objResponse = _objBLUSR01.GetById(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Add new user.
        /// </summary>
        /// <param name="objDTOUSR01">User DTO.</param>
        /// <returns>Result of add operation.</returns>
        [HttpPost("AddUser")]
        public IActionResult AddUser(DTOUSR01 objDTOUSR01)
        {
            _objBLUSR01.Operation = EnmEntryType.A;
            _objBLUSR01.PreSave(objDTOUSR01);
            _objResponse = _objBLUSR01.Validation();

            if (!_objResponse.IsError)
            {
                _objBLUSR01.Save();
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Edit existing user.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <param name="objDTOUSR01">User DTO.</param>
        /// <returns>Result of edit operation.</returns>
        [HttpPut("EditUser")]
        public IActionResult EditUser(int id, DTOUSR01 objDTOUSR01)
        {
            _objBLUSR01.Operation = EnmEntryType.E;
            _objBLUSR01.Id = id;
            _objBLUSR01.PreSave(objDTOUSR01);
            _objResponse = _objBLUSR01.Validation();

            if (!_objResponse.IsError)
            {
                _objBLUSR01.Save();
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>Result of delete operation.</returns>
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            _objBLUSR01.Operation = EnmEntryType.D;
            _objBLUSR01.Id = id;
            _objResponse = _objBLUSR01.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLUSR01.Delete();
            }

            return Ok(_objResponse);
        }
    }
}