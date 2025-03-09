using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using ToDoList.Infrastructure;
using ToDoList.Services;
using ToDoList.Utilities;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ObjectMappingProfile));
            services.AddSingleton<AppDbContext>();
            services.AddScoped<TaskService>();
            services.AddScoped<WorkLogService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            //mainWindow.Show();
        }
    }

}
