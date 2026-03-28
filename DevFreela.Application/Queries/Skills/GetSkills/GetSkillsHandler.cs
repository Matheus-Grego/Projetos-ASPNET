using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Queries.Skills.GetSkills;

public class GetSkillsHandler : IRequestHandler<GetSkillsQuery, ResultViewModel<List<TechnologyModel>>> 
{
    private readonly ISkillRepository _repository;

    public GetSkillsHandler(DevFreelaDbContext context, ISkillRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<List<TechnologyModel>>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _repository.GetAll();
        var model = skills.Select(x => TechnologyModel.FromEntity(x)).ToList();
        return ResultViewModel<List<TechnologyModel>>.Success(model);
    }
}