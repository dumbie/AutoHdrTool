using System.Windows;
using static ArnoldVinkStyles.AVDispatcherInvoke;
using static AutoHdrTool.AppVariables;

namespace AutoHdrTool
{
    partial class MainWindow
    {
        //Show status message
        public void ShowStatusMessage(string statusMessage)
        {
            try
            {
                //Show status
                border_Status.Visibility = Visibility.Visible;
                textblock_Status.Text = statusMessage;

                //Start notification timer
                vAVTimerStatus.Interval = 3000;
                vAVTimerStatus.TickSet = delegate
                {
                    try
                    {
                        DispatcherInvoke(delegate
                        {
                            //Stop notification timer
                            vAVTimerStatus.Stop();

                            //Hide status
                            border_Status.Visibility = Visibility.Collapsed;
                        });
                    }
                    catch { }
                };
                vAVTimerStatus.Start();
            }
            catch { }
        }
    }
}