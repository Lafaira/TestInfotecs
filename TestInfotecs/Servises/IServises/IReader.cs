using TestInfotecs.Models;

namespace TestInfotecs.Servises.IServises
{
    public interface IReader
    {
        List<CSVModel> ReadFile(IFormFile filename);
    }
}
