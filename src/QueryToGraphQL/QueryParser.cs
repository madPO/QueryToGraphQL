namespace QueryToGraphQL
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Helper;

    public class QueryParser
    {
        private Expression _expression;

        public QueryParser FromQuery(IQueryable query)
        {
            if(query == null)
                throw new ArgumentException("query");
            
            _expression = query.Expression;
            return this;
        }

        public string Build()
        {
            var entityType = _expression.Type.GenericTypeArguments.First();
            return $"{QueryTemplateConstant.Name} {QueryTemplateConstant.PrefixName}{entityType.Name} {{}}";
        }
        
    }
}