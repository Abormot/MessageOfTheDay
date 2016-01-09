// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   The Message Service interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MessageOfTheDay.Models;

namespace MessageOfTheDay.Services
{
    /// <summary>
    /// The Messgae Service interface
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Get Message of the day by params
        /// </summary>
        /// <param name="dayId">Day id</param>
        /// <param name="languageId">Language Id</param>
        /// <returns>Message entity</returns>
        MessageDTO GetMessageQuery(int dayId, int languageId);

        /// <summary>
        /// Set message text for the message
        /// </summary>
        /// <param name="id">Message id</param>
        /// <param name="messageText">New message text</param>
        /// <returns>Updated message entity</returns>
        MessageDTO SetMessageCommand(int id, string messageText);
    }
}
