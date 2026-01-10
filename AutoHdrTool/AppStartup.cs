using System.Diagnostics;
using System.Threading.Tasks;

namespace AutoHdrTool
{
    public class AppStartup
    {
        public static async Task Startup()
        {
            try
            {
                Debug.WriteLine("Welcome to application.");

                //Show application window
                AppVariables.WindowMain.Show();
            }
            catch { }
        }
    }
}