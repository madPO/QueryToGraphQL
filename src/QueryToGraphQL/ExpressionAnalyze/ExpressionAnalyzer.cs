namespace QueryToGraphQL.ExpressionAnalyze
{
    using System.Linq.Expressions;
    using Context;
    using Visitor;

    /// <summary>
    /// Анализатор выражения
    /// </summary>
    public class ExpressionAnalyzer
    {
        private readonly Context _context;
        
        public ExpressionAnalyzer()
        {
            _context = new Context();
        }

        /// <summary>
        /// Анализировать выражение
        /// </summary>
        public Context Analyze(Expression expression)
        {
            var visitor = new ExpressionAnalyzeVisitor(_context);
            visitor.Visit(expression);

            return _context;
        }
        
    }
}