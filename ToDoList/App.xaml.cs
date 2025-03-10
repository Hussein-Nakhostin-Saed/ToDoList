using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
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
        private Autofac.IContainer _container;

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    var builder = new ContainerBuilder();

        //    // ثبت وابستگی‌ها
        //    builder.RegisterType<AppDbContext>().AsSelf();
        //    builder.RegisterType<TaskService>().AsSelf();
        //    builder.RegisterType<MainWindow>().AsSelf();

        //    _container = builder.Build();

        //    // ایجاد MainWindow از Container
        //    var mainWindow = _container.Resolve<MainWindow>();
        //    mainWindow.Show();
        //}
        private IServiceProvider _serviceProvider;
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=ToDoList;Integrated Security=True;TrustServerCertificate=True");// config!.ConnectionString);
            });
            services.AddTransient<TaskService>();
            services.AddTransient<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
