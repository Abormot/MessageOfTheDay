using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using MessageOfTheDay;
using MessageOfTheDay.Controllers;
using MessageOfTheDay.Services;
using MessageOfTheDay.Models;
using System.Web.Http.Results;

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
            var expected = new List<Day>();
            expected.Add(new Day());

            _dayService.Setup(x => x.GetDaysQuery()).Returns(expected);
            
            // Act
            var result = _controller.GetDays();
            var response = result as OkNegotiatedContentResult<IList<Day>>;
            
            // Assert
            Assert.IsInstanceOf<System.Web.Http.IHttpActionResult>(result);
            Assert.IsNotNull(response.Content);
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
            var expected = new Message { 
                DayId = dayId,
                LanguageId = languageId,
                Text = string.Format("test messgae for day {0}, language {1}", dayId, languageId)
            };

            _messageService.Setup(x => x.GetMessageQuery(It.IsIn(dayId), It.IsIn(languageId))).Returns(expected);

            // Act
            var result = _controller.GetMessage(dayId, languageId);
            var response = result as OkNegotiatedContentResult<Message>;

            // Assert
            Assert.IsInstanceOf<System.Web.Http.IHttpActionResult>(result);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(expected, response.Content);
        }

        [Test]
        public void Test_GetMessage_PassEmptyParam_ReturnBadRequest()
        {
            // Act
            var result = _controller.GetMessage(0, 0);
            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void Test_SetMessage()
        {
            // Arrange
            var expected = new Message
            {
                Id = 1,
                Text = "new message text"
            };
            _messageService.Setup(x => x.SetMessageCommand(It.IsIn(expected.Id), It.IsIn(expected.Text))).Returns(expected);

            // Act
            var result = _controller.SetMessage(expected);
            var response = result as OkNegotiatedContentResult<Message>;

            // Assert
            Assert.IsInstanceOf<System.Web.Http.IHttpActionResult>(result);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(expected, response.Content);
        }
    }
}
