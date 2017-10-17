using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ChainTicker.Shell.Helpers;
using ChainTicker.Ui.WpfAssets.Themes;

namespace ChainTicker.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            // set the UI thread name so it's clear in the logs
            Thread.CurrentThread.Name = "UI";

            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            base.OnStartup(e);
            var appVersion = ReflectionHelpers.GetEntryAssemblyVersion();

            //TODO
            InitializeWpfResources();
        }


        private static void InitializeWpfResources()
        {
            AppTheme.Set(false);
        }


        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            var ex = dispatcherUnhandledExceptionEventArgs.Exception;
            var errorMessage = $"ChainTicker has encountered an unexpected error{Environment.NewLine}{Environment.NewLine}{ex.Message}";
            var caption = "Unexpected Error !";

            Debug.WriteLine(errorMessage);

            dispatcherUnhandledExceptionEventArgs.Handled = true;
        }
    }
}
