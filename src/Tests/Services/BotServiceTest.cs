using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Moq;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Modules;
using ShooterMcGavinBot.Services;

namespace Tests.Main
{
    [TestFixture]
    public class BotServiceTests : TestsBase
    {
        private Mock<IBotStringsContainer> _mockBotStringsCntr;
        private Dictionary<String, String> _commonBotStrings;
        
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
        }

        [Test]
        public void ValidModuleHelpText()
        {
            //ARRANGE
            var sutBotSvc = new BotService(_mockBotStringsCntr.Object);
            //ACT
            var embedObj = sutBotSvc.help(typeof(ShooterModule));            
            //ASSERT
            //should have the module's method attributes text
            Assert.That(embedObj.Description.Contains("Shows options of shooter command."), Is.EqualTo(true));      
            Assert.That(embedObj.Description.Contains("Shooter will roast someone."), Is.EqualTo(true));  
            Assert.That(embedObj.Description.Contains("(optional) The person that Shooter will roast. [@mention]"), Is.EqualTo(true));                      
        }

        [Test]
        public void InvalidModuleHelpText()
        {
            //ARRANGE
            var sutBotSvc = new BotService(_mockBotStringsCntr.Object);
            var typeObj = typeof(HelpModule);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutBotSvc.help(typeObj); });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo($"No commands in type {typeObj.Name}"));
        }
    }
}