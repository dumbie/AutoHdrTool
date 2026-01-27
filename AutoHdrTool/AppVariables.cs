using static ArnoldVinkCode.AVActions;

namespace AutoHdrTool
{
    public static class AppVariables
    {
        //Application Windows
        public static MainWindow WindowMain = new MainWindow();

        //Timers
        public static AVHighResTimer vAVTimerStatus = new AVHighResTimer();

        //Setting Variables
        public static bool BlockEvent = false;
    }
}