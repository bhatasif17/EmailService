using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailService.Models
{
    public class EmailViewModel
    {
        [RequiredIf("Recipients", null)]
        [MinLength(5, ErrorMessage = "Recipient Name must be at least 5 characters.")]
        public string RecipientName { get; set; }
        [Required]
        [EmailAddress]
        public string RecipientEmail { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Subject must be at least 10 characters.")]
        public string Subject { get; set; }
        public byte[] Attachment { get; set; }
        public IEnumerable<string> Recipients { get; set; }
    }

    public class RequiredIfAttribute : ValidationAttribute
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }
        public object _targetValue { get; set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            this._dependentProperty = dependentProperty;
            this._targetValue = targetValue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if ((dependentValue == null && _targetValue == null) || (dependentValue.Equals(_targetValue)))
                {
                    if (!_innerAttribute.IsValid(value))
                    {
                        string name = validationContext.DisplayName;
                        return new ValidationResult(ErrorMessage = name + " Is required.");
                    }
                }
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
            }
        }
    }
}
