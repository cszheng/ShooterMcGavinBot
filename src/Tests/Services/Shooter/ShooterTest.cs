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
    public class ShooterServiceTests : ServicesTestsBase
    {
        private BotStrings _mockShooterQuotes;

        public ShooterServiceTests()
        {   
            _mockShooterQuotes = new BotStrings($"{_testDir}/_testfiles/ShooterQUoteFiles/test.json");
            //make mock object           
            _mockBotStringsCntr = new Mock<IBotStringsContainer>();
            _mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockShooterQuotes);
        }

        [Test]
        public void ShooterRoastWithoutMention()
        {
            //ARRANGE
            var sutShooterService = new ShooterService(_mockBotStringsCntr.Object);
            //ACT
            var roastString = sutShooterService.roast();                   
            //ASSERT
            Assert.That(roastString, Is.EqualTo($"@here Shooter quote"));                 
        }

        [Test]
        public void ShooterRoastWithMention()
        {
            //ARRANGE
            var sutShooterService = new ShooterService(_mockBotStringsCntr.Object);
            //ACT
            var roastString = sutShooterService.roast("@someuser");                   
            //ASSERT
            var quoteStr =_mockBotStringsCntr.Object.getString("shooter", "");
            Assert.That(roastString, Is.EqualTo($"@someuser Shooter quote"));    
        }
    }
}