namespace UnitTest.Simple
{
    using System;
    using Helpers;
    using Models.Simple;
    using QueryToGraphQL;
    using Xunit;

    public class QueryParserTest
    {
        [Fact]
        public void queryparser_string_return()
        {
            QueryParserFactory.Create(TestHelper.CreateQuery<BaseModel>());
        }

        [Fact]
        public void queryparser_argument_exception()
        {
            Assert.Throws<ArgumentNullException>(() => QueryParserFactory.Create(null));
        }

        [Fact]
        public void queryparser_query_return()
        {
            var query = TestHelper.CreateQuery<BaseModel>();
            var result =  QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Contains("query", result);
        }

        [Fact]
        public void queryparser_query_name_condition()
        {
            var query = TestHelper.CreateQuery<BaseModel>();
            var result =  QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Contains("GetBaseModel", result);
        }
    }
}