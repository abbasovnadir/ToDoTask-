using Microsoft.AspNetCore.Mvc;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.API.Extensions;

public static class ActionResultExtensions
{
    public static IActionResult ToActionResult<T>(this IDataResult<T> result, ControllerBase controller)
    {
        return result.IsSuccess ? controller.Ok(result) : controller.BadRequest(result);
    }

    public static IActionResult ToActionResult(this Application.Utilities.Results.Interfaces.IResult result, ControllerBase controller)
    {
        return result.IsSuccess ? controller.Ok(result) : controller.BadRequest(result);
    }
}
