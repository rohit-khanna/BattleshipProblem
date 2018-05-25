using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Game.IO.Tests
{
    [TestFixture()]
    public class ReadTextFileTests
    {
        [Test(Description = "Test for Read from a text file")]
        public void ReadTest_Pass()
        {

            //Arrange
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestInputFiles/testInput.txt");
            IEnumerable<string> inputLines = null;

            //Act
            using (ReadTextFile readFile = new ReadTextFile())
            {
                inputLines = readFile.Read(path);

            }

            //Assert
            Assert.True(inputLines != null);
        }

        [Test(Description = "Test for Read from a text file: File Not Found")]
        public void ReadTest_Fail()
        {
            //Arrange
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"input99.txt");
            ActualValueDelegate<object> testDelegate;


            //Act
            using (ReadTextFile readFile = new ReadTextFile())
            {
                testDelegate = () => readFile.Read(path);
            }

            //Assert
            Assert.That(testDelegate, Throws.TypeOf<FileNotFoundException>());

        }
    }
}