using System;

namespace WizardLore
{
    public static class Serialization
    {
        /// <summary>
        /// Serialize the given board and player
        /// </summary>
        /// <param name="board"> The game board </param>
        /// <param name="path"> The path to the output file </param>
        /// <param name="currentPlayer"> The enum member representing the player who will play next turn </param>
        public static void Serialize(Board board, string path, Team currentPlayer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a board from the given file
        /// </summary>
        /// <param name="path"> Path to the file </param>
        /// <param name="startingPlayer"> The enum member representing the player who will play next turn </param>
        public static Board Deserialize(string path, out Team startingPlayer)
        {
            throw new NotImplementedException();
        }
    }
}