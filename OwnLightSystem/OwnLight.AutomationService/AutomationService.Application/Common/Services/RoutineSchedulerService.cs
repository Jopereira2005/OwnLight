using AutomationService.Application.Common.Jobs;
using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Domain.Entities;
using Quartz;

namespace AutomationService.Application.Common.Services;

public class RoutineSchedulerService(ISchedulerFactory schedulerFactory) : IRoutineSchedulerService
{
    private readonly ISchedulerFactory _schedulerFactory = schedulerFactory;

    public async Task ScheduleRoutineAsync(Routine routine)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var job = JobBuilder
            .Create<RoutineJob>()
            .WithIdentity($"RoutineJob-{routine.Id}")
            .UsingJobData("RoutineId", routine.Id.ToString())
            .Build();

        ITrigger trigger;

        if (routine.IsOneTime)
        {
            var executionDateTime = DateTime.Today.Add(routine.ExecutionTime);

            if (executionDateTime < DateTime.Now)
                executionDateTime = executionDateTime.AddDays(1);

            trigger = TriggerBuilder
                .Create()
                .WithIdentity($"RoutineTrigger-{routine.Id}")
                .StartAt(new DateTimeOffset(executionDateTime))
                .Build();
        }
        else if (routine.DaysOfWeek != null && routine.DaysOfWeek.Count != 0)
        {
            var cronExpression = CreateCronExpressionForDays(
                routine.DaysOfWeek,
                routine.ExecutionTime
            );

            trigger = TriggerBuilder
                .Create()
                .WithIdentity($"RoutineTrigger-{routine.Id}")
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExpression))
                .Build();
        }
        else
        {
            trigger = TriggerBuilder
                .Create()
                .WithIdentity($"RoutineTrigger-{routine.Id}")
                .StartNow()
                .WithSchedule(
                    CronScheduleBuilder.DailyAtHourAndMinute(
                        routine.ExecutionTime.Hours,
                        routine.ExecutionTime.Minutes
                    )
                )
                .Build();
        }

        await scheduler.ScheduleJob(job, trigger);
    }

    public async Task UpdateRoutineAsync(Routine routine)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var jobKey = new JobKey($"RoutineJob-{routine.Id}");
        await scheduler.DeleteJob(jobKey);

        var job = JobBuilder
            .Create<RoutineJob>()
            .WithIdentity($"RoutineJob-{routine.Id}")
            .UsingJobData("RoutineId", routine.Id.ToString())
            .Build();

        ITrigger trigger;

        if (routine.IsOneTime == true)
        {
            var executionDateTime = DateTime.Today.Add(routine.ExecutionTime);

            if (executionDateTime < DateTime.Now)
                executionDateTime = executionDateTime.AddDays(1);

            trigger = TriggerBuilder
                .Create()
                .WithIdentity($"RoutineTrigger-{routine.Id}")
                .StartAt(new DateTimeOffset(executionDateTime))
                .Build();
        }
        else if (routine.DaysOfWeek != null && routine.DaysOfWeek.Count != 0)
        {
            var cronExpression = CreateCronExpressionForDays(
                routine.DaysOfWeek,
                routine.ExecutionTime
            );

            trigger = TriggerBuilder
                .Create()
                .WithIdentity($"RoutineTrigger-{routine.Id}")
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExpression))
                .Build();
        }
        else
        {
            trigger = TriggerBuilder
                .Create()
                .WithIdentity($"RoutineTrigger-{routine.Id}")
                .StartNow()
                .WithSchedule(
                    CronScheduleBuilder.DailyAtHourAndMinute(
                        routine.ExecutionTime.Hours,
                        routine.ExecutionTime.Minutes
                    )
                )
                .Build();
        }

        await scheduler.ScheduleJob(job, trigger);
    }

    public async Task DeleteRoutineAsync(Guid routineId)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var jobKey = new JobKey($"RoutineJob-{routineId}");
        await scheduler.DeleteJob(jobKey);
    }

    private static string CreateCronExpressionForDays(
        List<DayOfWeek> daysOfWeek,
        TimeSpan executionTime
    )
    {
        var daysCron = string.Join(",", daysOfWeek.Select(d => ((int)d + 1) % 7));
        var cronExpression = $"{executionTime.Minutes} {executionTime.Hours} * * {daysCron}";

        return cronExpression;
    }
}