using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System.Collections.Generic;

namespace SampleFive.DependencyHelper
{
    public static class StructureMapRegistration
    {
        public static void Populate(this IContainer container, IEnumerable<ServiceDescriptor> descriptors)
        {
            var populator = new StructureMapPopulator(container);
            populator.Populate(descriptors);
        }
    }
}