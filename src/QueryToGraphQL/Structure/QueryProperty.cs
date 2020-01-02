namespace QueryToGraphQL.Structure
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using Dawn;
    using JetBrains.Annotations;

    public class QueryProperty
    {
        public string Name { get; }
        
        public string Alias { get; }

        public QueryProperty([NotNull]string name):this(name, name)
        {
        }

        public QueryProperty([NotNull]string name,[NotNull] string alias)
        {
            Guard.Argument(name).NotNull();
            Guard.Argument(alias).NotNull();
            
            Name = name;
            Alias = alias;
            _properties = new List<QueryProperty>();
        }

        private readonly List<QueryProperty> _properties;

        public ICollection<QueryProperty> Properties => _properties.ToImmutableArray();

        public void AddProperty([NotNull] string name)
        {
            AddProperty(name, name);
        }

        public void AddProperty([NotNull]string name,[NotNull] string alias)
        {
            Guard.Argument(name).NotNull();
            Guard.Argument(alias).NotNull();
            
            _properties.Add(new QueryProperty(name, alias));
        }

        public override string ToString()
        {
            if (Properties.Count != 0)
            {
                var result = new StringBuilder();
                result.Append(Alias);
                result.Append("{");
                result.Append(string.Join(",", Properties));
                result.Append("}");
                return result.ToString();
            }

            return Alias;
        }
    }
}