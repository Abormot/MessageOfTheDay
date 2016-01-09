// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   The Language Service interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MessageOfTheDay.Models;
using System.Collections.Generic;

namespace MessageOfTheDay.Services
{
    /// <summary>
    /// The language service interface
    /// </summary>
    public interface ILanguageService
    {
        /// <summary>
        /// Get all languages
        /// </summary>
        /// <returns>languages</returns>
        IList<LanguageDTO> GetLanguagesQuery();
    }
}
