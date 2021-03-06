using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
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
        
        [SetUp]
        public void SetUp()
        {   
            //Create shooter quotes
            _shooterQuotes = new Dictionary<string, string>();
            _shooterQuotes.Add("quote_0", "Shooter quote");
            //mock botstrings object
            _mockBotStrings = new Mock<IBotStrings>();
            _mockBotStrings.Setup(x => x.Container)
                           .Returns(_shooterQuotes);            
        }
        
        [Test]
        public async Task ShooterRoastWithoutMention()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns("creatoruser");
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object); 
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //create the service
            ShooterService sutShooterService = new ShooterService(mockConfig.Object, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, null));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo("@here Shooter quote"));                 
        }

        [Test]
        public async Task ShooterNonCreatorRoasNonCreator()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns("creatoruser");
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object);   
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //bot user's id
            mockCmdCntx.Setup(x => x.Client.CurrentUser.Id).Returns(1);
            //message user's username
            mockCmdCntx.Setup(x => x.User.Username).Returns("messageuser");
            //mock the user
            Mock<IUser> mockUser = new Mock<IUser>();
            mockUser.Setup(x => x.Mention).Returns("@someuser");
            //roast user's id
            mockUser.Setup(x => x.Id).Returns(2);
            //create the service
            ShooterService sutShooterService = new ShooterService(mockConfig.Object, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, mockUser.Object));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo("@someuser Shooter quote"));    
        }

        [Test]
        public async Task ShooterNonCreatorRoastCreator()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns("creatoruser");
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object);   
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //bot user's id
            mockCmdCntx.Setup(x => x.Client.CurrentUser.Id).Returns(1);
            //message user's username
            mockCmdCntx.Setup(x => x.User.Username).Returns("messageuser");
            //mnessage user's mention
             mockCmdCntx.Setup(x => x.User.Mention).Returns("@messageuser");
            //mock the user
            Mock<IUser> mockUser = new Mock<IUser>();
            //roast user's id
            mockUser.Setup(x => x.Id).Returns(2);
            mockUser.Setup(x => x.Username).Returns("creatoruser");
            //create the service
            ShooterService sutShooterService = new ShooterService(mockConfig.Object, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, mockUser.Object));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo("@messageuser Shooter quote"));    
        }

        [Test]
        public async Task ShooterNonCreatorRoastBot()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns("creatoruser");
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object);   
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //bot user's id
            mockCmdCntx.Setup(x => x.Client.CurrentUser.Id).Returns(1);
            //mnessage user's mention
            mockCmdCntx.Setup(x => x.User.Mention).Returns("@messageuser");
            //mock the user
            Mock<IUser> mockUser = new Mock<IUser>();
            //roast user's id
            mockUser.Setup(x => x.Id).Returns(1);
            //create the service
            ShooterService sutShooterService = new ShooterService(mockConfig.Object, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, mockUser.Object));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo("@messageuser Shooter quote"));    
        }

        [Test]
        public async Task ShooterCreatorRoastNonBot()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns("creatoruser");
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object);   
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //bot user's id
            mockCmdCntx.Setup(x => x.Client.CurrentUser.Id).Returns(1);
            //mnessage user's username
            mockCmdCntx.Setup(x => x.User.Username).Returns("creatoruser");
            //mock the user
            Mock<IUser> mockUser = new Mock<IUser>();
            //roast user's id
            mockUser.Setup(x => x.Id).Returns(2);
            //roast user's mention
            mockUser.Setup(x => x.Mention).Returns("@someuser");
            //create the service
            ShooterService sutShooterService = new ShooterService(mockConfig.Object, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, mockUser.Object));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo("@someuser Shooter quote"));    
        }

        [Test]
        public async Task ShooterCreatorRoastBot()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns("creatoruser");
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object);   
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = w;
                            return Task.FromResult<IUserMessage>(null);
                        });
            //bot user's id
            mockCmdCntx.Setup(x => x.Client.CurrentUser.Id).Returns(1);
            //mnessage user's username
            mockCmdCntx.Setup(x => x.User.Username).Returns("creatoruser");
            //mock the user
            Mock<IUser> mockUser = new Mock<IUser>();
            //roast user's id
            mockUser.Setup(x => x.Id).Returns(1);
            //create the service
            ShooterService sutShooterService = new ShooterService(mockConfig.Object, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.roast(mockCmdCntx.Object, mockUser.Object));               
            //ASSERT
            Assert.That(cntxMsg, Is.EqualTo("@here Shooter quote"));    
        }

        [Test]
        public async Task ShooterPewPewUrl()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxTtl = "";
            string cntxImgUrl = "";
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns("creatoruser");            
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getContainer("shooter"))
                               .Returns(_mockBotStrings.Object);
            mockBotStringsCntr.Setup(x => x.getString("shooter", "pewpewtitle"))
                              .Returns("PewPewTitle");
            mockBotStringsCntr.Setup(x => x.getString("shooter", "pewpewurl"))
                              .Returns("https://someurl/pewpewimg.png");
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxTtl = y.Title;
                            cntxImgUrl = y.Image.ToString();
                            return Task.FromResult<IUserMessage>(null);
                        });
            //create the service
            ShooterService sutShooterService = new ShooterService(mockConfig.Object, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutShooterService.pewpew(mockCmdCntx.Object));
            //ASSERT
            Assert.That(cntxTtl, Is.EqualTo("PewPewTitle"));
            Assert.That(cntxImgUrl, Is.EqualTo("https://someurl/pewpewimg.png"));
        }
    }
}