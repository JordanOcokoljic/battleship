using State;

namespace Demo
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            System.Console.Out.WriteLine("[Creating Player & Ship, and setting up the Board]");
            var jordan = Player.New("Jordan");
            var ship = Ship.New(Ship.Kind.Destroyer);
            jordan.Board.AddShip(2, 4, Board.Direction.West, ship);
            System.Console.Out.WriteLine($"Are all of {jordan.Name}'s ships sunk? {jordan.Board.AllSunk}");
            
            System.Console.Out.WriteLine("[Launching Attacks]");
            var hit = jordan.Board.Attack(3, 4);
            System.Console.Out.WriteLine($"Outcome of the attack on (3, 4) was {hit}");
            
            hit = jordan.Board.Attack(3, 4);
            System.Console.Out.WriteLine($"Outcome of the attack on (3, 4) was {hit}");
            
            hit = jordan.Board.Attack(6, 4);
            System.Console.Out.WriteLine($"Outcome of the attack on (6, 4) was {hit}");
            
            System.Console.Out.WriteLine("[Quickly finishing off the ship]");
            jordan.Board.Attack(2, 4);
            jordan.Board.Attack(4, 4);
            System.Console.Out.WriteLine($"Are all of {jordan.Name}'s ships sunk? {jordan.Board.AllSunk}");
        }
    }
}