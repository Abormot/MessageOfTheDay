namespace MessageOfTheDay.Models
{
    /// <summary>
    /// The Language model
    /// </summary>
    public class LanguageDTO
    {
        /// <summary>
        /// Gets or sets language Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets language name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets relative path to the image indicating language
        /// </summary>
        public string PartialFlagPath { get; set; }
    }
}