using System;
using System.IO;
using System.Threading.Tasks;

namespace XamarinDevOps.Configuration
{
    public interface IConfigurationStreamProvider : IDisposable
    {
        Task<Stream> GetStreamAsync();
    }
}