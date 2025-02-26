//using CsvHelper;
using CsvHelper.Configuration;
using FluentValidation;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Globalization;
using TestInfotecs.Data;
using TestInfotecs.Models;
using TestInfotecs.Servises.IServises;


namespace TestInfotecs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimescaleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CSVModel> _csvValidator;
        private readonly Servises.IServises.IReader _csvReader;
        private readonly ICreateResults _createResults;
        private readonly IFilters _filters;

        public TimescaleController(AppDbContext context, IValidator<CSVModel> csvValidator, IReader csvReader, ICreateResults createResults, IFilters filters)
        {
            _context = context;
            _csvValidator = csvValidator;
            _csvReader = csvReader;
            _createResults = createResults;
            _filters = filters;
        }

        [HttpPost]
        public async Task<IActionResult> GetDateCSV (IFormFile file)
        {
            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("На вход принимаются только файлы CSV"); 
            }

            try
            {
                var readRecords = _csvReader.ReadFile(file);

                if (readRecords.Count <1 || readRecords.Count >1000)
                {
                    return BadRequest("Количество записей в файле не должно быть меньше 1 и больше 1000"); 
                }

                foreach (var readRecord in readRecords)
                {
                    var validRecords = await _csvValidator.ValidateAsync(readRecord);

                    if (!validRecords.IsValid)
                    {
                        return BadRequest(validRecords.Errors); 
                        
                    }
                }
              
                var resultValues = readRecords.Select(v => new Values(DateTime.Parse(v.Date).ToUniversalTime(), v.ExecutionTime, v.Value, file.FileName)).ToList();

                var resultResults = _createResults.CreateResults(resultValues, file.FileName);


                bool fileExists = await _context.Values.AnyAsync(e => e.FileName == file.FileName);

                if (fileExists)
                {
                    var valuesToDelete = _context.Values.Where(e => e.FileName == file.FileName);
                    var resultToDelete = _context.Results.First(e => e.FileName == file.FileName);

                    _context.Values.RemoveRange(valuesToDelete);
                    _context.Results.Remove(resultToDelete);
                }
                else
                {
                    await _context.Values.AddRangeAsync(resultValues);
                   
                    await _context.Results.AddAsync(resultResults);
                    
                }

                await _context.SaveChangesAsync();

                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetResult([FromQuery] Filter filter )
        {
            IQueryable<Models.Results> dataResults = _context.Results;

            if (filter.DateFilter != null)
            {
                dataResults = _filters.DataFilter(dataResults, filter);
            }
             if (filter.FileName != null) 
            {
                dataResults = _filters.FileNameFilter(dataResults, filter);
            }
            if (filter.ExecutionTimeFilter != null)
            {
                dataResults = _filters.ExecutionTimeFilter(dataResults, filter);
            }
            if (filter.ValueFilter != null)
            {
                dataResults = _filters.ValueFilter(dataResults, filter);
            }

            List<Models.Results> filteredResults = await dataResults.ToListAsync();

            return Ok(filteredResults);
        }

        
        [HttpGet]
        [Route("/test")]
        public IActionResult NameDateFilter ()
        {
            var result = _context.Results.OrderBy(v => v.MinDate).ThenByDescending( v => v.FileName).Take(10);

            return Ok(result);
        }
        
    }
}
