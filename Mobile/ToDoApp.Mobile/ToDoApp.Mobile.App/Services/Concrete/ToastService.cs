using ToDoApp.Mobile.App.Services.Interfaces;

namespace ToDoApp.Mobile.App.Services.Concrete;
public class ToastService : IToastService
{
    public event Action<string, ToastType> OnShow;

    public void ShowToast(string message, ToastType type = ToastType.Info)
    {
        OnShow?.Invoke(message, type);
    }
}

