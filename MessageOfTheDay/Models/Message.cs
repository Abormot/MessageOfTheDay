using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MessageOfTheDay.Models
{
    public class Message
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Language Id is required")]
        public int LanguageId { get; set; }
        public string Language { get; set; }

        [Required(ErrorMessage = "Day Id is required")]
        public int DayId { get; set; }
        public string Day { get; set; }

        [Required(ErrorMessage="Parameter Text is required")]
        [StringLength(1024, MinimumLength=1, ErrorMessage="Message text must have a value with length min 1 and max 1024 characters")]
        public string Text { get; set; }
    }
}