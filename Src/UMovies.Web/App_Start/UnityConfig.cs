using CrossCutting.MainModule.IOC;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace UMovies.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            var unityContainer = new IocUnityContainer(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}