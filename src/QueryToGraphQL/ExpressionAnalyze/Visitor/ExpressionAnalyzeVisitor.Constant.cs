namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public partial class ExpressionAnalyzeVisitor
    {
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if(!node.Type.IsGenericType)
                throw new ArgumentException("Generic type expected!");

            _context.BaseType = node.Type.GenericTypeArguments.First();
            return node;
        }
    }
}