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

        public bool IsExist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<USR01>(u => u.R01F01 == id);
            }
        }

        public Response GetAll()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;

                return _objResponse;
            }
        }


        public Response GetById(int id)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;

                return _objResponse;
            }
        }


        public void PreSave(DTOUSR01 objDTO)
        {
            _objUSR01 = objDTO.Convert<USR01>();

            if (Operation == EnmEntryType.E)
            {
                _objUSR01.R01F01 = Id;
            }

        }
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

        public Response Save()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;

                return _objResponse;
            }
        }

        public Response Delete()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;

                return _objResponse;
            }
        }
    }
}
