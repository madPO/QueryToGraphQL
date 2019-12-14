namespace QueryToGraphQL.Parser
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public partial class QueryParser
    {
        private Expression _expression;

        private Type _returnType;

        private StringBuilder _queryString;

        public QueryParser()
        {
            _queryString = new StringBuilder();
        }

        public QueryParser FromQuery(IQueryable query)
        {
            if(query == null)
                throw new ArgumentException("query");
            
            _returnType = query.ElementType;
            _expression = query.Expression;
            return this;
        }

        public string Parse()
        {
            if(_expression == null)
                throw new ArgumentException("_expression");
                
            StartQuery(_expression, _queryString);
            QueryParametrs();
            _queryString.Append("{");
            StartQueryBody(_expression, _queryString);
            EndQueryBody(_expression, _queryString);
            _queryString.Append("}");
            return _queryString.ToString();
        }
        
    }
}