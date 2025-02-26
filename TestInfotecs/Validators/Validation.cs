using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;
using TestInfotecs.Models;

namespace TestInfotecs.Validators
{
    public class Validation : AbstractValidator<CSVModel>  
    {
        
        public Validation ()
        {
            RuleFor(x => x.Date).NotEmpty()
            .Must(DateFormat).WithMessage("Дата должна быть в формате (yyyy-MM-ddTHH:mm:ss.ffffZ)")
            .Must(DateMin).WithMessage("Дата должна быть больше 01.01.2000")
            .Must(DateMax).WithMessage("Дата должна быть меньше текущей");
            

            RuleFor(x => x.ExecutionTime)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Время выполнения не может быть меньше 0");

            RuleFor(x => x.Value)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Значение показателя не может быть меньше 0");
        }

        private bool DateFormat(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) return true;

            string pattern = @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{1,4}Z$";
            return Regex.IsMatch(dateString, pattern);
        }

        private bool DateMin(CSVModel model, string dateString, ValidationContext<CSVModel> context)
        {
            if (string.IsNullOrEmpty(dateString)) return true;

            if (!DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.ffffZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return true; 
            }

            return date > new DateTime(2000, 01, 01);
        }

        private bool DateMax(CSVModel model, string dateString, ValidationContext<CSVModel> context)
        {
            if (string.IsNullOrEmpty(dateString)) return true;

            if (!DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.ffffZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return true; 
            }

            return date < DateTime.Now;
        }

    }
}
