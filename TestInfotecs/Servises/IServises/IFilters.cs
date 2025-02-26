using TestInfotecs.Models;

namespace TestInfotecs.Servises.IServises
{
    public interface IFilters
    {
        IQueryable<Models.Results> DataFilter(IQueryable<Models.Results> dataResults, Filter filter);
        IQueryable<Models.Results> FileNameFilter(IQueryable<Models.Results> dataResults, Filter filter);
        IQueryable<Models.Results> ExecutionTimeFilter(IQueryable<Models.Results> dataResults, Filter filter);
        IQueryable<Models.Results> ValueFilter(IQueryable<Models.Results> dataResults, Filter filter);

    }
}
