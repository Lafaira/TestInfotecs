using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestInfotecs.Models
{
    public class Values
    {
        public int ValuesId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double ExecutionTime { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public string FileName { get; set; }

        
        public Values (DateTime date, double executionTime, double value, string fileName)
        {
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
            FileName = fileName;
        }
        

       
    }
}
