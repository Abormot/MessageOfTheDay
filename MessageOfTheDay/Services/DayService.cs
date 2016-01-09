using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System.Collections.Generic;
using System.Linq;
namespace MessageOfTheDay.Services
{
    public class DayService: IDayService
    {
        public IList<Day> GetDaysQuery()
        {
            using (var db = new MessagesDBEntities())
            {
                return db.Days.Select(x => new Day
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
        }
    }
}