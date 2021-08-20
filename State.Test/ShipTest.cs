using System;
using NUnit.Framework;

namespace State.Test
{
    [TestFixture]
    public class ShipTest
    {
        [TestCase(0, "Ship length must be greater than or equal to 1")]
        [TestCase(11, "Ship length must be less than or equal to 10")]
        public void ConstructorThrowsCorrectly(int length, string expected)
        {
            var ex = Assert.Throws<ArgumentException>(() => _ = new Ship(length));
            Assert.That(ex?.Message, Is.EqualTo(expected));
        }

        [TestCase(3, "Hit segment must be less than the ship's length")]
        [TestCase(-1, "Hit segment cannot be less than 0")]
        public void HitThrowsCorrectly(int segment, string expected)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Ship(2).Hit(segment));
            Assert.That(ex?.Message, Is.EqualTo(expected));
        }

        [Test]
        public void LengthReturnsTheProvidedValue()
        {
            var ship = new Ship(3);
            Assert.That(ship.Length, Is.EqualTo(3));
        }

        [Test]
        public void HitReturnsCorrectValue()
        {
            var ship = new Ship(4);
            Assert.That(ship.Hit(1), Is.True);
            Assert.That(ship.Hit(1), Is.False);
        }

        [Test]
        public void IsSunkIndicatesWhenAShipIsSunk()
        {
            var ship = new Ship(2);
            
            ship.Hit(1);
            Assert.That(ship.IsSunk, Is.False);

            ship.Hit(2);
            Assert.That(ship.IsSunk, Is.True);
        }
    }
}