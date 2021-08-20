using System;
using System.Collections.Generic;

namespace State
{
    /// <summary>
    ///     Class <c>Ship</c> represents a ship in a game of Battleship.
    /// </summary>
    public class Ship
    {
        /// <summary>
        ///     The "segments" of the ship that have been marked as destroyed, from the origin of the ship on the grid.
        /// </summary>
        private readonly List<int> _destroyed;

        /// <summary>
        ///     The length of the ship.
        /// </summary>
        public readonly int Length;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Ship" /> class.
        /// </summary>
        /// <param name="length">
        ///     The <see cref="Length" />, and by extension the hit points, of the <see cref="Ship" />.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     The provided length was greater than ten, or less than one.
        /// </exception>
        public Ship(int length)
        {
            Length = length switch
            {
                < 1 => throw new ArgumentException("Ship length must be greater than or equal to 1"),
                > 10 => throw new ArgumentException("Ship length must be less than or equal to 10"),
                _ => length
            };

            _destroyed = new List<int>();
        }

        /// <summary>
        ///     Indicates if the <see cref="Ship" /> has been sunk.
        ///     A ship is considered sunk if the number of hit segments equals the length of the ship.
        /// </summary>
        public bool IsSunk => _destroyed.Count == Length;

        /// <summary>
        ///     Marks a portion of the <see cref="Ship" /> as destroyed by an attack.
        /// </summary>
        /// <param name="segment">
        ///     The part of the <see cref="Ship" /> to hit.
        /// </param>
        /// <returns>
        ///     True if a new segment has been destroyed, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The provided segment was greater than the <see cref="Length" /> of the ship, or less than zero.
        /// </exception>
        public bool Hit(int segment)
        {
            if (segment >= Length) throw new ArgumentException("Hit segment must be less than the ship's length");

            if (segment < 0) throw new ArgumentException("Hit segment cannot be less than 0");

            if (_destroyed.Contains(segment)) return false;

            _destroyed.Add(segment);
            return true;
        }
    }
}