using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlayingAround461.Classes;

namespace PlayingAround461
{
    class Program
    {
        static void Main(string[] args)
        {
            DoesNotThrowStackOverflow();
            AlsoThrowStackOverflow();
            ThrowsStackOverflow();
        }

        private static void AlsoThrowStackOverflow()
        {
            var container = GetServiceContainer();
            container.RegisterBarByFoo();

            container.Register<IBar>((c) =>
            {
                var foo = c.GetInstance<Foo>();
                var factory = c.GetInstance<Func<Foo, IBar>>();
                return factory(foo);
            });

            container.GetInstance<IBar>();
        }

        private static void ThrowsStackOverflow()
        {
            var container = GetServiceContainer();
            container.RegisterBarByFoo();

            container.Register<IBar>((c) =>
            {
                var foo = c.GetInstance<Foo>();
                return c.GetInstance<Foo, IBar>(foo);
            });

            container.GetInstance<IBar>();
        }

        private static void DoesNotThrowStackOverflow()
        {
            var container = GetServiceContainer();
            container.RegisterBarFactoryByFoo();

            container.Register<IBar>((c) =>
            {
                var foo = c.GetInstance<Foo>();
                var factory = c.GetInstance<Func<Foo, IBar>>();
                return factory(foo);
            });

            container.GetInstance<IBar>();
        }

        private static IServiceContainer GetServiceContainer()
        {
            var container = new ServiceContainer();
            container.Register<Foo>();

            return container;
        }
    }
}
