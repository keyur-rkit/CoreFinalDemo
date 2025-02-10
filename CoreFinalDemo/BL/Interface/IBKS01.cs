using CoreFinalDemo.Models;
using CoreFinalDemo.Models.DTO;

namespace CoreFinalDemo.BL.Interface
{
    /// <summary>
    /// Interface for book operations.
    /// </summary>
    public interface IBKS01 : IDataHandler<DTOBKS01>
    {
        /// <summary>
        /// Gets or sets the book ID.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>Response with list of books.</returns>
        Response GetAll();

        /// <summary>
        /// Retrieves a book by ID.
        /// </summary>
        /// <param name="id">Book ID.</param>
        /// <returns>Response with book details.</returns>
        Response GetById(int id);

        /// <summary>
        /// Deletes a book.
        /// </summary>
        /// <returns>Response with delete result.</returns>
        Response Delete();
    }
}