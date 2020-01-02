namespace QueryToGraphQL.ExpressionAnalyze
{
    using System;
    using System.Linq.Expressions;
    using Context;
    using Dawn;
    using JetBrains.Annotations;
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
        /// <exception cref="ArgumentException"></exception>
        public Context Analyze([NotNull] Expression expression)
        {
            Guard.Argument(expression).NotNull();
            
            var visitor = new ExpressionAnalyzeVisitor(_context);
            visitor.Visit(expression);

            return _context;
        }
        
    }
}