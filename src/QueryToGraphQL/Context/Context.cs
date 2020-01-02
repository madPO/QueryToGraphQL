namespace QueryToGraphQL.Context
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Reflection;
    using Dawn;
    using JetBrains.Annotations;

    /// <summary>
    /// Контекст для запроса
    /// </summary>
    public class Context
    {
        private Type _baseType;

        private Type _returnType;

        private readonly Dictionary<string, object> _arguments;

        private readonly Dictionary<string, object> _variables;

        private readonly Dictionary<string, Type> _properties;

        public Context()
        {
            _arguments = new Dictionary<string, object>();
            _variables = new Dictionary<string, object>();
            _properties = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Базовый тип запроса
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public Type BaseType
        {
            get => _baseType;
            set
            {
                Guard.Argument(_baseType).Null(x => "The base type was already set!");
                _baseType = value;
            }
        }

        /// <summary>
        /// Тип возвращаемого значения
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public Type ReturnType
        {
            get => _returnType;
            set
            {
                Guard.Argument(_returnType).Null(x => "The return type was already set!");
                Guard.Argument(value).Require((x) => !x.IsPrimitive, (x) => "The return type must be an object!");

                _returnType = value;
            }
        }

        /// <summary>
        /// Добавить переменную запроса
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="variable"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddVariable([NotNull] string variableName, [NotNull] object variable)
        {
            Guard.Argument(variableName).NotEmpty();
            Guard.Argument(variable).NotNull();
            Guard.Argument(_variables)
                .Require(
                    x => !x.ContainsKey(variableName), 
                    d => $"Variable {variableName} already exists!");
            
            _variables.Add(variableName, variable);
        }

        /// <summary>
        /// Добавить аргумент запроса
        /// </summary>
        /// <param name="argumentName"></param>
        /// <param name="argument"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddArgument([NotNull] string argumentName,[NotNull] object argument)
        {
            Guard.Argument(argumentName).NotEmpty();
            Guard.Argument(argument).NotNull();
            Guard.Argument(_variables)
                .Require(
                    x => !x.ContainsKey(argumentName), 
                    d => $"Argument {argumentName} already exists!");

            _arguments.Add(argumentName, argument);
        }

        /// <summary>
        /// Добавить свойство
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddProperty([NotNull] string propertyName,[NotNull] Type propertyType)
        {
            Guard.Argument(propertyName).NotEmpty();
            Guard.Argument(propertyType).NotNull();
            Guard.Argument(_properties)
                .Require(
                    x => !x.ContainsKey(propertyName),
                    x => $"Property {propertyName} already exists!");

            _properties.Add(propertyName, propertyType);
        }

        /// <summary>
        /// Свойства
        /// </summary>
        public ImmutableSortedDictionary<string, Type> Properties
        {
            get
            {
                if (_properties == null || _properties.Count == 0)
                {
                    return BaseType
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(x => x.Name, x => x.PropertyType)
                        .ToImmutableSortedDictionary();
                }
                return _properties.ToImmutableSortedDictionary();
            }
        }
        
        /// <summary>
        /// Переменные
        /// </summary>
        public ImmutableSortedDictionary<string, object> Variables
        {
            get
            {
                return _variables.ToImmutableSortedDictionary();
            }
        }
        
        /// <summary>
        /// Аргументы
        /// </summary>
        public ImmutableSortedDictionary<string, object> Arguments
        {
            get
            {
                return _arguments.ToImmutableSortedDictionary();
            }
        }
    }
}