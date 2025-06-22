using System.Net.Http.Json;
using System.Text.Json;
using ToDoApp.Mobile.App.Models;
using ToDoApp.Mobile.App.Models.Enums;
using ToDoApp.Mobile.App.Services.Interfaces;

namespace ToDoApp.Mobile.App.Services.Concrete
{
    public class TodoItemService : ITodoItemService
    {
        private readonly AuthenticationService _authenticationService;
        private readonly HttpClient _httpClient;

        public TodoItemService(AuthenticationService authenticationService, HttpClient httpClient)
        {
            _authenticationService = authenticationService;
            _httpClient = httpClient;
        }

        public async Task<bool> Create(TodoItemCreateRequest todoItem)
        {
            try
            {

                var session = await _authenticationService.GetUserSession();
                if (session is null)
                    return false;

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessToken);

                var content = new StringContent(
                    JsonSerializer.Serialize(todoItem),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"/api/todo/", content);
                var content3 = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return false;

                var responseContent = await response.Content.ReadAsStringAsync();

                var resultWrapper = JsonSerializer.Deserialize<ApiResponseWrapper<TodoItemListResponse>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return resultWrapper != null && resultWrapper.isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var session = await _authenticationService.GetUserSession();
                if (session is null)
                    return false;

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessToken);

                var response = await _httpClient.DeleteAsync($"/api/todo/{id}");

                if (!response.IsSuccessStatusCode)
                    return false;

                var responseContent = await response.Content.ReadAsStringAsync();

                var resultWrapper = JsonSerializer.Deserialize<ApiResponseWrapper<object>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return resultWrapper != null && resultWrapper.isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<List<TodoItemListResponse>> GetAll()
        {
            try
            {
                var session = await _authenticationService.GetUserSession();
                if (session is null)
                    return new List<TodoItemListResponse>();          

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessToken);

                var response = await _httpClient.GetAsync($"/api/todo/user");

                if (!response.IsSuccessStatusCode)
                    return new List<TodoItemListResponse>();

                var responseContent = await response.Content.ReadAsStringAsync();

                var resultWrapper = JsonSerializer.Deserialize<ApiResponseWrapper<List<TodoItemListResponse>>>(
                    responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (resultWrapper != null && resultWrapper.isSuccess && resultWrapper.Data != null)
                    return resultWrapper.Data;

                return new List<TodoItemListResponse>();
            }
            catch (Exception)
            {
                return new List<TodoItemListResponse>();
            }
        }

        public async Task<List<TodoItemListResponse>> GetByFilter(TodoStatus todoStatus)
        {
            try
            {
                var session = await _authenticationService.GetUserSession();
                if (session is null)
                    return new List<TodoItemListResponse>();

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessToken);

                var response = await _httpClient.GetAsync($"/api/todo/userByFilter?todoStatus={(int)todoStatus}");

                if (!response.IsSuccessStatusCode)
                    return new List<TodoItemListResponse>();

                var responseContent = await response.Content.ReadAsStringAsync();

                var resultWrapper = JsonSerializer.Deserialize<ApiResponseWrapper<List<TodoItemListResponse>>>(
                    responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (resultWrapper != null && resultWrapper.isSuccess && resultWrapper.Data != null)
                    return resultWrapper.Data;

                return new List<TodoItemListResponse>();
            }
            catch (Exception)
            {
                return new List<TodoItemListResponse>();
            }
        }

        public async Task<TodoItemListResponse> GetById(int id)
        {
            try
            {
                var session = await _authenticationService.GetUserSession();
                if (session is null)
                    return new TodoItemListResponse();

                _httpClient.DefaultRequestHeaders.Authorization =
                   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessToken);

                var response = await _httpClient.GetAsync($"/api/todo/{id}");

                if (!response.IsSuccessStatusCode)
                    return new TodoItemListResponse();

                var responseContent = await response.Content.ReadAsStringAsync();

                var resultWrapper = JsonSerializer.Deserialize<ApiResponseWrapper<List<TodoItemListResponse>>>(
                    responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (resultWrapper != null && resultWrapper.isSuccess && resultWrapper.Data != null)
                    return resultWrapper.Data.FirstOrDefault();

                return new TodoItemListResponse();
            }
            catch (Exception)
            {
                return new TodoItemListResponse();
            }

        }

        public async Task<TodoItemListResponse> Update(TodoItemUpdateRequest todoItem)
        {
            try
            {
                var session = await _authenticationService.GetUserSession();
                if (session is null)
                    return new TodoItemListResponse();
                _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessToken);

                var content = JsonContent.Create(todoItem);
                var response = await _httpClient.PutAsync($"/api/todo/{todoItem.Id}", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resultWrapper = JsonSerializer.Deserialize<ApiResponseWrapper<TodoItemListResponse>>(responseContent,
                                        new JsonSerializerOptions
                                        {
                                            PropertyNameCaseInsensitive = true
                                        });

                    return resultWrapper?.Data;
                }

                return new TodoItemListResponse();
            }
            catch (Exception)
            {

                return new TodoItemListResponse();
            }
        }

        public async Task<bool> UpdateStatus(int id, TodoStatus Status)
        {
            try
            {
                var session = await _authenticationService.GetUserSession();
                if (session is null)
                    return false;

                var resultData = await GetById(id);
                if (resultData?.Id > 0)
                {
                    var request = new TodoItemUpdateRequest
                    {
                        Id = id,
                        Title = resultData.Title,
                        Description = resultData.Description,
                        DueDate = resultData.DueDate,
                        Rowstatus = true,
                        Status = Status
                    };
                    var resultUpdate = await Update(request);
                    if (resultUpdate?.Id > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
