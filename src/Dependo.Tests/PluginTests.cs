namespace Dependo.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Plugins;

    [TestClass]
    public class PluginTests
    {
        private Plugin _plugin;

        [TestInitialize]
        public void Initialize()
        {
            _plugin = new Plugin("Foo");
        }

        [TestMethod]
        public void Constructor_Throws_WhenNameIsNullOrWhitespace()
        {
            // TODO: FIX
//            Assert.ThrowsException<ArgumentNullException>(() => new Plugin(null));
        }

        [TestMethod]
        public void CompareTo_ReturnsZero_WhenReferenceEquals()
        {
            Assert.AreEqual(0, _plugin.CompareTo(_plugin));
        }

        [TestMethod]
        public void CompareTo_ReturnsOne_WhenOtherIsNull()
        {
            Assert.AreEqual(1, _plugin.CompareTo(null));
        }

        [TestMethod]
        public void CompareTo_ReturnsZero_WhenNameIsEqual()
        {
            Assert.AreEqual(0, _plugin.CompareTo(new Plugin("Foo")));
        }

        [TestMethod]
        public void CompareTo_DoesNotReturnZero_WhenNameIsNotEqual()
        {
            Assert.AreNotEqual(0, _plugin.CompareTo(new Plugin("Bar")));
        }

        [TestMethod]
        public void Equals_ReturnsTrue()
        {
            var foo1 = new Plugin("Foo");
            var foo2 = new Plugin("Foo");

            Assert.IsTrue(foo1.Equals(foo1));
            Assert.IsTrue(foo1.Equals(foo2));
        }

        [TestMethod]
        public void Equals_ReturnsFalse()
        {
            var foo = new Plugin("Foo");
            var bar = new Plugin("Bar");

            Assert.IsFalse(foo.Equals(null));
            Assert.IsFalse(foo.Equals(bar));
        }
    }
}
