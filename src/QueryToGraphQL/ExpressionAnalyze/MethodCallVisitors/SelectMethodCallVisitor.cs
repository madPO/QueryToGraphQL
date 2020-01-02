namespace QueryToGraphQL.ExpressionAnalyze.MethodCallVisitors
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Context;
    using Dawn;

    public class SelectMethodCallVisitor: IMethodCallVisitor
    {
        public bool HasVisit(MethodCallExpression node)
        {
            Guard.Argument(node)
                .NotNull()
                .Member(x => x.Method.Name, n => n.NotNull());

            return node.Method.Name.ToLower() == "select";
        }

        public void Visit(ExpressionVisitor visitor, MethodCallExpression node, Context context)
        {
            Guard.Argument(node)
                .NotNull()
                .Member(x => x.Method.ReturnType, r => r.NotNull())
                .Member(x => x.Arguments, a => a.NotEmpty());
            Guard.Argument(visitor).NotNull();
            Guard.Argument(context).NotNull();
                
            
            var queryType = node.Method.ReturnType;
            if (!queryType.IsGenericType)
                return;

            var type = queryType.GenericTypeArguments.First();
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in props)
            {
                context.AddProperty(prop.Name, prop.PropertyType);
            }
            
            visitor.Visit(node.Arguments.First());
        }
    }
}