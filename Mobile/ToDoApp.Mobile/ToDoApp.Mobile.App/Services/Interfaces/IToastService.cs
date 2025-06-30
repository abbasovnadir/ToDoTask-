namespace ToDoApp.Mobile.App.Services.Interfaces;

public interface IToastService
{
    event Action<string, ToastType> OnShow;

    void ShowToast(string message, ToastType type = ToastType.Info);
}
