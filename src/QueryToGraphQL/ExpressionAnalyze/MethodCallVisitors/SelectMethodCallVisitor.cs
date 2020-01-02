namespace QueryToGraphQL.ExpressionAnalyze.MethodCallVisitors
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Context;
    using Dawn;
    using JetBrains.Annotations;
    using Structure;

    public class SelectMethodCallVisitor : IMethodCallVisitor
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

            var unary = node.Arguments.FirstOrDefault(x => x is UnaryExpression) as UnaryExpression;
            var lambda = unary.Operand as LambdaExpression;
            var newExpression = lambda.Body as NewExpression;

            foreach (var argument in newExpression.Arguments)
            {
                var property = GetProperty(argument, context);
                if (!context.Properties.ContainsKey(property.Alias))
                {
                    context.AddProperty(property.Alias, property);
                }
            }
            
            visitor.Visit(node.Arguments.First(x => !(x is UnaryExpression)));
        }

        private QueryProperty GetProperty([NotNull] Expression expression, [NotNull] Context context)
        {
            Guard.Argument(expression).NotNull();
            Guard.Argument(context).NotNull();
            var member = expression as MemberExpression;
            if(member == null)
                throw new NotSupportedException();

            QueryProperty property = null;
            
            if (member.Expression is MemberExpression child)
            {
                property = GetProperty(child, context);
                if (property.Properties.All(x => x.Alias != member.Member.Name))
                {
                    property.AddProperty(member.Member.Name);
                }
            }
            else
            {
                if (context.Properties.ContainsKey(member.Member.Name))
                {
                    return context.Properties[member.Member.Name];
                }
                
                property = new QueryProperty(member.Member.Name);
            }

            return property;
        }
    }
}