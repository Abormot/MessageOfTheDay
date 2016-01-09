using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageOfTheDay.Services
{
    public class LanguageService: ILanguageService
    {
        public IList<Language> GetLanguagesQuery()
        {
            using (var db = new MessagesDBEntities())
            {
                return db.Languages.Select(x => new Language
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartialFlagPath = x.PartialFlagPath
                }).ToList();
            }
        }
    }
}