using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System.Linq;

namespace MessageOfTheDay.Services
{
    public class MessageService: IMessageService
    {
        public MessageDTO GetMessageQuery(int dayId, int languageId)
        {
            var result = new MessageDTO();
            using (var db = new MessagesDBEntities())
            {
                var message = db.Messages.FirstOrDefault(x => x.DayId == dayId && x.LanguageId == languageId);
                if (message != null)
                    result = new MessageDTO
                    {
                        Id = message.Id,
                        DayId = message.DayId ?? 0,
                        Day = message.Days != null ? message.Days.Name : "",
                        LanguageId = message.LanguageId ?? 0,
                        Language = message.Languages != null ? message.Languages.Name : "",
                        Text = message.Message
                    };
            }
            return result;
        }

        public MessageDTO SetMessageCommand(int id, string messageText)
        {
            using (var db = new MessagesDBEntities())
            {
                var message = db.Messages.FirstOrDefault(x => x.Id == id);
                if (message != null)
                {
                    message.Message = messageText;
                    db.SaveChanges();

                    return new MessageDTO
                           {
                               Id = message.Id,
                               DayId = message.DayId ?? 0,
                               Day = message.Days != null ? message.Days.Name : "",
                               LanguageId = message.LanguageId ?? 0,
                               Language = message.Languages != null ? message.Languages.Name : "",
                               Text = message.Message
                           };
                }
            }
            return null;
        }
    }
}