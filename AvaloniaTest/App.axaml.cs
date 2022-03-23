using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaTest.ViewModels;
using AvaloniaTest.Views;

namespace AvaloniaTest
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // desktop.MainWindow = new MainWindow
                // {
                //     DataContext = new MainViewModel()
                // };
                
                desktop.MainWindow = new LiveChartsSampleWindow() {
                    // DataContext = new LiveChartsSampleViewModel()
                };
                
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                // singleViewPlatform.MainView = new MainView
                // {
                //     DataContext = new MainViewModel()
                // };
                singleViewPlatform.MainView = new LiveChartsSampleView
                {
                    // DataContext = new LiveChartsSampleViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}