using System;
using System.IO;
using NUnit.Framework;

namespace Tests.Main
{
    public class TestsBase
    {
        private TextWriter _origConsoleStream;
        private StringWriter _consoleStream; 

        protected String _testDir;
        protected String _projectDir;

        public TestsBase() 
        {
            //project directory, where the .csproj is at
            _testDir = "../../..";
            _projectDir = $"{_testDir}/../ShooterMcGavinBot";
            //redirect console output
            _origConsoleStream = Console.Out; 
            _consoleStream = new StringWriter();
            Console.SetOut(_consoleStream);
        }

        ~TestsBase() 
        {
            Console.SetOut(_origConsoleStream);
        }

        protected string GetConsoleOutput() 
        {
            _consoleStream.Flush();
            String result = _consoleStream.GetStringBuilder().ToString().TrimEnd('\r', '\n');
            _consoleStream.GetStringBuilder().Clear();
            return result;
        }
    }
}
