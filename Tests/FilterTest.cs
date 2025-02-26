using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestInfotecs.Models;
using TestInfotecs.Servises;
using Results = TestInfotecs.Models.Results;

namespace Tests
{
    public class FilterTest
    {
        private readonly List<Results> _resultsData;
        private readonly Filters _filter;

        public FilterTest()
        {
            _filter = new Filters();
            _resultsData = new List<Results>
            {
            new Results { ResultsId=1, FileName="test1", DeltaDate=10576, MinDate= new DateTime(2001, 1, 03, 01, 30, 25, 45), SrExecutionTime=15, SrValue=9.87, MedianValues=3, MaxValues=16, MinValues=1},
            new Results { ResultsId=2, FileName="test2", DeltaDate=5012, MinDate= new DateTime(2008, 3, 09, 05, 30, 25, 45), SrExecutionTime=26, SrValue=12.907, MedianValues=7, MaxValues=19, MinValues=3},
            new Results { ResultsId=3, FileName="test3", DeltaDate=2638, MinDate= new DateTime(2015, 7, 15, 08, 30, 25, 45), SrExecutionTime=33, SrValue=21, MedianValues=9.78, MaxValues=25, MinValues=4.55},
            new Results { ResultsId=4, FileName="test4", DeltaDate=1012, MinDate= new DateTime(2019, 9, 23, 15, 30, 25, 45), SrExecutionTime=46, SrValue=26.75, MedianValues=12, MaxValues=33, MinValues=6.078},
            new Results { ResultsId=5, FileName="test5", DeltaDate=561, MinDate= new DateTime(2024, 12, 31, 23, 30, 25, 45), SrExecutionTime=59, SrValue= 37, MedianValues=18, MaxValues=41, MinValues=7},

            };
        }

        [Theory]
        [InlineData( 47, 60)]
        public void FilterExecutionTime_Valid_ReturnQueryable( double startExecutionTime, double endExecutionTime)
        {

            var query = _resultsData.AsQueryable();
            Filter filter = new Filter()
            {
                DateFilter = null,
                ExecutionTimeFilter = new ExecutionTimeFilter(startExecutionTime, endExecutionTime),
                ValueFilter = null
            };


            var result = _filter.ExecutionTimeFilter(query, filter).ToList();


            Assert.Equal(1, result.Count);
            Assert.Contains(result, p => p.FileName == "test5");
  
        }

        [Theory]
        [InlineData( 5, 10)]
        public void FilterValue_Valid_ReturnQueryable( double startValue, double endValue)
        {

            var query = _resultsData.AsQueryable();
            Filter filter = new Filter()
            {
                DateFilter = null,
                ExecutionTimeFilter = null,
                ValueFilter = new ValueFilter(startValue, endValue)
            };


            var result = _filter.ValueFilter(query, filter).ToList();


            Assert.Equal(1, result.Count);
            Assert.Contains(result, p => p.FileName == "test1");

        }

        [Theory]
        [InlineData( "test2")]
        public void FilterFileName_Valid_ReturnQueryable(  string fileName)
        {

            var query = _resultsData.AsQueryable();
            Filter filter = new Filter()
            {
                FileName = fileName,
                DateFilter = null,
                ExecutionTimeFilter = null,
                ValueFilter = null
            };


            var result = _filter.FileNameFilter(query, filter).ToList();


            Assert.Equal(1, result.Count);
            Assert.Contains(result, p => p.FileName == "test2");

        }

        [Theory]
        [InlineData("2018-10-27T10:30:00Z", "2021-10-27T10:30:00Z")]
        public void FilterDataFilter_Valid_ReturnQueryable(string startDate, string endDate)
        {
            DateTime date1 = DateTime.Parse(startDate);
            DateTime date2 = DateTime.Parse(endDate);

            var query = _resultsData.AsQueryable();

            Filter filter = new Filter()
            {
                FileName = null,
                DateFilter = new DateFilter() { StartDate= date1, EndDate = date2},
                ExecutionTimeFilter = null,
                ValueFilter = null
            };


            var result = _filter.DataFilter(query, filter).ToList();


            Assert.Equal(1, result.Count);
            Assert.Contains(result, p => p.FileName == "test4");

        }

    }
}
