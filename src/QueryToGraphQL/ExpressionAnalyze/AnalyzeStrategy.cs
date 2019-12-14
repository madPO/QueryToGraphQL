namespace QueryToGraphQL.ExpressionAnalyze
{
    using System.Linq.Expressions;
    using Abstraction;
    using Context;

    public class AnalyzeStrategy
    {
        private readonly IAnalyzeMethod _method;

        public AnalyzeStrategy(IAnalyzeMethod method)
        {
            _method = method;
        }
        
        public Context Execute(Context context, Expression expression)
        {
            _method.Analyze(context, expression);
            return context;
        }
    }
}