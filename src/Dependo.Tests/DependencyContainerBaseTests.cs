namespace Dependo.Tests
{
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
                .RegisterDependency(new Plugin("A"), "C")
                .RegisterDependency(new Plugin("B"))
                .RegisterDependency(new Plugin("C"), "B", "D")
                .RegisterDependency(new Plugin("D"))
                .RegisterDependency(new Plugin("E"), "A");

            container.ResolveDependencies();
        }
    }
}
