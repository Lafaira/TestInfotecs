using CsvHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestInfotecs.Models;
using TestInfotecs.Servises;
using TestInfotecs.Servises.IServises;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Xunit.Sdk;

namespace Tests
{
    public class CsvReadTest
    {
        
        [Fact]
        public void ReadCsvFile_ValidData_ReturnsCorrectCount()
        {
            string csvContent = "Date,ExecutionTime,Value\n2000-07-15T10:30:00.0000Z, 30, 0.87\n2010-12-16T10:30:00.0000Z, 45, 4";
            var file = MockFormFile.Create(csvContent);
            var processor = new TestInfotecs.Servises.CsvReader();

            List<CSVModel> result = processor.ReadFile(file);

            List<CSVModel> test = new List<CSVModel>()
            {
            new CSVModel
                { Date = "2000-07-15T10:30:00.0000Z",
                ExecutionTime = 30,
                Value = 0.87},
            new CSVModel
                { Date = "2010-12-16T10:30:00.0000Z",
                ExecutionTime = 45,
                Value = 4}
            };

            Assert.Equal(test.Count, result.Count);
            for (int i = 0; i < test.Count; i++)
            {
                Assert.Equal(test[i].Date, result[i].Date);
                Assert.Equal(test[i].ExecutionTime, result[i].ExecutionTime);
                Assert.Equal(test[i].Value, result[i].Value, 2); 
            }

        }
        
        [Theory]
        [InlineData("ExecutionTime,Value\n15.07.2000, 30, 0.87\n16.12.2010, 45, 4")]
        [InlineData("Date,Value\n15.07.2000, 30, 0.87\n16.12.2010, 45, 4")]
        [InlineData("Date,ExecutionTime\n15.07.2000, 30, 0.87\n16.12.2010, 45, 4")]
        public void ReadCsvFile_InvalidData_ReturnsException(string csvContent)
        {
            var file = MockFormFile.Create(csvContent);
            var processor = new TestInfotecs.Servises.CsvReader();

            var exception = Assert.Throws<Exception>(() => processor.ReadFile(file));

            Assert.Equal("Файл не содержит столбец или столбцы: Date, ExecutionTime, Value. Или допущена ошибка при их написании", exception.Message);

        }

    }

    public static class MockFormFile
    {
        public static IFormFile Create(string content, string fileName = "test.csv", string contentType = "text/csv")
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            var formFile = new Mock<IFormFile>();
            formFile.Setup(x => x.OpenReadStream()).Returns(stream);
            formFile.Setup(x => x.FileName).Returns(fileName);
            formFile.Setup(x => x.ContentType).Returns(contentType);
            formFile.Setup(x => x.Length).Returns(stream.Length);

            return formFile.Object;
        }
    }

}
