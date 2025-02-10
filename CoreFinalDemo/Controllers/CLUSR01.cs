using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.ENUM;
using CoreFinalDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFinalDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLUSR01 : ControllerBase
    {
        private Response _objResponse;
        private IUSR01 _objBLUSR01;

        public CLUSR01(IUSR01 objBLUSR01, Response objResponce)
        {
            _objResponse = objResponce;
            _objBLUSR01 = objBLUSR01;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            _objResponse = _objBLUSR01.GetAll();

            return Ok(_objResponse);
        }

        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            _objResponse = _objBLUSR01.GetById(id);
            return Ok(_objResponse);
        }

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
