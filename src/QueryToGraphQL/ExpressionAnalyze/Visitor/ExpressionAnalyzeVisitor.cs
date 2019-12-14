namespace QueryToGraphQL.ExpressionAnalyze.Visitor
{
    using System.Linq.Expressions;
    using Context;

    public partial class ExpressionAnalyzeVisitor : ExpressionVisitor
    {
        private readonly Context _context;

        public ExpressionAnalyzeVisitor(Context context)
        {
            _context = context;
        }
    }
}