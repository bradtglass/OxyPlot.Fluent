using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot.Fluent.Configurators;
using Xunit;
using Xunit.Sdk;

namespace OxyPlot.Fluent.Tests
{
    public class ConfiguratorRequirementChecks:IClassFixture<ConfiguratorsFixture>
    {
        private readonly ConfiguratorsFixture fixture;
        
        public ConfiguratorRequirementChecks(ConfiguratorsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void AllConfiguratorsCreateNewInstanceOfMatchingType()
        {
            foreach (IConfigurator configurator in fixture.AllConfigurators)
            {
                IConfigurator newInstance = configurator.CreateNewInstance();
                
                Assert.False(ReferenceEquals(configurator, newInstance),
                    $"Create instance returned the same instance for {configurator.GetType().FullName}");
                Assert.Equal(configurator.GetType(), newInstance.GetType());
            }
        }

        [Fact]
        public void ClassFixtureContainsAllNonAbstractConfigurators()
        {
            Type configuratorType = typeof(IConfigurator);
            IEnumerable<Type> typesInAssembly = configuratorType.Assembly
                .GetExportedTypes()
                .Where(t=>configuratorType.IsAssignableFrom(t))
                .Where(t=>!t.IsAbstract && t.IsClass);

            IEnumerable<Type> typesInFixture = fixture.AllConfigurators
                .Select(c => c.GetType())
                .Select(t => t.IsConstructedGenericType ? t.GetGenericTypeDefinition() : t)
                .ToList();

            foreach (Type type in typesInAssembly)
            {
                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                if (typesInFixture.All(c => c != type))
                    throw new XunitException(
                        $"An instance of the type {type.FullName} was not found on the test fixture");
            }
        }
    }
}