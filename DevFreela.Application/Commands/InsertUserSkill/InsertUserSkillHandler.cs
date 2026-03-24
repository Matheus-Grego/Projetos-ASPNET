using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkill;

public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;

    public InsertUserSkillHandler(DevFreelaDbContext context, IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
    {
        var userSkills = request.TechnologiesID.Select(s => new UserTechEntity(request.UserId, s)).ToList();

       _repository.InsertSkill(userSkills);

        return ResultViewModel.Success();
    }
}