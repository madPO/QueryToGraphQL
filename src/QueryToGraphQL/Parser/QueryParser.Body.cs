namespace QueryToGraphQL.Parser
{
    using System.Linq;
    using System.Text;
    using Context;
    using Dawn;
    using JetBrains.Annotations;

    public partial class QueryParser
    {
        private void StartQueryBody([NotNull] Context context, [NotNull] StringBuilder queryString)
        {
            Guard.Argument(context)
                .NotNull()
                .Member(x => x.Properties, p => p.NotEmpty());
            Guard.Argument(queryString).NotNull();
            
            var properties = context.Properties;
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