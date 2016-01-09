using MessageOfTheDay.Data;
using MessageOfTheDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageOfTheDay.Services
{
    public class MessageService: IMessageService
    {
        public Message GetMessageQuery(int dayId, int languageId)
        {
            var result = new Message();
            using (var db = new MessagesDBEntities())
            {
                var message = db.Messages.FirstOrDefault(x => x.DayId == dayId && x.LanguageId == languageId);
                if (message != null)
                    result = new Message
                    {
                        Id = message.Id,
                        DayId = message.DayId.HasValue ? message.DayId.Value : 0,
                        Day = message.Days != null ? message.Days.Name : "",
                        LanguageId = message.LanguageId.HasValue ? message.LanguageId.Value : 0,
                        Language = message.Languages != null ? message.Languages.Name : "",
                        Text = message.Message
                    };
            }
            return result;
        }

        public Message SetMessageCommand(int Id, string messageText)
        {
            try
            {
                using (var db = new MessagesDBEntities())
                {
                    var message = db.Messages.FirstOrDefault(x => x.Id == Id);
                    if (message != null)
                    {
                        message.Message = messageText;
                        db.SaveChanges();
                    }
                    return new Message
                    {
                        Id = message.Id,
                        DayId = message.DayId.HasValue ? message.DayId.Value : 0,
                        Day = message.Days != null ? message.Days.Name : "",
                        LanguageId = message.LanguageId.HasValue ? message.LanguageId.Value : 0,
                        Language = message.Languages != null ? message.Languages.Name : "",
                        Text = message.Message
                    }; ;
                }                
            }
            catch (Exception ex)
            {
                //Log Exception
                return null;
            }
        }
    }
}