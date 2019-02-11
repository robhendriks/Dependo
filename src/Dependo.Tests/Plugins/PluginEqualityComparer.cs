using System.Collections.Generic;

namespace Dependo.Tests.Plugins
{
    internal class PluginEqualityComparer : IEqualityComparer<Plugin>
    {
        public bool Equals(Plugin x, Plugin y)
        {
            return x?.Equals(y) ?? false;
        }

        public int GetHashCode(Plugin obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
