using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace State
{
    /// <summary>
    ///     Class <c>Ship</c> represents a ship in a game of Battleship.
    /// </summary>
    public class Ship
    {
        /// <summary>
        ///     The ships that are named as part of the 2002 Hasbro printing of the game Battleship.
        /// </summary>
        public enum Kind
        {
            Carrier,
            Battleship,
            Destroyer,
            Submarine,
            PatrolBoat
        }

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
        ///     Factory method for creating new <see cref="Ship" /> instances in accordance with the Hasbro game rules.
        /// </summary>
        /// <param name="kind">
        ///     The <see cref="Ship.Kind" /> of <see cref="Ship" /> to create.
        /// </param>
        /// <returns>
        ///     A <see cref="Ship" /> with a <see cref="Ship.Length" /> determined by the <see cref="Ship.Kind" /> in
        ///     accordance with the Hasbro game rules.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     The provided Kind was unsupported by the factory method.
        /// </exception>
        public static Ship New(Kind kind)
        {
            var length = kind switch
            {
                Kind.Carrier => 5,
                Kind.Battleship => 4,
                Kind.Destroyer => 3,
                Kind.Submarine => 3,
                Kind.PatrolBoat => 2,
                _ => throw new InvalidEnumArgumentException("Unsupported Kind for Ship factory method")
            };

            return new Ship(length);
        }

        /// <summary>
        ///     Marks a portion of the <see cref="Ship" /> as destroyed by an attack.
        /// </summary>
        /// <param name="segment">
        ///     The part of the <see cref="Ship" /> to hit.
        /// </param>
        /// <returns>
        ///     Returns <see cref="AttackOutcome.AlreadyDestroyed" /> if the hit is on an already hit segment, or
        ///     <see cref="AttackOutcome.DestroyedSegment" /> if a new segment has been destroyed.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The provided segment was greater than the <see cref="Length" /> of the ship, or less than zero.
        /// </exception>
        public AttackOutcome Hit(int segment)
        {
            if (segment >= Length) throw new ArgumentException("Hit segment must be less than the ship's length");

            if (segment < 0) throw new ArgumentException("Hit segment cannot be less than 0");

            if (_destroyed.Contains(segment)) return AttackOutcome.AlreadyDestroyed;

            _destroyed.Add(segment);
            return AttackOutcome.DestroyedSegment;
        }
    }
}