using DevFreela.Application.Commands.Project.DeleteProject;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.UnitTests.Fakes;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async void ProjectExists_Delete_Sucess_NSubstitute()
    {
        var project = FakeDataHelper.CreateFakeProject();

        var repository = Substitute.For<IProjectRepository>();
        repository.GetById(Guid.NewGuid()).Returns(Task.FromResult((ProjectEntity?)project));
        repository.Update(Arg.Any<ProjectEntity>()).Returns(Task.CompletedTask);

        var handler = new DeleteProjectHandler(repository);

        var command = new DeleteProjectCommand(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccessful);
        
        await repository.Received(1).GetById(new Guid());
        await repository.Received(1).Update(Arg.Any<ProjectEntity>());
    }
    
    [Fact]
        public async Task ProjectExists_Delete_Success_Moq()
        {
            // Arrange
            // var project = new Project("Projeto A", "Descrição de Projeto", 1, 2, 10000);

            var project = FakeDataHelper.CreateFakeProject();

            var repository = Mock.Of<IProjectRepository>(p =>
                p.GetById(It.IsAny<Guid>()) == Task.FromResult(project) &&
                p.Update(It.IsAny<ProjectEntity>()) == Task.CompletedTask
                );

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(Guid.NewGuid(), Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccessful);
            
            Mock.Get(repository).Verify(r => r.GetById(Guid.NewGuid()), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<ProjectEntity>()), Times.Once);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<Guid>()).Returns(Task.FromResult((ProjectEntity?)null));

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(Guid.NewGuid(), Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccessful);
            
            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

            await repository.Received(1).GetById(Arg.Any<Guid>());
            await repository.DidNotReceive().Update(Arg.Any<ProjectEntity>());

        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.GetById(It.IsAny<Guid>()) == Task.FromResult((ProjectEntity?) null)
            );
            
            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(Guid.NewGuid(), Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);


            Mock.Get(repository).Verify(r => r.GetById(Guid.NewGuid()), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<ProjectEntity>()), Times.Never);
        }
}