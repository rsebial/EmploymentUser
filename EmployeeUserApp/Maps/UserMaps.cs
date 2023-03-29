using EmployeeUserApp.Api.Dto;
using EmployeeUserApp.Domain.Commands;
using EmployeeUserApp.Domain.Models;
using EmployeeUserApp.Domain.Queries;
using Mapster;

namespace EmployeeUserApp.Api.Maps;

// defines maps between the model and dto's and the dto's and commands
public class UserMaps : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // queries
        config.NewConfig<UserQueryDto, UserSearch.Query>();

        // model
        config.NewConfig<User, UserSearchDto>();

        // commands
        config.NewConfig<UserCreateDto, UserCreate.Command>();
        config.NewConfig<UserUpdateDto, UserUpdate.Command>();
    }
}

