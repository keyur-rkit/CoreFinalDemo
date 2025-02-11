using System.ComponentModel.DataAnnotations;

namespace CoreFinalDemo.Models.DTO
{
    /// <summary>
    /// DTO for Login
    /// </summary>
    public class DTOLogin
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters.")]
        public string R01F03 { get; set; }
    }
}
