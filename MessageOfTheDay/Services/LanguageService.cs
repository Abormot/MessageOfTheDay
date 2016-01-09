using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System.Collections.Generic;
using System.Linq;

namespace MessageOfTheDay.Services
{
    public class LanguageService: ILanguageService
    {
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