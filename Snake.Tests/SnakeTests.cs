using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Snake.Tests
{
    [TestClass]
    public class SnakeTests
    {
        [TestMethod]
        public void Propogate()
        {
            Snake snake = new Snake(new Vector(1, 1));
            snake.Parts.Add(new Vector(1,2));
            snake.Parts.Add(new Vector(2, 2));
            snake.Parts.Add(new Vector(2, 3));
            snake.Parts.Add(new Vector(3, 3));

            Snake expected = new Snake(new Vector(1, 1));
            expected.Parts.Add(new Vector(1, 1));
            expected.Parts.Add(new Vector(1, 2));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(2, 3));

            snake.Propogate(snake.Parts.First());

            for (int i = 1; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
           
        }

        [TestMethod]
        public void PropogateFalse()
        {
            Snake snake = new Snake(new Vector(1, 1));
            snake.Parts.Add(new Vector(1, 2));
            snake.Parts.Add(new Vector(2, 2));
            snake.Parts.Add(new Vector(2, 3));
            snake.Parts.Add(new Vector(3, 3));

            Snake expected = new Snake(new Vector(1, 1));
            expected.Parts.Add(new Vector(1, 2));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(2, 3));
            expected.Parts.Add(new Vector(3, 3));

            snake.Propogate(snake.Parts.First());

            for (int i = 1; i < snake.Parts.Count; i++)
            {
                Assert.AreNotEqual(expected.Parts[i], snake.Parts[i]);
            }

        }

        [TestMethod]
        public void MoveRightFromNorth()
        {
            Snake snake = new Snake(new Vector(1, 1));
            snake.Parts.Add(new Vector(1, 2));
            snake.Parts.Add(new Vector(2, 2));
            snake.LastDirection = Directions.North;

            Snake expected = new Snake(new Vector(2, 1));
            expected.Parts.Add(new Vector(1, 1));
            expected.Parts.Add(new Vector(1, 2));
            expected.LastDirection = Directions.East;

            snake.NextMove = Command.Right;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

        [TestMethod]
        public void MoveLeftFromNorth()
        {
            Snake snake = new Snake(new Vector(2, 2));
            snake.Parts.Add(new Vector(2, 3));
            snake.Parts.Add(new Vector(3, 3));
            snake.LastDirection = Directions.North;

            Snake expected = new Snake(new Vector(1, 2));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(2, 3));
            expected.LastDirection = Directions.West;

            snake.NextMove = Command.Left;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

        [TestMethod]
        public void MoveRightFromSouth()
        {
            Snake snake = new Snake(new Vector(2, 2));
            snake.Parts.Add(new Vector(2, 1));
            snake.Parts.Add(new Vector(1, 1));
            snake.LastDirection = Directions.South;

            Snake expected = new Snake(new Vector(1, 2));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(2, 1));
            expected.LastDirection = Directions.West;

            snake.NextMove = Command.Right;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

        [TestMethod]
        public void MoveLeftFromSouth()
        {
            Snake snake = new Snake(new Vector(2, 2));
            snake.Parts.Add(new Vector(2, 1));
            snake.Parts.Add(new Vector(1, 1));
            snake.LastDirection = Directions.South;

            Snake expected = new Snake(new Vector(3, 2));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(2, 1));
            expected.LastDirection = Directions.East;

            snake.NextMove = Command.Left;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

        [TestMethod]
        public void MoveRightFromEast()
        {
            Snake snake = new Snake(new Vector(2, 2));
            snake.Parts.Add(new Vector(1, 2));
            snake.Parts.Add(new Vector(1, 1));
            snake.LastDirection = Directions.East;

            Snake expected = new Snake(new Vector(2, 3));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(1, 2));
            expected.LastDirection = Directions.South;

            snake.NextMove = Command.Right;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

        [TestMethod]
        public void MoveLeftFromEast()
        {
            Snake snake = new Snake(new Vector(2, 2));
            snake.Parts.Add(new Vector(1, 2));
            snake.Parts.Add(new Vector(1, 1));
            snake.LastDirection = Directions.East;

            Snake expected = new Snake(new Vector(2, 1));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(1, 2));
            expected.LastDirection = Directions.North;

            snake.NextMove = Command.Left;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

        [TestMethod]
        public void MoveRightFromWest()
        {
            Snake snake = new Snake(new Vector(2, 2));
            snake.Parts.Add(new Vector(3, 2));
            snake.Parts.Add(new Vector(3, 1));
            snake.LastDirection = Directions.West;

            Snake expected = new Snake(new Vector(2, 1));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(3, 2));
            expected.LastDirection = Directions.North;

            snake.NextMove = Command.Right;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

        [TestMethod]
        public void MoveLeftFromWest()
        {
            Snake snake = new Snake(new Vector(2, 2));
            snake.Parts.Add(new Vector(3, 2));
            snake.Parts.Add(new Vector(3, 1));
            snake.LastDirection = Directions.West;

            Snake expected = new Snake(new Vector(2, 3));
            expected.Parts.Add(new Vector(2, 2));
            expected.Parts.Add(new Vector(3, 2));
            expected.LastDirection = Directions.North;

            snake.NextMove = Command.Left;
            snake.Move();

            for (int i = 0; i < snake.Parts.Count; i++)
            {
                Assert.AreEqual(expected.Parts[i], snake.Parts[i]);
            }
        }

    }
}
