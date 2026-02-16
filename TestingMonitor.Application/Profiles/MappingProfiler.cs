using AutoMapper;
using TestingMonitor.Application.UseCases.Compilers.Update;
using TestingMonitor.Application.UseCases.Tests.Get;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Test, ItemDto>();

        CreateMap<TestGroup, ItemDto>();

        CreateMap<CompilerToUpdateCommand, Compiler>();
    }
}