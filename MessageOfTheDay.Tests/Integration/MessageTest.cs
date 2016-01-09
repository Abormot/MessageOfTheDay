using System.Linq;
using MessageOfTheDay.Services;
using MessageOfTheDay.Models;
using System.Web.Http.Results;
using MessageOfTheDay.Api;
using MessageOfTheDay.Data;

using NUnit.Framework;

namespace MessageOfTheDay.Tests.Integration
{
    [TestFixture]
    public class MessageTest
    {
        private ILanguageService _languageService;
        private IMessageService _messageService;
        private IDayService _dayService;
        private MessagesController _controller;

        [SetUp]
        public void SetUp()
        {
            _languageService = new LanguageService();
            _messageService = new MessageService();
            _dayService = new DayService();

            _controller = new MessagesController(_languageService, _dayService, _messageService);
        }

        [Test]
        [TestCase(1, 1)]
        public void Test_GetMessage_Integration(int dayId, int languageId)
        {          
            using (var db = new MessagesDBEntities())
            {
                // Arrange
                var dbMessage = db.Messages.First(x => x.DayId == dayId && x.LanguageId == languageId);
                
                //Act
                var result = _controller.GetMessage(dayId, languageId);
                var response = result as OkNegotiatedContentResult<Message>;

                // Assert
                Assert.IsInstanceOf<System.Web.Http.IHttpActionResult>(result);
                Assert.IsNotNull(response);
                Assert.AreEqual(dbMessage.Message, response.Content.Text);
            }
        }

        [Test]
        public void Test_SetMessage_Integration()
        {
            string oldValue;
            // Arrange
            var message = new Message
            {
                Id = 1,
                Text = "new message text"
            };
            using (var db = new MessagesDBEntities())
            {
                var dbMessage = db.Messages.First(x => x.Id == message.Id);
                oldValue = dbMessage.Message;
            }

            //Act
            var result = _controller.SetMessage(message);
            var response = result as OkNegotiatedContentResult<Message>;

            using (var db = new MessagesDBEntities())
            {
                //dbMessage should be changed
                var dbMessage = db.Messages.First(x => x.Id == 1);

                // Assert
                Assert.IsInstanceOf<System.Web.Http.IHttpActionResult>(result);
                Assert.IsNotNull(response);
                Assert.AreEqual(message.Text, response.Content.Text);
                Assert.AreEqual(message.Text, dbMessage.Message);

                //Get back value in db
                dbMessage.Message = oldValue;
                db.SaveChanges();
            }
        }
    }
}
