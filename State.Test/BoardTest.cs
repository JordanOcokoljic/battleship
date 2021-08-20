using System;
using NUnit.Framework;

namespace State.Test
{
    [TestFixture]
    public class BoardTest
    {
        [TestCase(-1, 0, Board.Direction.North, "Provided x coordinate was less than 0")]
        [TestCase(11, 0, Board.Direction.North, "Provided x coordinate was greater than or equal to 10")]
        [TestCase(0, -1, Board.Direction.North, "Provided y coordinate was less than 0")]
        [TestCase(0, 11, Board.Direction.North, "Provided y coordinate was greater than or equal to 10")]
        [TestCase(0, 9, Board.Direction.North, "Provided coordinates are invalid for the given ship")]
        [TestCase(2, 0, Board.Direction.East, "Provided coordinates are invalid for the given ship")]
        [TestCase(0, 2, Board.Direction.South, "Provided coordinates are invalid for the given ship")]
        [TestCase(9, 0, Board.Direction.West, "Provided coordinates are invalid for the given ship")]
        [TestCase(8, 7, Board.Direction.East, "Provided coordinates and ship would collide with another ship")]
        public void AddShipThrowsCorrectly(int x, int y, Board.Direction direction, string expected)
        {
            var board = new Board();
            board.AddShip(7, 7, Board.Direction.North, new Ship(2));

            var ex = Assert.Throws<ArgumentException>(() => board.AddShip(x, y, direction, new Ship(3)));
            Assert.That(ex?.Message, Is.EqualTo(expected));
        }

        [TestCase(3, 3, false)]
        [TestCase(4, 4, true)]
        [TestCase(4, 3, true)]
        [TestCase(4, 2, false)]
        public void AttackDamagesShip(int x, int y, bool hits)
        {
            var board = new Board();
            var ship = new Ship(3);
            ship.Hit(2);
            board.AddShip(4, 4, Board.Direction.South, ship);

            var hit = board.Attack(x, y);
            Assert.That(hit, Is.EqualTo(hits));
        }

        [Test]
        public void BoardCorrectlyReportsWhenAllShipsAreSunk()
        {
            var ship = new Ship(2);
            var board = new Board();

            board.AddShip(0, 0, Board.Direction.North, ship);
            Assert.That(board.AllSunk, Is.False);

            ship.Hit(0);
            ship.Hit(1);
            Assert.That(board.AllSunk, Is.True);
        }
    }
}