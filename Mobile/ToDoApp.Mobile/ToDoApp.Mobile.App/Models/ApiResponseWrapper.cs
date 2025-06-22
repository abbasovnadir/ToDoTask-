namespace ToDoApp.Mobile.App.Models;
public class ApiResponseWrapper<T>
{
    public bool isSuccess { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public int ResponseType { get; set; } 
}