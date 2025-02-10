using System.ComponentModel.DataAnnotations;

namespace CoreFinalDemo.Models.DTO
{
    /// <summary>
    /// DTO model for BKS01
    /// </summary>
    public class DTOBKS01
    {
        /// <summary>
        /// Title
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string S01F02 { get; set; }

        /// <summary>
        /// AuthorName
        /// </summary>
        [Required(ErrorMessage = "AuthorName is required.")]
        public string S01F03 { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, ErrorMessage = "ISBN must be 13 digits.", MinimumLength = 13)]
        public string S01F04 { get; set; }
    }
}
