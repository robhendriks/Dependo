namespace Dependo.Tests
{
    using System;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Plugins;

    [TestClass]
    public class DependencyContainerBaseTests
    {
        [TestMethod]
        public void Test()
        {
            var container = new PluginContainer()
                .RegisterDependency(new Plugin("A"), "B")
                .RegisterDependency(new Plugin("B"), "D")
                .RegisterDependency(new Plugin("C"), "B")
                .RegisterDependency(new Plugin("D"))
                .RegisterDependency(new Plugin("E"), "C", "A")
                .RegisterDependency(new Plugin("F"))
                .RegisterDependency(new Plugin("G"), "F");

            var roots = container.ResolveDependencies();

            foreach (var root in roots)
            {
                root.Walk((info, plugin) =>
                {
                    if (info.Level == 0)
                    {
                        Console.WriteLine("{0}", plugin.Key);
                    }
                    else
                    {
                        var c = info.IsLast ? '└' : '├';
                        Console.WriteLine("{0}{1} {2}", "".PadLeft((info.Level - 1) * 2, ' '), c, plugin.Key);
                    }
                });
            }

            // TODO: Verified functional testing, write unit tests!
        }
    }
}
