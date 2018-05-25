using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Game
{
    class Program
    {
        /// <summary>
        /// Main Method: Gateway to the System
        /// </summary>
        static void Main()
        {
            BattleshipGame _battleshipGame = new Game.BattleshipGame();

            string path = string.IsNullOrWhiteSpace(ReadSetting("input"))
                ? Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"input.txt") :
                ReadSetting("input");

            _battleshipGame.Play(_battleshipGame.PrepareGameSetup(path));
            Console.WriteLine();
        }

        /// <summary>
        /// REad App Settings from App.Config
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>string value</returns>
        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }


    }


}
