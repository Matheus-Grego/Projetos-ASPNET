using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill;

public class InsertSkillCommand : IRequest<ResultViewModel>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TechCategory Category { get; set; }
    
    public TechEntity ToEntity()
        => new (Name, Description,Category);
}