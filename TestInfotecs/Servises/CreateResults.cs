using TestInfotecs.Models;
using TestInfotecs.Servises.IServises;

namespace TestInfotecs.Servises
{
    public class CreateResults : ICreateResults
    {
        private Models.Results _results;

        public CreateResults()
        {
            _results = new Models.Results();
        }

        Models.Results ICreateResults.CreateResults(List<Values> model, string fileName)
        {
            _results.FileName = fileName; // мб передать имя файла так не оч красиво
            _results.DeltaDate = (model.Max(v => v.Date) - model.Min(v => v.Date)).TotalSeconds;
            _results.MinDate = model.Min(v => v.Date);
            _results.SrExecutionTime = model.Average(v => v.ExecutionTime);
            _results.SrValue = model.Average(v => v.Value);
            _results.MaxValues = model.Max(v => v.Value);
            _results.MinValues = model.Min(v => v.Value);
            var sortValues = model.OrderBy(v => v.Value).ToList();       
            int countValues = model.Count();
            if (countValues % 2 == 0)
            {
                _results.MedianValues = (sortValues[countValues / 2].Value + sortValues[countValues / 2 + 1].Value) / 2;
            }
            else
            {
                _results.MedianValues = sortValues[(countValues + 1) / 2].Value;
            }
            
            return _results;
        }
    }
}
