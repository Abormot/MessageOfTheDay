using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System.Linq;

namespace MessageOfTheDay.Services
{
    /// <summary>
    /// The Message Service
    /// </summary>
    public class MessageService: IMessageService
    {
        /// <summary>
        /// Get message of the day by param
        /// </summary>
        /// <param name="dayId">Day id</param>
        /// <param name="languageId">Language id</param>
        /// <returns>Message entity</returns>
        public MessageDTO GetMessage(int dayId, int languageId)
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

        /// <summary>
        /// Set message text for the message
        /// </summary>
        /// <param name="id">Message id</param>
        /// <param name="messageText">New message text</param>
        /// <returns>Updated message entity</returns>
        public void SetMessage(int id, string messageText)
        {
            using (var db = new MessagesDBEntities())
            {
                var message = db.Messages.FirstOrDefault(x => x.Id == id);
                if (message == null) return;
                message.Message = messageText;
                db.SaveChanges();
            }
        }
    }
}