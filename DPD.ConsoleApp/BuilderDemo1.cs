using System.Net.Http;
using Autofac;

namespace DPD.ConsoleApp
{
    internal static class BuilderDemo1
    {
        public static void Run()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<HttpClient>().AsSelf().SingleInstance();

            var container = containerBuilder.Build();

            var httpClient = container.Resolve<HttpClient>();
        }
    }
}