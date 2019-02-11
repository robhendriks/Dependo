namespace Dependo.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Plugins;

    [TestClass]
    public class DependencyContainerBaseTests
    {
        [TestMethod]
        public void Test()
        {
            var container = new PluginContainer()
                .RegisterDependency(new Plugin("Foo"))
                .RegisterDependency(new Plugin("Bar"))
                .RegisterDependency(new Plugin("Baz"));
        }
    }
}
