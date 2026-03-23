using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public interface IProjectService
{
    ResultViewModel<List<ProjectModel>> GetAllProjects(string search, int page = 0, int pageSize = 10);
    ResultViewModel<ProjectModel> GetProjectById(Guid projectId);
    ResultViewModel<Guid> InsertProject(CreateProjectInputModel project);
    ResultViewModel UpdateProject(ProjectModel model);
    ResultViewModel DeleteProject(Guid projectId);
    ResultViewModel InsertComments(CreateCommentInputModel Comment);
    ResultViewModel<List<CreateCommentInputModel>> GetComments(Guid projectId);
    ResultViewModel CompleteProject(Guid projectId);

}
