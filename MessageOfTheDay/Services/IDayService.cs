using MessageOfTheDay.Models;
using System.Collections.Generic;

namespace MessageOfTheDay.Services
{
    public interface IDayService
    {
        IList<DayDTO> GetDaysQuery();
    }
}
