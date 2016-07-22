using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace SampleFive.DependencyHelper
{
    internal class StructureMapServiceScopeFactory : IServiceScopeFactory
    {
        private IContainer _container;

        public StructureMapServiceScopeFactory(IContainer container)
        {
            _container = container;
        }

        public IServiceScope CreateScope()
        {
            return new StructureMapServiceScope(_container);
        }
    }
}