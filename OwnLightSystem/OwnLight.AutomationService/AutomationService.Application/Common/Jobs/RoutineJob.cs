using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Application.Features.Routine.Queries;
using AutomationService.Application.Features.RoutineLog.Commands;
using AutomationService.Domain.Enums;
using MediatR;
using Quartz;

namespace AutomationService.Application.Common.Jobs;

public class RoutineJob(
    IMediator mediator,
    IUserServiceClient userServiceClient,
    IDeviceServiceClient deviceServiceClient
) : IJob
{
    private readonly IMediator _mediator = mediator;
    private readonly IUserServiceClient _userServiceClient = userServiceClient;
    private readonly IDeviceServiceClient _deviceServiceClient = deviceServiceClient;

    public async Task Execute(IJobExecutionContext context)
    {
        var routineId = context.MergedJobDataMap.GetString("RoutineId");

        if (Guid.TryParse(routineId, out Guid parsedRoutineId))
        {
            var routine = await _mediator.Send(new GetRoutineByIdQuery(parsedRoutineId));

            if (routine != null)
            {
                var tokenResponse = await _userServiceClient.RefreshAccessTokenAsync(
                    routine.UserId
                );

                var accessToken = tokenResponse.AccessToken;
                var result = await _deviceServiceClient.ExecuteActionAsync(routine, accessToken);

                var logRoutineExecutionCommand = new LogRoutineExecutionCommand(
                    routine.Id,
                    routine.UserId,
                    routine.TargetId ?? Guid.Empty,
                    routine.ActionType,
                    result.IsSuccess ? ActionStatus.Success : ActionStatus.Failed,
                    result.ErrorMessage
                );
                await _mediator.Send(logRoutineExecutionCommand);

                if (!result.IsSuccess)
                    Console.WriteLine($"Erro ao executar a ação: {result.ErrorMessage}");
                else
                    Console.WriteLine("Ação executada com sucesso.");
            }
        }
    }
}
