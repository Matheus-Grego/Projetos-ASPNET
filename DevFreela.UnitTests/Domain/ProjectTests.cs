using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;

namespace DevFreela.UnitTests.Domain;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreates_Start_Success()
    {
        var project = new ProjectEntity("Project A","description", new Guid("f4886259-09a2-480c-b239-12e77abaf898"), new Guid("9ad6fe87-4dca-4d2d-a0f7-0d6271f2fb8d"), 10000);
        
        project.Start();
        
        Assert.Equal(ProjectStatus.OnGoing, project.Status);
    }

    [Fact]
    public void ProjectIsInInvalidState_Start_ThrowError()
    {
        var project = new ProjectEntity("Project A","description", new Guid("f4886259-09a2-480c-b239-12e77abaf898"), new Guid("9ad6fe87-4dca-4d2d-a0f7-0d6271f2fb8d"), 10000);
        project.Start();
        
        Action start = project.Start;
        
        var exception = Assert.Throws<InvalidOperationException>(start);
        
        Assert.Equal(ProjectEntity.INVALID_STATE_MESSAGE, exception.Message);
    }
}