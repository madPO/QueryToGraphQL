namespace QueryToGraphQL.Parser
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Helper;

    public partial class QueryParser
    {
        private void StartQuery(Expression expression, StringBuilder queryString)
        {
            var entityType = GetBaseType(expression);
            queryString.Append(QueryTemplateConstant.Query);
            queryString.Append(" ");
            queryString.Append(QueryTemplateConstant.GetPrefixConvention);
            queryString.Append(entityType.Name);
        }

        private Type GetBaseType(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
                return expression.Type.GenericTypeArguments.First();

            if (expression.NodeType == ExpressionType.Call)
                return (expression as MethodCallExpression)
                    ?.Arguments
                    ?.First()
                    ?.Type
                    ?.GenericTypeArguments
                    ?.First();

            return null;
        }
    }
}