namespace UnitTest.Simple
{
    using System.Linq;
    using Helpers;
    using Models.Simple;
    using QueryToGraphQL;
    using Xunit;

    public class SimpleQueryTest
    {
        [Fact]
        public void simple_query_for_all_field_of_basemodel()
        {
            var query = TestHelper.CreateQuery<BaseModel>();
            var result = QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Equal("query GetBaseModel{Id}", result);
        }
        
        [Fact]
        public void simple_query_for_all_field_of_episode()
        {
            var query = TestHelper.CreateQuery<Episode>();
            var result = QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Equal("query GetEpisode{Author,Id,Name}", result);
        }
        
        [Fact]
        public void simple_query_with_select_of_episode()
        {
            var query = TestHelper.CreateQuery<Episode>()
                .Select(x => new { x.Id });
            var result = QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Equal("query GetEpisode{Id}", result);
        }

        [Fact]
        public void simple_query_with_select_of_person()
        {
            var query = TestHelper.CreateQuery<Episode>()
                .Select(x => new { x.Id, x.Author.Name });
            var result = QueryParserFactory
                .Create(query)
                .Parse();
            
            Assert.Equal("query GetEpisode{Author{Name},Id}", result);
        }
    }
}