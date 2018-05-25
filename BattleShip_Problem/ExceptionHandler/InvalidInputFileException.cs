using System;

namespace Game.ExceptionHandler
{
    [Serializable]
    public class InvalidInputFileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputFileException"/> class.
        /// <para>Invoke this when File at the path is empty</para>
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public InvalidInputFileException(string filePath)
        : base(string.Format("Input File located at:  {0} is Empty!", filePath))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputFileException"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="message">The message.</param>
        public InvalidInputFileException(string filePath, string message)
        : base(string.Format("Input File located at:  {0} has : {1} !", filePath, message))
        {

        }
    }
}
