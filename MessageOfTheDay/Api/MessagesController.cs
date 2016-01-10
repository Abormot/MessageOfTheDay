using System;
using System.Linq;
using System.Web.Http;
using MessageOfTheDay.Models;
using MessageOfTheDay.Services;

namespace MessageOfTheDay.Api
{
    /// <summary>
    /// The Messages controller
    /// </summary>
    public class MessagesController : ApiController
    {
        private readonly ILanguageService _languageService;
        private readonly IDayService _dayService;
        private readonly IMessageService _messageSevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="languageService">The Languages Service</param>
        /// <param name="dayService">The Days Service</param>
        /// <param name="messageSevice">The Messages Service</param>
        public MessagesController(ILanguageService languageService, IDayService dayService, IMessageService messageSevice)
        {
            if (languageService == null)
                throw new ArgumentNullException("languageService");
            _languageService = languageService;

            if (dayService == null)
                throw new ArgumentNullException("dayService");
            _dayService = dayService;

            if (messageSevice == null)
                throw new ArgumentNullException("messageSevice");
            _messageSevice = messageSevice;
        }

        /// <summary>
        /// Get all languages
        /// </summary>
        /// <returns>languages collection</returns>
        [HttpGet]
        public IHttpActionResult GetLanguages()
        {
            var result = _languageService.GetLanguages().ToArray();
            return Ok(result);
        }

        /// <summary>
        /// Get all days
        /// </summary>
        /// <returns>days collection</returns>
        [HttpGet]
        public IHttpActionResult GetDays()
        {
            var result = _dayService.GetDays();
            return Ok(result);
        }

        /// <summary>
        /// Get Message by params
        /// </summary>
        /// <param name="dayId">Day Id</param>
        /// <param name="languageId">Language Id</param>
        /// <returns>Message entity</returns>
        [HttpGet]
        public IHttpActionResult GetMessage(int dayId, int languageId)
        {
            var result = _messageSevice.GetMessage(dayId, languageId);
            if (result == null)
            {
                return NotFound(); 
            }
            return Ok(result);
        }

        /// <summary>
        /// Change Message Text
        /// </summary>
        /// <param name="message">Message entity</param>
        /// <returns>Updated message entity</returns>
        [HttpPost]
        public IHttpActionResult SetMessage([FromBody] MessageDTO message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _messageSevice.SetMessage(message.Id, message.Text);
            return Ok();
        }
    }
}
