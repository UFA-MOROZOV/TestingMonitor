using AutoMapper;
using TestingMonitor.Application.UseCases.Compilers.Update;
using TestingMonitor.Application.UseCases.HeaderFiles.Get;
using TestingMonitor.Application.UseCases.Models;
using TestingMonitor.Application.UseCases.TaskExecutions.Get;
using TestingMonitor.Application.UseCases.Tests.Get;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Test, TestItemDto>();

        CreateMap<TestGroup, TestItemDto>();

        CreateMap<CompilerToUpdateCommand, Compiler>();

        CreateMap<HeaderFile, HeaderFileDto>();

        CreateMap<Compiler, CompilerDto>();

        CreateMap<CompilerTask, CompilerTaskDto>()
            .ForMember(x => x.IsCompleted, x => x.MapFrom(y => y.DateOfCompletion));
    }
}