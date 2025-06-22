using ToDoApp.Mobile.App.Services.Interfaces;

namespace ToDoApp.Mobile.App.Platforms.AndroidDevices;
public class ApplicationCloser : IApplicationCloser
{
    public void Close()
    {
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
    }
}