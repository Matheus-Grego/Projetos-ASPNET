using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.UnitTests.Fakes;
using MediatR;
using Moq;
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
    
    [Fact]
    public async Task InputDataAreOk_Insert_Success_Moq()
    {
        // Arrange
        
        
        var mediator = Substitute.For<IMediator>();
        var repository = Mock
            .Of<IProjectRepository>(r => r.Add(It.IsAny<ProjectEntity>()) == Task.FromResult(Guid.NewGuid()));

        //var command = new InsertProjectCommand
        //{
        //    Title = "Project A",
        //    Description = "Descrição do Projeto",
        //    TotalCost = 20000,
        //    IdClient = 1,
        //    IdFreelancer = 2
        //};

        var command = FakeDataHelper.CreateFakeInsertProjectCommand();

        var handler = new InsertProjectHandler(mediator,repository);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccessful);

        // mock.Verify(m => m.Add(It.IsAny<Project>()), Times.Once);

        Mock.Get(repository).Verify(m => m.Add(It.IsAny<ProjectEntity>()), Times.Once);
    }
}