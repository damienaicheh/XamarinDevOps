using Android.Content;
using Android.Content.Res;
using System;
using System.IO;
using System.Threading.Tasks;
using XamarinDevOps.Configuration;

namespace XamarinDevOps.Droid.Configuration
{
    public class AndroidConfigurationStreamProvider : IConfigurationStreamProvider
    {
        private const string ConfigurationFilePath = "config.json";

        private readonly Func<Context> _contextProvider;

        private Stream _readingStream;

        public AndroidConfigurationStreamProvider(Func<Context> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public Task<Stream> GetStreamAsync()
        {
            ReleaseUnmanagedResources();

            AssetManager assets = _contextProvider().Assets;

            _readingStream = assets.Open(ConfigurationFilePath);

            return Task.FromResult(_readingStream);
        }

        private void ReleaseUnmanagedResources()
        {
            _readingStream?.Dispose();
            _readingStream = null;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~AndroidConfigurationStreamProvider()
        {
            ReleaseUnmanagedResources();
        }
    }
}
