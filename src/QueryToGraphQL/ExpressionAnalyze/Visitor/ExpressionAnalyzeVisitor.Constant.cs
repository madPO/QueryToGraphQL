namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
    using System.Linq;
    using System.Linq.Expressions;
    using Dawn;
    using JetBrains.Annotations;

    public partial class ExpressionAnalyzeVisitor
    {
        protected override Expression VisitConstant([NotNull] ConstantExpression node)
        {
            Guard.Argument(node)
                .Member(x => x.Type, t => t.NotNull())
                .Member(
                    x => x.Type,
                    t => t.Require(x => x.IsGenericType, s => "Generic type expected!"))
                .Member(x => x.Type.GenericTypeArguments, a => a.NotEmpty());

            _context.BaseType = node.Type.GenericTypeArguments.First();
            return node;
        }
    }
}