using MessageOfTheDay.Models;
using System.Collections.Generic;

namespace MessageOfTheDay.Services
{
    public interface ILanguageService
    {
        IList<LanguageDTO> GetLanguagesQuery();
    }
}
