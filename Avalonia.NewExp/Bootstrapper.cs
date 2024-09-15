using Avalonia.NewExp.Core.ServiceCollectionBuilders;
using Avalonia.NewExp.ViewModels;
using Avalonia.NewExp.ViewModels.Dialogs;
using Avalonia.NewExp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Avalonia.NewExp
{
    public class Bootstrapper
    {
        private static Bootstrapper? _instance;
        public static Bootstrapper GetInstance() => _instance ??= new Bootstrapper();

        public IServiceProvider Setup()
        {
            var services = new ServiceCollection();
            services.AddCoreServices();

            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<SecondViewModel>();
            services.AddTransient<TestModalWindowViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });

            return services.BuildServiceProvider();
        }
    }
}
