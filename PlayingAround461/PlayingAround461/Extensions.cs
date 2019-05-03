using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlayingAround461.Classes;

namespace PlayingAround461
{
    public static class Extensions
    {
        public static IServiceContainer RegisterBarFactoryByFoo(this IServiceContainer serviceContainer)
        {
            serviceContainer.Register<Func<Foo, IBar>>(c =>
            {
                return (foo) => Barfactory(c, foo);
            }, new PerContainerLifetime()); // This PerContainerLifeTime is for the Function, not the result of the Function

            return serviceContainer;
        }

        public static IServiceContainer RegisterBarByFoo(this IServiceContainer serviceContainer)
        {
            serviceContainer.Register<Foo, IBar>((c, tenant) =>
            {
                return Barfactory(c, tenant);
            });

            return serviceContainer;
        }

        public static IBar Barfactory(IServiceFactory factory, Foo foo)
        {
            return new Bar(foo);
        }
    }
}
