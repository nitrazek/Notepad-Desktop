using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotepadDesktop.repositories;
using NotepadDesktop.viewModels;
using NotepadDesktop.views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace NotepadDesktop
{
    public partial class App : Application
    {
        private static IHost? AppHost { get; set; }

        private IHost BuildAppHost()
        {
            var builder = Host.CreateDefaultBuilder();
            builder.ConfigureServices((context, services) =>
            {
                //DbContext

                //Views
                services.AddSingleton<MainWindow>();
                services.AddSingleton<AdvancedSearchWindow>();
                services.AddSingleton<ConfirmationWindow>();
                services.AddSingleton<NoteEditorWindow>();
                services.AddSingleton<PasswordWindow>();
                services.AddSingleton<FolderNameWindow>();
                //ViewModels
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<NoteEditorViewModel>();
                services.AddSingleton<ConfirmationViewModel>();
                services.AddSingleton<PasswordViewModel>();
                services.AddSingleton<FolderNameViewModel>();
                //Repositories
                services.AddSingleton<FolderRepository>();
            });
            builder.UseDefaultServiceProvider(options => options.ValidateScopes = false);
            return builder.Build();
        }

        public App()
        {
            AppHost = BuildAppHost();
            MainWindow window = AppHost.Services.GetRequiredService<MainWindow>();
            window.Show();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }

}
