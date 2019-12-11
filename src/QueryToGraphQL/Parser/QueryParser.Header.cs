namespace QueryToGraphQL.Parser
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Helper;

    public partial class QueryParser
    {
        private void StartQuery(Expression expression, StringBuilder queryString)
        {
            var entityType = _expression.Type.GenericTypeArguments.First();
            queryString.Append(QueryTemplateConstant.Query);
            queryString.Append(" ");
            queryString.Append(QueryTemplateConstant.GetPrefixConvention);
            queryString.Append(entityType.Name);
        }
        
    }
}