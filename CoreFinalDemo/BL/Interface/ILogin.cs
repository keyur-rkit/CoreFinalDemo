using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;
using CoreFinalDemo.Models.POCO;

namespace CoreFinalDemo.BL.Interface
{
    public interface ILogin
    {
        Response GenerateJwt(USR01 objUSR01);

        USR01 AuthenticatUser(DTOLogin objDTOLogin);
    }
}
