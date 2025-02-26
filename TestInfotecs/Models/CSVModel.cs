using CsvHelper.Configuration.Attributes;

namespace TestInfotecs.Models
{
    public class CSVModel
    {
        
        public string Date { get; set; }
        public int ExecutionTime { get; set; }
        public double Value { get; set; }
    }
}
