namespace State
{
    /// <summary>
    ///     Class <c>Player</c> represents a player in a game of Battleship.
    /// </summary>
    public class Player
    {
        /// <summary>
        ///     Easier method for creating a <see cref="Player"/> creates the board itself.
        /// </summary>
        /// <param name="name">
        ///     The name of the player.
        /// </param>
        /// <returns>
        ///     The fully initialized <see cref="Player"/> instance.
        /// </returns>
        public static Player New(string name) => new Player(name, new Board());
        
        /// <summary>
        ///     The <see cref="Board" /> associated with this <see cref="Player" />.
        /// </summary>
        public readonly Board Board;

        /// <summary>
        ///     The in game name of the player.
        /// </summary>
        public readonly string Name;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="name">
        ///     The in game name the player has chosen to use.
        /// </param>
        /// <param name="board">
        ///     The <see cref="Board" /> the player controls.
        /// </param>
        public Player(string name, Board board)
        {
            Name = name;
            Board = board;
        }
    }
}