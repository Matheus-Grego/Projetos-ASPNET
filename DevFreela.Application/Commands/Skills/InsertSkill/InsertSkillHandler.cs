using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.Skills.InsertSkill;

public class InsertSkillHandler : IRequestHandler<InsertSkillCommand,ResultViewModel>
{
    private readonly ISkillRepository _repository;

    public InsertSkillHandler(DevFreelaDbContext context, ISkillRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();
        _repository.Add(entity);
        return ResultViewModel.Success();
    }
}