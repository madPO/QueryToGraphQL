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
            
            queryString.Append(string.Join(",", context.Properties.Values));
        }
        
        private void EndQueryBody(Context context, StringBuilder queryString)
        {
            
        }
    }
}