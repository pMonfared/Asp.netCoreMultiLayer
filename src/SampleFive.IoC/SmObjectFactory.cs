using SampleFive.ServiceLayer;
using StructureMap;
using System;
using System.Threading;

namespace SampleFive.IoC
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);
        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }
        }
        private static Container DefaultContainer()
        {
            return new Container(ioc =>
            {
                ioc.Scan(_ =>
                {
                    _.AssemblyContainingType<IMessagesSampleService>();
                    _.WithDefaultConventions();
                });
            });
        }
    }
}