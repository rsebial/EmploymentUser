using EmployeeUserApp.Api.Dto;
using EmployeeUserApp.Domain.Commands;
using EmployeeUserApp.Domain.Models;
using EmployeeUserApp.Domain.Queries;
using NUnit.Framework;

namespace EmployeeUserApp.Api.Tests.Maps;

internal class UserMapTests : TestBase
{
    [Test]
    public void Map_QueryDto_To_Query()
    {
        // arrange
        var item = Fixture.Create<UserQueryDto>();

        // act
        var actual = item.Adapt<UserSearch.Query>();

        // assert
        Assert.IsNotNull(actual); 
        Assert.That(actual.FirstName, Is.EqualTo(item.FirstName));
        Assert.That(actual.LastName, Is.EqualTo(item.LastName));
    }

    [Test]
    public void Map_User_To_SearchDto()
    {
        // arrange
        var item = Fixture.Create<User>();

        // act
        var actual = item.Adapt<UserSearchDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.FirstName, Is.EqualTo(item.FirstName));
        Assert.That(actual.LastName, Is.EqualTo(item.LastName));        
    }


    [Test]
    public void Map_User_To_GetDto()
    {
        // arrange
        var item = Fixture.Create<User>();

        // act
        var actual = item.Adapt<UserGetDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.FirstName, Is.EqualTo(item.FirstName));
        Assert.That(actual.FirstName, Is.EqualTo(item.LastName));
    }

    [Test]
    public void Map_CreateDto_To_Command()
    {
        // arrange
        var item = Fixture.Create<UserCreateDto>();

        // act
        var actual = item.Adapt<UserCreate.Command>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.FirstName, Is.EqualTo(item.FirstName));
        Assert.That(actual.LastName, Is.EqualTo(item.LastName));
    }

    [Test]
    public void Map_UpdateDto_To_Command()
    {
        // arrange
        var item = Fixture.Create<UserUpdateDto>();

        // act
        var actual = item.Adapt<UserUpdate.Command>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.FirstName, Is.EqualTo(item.FirstName));
        Assert.That(actual.LastName, Is.EqualTo(item.LastName));
    }
}
