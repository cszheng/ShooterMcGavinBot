using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Services;

namespace Tests.Main
{
    [TestFixture]
    public class ShooterServiceTests : ServicesTestsBase
    {
        private Dictionary<string, string> _shooterQuotes;
        private Mock<IBotStrings> _mockBotStrings;


        public ShooterServiceTests()
        {   
            //mock some quotes
            _shooterQuotes = new Dictionary<string, string>();
            _shooterQuotes.Add("quote_0", "Shooter quote");
            //mock botstrings object
            _mockBotStrings = new Mock<IBotStrings>();
            _mockBotStrings.Setup(x => x.Container)
                           .Returns(_shooterQuotes);
            //make mock object           
            _mockBotStringsCntr = new Mock<IBotStringsContainer>();
            _mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object);
        }

        [Test]
        public void ShooterRoastWithoutMention()
        {
            //ARRANGE
            ShooterService sutShooterService = new ShooterService(_mockBotStringsCntr.Object);
            //ACT
            string roastString = sutShooterService.roast();                   
            //ASSERT
            Assert.That(roastString, Is.EqualTo($"@here Shooter quote"));                 
        }

        [Test]
        public void ShooterRoastWithMention()
        {
            //ARRANGE
            ShooterService sutShooterService = new ShooterService(_mockBotStringsCntr.Object);
            //ACT
            string roastString = sutShooterService.roast("@someuser");                   
            //ASSERT
            string quoteStr =_mockBotStringsCntr.Object.getString("shooter", "");
            Assert.That(roastString, Is.EqualTo($"@someuser Shooter quote"));    
        }
    }
}