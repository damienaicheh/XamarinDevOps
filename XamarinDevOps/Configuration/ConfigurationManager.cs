using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinDevOps.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly SemaphoreSlim _semaphoreSlim;

        private static IConfigurationStreamProviderFactory _factory;

        private bool _initialized;
        private Config _configuration;

        protected ConfigurationManager()
        {
            _semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        public static IConfigurationManager Instance { get ; } = new ConfigurationManager();

        public static void Initialize(IConfigurationStreamProviderFactory factory)
        {
            _factory = factory;
        }

        public async Task<Config> GetAsync(CancellationToken cancellationToken)
        {
            await InitializeAsync(cancellationToken).ConfigureAwait(false);

            if (_configuration == null)
                throw new InvalidOperationException("Configuration should not be null");

            return _configuration;
        }

        private async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (_initialized)
                return;

            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

                if (_initialized)
                    return;

                var configuration = await ReadAsync().ConfigureAwait(false);
                _initialized = true;
                _configuration = configuration;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<Config> ReadAsync()
        {
            using (var streamProvider = _factory.Create())
            using (var stream = await streamProvider.GetStreamAsync().ConfigureAwait(false))
            {
                var configuration = Deserialize<Config>(stream);
                return configuration;
            }
        }

        private T Deserialize<T>(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new Newtonsoft.Json.JsonTextReader(sr))
            {
                var js = new Newtonsoft.Json.JsonSerializer();
                var value = js.Deserialize<T>(jtr);
                return value;
            }
        }
    }
}