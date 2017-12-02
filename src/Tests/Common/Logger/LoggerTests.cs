using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Discord;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    [TestFixture]
    public class LoggerTests : TestsBase
    {
        [Test]
        public void LogMessageSync()
        {   
            //ARRANGE
            ILogger sutLogger = new Logger();
            String logMessage = "Testing message to log sync";            
            //ACT
            sutLogger.Log(logMessage);
            String loggerOutput = GetConsoleOutput();
            //ASSERT
            Assert.That(loggerOutput, Is.EqualTo(logMessage));
        }

        [Test]
        public async Task LogMessageAsync()
        {
            //ARRANGE 
            ILogger sutLogger = new Logger();
            String logMessage = "Testing message to log async";               
            LogMessage logMessageObj = new LogMessage(LogSeverity.Info, "Message Source", logMessage);           
            //ACT
            await Task.Run(() => sutLogger.Log(logMessage));
            String loggerOutput = GetConsoleOutput();
            //ASSERT
            Assert.That(loggerOutput, Is.EqualTo(logMessage));
        }
    }
}