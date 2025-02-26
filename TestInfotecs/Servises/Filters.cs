using TestInfotecs.Models;
using TestInfotecs.Servises.IServises;

namespace TestInfotecs.Servises
{
    public class Filters : IFilters
    {
        public IQueryable<Models.Results> DataFilter( IQueryable<Models.Results> dataResults, Filter filter)
        {
            if (filter.DateFilter.StartDate != DateTime.MinValue && filter.DateFilter.EndDate != DateTime.MinValue)
            {
                dataResults = dataResults.Where(v => v.MinDate >= filter.DateFilter.StartDate.ToUniversalTime() && v.MinDate <= filter.DateFilter.EndDate.ToUniversalTime());
                return dataResults;
            }
            if (filter.DateFilter.StartDate != DateTime.MinValue && filter.DateFilter.EndDate == DateTime.MinValue)
            {
                dataResults = dataResults.Where(v => v.MinDate >= filter.DateFilter.StartDate.ToUniversalTime());
                return dataResults;
            }
            if (filter.DateFilter.StartDate == DateTime.MinValue && filter.DateFilter.EndDate != DateTime.MinValue)
            {
                dataResults = dataResults.Where(v => v.MinDate <= filter.DateFilter.EndDate.ToUniversalTime());
                return dataResults;
            }

            return dataResults;

        }

        public IQueryable<Models.Results> FileNameFilter(IQueryable<Models.Results> dataResults, Filter filter)
        {
            return dataResults = dataResults.Where(v => v.FileName == filter.FileName);
        }

        public IQueryable<Models.Results> ExecutionTimeFilter(IQueryable<Models.Results> dataResults, Filter filter)
        {
            if (filter.ExecutionTimeFilter.StartExecutionTime != 0 && filter.ExecutionTimeFilter.EndExecutionTime == 0)
            {
                dataResults = dataResults.Where(v => v.SrExecutionTime >= filter.ExecutionTimeFilter.StartExecutionTime);
                return dataResults;
            }
            if (filter.ExecutionTimeFilter.StartExecutionTime == 0 && filter.ExecutionTimeFilter.EndExecutionTime != 0)
            {
                dataResults = dataResults.Where(v => v.SrExecutionTime <= filter.ExecutionTimeFilter.EndExecutionTime);
                return dataResults;
            }
            if (filter.ExecutionTimeFilter.StartExecutionTime != 0 && filter.ExecutionTimeFilter.EndExecutionTime != 0)
            {
                dataResults = dataResults.Where(v => v.SrExecutionTime >= filter.ExecutionTimeFilter.StartExecutionTime && v.SrExecutionTime <= filter.ExecutionTimeFilter.EndExecutionTime);
                return dataResults;
            }

            return dataResults;
        }

        public IQueryable<Models.Results> ValueFilter(IQueryable<Models.Results> dataResults, Filter filter)
        {
            if (filter.ValueFilter.StartValue != 0 && filter.ValueFilter.EndValue == 0)
            {
                dataResults = dataResults.Where(v => v.SrValue >= filter.ValueFilter.StartValue);
                return dataResults;
            }
            if (filter.ValueFilter.StartValue == 0 && filter.ValueFilter.EndValue != 0)
            {
                dataResults = dataResults.Where(v => v.SrValue <= filter.ValueFilter.EndValue);
                return dataResults;
            }
            if (filter.ValueFilter.StartValue != 0 && filter.ValueFilter.EndValue != 0)
            {
                dataResults = dataResults.Where(v => v.SrValue >= filter.ValueFilter.StartValue && v.SrValue <= filter.ValueFilter.EndValue);
                return dataResults;
            }

            return dataResults;
        }
    }
}
