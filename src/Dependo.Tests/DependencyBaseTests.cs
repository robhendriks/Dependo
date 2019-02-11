namespace Dependo.Tests
{
    using System;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Plugins;

    [TestClass]
    public class DependencyBaseTests
    {
        private Plugin _parent;
        private Plugin _childA;
        private Plugin _childB;

        [TestInitialize]
        public void Initialize()
        {
            _parent = new Plugin("Parent");
            _childA = new Plugin("ChildA");
            _childB = new Plugin("ChildB");
            _parent.AddChild(_childA);
            _parent.AddChild(_childB);
        }

        [TestMethod]
        public void HasChild_ReturnsTrue()
        {
            Assert.IsTrue(_parent.HasChild(_childA));
            Assert.IsTrue(_parent.HasChild(_childB));
            Assert.IsTrue(_parent.HasChild(new Plugin("ChildA")));
            Assert.IsTrue(_parent.HasChild(new Plugin("ChildB")));
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
            Assert.ThrowsException<DependencyException>(() => _parent.AddChild(new Plugin("ChildA")));
            Assert.ThrowsException<DependencyException>(() => _parent.AddChild(new Plugin("ChildB")));
        }

        [TestMethod]
        public void TryAddChild_ReturnsTrue_WhenChildIsAdded()
        {
            Assert.IsTrue(_parent.TryAddChild(new Plugin("ChildC")));
            Assert.IsTrue(_parent.TryAddChild(new Plugin("ChildD")));
        }

        [TestMethod]
        public void TryAddChild_ReturnsFalse_WhenChildWasNotAdded()
        {
            Assert.IsFalse(_parent.TryAddChild(_childA));
            Assert.IsFalse(_parent.TryAddChild(_childB));
            Assert.IsFalse(_parent.TryAddChild(new Plugin("ChildA")));
            Assert.IsFalse(_parent.TryAddChild(new Plugin("ChildB")));
        }

        [TestMethod]
        public void RemoveChild_Throws_WhenChildIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _parent.RemoveChild(null));
        }

        [TestMethod]
        public void RemoveChild_Throws_WhenChildIsNotPresent()
        {
            Assert.ThrowsException<DependencyException>(() => _parent.RemoveChild(new Plugin("ChildC")));
            Assert.ThrowsException<DependencyException>(() => _parent.RemoveChild(new Plugin("ChildD")));
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
            Assert.IsFalse(_parent.TryRemoveChild(new Plugin("ChildC")));
            Assert.IsFalse(_parent.TryRemoveChild(new Plugin("ChildD")));
        }

        [TestMethod]
        public void HasChild_ReturnsFalse()
        {
            Assert.IsFalse(_parent.HasChild(new Plugin("ChildC")));
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
            Assert.IsFalse(new Plugin("ChildA").IsChildOf(_parent));
            Assert.IsFalse(new Plugin("ChildB").IsChildOf(_parent));
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
            Assert.IsFalse(_parent.IsParentOf(new Plugin("ChildA")));
            Assert.IsFalse(_parent.IsParentOf(new Plugin("ChildB")));
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
            Assert.IsFalse(_childA.IsSiblingOf(new Plugin("ChildB")));
            Assert.IsFalse(_childB.IsSiblingOf(new Plugin("ChildA")));
        }
    }
}
