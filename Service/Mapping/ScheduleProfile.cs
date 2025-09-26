using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Service.Mapping;

public class ScheduleProfile:Profile
{
    public ScheduleProfile()
    {
        CreateMap<Schedule, ScheduleDto>();
    }
}