using MessageOfTheDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageOfTheDay.Services
{
    public interface ILanguageService
    {
        IList<Language> GetLanguagesQuery();
    }
}
