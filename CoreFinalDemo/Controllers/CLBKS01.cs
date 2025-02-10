using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Filters;
using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.ENUM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFinalDemo.Controllers
{
    /// <summary>
    /// Book controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLBKS01 : ControllerBase
    {
        private Response _objResponse;
        private IBKS01 _objBLBKS01;

        public CLBKS01(IBKS01 objBLBKS01, Response objResponce)
        {
            _objResponse = objResponce;
            _objBLBKS01 = objBLBKS01;
        }

        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns>List of books.</returns>
        [HttpGet("GetAllBooks")]
        [JWTAuthorizationFilter]
        public IActionResult GetAllBooks()
        {
            _objResponse = _objBLBKS01.GetAll();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Get book by ID.
        /// </summary>
        /// <param name="id">Book ID.</param>
        /// <returns>Book details.</returns>
        [HttpGet("GetBookById")]
        [JWTAuthorizationFilter]
        public IActionResult GetBookById(int id)
        {
            _objResponse = _objBLBKS01.GetById(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Add new book.
        /// </summary>
        /// <param name="objDTOBKS01">Book DTO.</param>
        /// <returns>Result of add operation.</returns>
        [HttpPost("AddBook")]
        [JWTAuthorizationFilter("Admin")]
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

        /// <summary>
        /// Edit existing book.
        /// </summary>
        /// <param name="id">Book ID.</param>
        /// <param name="objDTOBKS01">Book DTO.</param>
        /// <returns>Result of edit operation.</returns>
        [HttpPut("EditBook")]
        [JWTAuthorizationFilter("Admin")]
        public IActionResult EditBook(int id, DTOBKS01 objDTOBKS01)
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

        /// <summary>
        /// Delete book.
        /// </summary>
        /// <param name="id">Book ID.</param>
        /// <returns>Result of delete operation.</returns>
        [HttpDelete("DeleteBook")]
        [JWTAuthorizationFilter("Admin")]
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