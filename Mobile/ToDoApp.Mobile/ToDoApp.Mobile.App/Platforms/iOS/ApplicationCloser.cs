using ToDoApp.Mobile.App.Services.Interfaces;

namespace ToDoApp.Mobile.App.Platforms.iOS
{
    public class ApplicationCloser : IApplicationCloser
    {
        public void Close()
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}