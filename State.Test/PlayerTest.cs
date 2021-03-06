using NUnit.Framework;

namespace State.Test
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void NameReturnsTheNameAssociatedWithThePlayer()
        {
            var player = new Player("Jordan", new Board());
            Assert.That(player.Name, Is.EqualTo("Jordan"));
        }

        [Test]
        public void BoardReturnsTheBoardAssociatedWithThePlayer()
        {
            var board = new Board();
            var player = new Player("Jordan", board);
            Assert.That(player.Board, Is.SameAs(board));
        }

        [Test]
        public void FactoryMethodCreatesPlayerCorrectly()
        {
            var player = Player.New("Jordan");
            Assert.That(player.Name, Is.EqualTo("Jordan"));
            Assert.That(player.Board, Is.Not.Null);
        }
    }
}