namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public partial class ExpressionAnalyzeVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var methodName = node.Method.Name.ToLower();
            switch (methodName)
            {
                case "select":
                    SelectVisit(node);
                    break;
            }

            return node;
        }

        private void SelectVisit(MethodCallExpression node)
        {
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