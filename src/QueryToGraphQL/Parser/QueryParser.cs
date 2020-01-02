namespace QueryToGraphQL.Parser
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Dawn;
    using ExpressionAnalyze;
    using JetBrains.Annotations;

    public partial class QueryParser
    {
        private Expression _expression;
        
        private readonly StringBuilder _queryString;

        public QueryParser()
        {
            _queryString = new StringBuilder();
        }

        public QueryParser FromQuery([NotNull] IQueryable query)
        {
            Guard.Argument(query)
                .NotNull()
                .Member(x => x.Expression, x => x.NotNull());

            _expression = query.Expression;
            return this;
        }

        public string Parse()
        {
            Guard.Argument(_expression).NotNull();
                
            var analyzer = new ExpressionAnalyzer();
            var context = analyzer.Analyze(_expression);
            StartQuery(context, _queryString);
            QueryParametrs(context, _queryString);
            _queryString.Append("{");
            StartQueryBody(context, _queryString);
            EndQueryBody(context, _queryString);
            _queryString.Append("}");
            return _queryString.ToString();
        }
        
    }
}