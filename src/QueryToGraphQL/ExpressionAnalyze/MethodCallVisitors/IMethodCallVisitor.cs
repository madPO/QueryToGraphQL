namespace QueryToGraphQL.ExpressionAnalyze.MethodCallVisitors
{
    using System.Linq.Expressions;
    using Context;
    using JetBrains.Annotations;

    public interface IMethodCallVisitor
    {
        bool HasVisit([NotNull] MethodCallExpression node);

        void Visit([NotNull] ExpressionVisitor visitor, [NotNull] MethodCallExpression node,[NotNull] Context context);
    }
}