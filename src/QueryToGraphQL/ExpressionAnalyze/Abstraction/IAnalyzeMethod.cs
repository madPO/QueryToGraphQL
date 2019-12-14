namespace QueryToGraphQL.ExpressionAnalyze.Abstraction
{
    using System.Linq.Expressions;
    using Context;

    /// <summary>
    /// Метод анализа выражения
    /// </summary>
    public interface IAnalyzeMethod
    {
        /// <summary>
        /// Анализировать выыражение
        /// </summary>
        Context Analyze(Context context, Expression expression);
    }
}