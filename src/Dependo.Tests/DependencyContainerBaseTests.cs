namespace Dependo.Tests
{
    using System.Linq;
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
                .RegisterDependency(new Plugin("E"), "C", "A");

            var sortedDependencies = container.ResolveDependencies();

            // TODO: Verified functional testing, write unit tests!
        }
    }
}
