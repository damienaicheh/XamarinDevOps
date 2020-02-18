using XamarinDevOps.Configuration;

namespace XamarinDevOps.iOS.Configuration
{
    public class IOSConfigurationStreamProviderFactory : IConfigurationStreamProviderFactory
    {
        public IConfigurationStreamProvider Create()
        {
            return new IOSConfigurationStreamProvider();
        }
    }
}