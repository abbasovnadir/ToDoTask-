using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using ToDoApp.Mobile.App.Services.Interfaces;
using ToDoApp.Mobile.App.Services.Concrete;
using Blazored.Modal;

namespace ToDoApp.Mobile.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddBlazoredModal();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<AuthenticationService>();
            builder.Services.AddScoped<ResetPassState>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<ICustomAuthenticationStateProvider, CustomAuthenticationStateProvider>(); 
            builder.Services.AddSingleton<IToastService, ToastService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif



#if ANDROID
            builder.Services.AddSingleton<IApplicationCloser, Platforms.AndroidDevices.ApplicationCloser>();
            #elif IOS
                    builder.Services.AddSingleton<IApplicationCloser, Platforms.iOS.ApplicationCloser>();
            #else
                    builder.Services.AddSingleton<IApplicationCloser, DefaultApplicationCloser>(); 
            #endif

            // **✅ HttpClient servisini ekleyelim**
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5131") });
            //builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<HttpClient>(sp =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://freetestapi.bsite.net")
                    //BaseAddress = new Uri("http://localhost:5120")
                };

                return client;
            });

            builder.Services.AddSingleton<ITodoItemService, TodoItemService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
