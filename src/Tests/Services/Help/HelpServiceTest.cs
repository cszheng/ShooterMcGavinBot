using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Moq;
using ShooterMcGavinBot.Main;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Modules;
using ShooterMcGavinBot.Services;

namespace Tests.Main
{
    [TestFixture]
    public class HelpServiceTests : TestsBase
    {
        private Mock<IBotStringsContainer> _mockBotStringsCntr;
        private Dictionary<String, String> _commonBotStrings;
        private Assembly _projectAssembly;
        
        [SetUp]
        public void SetUp()
        {
            //mock the common botstrings
            _commonBotStrings = new Dictionary<String, String>();
            _commonBotStrings.Add("section_breaks", "**--------------------------------------------------------------------------------**");
            _commonBotStrings.Add("command_header", "__**Commands:**__");
            _commonBotStrings.Add("command_description", "**{0}** - *{1}*");
            _commonBotStrings.Add("method_description", "**{0}** - *{1}*");
            _commonBotStrings.Add("parameter_description", "    __{0}__ - *{1}*");
            //make mock object           
            _mockBotStringsCntr = new Mock<IBotStringsContainer>();
            _mockBotStringsCntr.Setup(x => x.getString("common", It.IsAny<String>()))
                               .Returns((String x, String y) => { return _commonBotStrings[y]; });
            //get assembly of the project class
            _projectAssembly = typeof(Program).Assembly;
        }

        [Test]
        public void CommandsInNamespaceExist()
        {
            //ARRANGE
            var sutHelpService = new HelpService(_projectAssembly, _mockBotStringsCntr.Object);
            //ACT
            var embedObj = sutHelpService.help(typeof(HelpModule));                   
            //ASSERT
            //get the description and split by newline
            var embedDescLst = embedObj.Description.Split("\n");
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
            var sutHelpService = new HelpService(_projectAssembly, _mockBotStringsCntr.Object);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutHelpService.help(typeof(HelpServiceTests)); });
            //ASSERT      
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("No command modules in assembly or namespace"));
        }
    }
}