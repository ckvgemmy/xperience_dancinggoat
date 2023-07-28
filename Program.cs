using System;

using CMS.Helpers;

using DancingGoat;
using DancingGoat.Helpers.Generator;
using DancingGoat.PageTemplates;

using Kentico.Activities.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.CrossSiteTracking.Web.Mvc;
using Kentico.Forms.Web.Mvc;
using Kentico.Membership;
using Kentico.OnlineMarketing.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Kentico.Xperience.GraphQL;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// This is a route controller constraint for pages not handled by the content tree-based router.
/// The constraint limits the match to a list of specified controllers for pages not handled by the content tree-based router.
/// The constraint ensures that broken URLs lead to a "404 page not found" page and are not handled by a controller dedicated to the component or 
/// to a page handled by the content tree-based router (which would lead to an exception).
/// </summary>
const string CONSTRAINT_FOR_NON_ROUTER_PAGE_CONTROLLERS = "Account|Consent|Subscription|Coffees";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddKentico(features =>
{
    features.UsePageBuilder(new PageBuilderOptions
    {
        DefaultSectionIdentifier = ComponentIdentifiers.SINGLE_COLUMN_SECTION,
        RegisterDefaultSection = false
    });

    features.UseActivityTracking();

    features.UseCrossSiteTracking(new CrossSiteTrackingOptions
    {
        ConsentSettings = new CrossSiteTrackingConsentOptions
        {
            // This is a sample consent that can be created via sample data generator application.
            // You can replace it by your own consent.
            ConsentName = TrackingConsentGenerator.CONSENT_NAME,
            AgreeCookieLevel = CookieLevel.All
        }
    });

    features.UseEmailStatisticsLogging();
    features.UseEmailMarketing();

    features.UsePageRouting();
});

builder.Services.AddDancingGoatServices();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddLocalization()
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResources));
    });

builder.Services.Configure<KenticoRequestLocalizationOptions>(options =>
{
    options.RequestCultureProviders.Add(new RouteDataRequestCultureProvider
    {
        RouteDataStringKey = "culture",
        UIRouteDataStringKey = "culture"
    });
});

builder.Services.Configure<FormBuilderBundlesOptions>(options =>
{
    options.JQueryCustomBundleWebRootPath = "Scripts/jquery-3.5.1.min.js";
});

builder.Services.AddKenticoGraphQL(new KenticoGraphQLOptions
{
    UseQueryDebugTool = builder.Environment.IsDevelopment()
});

ConfigureMembershipServices(builder.Services);
ConfigurePageBuilderFilters();

var app = builder.Build();
app.InitKentico();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();


app.UseKentico();

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseAuthorization();

app.UseKenticoGraphQL();

app.Kentico().MapRoutes();
app.MapControllerRoute(
       name: "error",
       pattern: "error/{code}",
       defaults: new { controller = "HttpErrors", action = "Error" }
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}",
    constraints: new
    {
        controller = CONSTRAINT_FOR_NON_ROUTER_PAGE_CONTROLLERS
    });

app.Run();


static void ConfigureMembershipServices(IServiceCollection services)
{
    services.AddIdentity<ApplicationUser, NoOpApplicationRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 0;
        // Ensures, that disabled member cannot sign in.
        options.SignIn.RequireConfirmedAccount = true;
    })
        .AddUserStore<ApplicationUserStore<ApplicationUser>>()
        .AddRoleStore<NoOpApplicationRoleStore>()
        .AddUserManager<UserManager<ApplicationUser>>()
        .AddSignInManager<SignInManager<ApplicationUser>>();

    services.ConfigureApplicationCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(14);
        options.AccessDeniedPath = new PathString("/account/login");
    });

    // Sets the validation interval of members security stamp to zero so member's security stamp is validated with each request.
    services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.Zero);

    services.AddAuthorization();
}


static void ConfigurePageBuilderFilters()
{
    PageBuilderFilters.PageTemplates.Add(new ArticlePageTemplatesFilter());
    PageBuilderFilters.PageTemplates.Add(new LandingPageTemplatesFilter());
    PageBuilderFilters.PageTemplates.Add(new SubscriptionPageTemplatesFilter());
}
