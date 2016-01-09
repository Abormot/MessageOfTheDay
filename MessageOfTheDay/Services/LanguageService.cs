// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   The Language Service 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System.Collections.Generic;
using System.Linq;

namespace MessageOfTheDay.Services
{
    /// <summary>
    /// The Language Service
    /// </summary>
    public class LanguageService: ILanguageService
    {
        /// <summary>
        /// Get all languages
        /// </summary>
        /// <returns>Languages collection</returns>
        public IList<LanguageDTO> GetLanguagesQuery()
        {
            using (var db = new MessagesDBEntities())
            {
                return db.Languages.Select(x => new LanguageDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartialFlagPath = x.PartialFlagPath
                }).ToList();
            }
        }
    }
}