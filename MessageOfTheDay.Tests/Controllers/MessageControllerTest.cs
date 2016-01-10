using System.Collections.Generic;
using System.Web.Http;
using MessageOfTheDay.Services;
using MessageOfTheDay.Models;
using System.Web.Http.Results;
using MessageOfTheDay.Api;

using Moq;

using NUnit.Framework;

namespace MessageOfTheDay.Tests.Controllers
{
    [TestFixture]
    public class MessageControllerTest
    {
        private Mock<ILanguageService> _languageService;
        private Mock<IMessageService> _messageService;
        private Mock<IDayService> _dayService;

        private MessagesController _controller;

        [SetUp]
        public void SetUp()
        {
            _languageService = new Mock<ILanguageService>();
            _messageService = new Mock<IMessageService>();
            _dayService = new Mock<IDayService>();
            _controller = new MessagesController(_languageService.Object, _dayService.Object, _messageService.Object);
        }

        [Test]
        public void Test_GetDays()
        {
            // Arrange
            var expected = new List<DayDTO> { new DayDTO() };
            _dayService.Setup(x => x.GetDays()).Returns(expected);
            
            // Act
            var result = _controller.GetDays();
            var response = result as OkNegotiatedContentResult<IList<DayDTO>>;
            
            // Assert
            Assert.IsInstanceOf<IHttpActionResult>(result);
            Assert.IsNotNull(response);
            Assert.AreEqual(expected, response.Content);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(2, 3)]
        [TestCase(7, 1)]
        public void Test_GetMessage(int dayId, int languageId)
        {
            // Arrange
            var expected = new MessageDTO { 
                DayId = dayId,
                LanguageId = languageId,
                Text = string.Format("test messgae for day {0}, language {1}", dayId, languageId)
            };

            _messageService.Setup(x => x.GetMessage(It.IsIn(dayId), It.IsIn(languageId))).Returns(expected);

            // Act
            var result = _controller.GetMessage(dayId, languageId);
            var response = result as OkNegotiatedContentResult<MessageDTO>;

            // Assert
            Assert.IsInstanceOf<IHttpActionResult>(result);
            Assert.IsNotNull(response);
            Assert.AreEqual(expected, response.Content);
        }

        [Test]
        public void Test_GetMessage_PassEmptyParam_ReturnBadRequest()
        {
            // Act
            var result = _controller.GetMessage(0, 0);
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Test_SetMessage()
        {
            // Arrange
            var expected = new MessageDTO
            {
                Id = 1,
                Text = "new message text"
            };
            _messageService.Setup(x => x.SetMessage(expected.Id, expected.Text));

            // Act
            var result = _controller.SetMessage(expected);

            // Assert
            Assert.IsInstanceOf<IHttpActionResult>(result);
        }
    }
}
