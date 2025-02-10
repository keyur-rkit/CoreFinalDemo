﻿using CoreFinalDemo.BL.Interface;
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
    public class BLBKS01 : IBKS01
    {
        private readonly IDbConnectionFactory _dbFactory;
        private Response _objResponse;
        private BKS01 _objBKS01;


        public EnmEntryType Operation { get; set; }
        public int Id { get; set; }


        public BLBKS01(IDbConnectionFactory dbFactory, Response objResponse)
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
                return db.Exists<BKS01>(b => b.S01F01 == id);
            }
        }

        public Response GetAll()
        {
            try
            {
                List<BKS01> result = new List<BKS01>();

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    result = db.Select<BKS01>().ToList();
                }

                if (result.Count == 0)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Zero books available";
                    _objResponse.Data = null;

                    return _objResponse;
                }
                _objResponse.IsError = false;
                _objResponse.Data = result;
                _objResponse.Message = "Books retrieved successfully";

                return _objResponse;
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
                    _objResponse.Message = "Book does not exist";
                    _objResponse.Data = null;

                    return _objResponse;
                }
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    _objResponse.Data = db.SingleById<BKS01>(id);
                    _objResponse.Message = "Book retrieved successfully";
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


        public void PreSave(DTOBKS01 objDTO)
        {
            _objBKS01 = objDTO.Convert<BKS01>();

            if (Operation == EnmEntryType.E)
            {
                _objBKS01.S01F01 = Id;
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
                    _objResponse.Message = "Book not found";
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
                        _objBKS01.S01F01 = (int)db.Insert(_objBKS01, selectIdentity: true);
                        _objResponse.Message = $"Book added with Id {_objBKS01.S01F01}";
                        return _objResponse;
                    }
                    else if (Operation == EnmEntryType.E)
                    {
                        db.Update(_objBKS01);
                        _objResponse.Message = $"Book with Id {Id} edited";
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
                        db.DeleteById<BKS01>(Id);
                        _objResponse.Message = $"Book with Id {Id} deleted";

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
