using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Project.InsertProject;

public class InsertProjectCommand : IRequest<ResultViewModel<Guid>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid IdClient { get; set; }
    public Guid IdDeveloper { get; set; }
    public decimal TotalCost { get; set; }

    public ProjectEntity ToEntity()
        => new(Title, Description, IdDeveloper,IdClient, TotalCost);
}