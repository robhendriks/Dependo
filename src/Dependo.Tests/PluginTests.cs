namespace Dependo.Tests
{
    using Items;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PluginTests
    {
        private Item _plugin;

        [TestInitialize]
        public void Initialize()
        {
            _plugin = new Item("Foo");
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
            Assert.AreEqual(0, _plugin.CompareTo(new Item("Foo")));
        }

        [TestMethod]
        public void CompareTo_DoesNotReturnZero_WhenNameIsNotEqual()
        {
            Assert.AreNotEqual(0, _plugin.CompareTo(new Item("Bar")));
        }

        [TestMethod]
        public void Equals_ReturnsTrue()
        {
            var foo1 = new Item("Foo");
            var foo2 = new Item("Foo");

            Assert.IsTrue(foo1.Equals(foo1));
            Assert.IsTrue(foo1.Equals(foo2));
        }

        [TestMethod]
        public void Equals_ReturnsFalse()
        {
            var foo = new Item("Foo");
            var bar = new Item("Bar");

            Assert.IsFalse(foo.Equals(null));
            Assert.IsFalse(foo.Equals(bar));
        }
    }
}
