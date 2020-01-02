namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
    using System.Linq.Expressions;
    using Context;
    using Dawn;
    using JetBrains.Annotations;
    using MethodCallVisitors;

    public partial class ExpressionAnalyzeVisitor : ExpressionVisitor
    {
        private readonly Context _context;

        private readonly MethodCallVisitorsFactory _methodCallFactory;

        public ExpressionAnalyzeVisitor([NotNull] Context context)
        {
            Guard.Argument(context).NotNull();

            _context = context;
            _methodCallFactory = new MethodCallVisitorsFactory();
        }

        static ExpressionAnalyzeVisitor()
        {
            MethodCallVisitorsFactory.Register<SelectMethodCallVisitor>();
        }
    }
}