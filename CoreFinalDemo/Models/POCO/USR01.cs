using CoreFinalDemo.Models.ENUM;
using ServiceStack.DataAnnotations;

namespace CoreFinalDemo.Models.POCO
{
    /// <summary>
    /// POCO model of Users
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        [IgnoreOnUpdate]
        public EnmRole R01F04 { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime R01F05 { get; set; } = DateTime.Now;

        /// <summary>
        /// UpdatedAt
        /// </summary>
        public DateTime R01F06 { get; set; } = DateTime.Now;
    }
}
