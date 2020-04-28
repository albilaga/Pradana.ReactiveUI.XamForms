using ReactiveUI;
using Sextant;
using Sextant.XamForms;
using Splat;

namespace Pradana.ReactiveUI.XamForms.Extensions
{
    public static class SplatExtensions
    {
        public static IMutableDependencyResolver RegisterSextant(this IMutableDependencyResolver mutableDependencyResolver)
        {
            return mutableDependencyResolver
                .RegisterNavigationView(() => new NavigationView(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current))
                .RegisterParameterViewStackService();
        }
    }
}
