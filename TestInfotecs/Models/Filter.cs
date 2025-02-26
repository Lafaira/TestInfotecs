using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestInfotecs.Models
{
    public class Filter
    {
        public string? FileName { get; set; }
        public DateFilter? DateFilter { get; set; }

        public ExecutionTimeFilter? ExecutionTimeFilter { get; set;}
        public ValueFilter? ValueFilter { get; set; }


    }

    public class DateFilter
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH-mm-ss.ffffZ}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH-mm-ss.ffffZ}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
    //public record DateFilter (DateTime StartDate, DateTime EndDate);
    public record ExecutionTimeFilter(double StartExecutionTime, double EndExecutionTime);
    public record ValueFilter(double StartValue, double EndValue);

    
}
