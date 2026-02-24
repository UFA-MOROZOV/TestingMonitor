using AutoMapper;
using TestingMonitor.Application.UseCases.Compilers.Update;
using TestingMonitor.Application.UseCases.CompilerTasks.Get;
using TestingMonitor.Application.UseCases.CompilerTasks.GetById;
using TestingMonitor.Application.UseCases.HeaderFiles.Get;
using TestingMonitor.Application.UseCases.Models;
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

        CreateMap<CompilerTask, GetCompilerTaskByIdResponse>();

        CreateMap<CompilerTask, CompilerTaskDto>()
            .ForMember(x => x.IsCompleted, x => x.MapFrom(y => y.DateOfCompletion != null));
    }
}