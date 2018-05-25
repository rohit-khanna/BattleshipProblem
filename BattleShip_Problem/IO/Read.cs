using System;
using System.Collections.Generic;
using System.IO;

namespace Game.IO
{
    /// <summary>
    /// This class will be used to provide READ functioanlity.
    /// </summary>
    public class ReadTextFile : IRead
    {
        #region Interface Members

        /// <summary>
        /// Method to read the Input File and return it as List of strings
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// File Contents as List of string
        /// </returns>
        /// <exception cref="FileNotFoundException">Input File Not Found Exception</exception>
        public IEnumerable<string> Read(string filePath)
        {
            var linesOfFile = new List<string>();

            if (IsFilepathExistant(filePath))
            {
                linesOfFile.AddRange(File.ReadLines(filePath));
            }
            else
            {
                throw new FileNotFoundException("Input File path doesnot exists in system. Terminating !");
            }
            return linesOfFile;
        }


        #endregion


        /// <summary>
        /// Determines whether [filepath exists] 
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if [filepath existant] ; otherwise, <c>false</c>.
        /// </returns>
        private bool IsFilepathExistant(string filePath)
        {
            return File.Exists(filePath);
        }

        // TODO
        public void Dispose()
        {
            GC.Collect();
        }

    }
}
