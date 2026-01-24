using System.Windows;
using System.Windows.Threading;
using G00DS0ULRPG.Core;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var exceptionMessageText =
                $"An exception occurred: {e.Exception.Message}\r\n\r\nat: {e.Exception.StackTrace}";

            if (e.Exception.InnerException != null)
            {
                exceptionMessageText += $"\r\n\r\nInner Exception: {e.Exception.InnerException.Message}\r\n\r\nat: {e.Exception.InnerException.StackTrace}";
            }

            LoggingServices.Log(e.Exception);

            //TODO: Create a different window to display the exception message

            MessageBox.Show(exceptionMessageText, "Unhandled Exception", MessageBoxButton.OK);
        }
    }

}
