using CoreFinalDemo.Models.ENUM;
using System.ComponentModel.DataAnnotations;

namespace CoreFinalDemo.Models.DTO
{
    /// <summary>
    /// DTO for POCO USR01 
    /// </summary>
    public class DTOUSR01
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
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password should be between 6 and 100 characters.")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        [Required(ErrorMessage = "Role is required.")]
        [EnumDataType(typeof(EnmRole), ErrorMessage = "Invalid role.")]
        public EnmRole R01F04 { get; set; }

    }
}
