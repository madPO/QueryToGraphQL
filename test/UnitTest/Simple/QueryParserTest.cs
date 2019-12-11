namespace UnitTest.Simple
{
    using System;
    using System.Linq;
    using Models.Simple;
    using QueryToGraphQL;
    using Xunit;

    public class QueryParserTest
    {
        [Fact]
        public void queryparser_string_return()
        {
            QueryParserFactory.Create(Enumerable.Empty<BaseModel>().AsQueryable());
        }

        [Fact]
        public void queryparser_argument_exception()
        {
            Assert.Throws<ArgumentException>(() => QueryParserFactory.Create(null));
        }

        [Fact]
        public void queryparser_query_return()
        {
            var query = Enumerable.Empty<BaseModel>().AsQueryable();
            var result =  QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Contains("query", result);
        }

        [Fact]
        public void queryparser_query_name_condition()
        {
            var query = Enumerable.Empty<BaseModel>().AsQueryable();
            var result =  QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Contains("GetBaseModel", result);
        }
    }
}