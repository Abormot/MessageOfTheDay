using MessageOfTheDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageOfTheDay.Services
{
    public interface IMessageService
    {
        Message GetMessageQuery(int dayId, int languageId);
        Message SetMessageCommand(int Id, string messageText);
    }
}
