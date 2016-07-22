using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace SampleFive.DependencyHelper
{
    internal class StructureMapServiceScope : IServiceScope
    {
        private readonly IContainer _container;
        private readonly IContainer _childContainer;
        private IServiceProvider _provider;

        public StructureMapServiceScope(IContainer container)
        {
            _container = container;
            _childContainer = _container.GetNestedContainer();
            _provider = new StructureMapServiceProvider(_childContainer, true);
        }

        public IServiceProvider ServiceProvider => _provider;

        public void Dispose()
        {
            _provider = null;
            if (_childContainer != null)
                _childContainer.Dispose();
        }
    }
}