using Microsoft.AspNetCore.Mvc;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;

namespace ToDoApp.BackEnd.API.Controllers;
public abstract class BaseController : ControllerBase
{
    protected TokenPayloadDto CurrentUser => HttpContext.Items["TokenPayload"] as TokenPayloadDto;
}
