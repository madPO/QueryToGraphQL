namespace QueryToGraphQL.Context
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Контекст для запроса
    /// </summary>
    public class Context
    {
        private Type _baseType;

        private Type _returnType;

        private Dictionary<string, object> _arguments;

        private Dictionary<string, object> _variables;

        private Dictionary<string, Type> _properties;

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
                if(_baseType != null)
                    throw new ArgumentException("The base type was already set!");

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
                if(_returnType != null)
                    throw new ArgumentException("The return type was already set!");
                
                if(value.IsPrimitive)
                    throw new ArgumentException("The return type must be an object!");

                _returnType = value;
            }
        }

        /// <summary>
        /// Добавить переменную запроса
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="variable"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddVariable(string variableName, object variable)
        {
            if(ContainsVariable(variableName))
                throw new ArgumentException($"Variable {variableName} already exists!");
            
            _variables.Add(variableName, variable);
        }

        /// <summary>
        /// Наличие переменной
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public bool ContainsVariable(string variableName)
        {
            return _variables.ContainsKey(variableName);
        }
        
        /// <summary>
        /// Добавить аргумент запроса
        /// </summary>
        /// <param name="argumentName"></param>
        /// <param name="argument"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddArgument(string argumentName, object argument)
        {
            if(ContainsArgument(argumentName))
                throw new ArgumentException($"Argument {argumentName} already exists!");
            
            _arguments.Add(argumentName, argument);
        }

        /// <summary>
        /// Наличие аргумента
        /// </summary>
        /// <param name="argumentName"></param>
        /// <returns></returns>
        public bool ContainsArgument(string argumentName)
        {
            return _arguments.ContainsKey(argumentName);
        }

        /// <summary>
        /// Добавить свойство
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddProperty(string propertyName, Type propertyType)
        {
            if(ContainsArgument(propertyName))
                throw new ArgumentException($"Property {propertyName} already exists!");
            
            _properties.Add(propertyName, propertyType);

        }

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
    }
}