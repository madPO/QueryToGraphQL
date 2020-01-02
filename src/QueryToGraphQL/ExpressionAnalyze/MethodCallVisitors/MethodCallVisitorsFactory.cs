namespace QueryToGraphQL.ExpressionAnalyze.MethodCallVisitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Dawn;
    using JetBrains.Annotations;

    public class MethodCallVisitorsFactory
    {
        private static readonly List<Type> Visitors = new List<Type>();

        private readonly IMethodCallVisitor[] _createdVisitors;

        public static void Register<TType>() where TType : IMethodCallVisitor, new()
        {
            var type = typeof(TType);
            if (!Visitors.Contains(type))
            {
                Visitors.Add(type);
            }
        }

        public MethodCallVisitorsFactory()
        {
            _createdVisitors = Visitors
                .Select(x => Activator.CreateInstance(x) as IMethodCallVisitor)
                .ToArray();
        }

        public IMethodCallVisitor Get([NotNull] MethodCallExpression node)
        {
            Guard.Argument(node).
                NotNull()
                .Member(x => x.Method.Name, n => n.NotNull());

            return _createdVisitors.FirstOrDefault(x => x.HasVisit(node));
        }
    }
}