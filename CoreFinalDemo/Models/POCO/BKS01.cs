using ServiceStack.DataAnnotations;

namespace CoreFinalDemo.Models.POCO
{
    /// <summary>
    /// POCO model of BKS01
    /// </summary>
    public class BKS01
    {
        /// <summary>
        /// BookId
        /// </summary>
        public int S01F01 { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string S01F02 { get; set; }

        /// <summary>
        /// AuthorName
        /// </summary>
        public string S01F03 { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string S01F04 { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime S01F05 { get; set; } = DateTime.Now;

        /// <summary>
        /// UpdatedAt
        /// </summary>
        public DateTime S01F06 { get; set; } = DateTime.Now;
    }
}
