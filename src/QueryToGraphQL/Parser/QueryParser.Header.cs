namespace QueryToGraphQL.Parser
{
    using System.Text;
    using Context;
    using Helper;

    public partial class QueryParser
    {
        private void StartQuery(Context context, StringBuilder queryString)
        {
            queryString.Append(QueryTemplateConstant.Query);
            queryString.Append(" ");
            queryString.Append(QueryTemplateConstant.GetPrefixConvention);
            queryString.Append(context.BaseType.Name);
        }
    }
}