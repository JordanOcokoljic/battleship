using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace State
{
    /// <summary>
    ///     Class <c>Board</c> represents a board in a game of Battleship.
    /// </summary>
    public class Board
    {
        /// <summary>
        ///     Represents the cardinal directions in which a <see cref="Ship" /> can face on the board.
        /// </summary>
        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        /// <summary>
        ///     The <see cref="Ship" />s that have been added to the board.
        /// </summary>
        private readonly Dictionary<(int x, int y, Direction direction), Ship> _ships;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Board" /> class.
        /// </summary>
        public Board()
        {
            _ships = new Dictionary<(int x, int y, Direction direction), Ship>();
        }

        /// <summary>
        ///     Indicates if all the <see cref="Ship" />s on the board have been sunk.
        /// </summary>
        public bool AllSunk => _ships.All(item => item.Value.IsSunk);

        /// <summary>
        ///     Gets the <see cref="Ship" /> at the provided location.
        /// </summary>
        /// <param name="x">
        ///     The <c>x</c> coordinate on the board to get a ship from.
        /// </param>
        /// <param name="y">
        ///     The <c>y</c> coordinate on the board to get a ship from.
        /// </param>
        /// <returns>
        ///     The <see cref="Ship" /> at the given location, or null if no ship is located on that tile.
        /// </returns>
        private Ship GetShipAtLocation(int x, int y)
        {
            foreach (var ((shipX, shipY, direction), ship) in _ships)
            {
                var (minX, maxX) = direction switch
                {
                    Direction.East => (shipX - ship.Length, shipX),
                    Direction.West => (shipX, shipX + ship.Length),
                    _ => (shipX, shipX)
                };

                var (minY, maxY) = direction switch
                {
                    Direction.North => (shipY, shipY + ship.Length),
                    Direction.South => (shipY - ship.Length, shipY),
                    _ => (shipY, shipY)
                };

                if (!(x >= minX && x <= maxX && y >= minY && y <= maxY)) continue;

                return ship;
            }

            return null;
        }

        /// <summary>
        ///     Returns the origin coordinates for a <see cref="Ship" />.
        /// </summary>
        /// <param name="ship">
        ///     The ship to get the origin coordinates of.
        /// </param>
        /// <returns>
        ///     The origin coordinates of the ship.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The provided <c>ship</c> was not on the board.
        /// </exception>
        private (int x, int y) GetShipOrigin(Ship ship)
        {
            try
            {
                var (position, _) = _ships.Single(item => item.Value == ship);
                return (position.x, position.y);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Given ship was not on the board");
            }
        }

        /// <summary>
        ///     Adds a <see cref="Ship" /> to the board at the given coordinates.
        /// </summary>
        /// <param name="x">
        ///     The x coordinate to place the ship at.
        /// </param>
        /// <param name="y">
        ///     The y coordinate to place the ship at.
        /// </param>
        /// <param name="direction">
        ///     The direction the ship is facing towards on the <see cref="Board" />.
        /// </param>
        /// <param name="ship">
        ///     The <see cref="Ship" /> to add to the board.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Either of the provided positional arguments are less than zero or greater than ten.
        ///     Or the provided coordinates and direction result in part of the <see cref="Ship" /> being off the board.
        /// </exception>
        public void AddShip(int x, int y, Direction direction, Ship ship)
        {
            switch (x)
            {
                case < 0:
                    throw new ArgumentException("Provided x coordinate was less than 0");
                case >= 10:
                    throw new ArgumentException("Provided x coordinate was greater than or equal to 10");
            }

            switch (y)
            {
                case < 0:
                    throw new ArgumentException("Provided y coordinate was less than 0");
                case >= 10:
                    throw new ArgumentException("Provided y coordinate was greater than or equal to 10");
            }

            var invalid = direction switch
            {
                Direction.North => y + ship.Length >= 10,
                Direction.East => x - ship.Length < 0,
                Direction.South => y - ship.Length < 0,
                Direction.West => x + ship.Length >= 10,
                _ => throw new InvalidEnumArgumentException("Unsupported direction")
            };
            
            if (invalid)
                throw new ArgumentException("Provided coordinates are invalid for the given ship");

            for (var i = 0; i < ship.Length; i++)
            {
                var (checkX, checkY) = direction switch
                {
                    Direction.North => (x, y - i),
                    Direction.East => (x - i, y),
                    Direction.South => (x, y + i),
                    Direction.West => (x + i, y),
                    _ => throw new InvalidEnumArgumentException("Unsupported direction")
                };

                if (GetShipAtLocation(checkX, checkY) != null)
                    throw new ArgumentException("Provided coordinates and ship would collide with another ship");
            }

            _ships.Add((x, y, direction), ship);
        }

        /// <summary>
        ///     Registers an attack on the board.
        /// </summary>
        /// <param name="x">
        ///     The <c>x</c> coordinate of the tile on the board being attacked.
        /// </param>
        /// <param name="y">
        ///     The <c>y</c> coordinate of the tile on the board being attacked.
        /// </param>
        /// <returns>
        ///     If the attack resulted in a new hit on a ship.
        /// </returns>
        public bool Attack(int x, int y)
        {
            var ship = GetShipAtLocation(x, y);
            if (ship == null) return false;

            var (hitX, hitY) = GetShipOrigin(ship);
            var segment = Math.Abs(x - hitX + y - hitY);
            return ship.Hit(segment);
        }
    }
}