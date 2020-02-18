using System.Threading;
using System.Threading.Tasks;

namespace XamarinDevOps.Configuration
{
    public interface IConfigurationManager
    {
        Task<Config> GetAsync(CancellationToken cancellationToken);
    }
}