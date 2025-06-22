using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BackEnd.API.ApiDtos.ToDoItems;
using ToDoApp.BackEnd.API.Attributes;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Queries;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Domain.Enums;

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
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet("userByFilter")]
    public async Task<IActionResult> GetUserTodosByFilter([FromQuery]  TodoStatus todoStatus)
    {
        var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.UserId == CurrentUser.UserId && x.Status == todoStatus));
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.UserId == CurrentUser.UserId && x.ID == id));
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetStaticDataByUser()
    {
        var result = await _mediator.Send(new GetByFilterIdTodoItemQuery(x => x.UserId == CurrentUser.UserId));
        if (result.IsSuccess && result.Data.Count() > 0)
        {
            var pendingData = result.Data.Where(x => x.Status == Domain.Enums.TodoStatus.Pending && x.RowStatus);
            var inProgressData = result.Data.Where(x => x.Status == Domain.Enums.TodoStatus.InProgress && x.RowStatus);
            var complateData = result.Data.Where(x => x.Status == Domain.Enums.TodoStatus.Completed && x.RowStatus);
            var canceledData = result.Data.Where(x => x.Status == Domain.Enums.TodoStatus.Cancelled && x.RowStatus);
            var archiveData = result.Data.Where(x => !x.RowStatus);

            return Ok(new
            {
                success = result.IsSuccess,
                pendingData,
                inProgressData,
                complateData,
                canceledData,
                archiveData
            });
        }
        else
        {

            return BadRequest(result);
        }
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
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(int id,TodoItemUpdateRequest request)
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
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        var command = new DeleteTodoItemCommand(id);

        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
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
