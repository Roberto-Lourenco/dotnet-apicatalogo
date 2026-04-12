using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace APICatalogo.WebAPI.Extensions;

public static class LocalizationConfigExtensions
{
    public static IApplicationBuilder UseApplicationLocalization(this IApplicationBuilder app)
    {
        var defaultCulture = new CultureInfo("en-US");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = [defaultCulture],
            SupportedUICultures = [defaultCulture]
        };

        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}
