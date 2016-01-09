using MessageOfTheDay.Models;
using System.Collections.Generic;

namespace MessageOfTheDay.Services
{
    /// <summary>
    /// The Day Service interface
    /// </summary>
    public interface IDayService
    {
        /// <summary>
        /// Get all days 
        /// </summary>
        /// <returns>day of week collection</returns>
        IList<DayDTO> GetDaysQuery();
    }
}
