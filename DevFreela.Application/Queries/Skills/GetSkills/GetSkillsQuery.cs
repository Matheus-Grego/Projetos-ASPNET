using DevFreela.Application.Models;
using DevFreela.Domain.Enums;
using MediatR;

namespace DevFreela.Application.Queries.Skills.GetSkills;

public class GetSkillsQuery : IRequest<ResultViewModel<List<TechnologyModel>>>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public TechCategory Category { get; private set; }
}