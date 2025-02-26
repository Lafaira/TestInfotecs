using TestInfotecs.Models;

namespace TestInfotecs.Servises.IServises
{
    public interface ICreateResults
    {
        Models.Results CreateResults(List<Values> model, string fileName); 
    }
}
