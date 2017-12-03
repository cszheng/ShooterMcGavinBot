using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Moq;
using Discord;
using ShooterMcGavinBot.Main;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Modules;
using ShooterMcGavinBot.Services;

namespace Tests.Main
{
    [TestFixture]
    public class HelpServiceTests : ServicesTestsBase
    {
        private Assembly _projectAssembly;

        public HelpServiceTests()
        {
            _projectAssembly = typeof(Program).Assembly;
        }

        [Test]
        public void CommandsInNamespaceExist()
        {
            //ARRANGE
            HelpService sutHelpService = new HelpService(_projectAssembly, _mockBotStringsCntr.Object);
            //ACT
            Embed embedObj = sutHelpService.help(typeof(HelpModule));                   
            //ASSERT
            //get the description and split by newline
            string[] embedDescLst = embedObj.Description.Split("\n");
            //should have the module's method attributes text
            Assert.That(embedDescLst.Length, Is.GreaterThanOrEqualTo(3));
            Assert.That(embedDescLst[0].Contains("Commands:"), Is.EqualTo(true));
            Assert.That(embedDescLst[1].Contains("help"), Is.EqualTo(true));
            Assert.That(embedDescLst[2].Contains("shooter"), Is.EqualTo(true));                     
        }

        [Test]
        public void CommandsInNamespaceNotExist()
        {
            //ARRANGE
            HelpService sutHelpService = new HelpService(_projectAssembly, _mockBotStringsCntr.Object);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutHelpService.help(typeof(HelpServiceTests)); });
            //ASSERT      
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("No command modules in assembly or namespace"));
        }
    }
}