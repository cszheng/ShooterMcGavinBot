using NUnit.Framework;
using Moq;
using ShooterMcGavinBot.Common;
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