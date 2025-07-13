using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailParameterCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailTemplateCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailParameterHandlers;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailTemplateHandler;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Handlers;
using ToDoApp.BackEnd.Application.Mapper.Profiles.Common;
using ToDoApp.BackEnd.Application.Utilities.ValidationRules.ApplicationValidations.EmailTemplateValidators;
using ToDoApp.BackEnd.Application.Utilities.ValidationRules.ApplicationValidations.MailParameterValidations;
using ToDoApp.BackEnd.Application.Utilities.ValidationRules.TodoItemValidations;
using ToDoApp.BackEnd.Application.Utilities.ValidationRules.UserLoginValidations.UserDtoValidators;

namespace ToDoApp.BackEnd.Application;
public static class ServiceRegistration
{
    public static void AddApplicationService(this IServiceCollection service)
    {
        #region Mediatr

        #region ApplicationOperations  
        #region EmailParameter  
        service.AddMediatR(typeof(CreateEmailParameterCommandHandler).Assembly);
        service.AddMediatR(typeof(DeleteEmailParameterCommandHandler).Assembly);
        service.AddMediatR(typeof(GetAllEmailParameterQueryHandler).Assembly);
        service.AddMediatR(typeof(GetByFilterEmailParameterQueryHandler).Assembly);
        service.AddMediatR(typeof(UpdateEmailParameterCommandHandler).Assembly);
        #endregion

        #region EmailTemplate  
        service.AddMediatR(typeof(CreateEmailTemplateCommandHandler).Assembly);
        service.AddMediatR(typeof(DeleteEmailTemplateCommandHandler).Assembly);
        service.AddMediatR(typeof(GetAllEmailTemplateQueryHandler).Assembly);
        service.AddMediatR(typeof(GetByFilterEmailTemplateQueryHandler).Assembly);
        service.AddMediatR(typeof(UpdateEmailTemplateCommandHandler).Assembly);
        #endregion


        #region ToDo Item  
        service.AddMediatR(typeof(CreateTodoItemCommandHandler).Assembly);
        service.AddMediatR(typeof(DeleteTodoItemCommandHandler).Assembly);
        service.AddMediatR(typeof(GetAllTodoItemQueryHandler).Assembly);
        service.AddMediatR(typeof(GetByFilterIdTodoItemQueryHandler).Assembly);
        service.AddMediatR(typeof(UpdateTodoItemCommandHandler).Assembly);
        #endregion

        #endregion

        #endregion

        #region Mapper

        #region mapper service
        service.AddAutoMapper(typeof(BaseProfile<,>).Assembly);
        #endregion

        #endregion

        #region Fluent validation

        #region ApplicationValidation
        #region EmailTemplateValidators
        service.AddScoped<IValidator<CreateEmailTemplateCommand>, CreateEmailTemplateCommandValidator>();
        service.AddScoped<IValidator<UpdateEmailTemplateCommand>, UpdateEmailTemplateCommandValidator>();
        #endregion

        #region MailParameterValidations
        service.AddScoped<IValidator<CreateEmailParameterCommand>, CreateEmailParameterCommandValidator>();
        service.AddScoped<IValidator<UpdateEmailParameterCommand>, UpdateEmailParameterCommandValidator>();
        #endregion

        #endregion

        #region UserLoginValidations
        service.AddScoped<IValidator<UserLoginDtos>, UserLoginDtoValidator>();
        service.AddScoped<IValidator<UserRegisterDtos>, UserRegisterDtoValidator>();
        #endregion       

        #region TodoItemValidations
        service.AddScoped<IValidator<CreateTodoItemCommand>, CreateTodoItemCommandValidator>();
        service.AddScoped<IValidator<UpdateTodoItemCommand>, UpdateTodoItemCommandValidator>();
        #endregion       

        #endregion
    }
}
