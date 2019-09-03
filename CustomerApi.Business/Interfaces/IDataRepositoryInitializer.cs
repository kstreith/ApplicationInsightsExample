using System.Threading.Tasks;

namespace CustomerApi.Business.Interfaces
{
    public interface IDataRepositoryInitializer
    {
        Task InitAsync();
    }
}
