using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;

namespace CoreFinalDemo.BL.Interface
{
    public interface IBKS01 : IDataHandler<DTOBKS01>
    {
        int Id { get; set; }    

        Response GetAll();

        Response GetById(int id);

        Response Delete();

    }
}
