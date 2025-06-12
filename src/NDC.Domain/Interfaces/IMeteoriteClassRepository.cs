using NDC.Domain.Entities;

namespace NDC.Domain.Interfaces;

public interface IMeteoriteClassRepository
{
    Task<IEnumerable<MeteoriteClass>> GetClassesByClassNames(params string[] classNames);

    Task<IEnumerable<MeteoriteClass>> GetAll();

    Task BulkInsertAsync(IEnumerable<MeteoriteClass> classes);
}