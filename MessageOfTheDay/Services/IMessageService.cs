using MessageOfTheDay.Models;

namespace MessageOfTheDay.Services
{
    public interface IMessageService
    {
        Message GetMessageQuery(int dayId, int languageId);
        Message SetMessageCommand(int id, string messageText);
    }
}
