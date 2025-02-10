using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Extensions;
using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.ENUM;
using CoreFinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace CoreFinalDemo.BL.Services
{
    /// <summary>
    /// User service for handling user operations.
    /// </summary>
    public class BLUSR01 : IUSR01
    {
        private readonly IDbConnectionFactory _dbFactory;
        private Response _objResponse;
        private USR01 _objUSR01;


        public EnmEntryType Operation { get; set; }
        public int Id { get; set; }


        public BLUSR01(IDbConnectionFactory dbFactory, Response objResponse)
        {
            _objResponse = objResponse;
            _dbFactory = dbFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        /// <summary>
        /// Check if user exists.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>True if user exists, otherwise false.</returns>
        public bool IsExist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<USR01>(u => u.R01F01 == id);
            }
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Response with list of users.</returns>
        public Response GetAll()
        {
            //try
            //{
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    List<USR01> result = db.Select<USR01>().ToList();
                    if (result.Count == 0)
                    {
                        _objResponse.IsError = true;
                        _objResponse.Message = "Zero users available";
                        _objResponse.Data = null;

                        return _objResponse;
                    }
                    _objResponse.IsError = false;
                    _objResponse.Data = result;
                    _objResponse.Message = "Users retrieved successfully";

                    return _objResponse;
                }
            //}
            //catch (Exception ex)
            //{
            //    _objResponse.IsError = true;
            //    _objResponse.Message = ex.Message;

            //    return _objResponse;
            //}
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>Response with user details.</returns>
        public Response GetById(int id)
        {
            //try
            //{
                if (!IsExist(id))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "User does not exist";
                    _objResponse.Data = null;

                    return _objResponse;
                }
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    _objResponse.Data = db.SingleById<USR01>(id);
                    _objResponse.Message = "User retrieved successfully";
                    return _objResponse;
                }
            //}
            //catch (Exception ex)
            //{
            //    _objResponse.IsError = true;
            //    _objResponse.Message = ex.Message;

            //    return _objResponse;
            //}
        }

        /// <summary>
        /// Prepare user data for saving.
        /// </summary>
        /// <param name="objDTO">User DTO.</param>
        public void PreSave(DTOUSR01 objDTO)
        {
            _objUSR01 = objDTO.Convert<USR01>();

            if (Operation == EnmEntryType.E)
            {
                _objUSR01.R01F01 = Id;
            }

        }

        /// <summary>
        /// Validate user data.
        /// </summary>
        /// <returns>Response with validation result.</returns>
        public Response Validation()
        {
            if (Operation == EnmEntryType.E || Operation == EnmEntryType.D)
            {
                if (Id <= 0)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Invalid BookId";
                }
                else if (!IsExist(Id))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "User not found";
                }
            }

            return _objResponse;
        }

        /// <summary>
        /// Save user data.
        /// </summary>
        /// <returns>Response with save result.</returns>
        public Response Save()
        {
            //try
            //{
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {

                    if (Operation == EnmEntryType.A)
                    {
                        _objUSR01.R01F01 = (int)db.Insert(_objUSR01, selectIdentity: true);
                        _objResponse.Message = $"User added with Id {_objUSR01.R01F01}";
                        return _objResponse;
                    }
                    else if (Operation == EnmEntryType.E)
                    {
                        db.Update(_objUSR01);
                        _objResponse.Message = $"User with Id {Id} edited";
                        return _objResponse;
                    }
                    else
                    {
                        _objResponse.IsError = true;
                        _objResponse.Message = "Invalid Operation type";
                        return _objResponse;
                    }

                }
            //}
            //catch (Exception ex)
            //{
            //    _objResponse.IsError = true;
            //    _objResponse.Message = ex.Message;

            //    return _objResponse;
            //}
        }

        /// <summary>
        /// Delete user data.
        /// </summary>
        /// <returns>Response with delete result.</returns>
        public Response Delete()
        {
            //try
            //{
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    if (Operation == EnmEntryType.D)
                    {
                        db.DeleteById<USR01>(Id);
                        _objResponse.Message = $"User with Id {Id} deleted";

                        return _objResponse;
                    }
                    else
                    {
                        _objResponse.IsError = true;
                        _objResponse.Message = "Invalid Operation type";
                        return _objResponse;
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    _objResponse.IsError = true;
            //    _objResponse.Message = ex.Message;

            //    return _objResponse;
            //}
        }
    }
}
