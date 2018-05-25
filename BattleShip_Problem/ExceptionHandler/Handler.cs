using Game.IO;
using System;

namespace Game.ExceptionHandler
{
    public sealed class Handler
    {
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="throwAndTerminateApp">if set to <c>true</c> [throw and terminate application].</param>
        /// <exception cref="System.Exception"></exception>
        public static void HandleException(string message, bool throwAndTerminateApp)
        {
            if (throwAndTerminateApp)
                throw new Exception(message);
            else
            {
                Write.WriteToConsole(new Model.ErrorResponse() { Message = message });
            }
        }
    }
}
