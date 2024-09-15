using Avalonia.NewExp.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Avalonia.NewExp.Core.ServiceCollectionBuilders
{
    public static class AddCoreServicesServiceCollectionExtension
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            // Это для ViewModelFactory
            services.AddSingleton<Func<Type, ObservableObject>>(s => viewModelType => (ObservableObject)s.GetRequiredService(viewModelType));
            services.AddSingleton<IViewModelLocator, ViewModelLocator>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<INavigationService, NavigationService>();
        }
    }
}
