using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Filters;
using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.ENUM;
using Microsoft.AspNetCore.Mvc;

namespace CoreFinalDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTAuthorizationFilter("User")]
    public class CLBKS01 : ControllerBase
    {
        private Response _objResponse;
        private IBKS01 _objBLBKS01;

        public CLBKS01(IBKS01 objBLBKS01, Response objResponce) 
        { 
            _objResponse = objResponce;
            _objBLBKS01 = objBLBKS01;
        }

        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            _objResponse = _objBLBKS01.GetAll();

            return Ok(_objResponse);
        }

        [HttpGet("GetBookById")]
        public IActionResult GetBookById(int id)
        {
            _objResponse = _objBLBKS01.GetById(id);
            return Ok(_objResponse);
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(DTOBKS01 objDTOBKS01)
        {
            _objBLBKS01.Operation = EnmEntryType.A;
            _objBLBKS01.PreSave(objDTOBKS01);
            _objResponse = _objBLBKS01.Validation();

            if (!_objResponse.IsError)
            {
                _objBLBKS01.Save();
            }

            return Ok(_objResponse);
        }

        [HttpPut("EditBook")]
        public IActionResult EditBook(int id,DTOBKS01 objDTOBKS01)
        {
            _objBLBKS01.Operation = EnmEntryType.E;
            _objBLBKS01.Id = id;
            _objBLBKS01.PreSave(objDTOBKS01);
            _objResponse = _objBLBKS01.Validation();

            if (!_objResponse.IsError)
            {
                _objBLBKS01.Save();
            }

            return Ok(_objResponse);
        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            _objBLBKS01.Operation = EnmEntryType.D;
            _objBLBKS01.Id = id;
            _objResponse = _objBLBKS01.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLBKS01.Delete();
            }

            return Ok(_objResponse);
        }
    }
}
