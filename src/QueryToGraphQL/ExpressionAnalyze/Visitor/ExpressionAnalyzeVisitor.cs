namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
    using System.Linq.Expressions;
    using Context;
    using Dawn;
    using JetBrains.Annotations;

    public partial class ExpressionAnalyzeVisitor : ExpressionVisitor
    {
        private readonly Context _context;

        public ExpressionAnalyzeVisitor([NotNull]Context context)
        {
            Guard.Argument(context).NotNull();
            
            _context = context;
        }
    }
}