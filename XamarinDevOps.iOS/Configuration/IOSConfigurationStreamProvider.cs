using System;
using System.IO;
using System.Threading.Tasks;
using XamarinDevOps.Configuration;

namespace XamarinDevOps.iOS.Configuration
{
    public class IOSConfigurationStreamProvider : IConfigurationStreamProvider
    {
        private const string ConfigurationFilePath = "Assets/config.json";
        
        private Stream _readingStream;

        public Task<Stream> GetStreamAsync()
        {
            ReleaseUnmanagedResources();
            _readingStream = new FileStream(ConfigurationFilePath, FileMode.Open, FileAccess.Read);
            
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

        ~IOSConfigurationStreamProvider()
        {
            ReleaseUnmanagedResources();
        }
    }
}