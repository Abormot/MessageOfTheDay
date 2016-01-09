using MessageOfTheDay.Models;
using MessageOfTheDay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MessageOfTheDay.Controllers
{
    public class MessagesController : ApiController
    {
        private readonly ILanguageService _languageService;
        private readonly IDayService _daysService;
        private readonly IMessageService _messageSevice;

        public MessagesController(ILanguageService languageService, IDayService daysService, IMessageService messageSevice)
        {
            if (languageService == null)
                throw new ArgumentNullException("ILanguageService passed to MessagesController is null");
            _languageService = languageService;

            if (daysService == null)
                throw new ArgumentNullException("IDaysService passed to MessagesController is null");
            _daysService = daysService;

            if (messageSevice == null)
                throw new ArgumentNullException("IMessageSevice passed to MessagesController is null");
            _messageSevice = messageSevice;
        }

        // GET api/Messages/GetLanguages
        [HttpGet]
        public IHttpActionResult GetLanguages()
        {
            var result = _languageService.GetLanguagesQuery().ToArray();
            if (result.Any())
                return Ok(result);
            return BadRequest();
        }

        // GET api/Messages/GetDays
        [HttpGet]
        public IHttpActionResult GetDays()
        {
            var result = _daysService.GetDaysQuery().ToArray();
            if (result.Any())
                return Ok(result);
            return BadRequest();
        }

        // GET api/Messages/GetMessage?dayid=<value>&languageID=<value>
        [HttpGet]
        public IHttpActionResult GetMessage(int dayId, int languageId)
        {
            var result = _messageSevice.GetMessageQuery(dayId, languageId);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        // POST api/Messages/SetMessage
        [HttpPost]
        public IHttpActionResult SetMessage([FromUri] int Id, [FromBody] Message message)
        {
            if (ModelState.IsValid)
            {
                return Ok(_messageSevice.SetMessageCommand(Id, message.Text));
            }
            else
            {
                var error = ModelState.Values.SelectMany(modelState => modelState.Errors).FirstOrDefault();
                return BadRequest(error!= null ? error.ErrorMessage : "Some error occured");
            }
        }
    }
}
