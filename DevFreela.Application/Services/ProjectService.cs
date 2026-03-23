using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectService(DevFreelaDbContext context)
    {
        _dbContext = context;
    }

    public ResultViewModel<List<ProjectModel>> GetAllProjects(string search = "", int page = 0, int pageSize = 10)
    {
        var projects = _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .Where(p => !p.IsDeleted && (search == "" || p.Title.ToLower().Contains(search.ToLower()) ||
                                         p.Description.ToLower().Contains(search.ToLower())))
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();

        var model = projects.Select(ProjectModel.FromEntity).ToList();

        return ResultViewModel<List<ProjectModel>>.Success(model);
    }

    public ResultViewModel<ProjectModel> GetProjectById(Guid projectId)
    {
        var project = _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .SingleOrDefault(p => p.Id == projectId);

        if (project == null)
            return ResultViewModel<ProjectModel>.Failed("Not found");

        var model = ProjectModel.FromEntity(project);

        return ResultViewModel<ProjectModel>.Success(model);
    }

    public ResultViewModel<Guid> InsertProject(CreateProjectInputModel project)
    {
        try
        {
            var entity = CreateProjectInputModel.toEntity(project);
            _dbContext.Projects.Add(entity);
            _dbContext.SaveChanges();

            return ResultViewModel<Guid>.Success(entity.Id);
        }
        catch (Exception e)
        {
            return ResultViewModel<Guid>.Failed(e.Message);
        }
    }

    public ResultViewModel UpdateProject(ProjectModel model)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == model.Id);

        if (project == null)
            return ResultViewModel.Failed("Not found");

        project.Update(model.Title, model.Description, model.TotalCost);

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel DeleteProject(Guid projectId)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == projectId);

        if (project == null)
            return ResultViewModel.Failed("Not found");

        project.SetAsDeleted();
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel InsertComments(CreateCommentInputModel Comment)
    {
        try
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == Comment.ProjectId);
            if (project == null)
                return ResultViewModel.Failed("Project not found");

            var comments = new CommentsEntity(Comment.ProjectId, Comment.UserId, Comment.Content);

            _dbContext.Comments.Add(comments);
            _dbContext.SaveChanges();
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Failed(e.Message);
        }

    }

    public ResultViewModel<List<CreateCommentInputModel>> GetComments(Guid projectId)
    {
        try
        {
            var project = _dbContext.Projects.Include(x => x.Comments).SingleOrDefault(x => x.Id == projectId);

            if (project == null)
                return ResultViewModel<List<CreateCommentInputModel>>.Failed("Project Not found");

            var comments = project.Comments
                .Where(c => c.IsDeleted == false)
                .Select(CreateCommentInputModel.FromEntity)
                .ToList();

            return ResultViewModel<List<CreateCommentInputModel>>.Success(comments);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<CreateCommentInputModel>>.Failed(e.Message);
        }
    }
}
