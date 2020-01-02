namespace UnitTest.Context
{
    using System;
    using AutoFixture;
    using QueryToGraphQL.Context;
    using Xunit;

    public class ContextTest
    {
        [Fact]
        public void add_basetype_in_context()
        {
            var fixture = new Fixture();
            var context = new Context();

            context.BaseType = fixture.Create<Type>();
        }
        
        [Fact]
        public void change_basetype_in_context()
        {
            var fixture = new Fixture();
            var context = new Context();

            context.BaseType = fixture.Create<Type>();

            Assert.Throws<ArgumentException>(() => context.BaseType = fixture.Create<Type>());
        }
        
        [Fact]
        public void add_return_in_context()
        {
            var fixture = new Fixture();
            var context = new Context();

            context.ReturnType = fixture.Create<Type>();
        }
        
        [Fact]
        public void change_return_in_context()
        {
            var fixture = new Fixture();
            var context = new Context();

            context.ReturnType = fixture.Create<Type>();

            Assert.Throws<ArgumentException>(() => context.ReturnType = fixture.Create<Type>());
        }

        [Fact]
        public void set_primitive_return_type()
        {
            var context = new Context();

            Assert.Throws<ArgumentException>(() => context.ReturnType = typeof(int));
        }

        [Fact]
        public void add_context_variable()
        {
            var fixture = new Fixture();
            var context = new Context();
            
            context.AddVariable(fixture.Create<string>(), fixture.Create<object>());
        }
        
        [Fact]
        public void contains_context_variable()
        {
            var fixture = new Fixture();
            var context = new Context();
            var varName = fixture.Create<string>();
            
            context.AddVariable(varName, fixture.Create<object>());
            
            Assert.True(context.Variables.ContainsKey(varName));
        }

        [Fact]
        public void add_not_uniq_context_variable()
        {
            var fixture = new Fixture();
            var context = new Context();
            var varName = fixture.Create<string>();
            
            context.AddVariable(varName, fixture.Create<object>());
            
            Assert.Throws<ArgumentException>(() => context.AddVariable(varName, fixture.Create<object>()));
        }
        
        [Fact]
        public void add_context_arguments()
        {
            var fixture = new Fixture();
            var context = new Context();
            
            context.AddArgument(fixture.Create<string>(), fixture.Create<object>());
        }
        
        [Fact]
        public void contains_context_argument()
        {
            var fixture = new Fixture();
            var context = new Context();
            var argName = fixture.Create<string>();
            
            context.AddArgument(argName, fixture.Create<object>());
            
            Assert.True(context.Arguments.ContainsKey(argName));
        }

        [Fact]
        public void add_not_uniq_context_argument()
        {
            var fixture = new Fixture();
            var context = new Context();
            var argName = fixture.Create<string>();
            
            context.AddArgument(argName, fixture.Create<object>());
            
            Assert.Throws<ArgumentException>(() => context.AddArgument(argName, fixture.Create<object>()));
        }
    }
}