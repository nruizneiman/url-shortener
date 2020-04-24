using System;
using System.ComponentModel.DataAnnotations;

namespace US.Application.Validation
{
    public class CustomValidation
    {
        public sealed class CheckUrlValid : ValidationAttribute
        {
            protected override ValidationResult IsValid(object url, ValidationContext validationContext)
            {
                Uri uriResult;

                bool result = Uri.TryCreate(url.ToString(), UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (result)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Please enter a valid Url");
            }
        }
    }
}
