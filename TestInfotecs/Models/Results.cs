namespace TestInfotecs.Models
{
    public class Results
    {
        public int ResultsId { get; set; } 
        public string FileName { get; set; }
        public double DeltaDate { get; set; }
        public DateTime MinDate { get; set; }
        public double SrExecutionTime {  get; set; }
        public double SrValue { get; set; }
        public double MedianValues { get; set; }
        public double MaxValues { get; set; }
        public double MinValues { get; set; }



    }
}
