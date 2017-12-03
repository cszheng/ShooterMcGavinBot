using System;
using NUnit.Framework;
using Moq;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Modules;
using ShooterMcGavinBot.Services;

namespace Tests.Main
{
    [TestFixture]
    public class BotServiceTests : ServicesTestsBase
    {
       
        [Test]
        public void CommandModuleAttributesExist()
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
        public void CommandModuleAttributesNotExist()
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