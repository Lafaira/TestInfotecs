using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Globalization;
using TestInfotecs.Models;
using TestInfotecs.Servises.IServises;

namespace TestInfotecs.Servises
{
    public class CsvReader : IServises.IReader
    {
        public List<CSVModel> ReadFile(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null, 
                TrimOptions = TrimOptions.Trim,

            };

            using var csv = new CsvHelper.CsvReader(reader, config);

            try
            {
                var result = csv.GetRecords<CSVModel>().ToList(); 
                return result;
            }
            catch
            {
                throw new Exception("Файл не содержит столбец или столбцы: Date, ExecutionTime, Value. Или допущена ошибка при их написании");
                
            }

        }
    }


}
