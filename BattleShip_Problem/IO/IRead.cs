using System;
using System.Collections.Generic;

namespace Game.IO
{
    interface IRead : IDisposable
    {


        /// <summary>
        /// Method to read the Input File and return it as List of strings
        /// </summary>
        /// <returns>IEnumerable List</returns>
        /// <exception cref="FileNotFoundException">Input File Not Found Exception</exception>
        IEnumerable<string> Read(string filePath);

    }
}
