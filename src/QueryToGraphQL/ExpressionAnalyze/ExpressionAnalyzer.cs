namespace QueryToGraphQL.ExpressionAnalyze
{
    using System.Linq.Expressions;
    using Context;

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
            var strategy = AnalyzeFactory.CreateStrategy(expression);
            strategy.Execute(_context, expression);

            return _context;
        }
        
    }
}