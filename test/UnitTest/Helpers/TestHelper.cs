namespace UnitTest.Helpers
{
    using System.Linq;

    /// <summary>
    /// Класс помощник для тестов
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Создать запрос
        /// </summary>
        public static IQueryable<TModel> CreateQuery<TModel>() where TModel: class
        {
            return Enumerable.Empty<TModel>().AsQueryable();
        }
    }
}