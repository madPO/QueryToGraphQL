namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
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
            
            var methodName = node.Method.Name.ToLower();
            switch (methodName)
            {
                case "select":
                    SelectVisit(node);
                    break;
            }

            return node;
        }

        private void SelectVisit([NotNull] MethodCallExpression node)
        {
            Guard.Argument(node)
                .NotNull()
                .Member(x => x.Method.ReturnType, r => r.NotNull())
                .Member(x => x.Arguments, a => a.NotEmpty());
                
            
            var queryType = node.Method.ReturnType;
            if (!queryType.IsGenericType)
                return;

            var type = queryType.GenericTypeArguments.First();
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in props)
            {
                _context.AddProperty(prop.Name, prop.PropertyType);
            }
            
            Visit(node.Arguments.First());
        }
    }
}