using MessageOfTheDay.Models;

namespace MessageOfTheDay.Services
{
    public interface IMessageService
    {
        MessageDTO GetMessageQuery(int dayId, int languageId);
        MessageDTO SetMessageCommand(int id, string messageText);
    }
}
