using System;
using NUnit.Framework;

namespace State.Test
{
    [TestFixture]
    public class ShipTest
    {
        [Test]
        public void ConstructorThrowsIfLengthIsTooSmall()
        {
            var ex = Assert.Throws<ArgumentException>(() => _ = new Ship(0));
            Assert.That(ex?.Message, Is.EqualTo("Ship length must be greater than or equal to 1"));
        }

        [Test]
        public void ConstructorThrowsIfLengthIsTooGreat()
        {
            var ex = Assert.Throws<ArgumentException>(() => _ = new Ship(11));
            Assert.That(ex?.Message, Is.EqualTo("Ship length must be less than or equal to 10"));
        }

        [Test]
        public void HitThrowsIfSegmentIsTooGreat()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Ship(2).Hit(3));
            
            Assert.That(ex?.Message, Is.EqualTo("Hit segment must be less than or equal to the ship's length"));
        }

        [Test]
        public void HitThrowsIfSegmentIsTooSmall()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Ship(2).Hit(0));

            Assert.That(ex?.Message, Is.EqualTo("Hit segment must be greater than 0"));
        }
        
        [Test]
        public void LengthReturnsTheProvidedValue()
        {
            const int length = 3;
            var ship = new Ship(3);
            Assert.That(ship.Length, Is.EqualTo(length));
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