using CoreFinalDemo.Models;
using CoreFinalDemo.Models.ENUM;

namespace CoreFinalDemo.BL.Interface
{
    public interface IDataHandler<T> where T : class
    {
        EnmEntryType Operation { get; set; }

        void PreSave(T objDTO);

        Response Validation();

        Response Save();

    }
}
