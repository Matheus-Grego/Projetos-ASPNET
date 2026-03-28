using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using MediatR;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class InsertProjectHandlerTests
{
    [Fact]
    public async Task InputDataAreOk_Insert_Success_NSubstitute()
    {
        //arrange
        var repository = Substitute.For<IProjectRepository>();
        var mediator = Substitute.For<IMediator>();
        repository.Add(Arg.Any<ProjectEntity>()).Returns(Task.FromResult(Guid.NewGuid()));

        var command = new InsertProjectCommand
        {
            Description = "Description",
            Title = "Title",
            IdClient = Guid.NewGuid(),
            IdDeveloper = Guid.NewGuid(),
            TotalCost = 1000
        };
        
        var handler = new InsertProjectHandler(mediator,repository);
        
        //act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccessful);
    }
}