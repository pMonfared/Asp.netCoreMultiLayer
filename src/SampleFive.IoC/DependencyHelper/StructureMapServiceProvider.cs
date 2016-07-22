using StructureMap;
using System;

namespace SampleFive.DependencyHelper
{
    internal class StructureMapServiceProvider : IServiceProvider
    {
        private readonly IContainer _container;

        public StructureMapServiceProvider(IContainer container, bool scoped = false)
        {
            _container = container;
        }

        public object GetService(Type type)
        {
            try
            {
                return _container.GetInstance(type);
            }
            catch
            {
                return null;
            }
        }
    }
}