using ToDoApp.Mobile.App.Services.Interfaces;

namespace ToDoApp.Mobile.App.Services.Concrete;

public class DefaultApplicationCloser : IApplicationCloser
{
    public void Close()
    {
        Application.Current.Quit();
    }
}
