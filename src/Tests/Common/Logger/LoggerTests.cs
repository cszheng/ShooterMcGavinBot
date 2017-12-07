using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Discord;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    [TestFixture]
    public class LoggerTests : TestsBase
    {    
        private TextWriter _origConsoleStream;
        private StringWriter _redirConsoleStream;

        [SetUp]
        public void SetUp()
        {
            //redirect console output
            _origConsoleStream = Console.Out;
            _redirConsoleStream = new StringWriter();
            Console.SetOut(_redirConsoleStream);
        }

        [TearDown]
        public void TearDown()
        {   
            //reset console stream
            Console.SetOut(_origConsoleStream);            
        }

        private string GetConsoleOutput() 
        {
            _redirConsoleStream.Flush();
            string result = _redirConsoleStream.GetStringBuilder().ToString().TrimEnd('\r', '\n');
            _redirConsoleStream.GetStringBuilder().Clear();
            return result;
        }

        [Test]
        public void LogMessageSync()
        {   
            //ARRANGE          
            Logger sutLogger = new Logger();
            string logMessage = "Testing message to log sync";            
            //ACT
            sutLogger.Log(logMessage);
            string loggerOutput = GetConsoleOutput();
            //ASSERT
            Assert.That(loggerOutput, Is.EqualTo(logMessage));
        }

        [Test]
        public async Task LogMessageAsync()
        {
            //ARRANGE 
            Logger sutLogger = new Logger();
            string logMessage = "Testing message to log async";               
            LogMessage logMessageObj = new LogMessage(LogSeverity.Info, "Message Source", logMessage);           
            //ACT
            await Task.Run(() => sutLogger.Log(logMessage));
            string loggerOutput = GetConsoleOutput();
            //ASSERT
            Assert.That(loggerOutput, Is.EqualTo(logMessage));
        }
    }
}