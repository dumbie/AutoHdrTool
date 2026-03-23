using System.Windows;
using static ArnoldVinkCode.AVProcess;
using static ArnoldVinkCode.AVStartup;

namespace AutoHdrTool
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                //Setup application defaults
                SetupDefaults(ProcessPriorityClasses.Normal, true);

                //Run application startup code
                await AppStartup.Startup();
            }
            catch { }
        }
    }
}
