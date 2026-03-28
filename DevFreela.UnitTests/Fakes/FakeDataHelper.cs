using Bogus;
using DevFreela.Application.Commands.Project.DeleteProject;
using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Domain.Entities;

namespace DevFreela.UnitTests.Fakes;

public class FakeDataHelper
{
    private static readonly Faker _faker = new();

    public static ProjectEntity CreateFakeProjectV1()
    {
        return new ProjectEntity(
            _faker.Commerce.ProductName(),
            _faker.Lorem.Sentence(),
            _faker.Random.Guid(),
            _faker.Random.Guid(),
            _faker.Random.Decimal(1000, 10000)
        );
    }

    private static readonly Faker<ProjectEntity> _projectFaker = new Faker<ProjectEntity>()
        .CustomInstantiator(f => new ProjectEntity(
            f.Commerce.ProductName(),
            f.Lorem.Sentence(),
            f.Random.Guid(),
            f.Random.Guid(),
            f.Random.Decimal(1000, 10000)
        ));

    private static readonly Faker<InsertProjectCommand> _insertProjectCommandFaker = new Faker<InsertProjectCommand>()
        .RuleFor(c => c.Title, f => f.Commerce.ProductName())
        .RuleFor(c => c.Description, f => f.Lorem.Sentence())
        .RuleFor(c => c.IdDeveloper, f => f.Random.Guid())
        .RuleFor(c => c.IdClient, f => f.Random.Guid())
        .RuleFor(c => c.TotalCost, f => f.Random.Decimal(1000, 10000));

    public static ProjectEntity CreateFakeProject() => _projectFaker.Generate();

    public static List<ProjectEntity> CreateFakeProjectList() => _projectFaker.Generate(5);

    public static InsertProjectCommand CreateFakeInsertProjectCommand()
        => _insertProjectCommandFaker.Generate();

    // Precisamos realmente disso?
    public static DeleteProjectCommand CreateFakeDeleteProjectCommand(Guid projectId, Guid userId)
        => new(projectId, userId);
}