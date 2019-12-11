namespace QueryToGraphQL
{
    using System.Linq;
    
    public static class QueryParserFactory
    {
        public static string Create(IQueryable query)
        {
            var parser = new QueryParser();
            parser.FromQuery(query);
            return parser.Build();
        }
    }
}