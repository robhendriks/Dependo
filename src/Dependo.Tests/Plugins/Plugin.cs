namespace Dependo.Tests.Plugins
{
    using System;

    internal class Plugin : DependencyBase<Plugin, string>
    {
        public string Name { get; }

        public override string Key => Name;

        public Plugin(string name) : base(() => new PluginEqualityComparer())
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override int CompareTo(Plugin other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}
