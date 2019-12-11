namespace QueryToGraphQL.Parser
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;

    public partial class QueryParser
    {
        private void StartQueryBody(Expression expression, StringBuilder queryString)
        {
            var entityType = expression.Type.GenericTypeArguments.First();
            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var last = properties.Last();
            foreach (var property in properties)
            {
                queryString.Append(property.Name);
                if(property != last)
                    queryString.Append(",");
            }
        }
        
        private void EndQueryBody(Expression expression, StringBuilder queryString)
        {
            
        }
    }
}