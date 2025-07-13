using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BackEnd.API.ApiDtos.ToDoItems;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Queries;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Domain.Enums;
using ToDoApp.BackEnd.API.Extensions;

namespace ToDoApp.BackEnd.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ToDoController : BaseController
{
    private readonly IMediator _mediator;
    public ToDoController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("user")]
    public async Task<IActionResult> GetUserTodos()
    {
        var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.UserId == CurrentUser.UserId));
        return result.ToActionResult(this);
    }

    [HttpGet("userByFilter")]
    public async Task<IActionResult> GetUserTodosByFilter([FromQuery] TodoStatus todoStatus)
    {
        var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.UserId == CurrentUser.UserId && x.Status == todoStatus));
        return result.ToActionResult(this);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.UserId == CurrentUser.UserId && x.ID == id));
        return result.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetStaticDataByUser()
    {
        var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.UserId == CurrentUser.UserId));
        if (!result.IsSuccess || result.Data is null || !result.Data.Any())
            return BadRequest(result);

        var dto = new TodoStatusSummaryDto
        {
            Pending = result.Data.Where(x => x.Status == TodoStatus.Pending && x.RowStatus),
            InProgress = result.Data.Where(x => x.Status == TodoStatus.InProgress && x.RowStatus),
            Completed = result.Data.Where(x => x.Status == TodoStatus.Completed && x.RowStatus),
            Canceled = result.Data.Where(x => x.Status == TodoStatus.Cancelled && x.RowStatus),
            Archived = result.Data.Where(x => !x.RowStatus)
        };

        return Ok(new { success = true, data = dto });
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoItem(TodoItemCreateRequest request)
    {
        var command = new CreateTodoItemCommand(
            request.Title,
            request.Description,
            request.DueDate,
            request.Status,
            CurrentUser.UserId,
            CurrentUser.Email,
            CommandInfo.RowStatusIsActive);

        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(int id, TodoItemUpdateRequest request)
    {
        var command = new UpdateTodoItemCommand(
            id,
            request.Title,
            request.Description,
            request.DueDate,
            request.Status,
            CurrentUser.UserId,
            CurrentUser.Email,
            request.Rowstatus);

        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        var command = new DeleteTodoItemCommand(id);

        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    //[HasAnyRole("Super Admin")]
    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //{
    //    var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.RowStatus || !x.RowStatus));
    //    if (result.IsSuccess)
    //    {
    //        return Ok(result);
    //    }
    //    else
    //    {
    //        return BadRequest(result);
    //    }
    //}
}
