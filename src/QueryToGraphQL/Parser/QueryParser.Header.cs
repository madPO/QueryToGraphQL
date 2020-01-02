namespace QueryToGraphQL.Parser
{
    using System.Text;
    using Context;
    using Dawn;
    using Helper;
    using JetBrains.Annotations;

    public partial class QueryParser
    {
        private void StartQuery([NotNull] Context context, StringBuilder queryString)
        {
            Guard.Argument(context)
                .NotNull()
                .Member(x => x.BaseType.Name, x => x.NotNull());
            Guard.Argument(queryString).NotNull();
            
            queryString.Append(QueryTemplateConstant.Query);
            queryString.Append(" ");
            queryString.Append(QueryTemplateConstant.GetPrefixConvention);
            queryString.Append(context.BaseType.Name);
        }
    }
}