using System;
using StructureMap;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StructureMap.Configuration.DSL.Expressions;

namespace SampleFive.DependencyHelper
{
    internal class StructureMapPopulator
    {
        private IContainer _container;

        public StructureMapPopulator(IContainer container)
        {
            _container = container;
        }

        public void Populate(IEnumerable<ServiceDescriptor> descriptors)
        {
            _container.Configure(c =>
            {
                c.For<IServiceProvider>().Use(new StructureMapServiceProvider(_container));
                c.For<IServiceScopeFactory>().Use<StructureMapServiceScopeFactory>();

                foreach (var descriptor in descriptors)
                {
                    switch (descriptor.Lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            Use(c.For(descriptor.ServiceType).Singleton(), descriptor);
                            break;
                        case ServiceLifetime.Transient:
                            Use(c.For(descriptor.ServiceType), descriptor);
                            break;
                        case ServiceLifetime.Scoped:
                            Use(c.For(descriptor.ServiceType), descriptor);
                            break;
                    }
                }
            });
        }

        private static void Use(GenericFamilyExpression expression, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationFactory != null)
            {
                expression.Use(Guid.NewGuid().ToString(), context => { return descriptor.ImplementationFactory(context.GetInstance<IServiceProvider>()); });
            }
            else if (descriptor.ImplementationInstance != null)
            {
                expression.Use(descriptor.ImplementationInstance);
            }
            else if (descriptor.ImplementationType != null)
            {
                expression.Use(descriptor.ImplementationType);
            }
            else
            {
                throw new InvalidOperationException("IServiceDescriptor is invalid");
            }
        }
    }
}