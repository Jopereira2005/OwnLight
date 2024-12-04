using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Group.Commands;
using AutomationService.Application.Features.Room.Commands;
using AutomationService.Application.Features.Routine.Commands;
using AutomationService.Domain.Entities;

namespace AutomationService.Application.Common.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateRoutineCommand, Routine>();
        CreateMap<UpdateRoutineNameCommand, Routine>();
        CreateMap<UpdateRoutineCommand, Routine>();
        CreateMap<Routine, RoutineResponseDTO>()
            .ForMember(
                dest => dest.ActionType,
                opt => opt.MapFrom(src => src.ActionType.ToString())
            )
            .ForMember(
                dest => dest.ActionTarget,
                opt => opt.MapFrom(src => src.ActionTarget.ToString())
            );

        CreateMap<RoutineExecutionLog, RoutineLogDTO>()
            .ForMember(
                dest => dest.ActionType,
                opt => opt.MapFrom(src => src.ActionType.ToString())
            )
            .ForMember(
                dest => dest.ActionTarget,
                opt => opt.MapFrom(src => src.ActionTarget.ToString())
            )
            .ForMember(
                dest => dest.ActionStatus,
                opt => opt.MapFrom(src => src.ActionStatus.ToString())
            );

        CreateMap<CreateGroupCommand, Group>();
        CreateMap<UpdateGroupCommand, Group>();
        CreateMap<Group, GroupResponseDTO>();
        CreateMap<Group, GroupDevicesDTO>()
            .ForMember(
                dest => dest.DeviceIds,
                opt => opt.MapFrom(src => src.DeviceIdsList.Select(id => id.ToString()).ToList())
            );

        CreateMap<CreateRoomCommand, Room>();
        CreateMap<UpdateRoomCommand, Room>();
        CreateMap<Room, RoomResponseDTO>();
        CreateMap<Room, RoomDevicesDTO>()
            .ForMember(
                dest => dest.DeviceIds,
                opt => opt.MapFrom(src => src.DeviceIdsList.Select(id => id.ToString()).ToList())
            );
    }
}
