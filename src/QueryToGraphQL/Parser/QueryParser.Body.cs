namespace QueryToGraphQL.Parser
{
    using System.Collections.Immutable;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Context;

    public partial class QueryParser
    {
        private void StartQueryBody(Context context, StringBuilder queryString)
        {
            var properties = context.Properties;
            if (context.Properties.IsEmpty)
            {
                properties = context.BaseType
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(x => x.Name, x => x.PropertyType)
                    .ToImmutableDictionary();
            }
                
            var last = properties.Last().Key;
            foreach (var property in properties)
            {
                queryString.Append(property.Key);
                if(property.Key != last)
                    queryString.Append(",");
            }
        }
        
        private void EndQueryBody(Context context, StringBuilder queryString)
        {
            
        }
    }
}