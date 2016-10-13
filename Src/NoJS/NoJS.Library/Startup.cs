using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc.Razor;

using Microsoft.AspNetCore.Routing;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection.Extensions;

using Microsoft.Extensions.Options;

namespace NoJS.Library {
    public static class Configuration {
        public static IRouteBuilder MapDeviceSwitcher(this IRouteBuilder route)

            => route.MapDeviceSwitcher<PreferenceSwitcher>();

        public static IRouteBuilder MapDeviceSwitcher<TSwitcher>(this IRouteBuilder route)

            where TSwitcher : PreferenceSwitcher

            => route.MapGet(

                route.ServiceProvider.GetService<IOptions<SwitcherOptions>>().Value.SwitchUrl + "/{device}",

                route.ServiceProvider.GetRequiredService<TSwitcher>().Handle);
    }
}