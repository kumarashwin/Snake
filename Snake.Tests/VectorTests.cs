using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake;

namespace Snake.Tests
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void AddVector1()
        {
            Vector vector = new Vector(1, 1);
           
            var expected = new Vector(3, 4);
            vector.Add(new Vector(2,3));

            Assert.AreEqual(
                expected: expected.X,
                actual: vector.X);

            Assert.AreEqual(
                expected: expected.Y,
                actual: vector.Y);
        }

        [TestMethod]
        public void AddVector2()
        {
            Vector vector = new Vector(4, -1);

            var expected = new Vector(-5, 3);
            vector.Add(new Vector(-9, 4));

            Assert.AreEqual(
                expected: expected.X,
                actual: vector.X);

            Assert.AreEqual(
                expected: expected.Y,
                actual: vector.Y);
        }

        [TestMethod]
        public void EqualsAnotherVector()
        {
            var vector1 = new Vector(1, 3);
            var vector2 = new Vector(4, -3);
            var vector3 = new Vector(1, 3);
            var vector4 = new Vector(4, -3);
            var vector5 = new Vector(-3, 4);

            Assert.IsTrue(vector1.Equals(vector3));
            Assert.IsTrue(vector4.Equals(vector2));
            Assert.IsFalse(vector1.Equals(vector2));
            Assert.IsFalse(vector5.Equals(vector2));
        }

        [TestMethod]
        public void VectorToString()
        {
            Vector vector = new Vector(1, 2, "test");
            Assert.AreEqual(
                expected: "test - X: 1, Y: 2",
                actual: vector.ToString());
        }
    }
}
