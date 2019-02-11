namespace Dependo.Tests
{
    using System;
    using Exceptions;
    using Items;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DependencyBaseTests
    {
        private Item _parent;
        private Item _childA;
        private Item _childB;

        [TestInitialize]
        public void Initialize()
        {
            _parent = new Item("Parent");
            _childA = new Item("ChildA");
            _childB = new Item("ChildB");
            _parent.AddChild(_childA);
            _parent.AddChild(_childB);
        }

        [TestMethod]
        public void HasChild_ReturnsTrue()
        {
            Assert.IsTrue(_parent.HasChild(_childA));
            Assert.IsTrue(_parent.HasChild(_childB));
            Assert.IsTrue(_parent.HasChild(new Item("ChildA")));
            Assert.IsTrue(_parent.HasChild(new Item("ChildB")));
        }

        [TestMethod]
        public void AddChild_Throws_WhenChildIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _parent.AddChild(null));
        }

        [TestMethod]
        public void AddChild_Throws_WhenChildIsAlreadyPresent()
        {
            Assert.ThrowsException<DependencyException>(() => _parent.AddChild(_childA));
            Assert.ThrowsException<DependencyException>(() => _parent.AddChild(_childB));
            Assert.ThrowsException<DependencyException>(() => _parent.AddChild(new Item("ChildA")));
            Assert.ThrowsException<DependencyException>(() => _parent.AddChild(new Item("ChildB")));
        }

        [TestMethod]
        public void TryAddChild_ReturnsTrue_WhenChildIsAdded()
        {
            Assert.IsTrue(_parent.TryAddChild(new Item("ChildC")));
            Assert.IsTrue(_parent.TryAddChild(new Item("ChildD")));
        }

        [TestMethod]
        public void TryAddChild_ReturnsFalse_WhenChildWasNotAdded()
        {
            Assert.IsFalse(_parent.TryAddChild(_childA));
            Assert.IsFalse(_parent.TryAddChild(_childB));
            Assert.IsFalse(_parent.TryAddChild(new Item("ChildA")));
            Assert.IsFalse(_parent.TryAddChild(new Item("ChildB")));
        }

        [TestMethod]
        public void RemoveChild_Throws_WhenChildIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _parent.RemoveChild(null));
        }

        [TestMethod]
        public void RemoveChild_Throws_WhenChildIsNotPresent()
        {
            Assert.ThrowsException<DependencyException>(() => _parent.RemoveChild(new Item("ChildC")));
            Assert.ThrowsException<DependencyException>(() => _parent.RemoveChild(new Item("ChildD")));
        }

        [TestMethod]
        public void TryRemoveChild_ReturnsTrue_WhenChildIsRemoved()
        {
            Assert.IsTrue(_parent.TryRemoveChild(_childA));
            Assert.IsTrue(_parent.TryRemoveChild(_childB));
        }

        [TestMethod]
        public void TryRemoveChild_ReturnsFalse_WhenChildWasNotRemove()
        {
            Assert.IsFalse(_parent.TryRemoveChild(new Item("ChildC")));
            Assert.IsFalse(_parent.TryRemoveChild(new Item("ChildD")));
        }

        [TestMethod]
        public void HasChild_ReturnsFalse()
        {
            Assert.IsFalse(_parent.HasChild(new Item("ChildC")));
        }

        [TestMethod]
        public void IsChildOf_ReturnsTrue()
        {
            Assert.IsTrue(_childA.IsChildOf(_parent));
            Assert.IsTrue(_childB.IsChildOf(_parent));
        }

        [TestMethod]
        public void IsChildOf_ReturnsFalse()
        {
            Assert.IsFalse(new Item("ChildA").IsChildOf(_parent));
            Assert.IsFalse(new Item("ChildB").IsChildOf(_parent));
        }

        [TestMethod]
        public void IsParentOf_ReturnsTrue()
        {
            Assert.IsTrue(_parent.IsParentOf(_childA));
            Assert.IsTrue(_parent.IsParentOf(_childB));
        }

        [TestMethod]
        public void IsParentOf_ReturnsFalse()
        {
            Assert.IsFalse(_parent.IsParentOf(new Item("ChildA")));
            Assert.IsFalse(_parent.IsParentOf(new Item("ChildB")));
        }

        [TestMethod]
        public void IsSiblingOf_ReturnsTrue()
        {
            Assert.IsTrue(_childA.IsSiblingOf(_childB));
            Assert.IsTrue(_childB.IsSiblingOf(_childA));
        }

        [TestMethod]
        public void IsSiblingOf_ReturnsFalse()
        {
            Assert.IsFalse(_childA.IsSiblingOf(new Item("ChildB")));
            Assert.IsFalse(_childB.IsSiblingOf(new Item("ChildA")));
        }

        [TestMethod]
        public void ValidateTree()
        {
            var root = new Item("Root");

            var a = new Item("A");
            var aa = new Item("AA");
            var ab = new Item("AB");
            var aba = new Item("ABA");
            var abb = new Item("ABB");

            ab.AddChild(abb);
            ab.AddChild(aba);
            a.AddChild(ab);
            a.AddChild(aa);
            root.AddChild(a);

            Assert.IsTrue(root.IsAncestorOf(a));
            Assert.IsTrue(root.IsAncestorOf(aa));
            Assert.IsTrue(root.IsAncestorOf(ab));
            Assert.IsTrue(root.IsAncestorOf(aba));
            Assert.IsTrue(root.IsAncestorOf(abb));

            Assert.IsTrue(a.IsDescendantOf(root));
            Assert.IsTrue(aa.IsDescendantOf(root));
            Assert.IsTrue(ab.IsDescendantOf(root));
            Assert.IsTrue(aba.IsDescendantOf(root));
            Assert.IsTrue(abb.IsDescendantOf(root));

            Assert.IsTrue(aa.IsSiblingOf(ab));
            Assert.IsTrue(ab.IsSiblingOf(aa));

            Assert.IsTrue(aba.IsSiblingOf(abb));
            Assert.IsTrue(abb.IsSiblingOf(aba));

            Assert.IsTrue(ab.IsAncestorOf(aba));
            Assert.IsTrue(ab.IsAncestorOf(abb));
            Assert.IsTrue(aba.IsDescendantOf(ab));
            Assert.IsTrue(abb.IsDescendantOf(ab));

            Assert.IsFalse(aba.IsAncestorOf(abb));
            Assert.IsFalse(abb.IsAncestorOf(aba));

            Assert.IsFalse(abb.IsDescendantOf(aba));
            Assert.IsFalse(aba.IsDescendantOf(abb));

            Assert.IsFalse(aa.IsAncestorOf(aba));
            Assert.IsFalse(aa.IsAncestorOf(abb));
            Assert.IsFalse(aba.IsDescendantOf(aa));
            Assert.IsFalse(abb.IsDescendantOf(aa));
        }
    }
}
