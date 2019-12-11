namespace UnitTest.Simple
{
    using System.Linq;
    using Models.Simple;
    using QueryToGraphQL;
    using Xunit;

    public class SimpleQueryTest
    {
        [Fact]
        public void simple_query_for_all_field()
        {
            var query = Enumerable.Empty<BaseModel>().AsQueryable();
            var result = QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Equal("query GetBaseModel{Id}", result);
        }
    }
}