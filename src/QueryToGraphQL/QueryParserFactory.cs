namespace QueryToGraphQL
{
    using System.Linq;
    using Dawn;
    using JetBrains.Annotations;
    using Parser;

    public static class QueryParserFactory
    {
        public static QueryParser Create([NotNull] IQueryable query)
        {
            Guard.Argument(query).NotNull();
            
            var parser = new QueryParser();
            return parser.FromQuery(query);
        }
    }
}