namespace Dependo.Tests
{
    using System;
    using System.Linq;
    using Extensions;
    using Items;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DependencyContainerBaseTests
    {
        [TestMethod]
        public void Test()
        {
            var container = new ItemContainer()
                .RegisterDependency(new Item("A"), "B")
                .RegisterDependency(new Item("B"), "D")
                .RegisterDependency(new Item("C"), "B")
                .RegisterDependency(new Item("D"))
                .RegisterDependency(new Item("E"), "C", "A")
                .RegisterDependency(new Item("F"))
                .RegisterDependency(new Item("G"), "F");

            var roots = container.ResolveDependencies().ToList();

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
