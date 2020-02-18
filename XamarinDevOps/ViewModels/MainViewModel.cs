using System;
using System.Threading;
using System.Threading.Tasks;
using XamarinDevOps.Configuration;

namespace XamarinDevOps.ViewModels
{
    public class MainViewModel
    {
        public async Task<Config> LoadConfigurationAsync()
        {
            using (var cts = new CancellationTokenSource())
            {
                return await ConfigurationManager.Instance.GetAsync(cts.Token);
            }
        }
    }
}
