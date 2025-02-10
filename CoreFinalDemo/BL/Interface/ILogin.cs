using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.POCO;

namespace CoreFinalDemo.BL.Interface
{
    /// <summary>
    /// Interface for login operations.
    /// </summary>
    public interface ILogin
    {
        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        /// <param name="objUSR01">Authenticated user.</param>
        /// <returns>Response containing the JWT token.</returns>
        Response GenerateJwt(USR01 objUSR01);

        /// <summary>
        /// Authenticates the user based on login details.
        /// </summary>
        /// <param name="objDTOLogin">Login DTO.</param>
        /// <returns>Authenticated user.</returns>
        USR01 AuthenticatUser(DTOLogin objDTOLogin);
    }
}