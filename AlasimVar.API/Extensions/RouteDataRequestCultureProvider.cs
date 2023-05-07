using Microsoft.AspNetCore.Localization;

namespace AlasimVar.API.Extensions;

public class RouteDataRequestCultureProvider: RequestCultureProvider
{
    public int IndexOfCulture;
    public int IndexofUICulture;
    
    public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        string culture = null;
        string uiCulture = null;

        culture = uiCulture = httpContext.Request.Headers["Accept-Language"].ToString();

        var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

        return Task.FromResult(providerResultCulture);
    }
}