namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Dawn;
    using JetBrains.Annotations;

    public partial class ExpressionAnalyzeVisitor
    {
        protected override Expression VisitMethodCall([NotNull] MethodCallExpression node)
        {
            Guard.Argument(node)
                .NotNull()
                .Member(x => x.Method.Name, n => n.NotNull());

            var visitor = _methodCallFactory.Get(node);
            if(visitor == null)
                throw new ArgumentNullException($"There is no visitor for method {node.Method.Name}!");
            
            visitor.Visit(this, node, _context);
            return node;
        }
    }
}