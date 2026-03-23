using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectModel>>>
{
    public GetAllProjectsQuery(string search, int page)
    {
        Search = search;
        Page = page;
    }
    public string Search { get; set; } = "";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}