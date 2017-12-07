using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Services;

namespace Tests.Main
{
    [TestFixture]
    public class ShooterServiceTests : TestsBase
    {
        private Dictionary<string, string> _shooterQuotes;        
        private Mock<IBotStrings> _mockBotStrings;
        private Mock<IBotStringsContainer> _mockBotStringsCntr;

        [SetUp]
        public void SetUp()
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
        public async Task ShooterRoastWithoutMention()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";  
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //create the service
            ShooterService sutShooterService = new ShooterService(_mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, null));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo($"@here Shooter quote"));                 
        }

        [Test]
        public async Task ShooterRoastWithMention()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";  
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //mock the user
            Mock<IUser> mockUser = new Mock<IUser>();
            mockUser.Setup(x => x.Mention).Returns("@someuser");
            //create the service
            ShooterService sutShooterService = new ShooterService(_mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, mockUser.Object));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo($"@someuser Shooter quote"));    
        }
    }
}