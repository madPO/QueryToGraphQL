namespace QueryToGraphQL.Parser
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using ExpressionAnalyze;

    public partial class QueryParser
    {
        private Expression _expression;
        
        private readonly StringBuilder _queryString;

        public QueryParser()
        {
            _queryString = new StringBuilder();
        }

        public QueryParser FromQuery(IQueryable query)
        {
            if(query == null)
                throw new ArgumentException("query");

            _expression = query.Expression;
            return this;
        }

        public string Parse()
        {
            if(_expression == null)
                throw new ArgumentException("_expression");
                
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