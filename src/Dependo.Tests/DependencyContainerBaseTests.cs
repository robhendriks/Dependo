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
                .RegisterDependency(new Item("A")
                    .DependsOn("B"))
                .RegisterDependency(new Item("B")
                    .DependsOn("D"))
                .RegisterDependency(new Item("C")
                    .DependsOn("B"))
                .RegisterDependency(new Item("D"))
                .RegisterDependency(new Item("E")
                    .DependsOn("C")
                    .DependsOn("A"))
                .RegisterDependency(new Item("F"))
                .RegisterDependency(new Item("G")
                    .DependsOn("F"));

            var roots = container.ResolveDependencies().ToList();

            foreach (var root in roots)
            {
                root.Walk((info, plugin) =>
                {
                    // Print
                    Console.WriteLine("{0}{1}", "".PadLeft(info.Level > 0 ? info.Level * 2 : 0, '.'), plugin.Key);
                });
            }
        }
    }
}
