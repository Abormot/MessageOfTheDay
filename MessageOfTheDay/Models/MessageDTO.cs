using System.ComponentModel.DataAnnotations;

namespace MessageOfTheDay.Models
{
    /// <summary>
    /// The Message model
    /// </summary>
    public class MessageDTO
    {
        /// <summary>
        /// Gets or sets message id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets language id 
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets language name
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets day id 
        /// </summary>
        public int DayId { get; set; }

        /// <summary>
        /// gets or sets day name
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Gets or sets message text
        /// </summary>
        [Required(ErrorMessage="Parameter Text is required")]
        [StringLength(1024, MinimumLength=1, ErrorMessage="Message text must have a value with length min 1 and max 1024 characters")]
        public string Text { get; set; }
    }
}