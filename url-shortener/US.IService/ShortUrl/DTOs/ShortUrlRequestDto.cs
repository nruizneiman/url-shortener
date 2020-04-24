using System;
using System.ComponentModel.DataAnnotations;
using static US.Application.Validation.CustomValidation;

namespace US.IService.ShortUrl.DTOs
{
    public class ShortUrlRequestDto
    {
        [Required]
        [CheckUrlValid(ErrorMessage = "Please enter a valid URL")]
        public string LongURL { get; set; }

        public DateTime TimeStamp
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
