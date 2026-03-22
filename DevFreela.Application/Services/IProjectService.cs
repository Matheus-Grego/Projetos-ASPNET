using DevFreela.Application.Models;

namespace DevFreela.Application.Services;

public interface IProjectService
{
    ResultViewModel<List<ProjectModel>> GetAllProjects();
    ResultViewModel<ProjectModel> GetProjectById(Guid id);
    ResultViewModel<int> CreateProject(CreateProjectInputModel project);
    ResultViewModel<ProjectModel> UpdateProject(ProjectModel project);
}