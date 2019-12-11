namespace QueryToGraphQL
{
    using System.Linq;
    using Parser;

    public static class QueryParserFactory
    {
        public static QueryParser Create(IQueryable query)
        {
            var parser = new QueryParser();
            return parser.FromQuery(query);
        }
    }
}