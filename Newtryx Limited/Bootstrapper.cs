using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Newtryx.BI;
using Unity.Mvc3;

namespace Newtryx_Limited
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IConnectionManager, ConnectionManager>();
            container.RegisterType<IRestaurant, Restaurant>();
            container.RegisterType<IReservation, Reservation>();
            container.RegisterType<IOrder, Order>();

            return container;
        }
    }
}