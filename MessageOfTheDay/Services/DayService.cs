// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   The Day of week Service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System.Collections.Generic;
using System.Linq;

namespace MessageOfTheDay.Services
{
    /// <summary>
    /// Day of week Service
    /// </summary>
    public class DayService: IDayService
    {
        /// <summary>
        /// Get all days
        /// </summary>
        /// <returns>Day of week collection</returns>
        public IList<DayDTO> GetDaysQuery()
        {
            using (var db = new MessagesDBEntities())
            {
                return db.Days.Select(x => new DayDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
        }
    }
}