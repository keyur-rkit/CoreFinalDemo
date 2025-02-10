using CoreFinalDemo.Models;
using CoreFinalDemo.Models.ENUM;

namespace CoreFinalDemo.BL.Interface
{
    /// <summary>
    /// Interface for data handling operations.
    /// </summary>
    /// <typeparam name="T">Type of the data object.</typeparam>
    public interface IDataHandler<T> where T : class
    {
        /// <summary>
        /// Gets or sets the operation type.
        /// </summary>
        EnmEntryType Operation { get; set; }

        /// <summary>
        /// Prepares the data object for saving.
        /// </summary>
        /// <param name="objDTO">The data transfer object.</param>
        void PreSave(T objDTO);

        /// <summary>
        /// Validates the data object.
        /// </summary>
        /// <returns>Response with validation result.</returns>
        Response Validation();

        /// <summary>
        /// Saves the data object.
        /// </summary>
        /// <returns>Response with save result.</returns>
        Response Save();
    }
}