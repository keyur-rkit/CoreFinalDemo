using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.Models.POCO;
using CoreFinalDemo.Models;
using ServiceStack.Data;
using System.Data;
using ServiceStack.OrmLite;
using CoreFinalDemo.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace CoreFinalDemo.BL.Services
{
    public class BLLogin : ILogin
    {
        private readonly IDbConnectionFactory _dbFactory;
        private Response _objResponse;
        private IConfiguration _config;

        public BLLogin(IDbConnectionFactory dbFactory, Response objResponse,IConfiguration config)
        {
            _objResponse = objResponse;
            _dbFactory = dbFactory;
            _config = config;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        public USR01 AuthenticatUser(DTOLogin objDTOLogin)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Single<USR01>(u => u.R01F02 == objDTOLogin.R01F02
                    && u.R01F03 == objDTOLogin.R01F03);
            }
        }

        public Response GenerateJwt(USR01 objUSR01)
        {
            try
            {
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                string role = objUSR01.R01F04.ToString();

                Claim[] claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, objUSR01.R01F02),
                new Claim(ClaimTypes.Role, role) // Dynamically setting the role
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _config["Jwt:Isuuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                _objResponse.Data = new JwtSecurityTokenHandler().WriteToken(token);
                _objResponse.Message = "Authentication Successful";

                return _objResponse;
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
