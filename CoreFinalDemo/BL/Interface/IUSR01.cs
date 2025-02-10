using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;

namespace CoreFinalDemo.BL.Interface
{
    /// <summary>
    /// Interface for user operations.
    /// </summary>
    public interface IUSR01 : IDataHandler<DTOUSR01>
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Response with list of users.</returns>
        Response GetAll();

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>Response with user details.</returns>
        Response GetById(int id);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <returns>Response with delete result.</returns>
        Response Delete();
    }
}