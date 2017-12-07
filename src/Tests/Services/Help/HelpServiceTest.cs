using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class HelpServiceTests: TestsBase
    {
        [Test]
        public async Task CommandsInNamespaceExist()
        {
            //ARRANGE    
            //string for capturing command context message
            string cntxMsg = "";       
            //get project assembly
            Assembly projectAssembly = typeof(ShooterMcGavinBot.Main.Program).Assembly;
            //setup common botstrings
            Dictionary<string, string> commonBotStrings = new Dictionary<string, string>();
            commonBotStrings.Add("command_header", "__**Commands:**__");
            commonBotStrings.Add("command_description", "**{0}** - *{1}*");
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            mockBotStringsCntr.Setup(x => x.getString("common", It.IsAny<string>()))
                               .Returns((string x, string y) => { return commonBotStrings[y]; });
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            mockCmdCntx.Setup(x => x.Channel.SendMessageAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<Embed>(), It.IsAny<RequestOptions>()))
                       .Returns((string w, bool x, Embed y, RequestOptions z) => 
                        {
                            cntxMsg = y.Description;
                            return Task.FromResult<IUserMessage>(null);
                        });
            HelpService sutHelpService = new HelpService(projectAssembly, mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutHelpService.help(mockCmdCntx.Object, typeof(HelpModule)));             
            //ASSERT
            //get the description and split by newline
            string[] embedDescLst = cntxMsg.Split("\n");
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
            //get project assembly
            Assembly projectAssembly = typeof(ShooterMcGavinBot.Main.Program).Assembly;
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            //no setups on mocks needed because it should fail earlier
            HelpService sutHelpService = new HelpService(projectAssembly, mockBotStringsCntr.Object);
            //ACT** Delegated action      
            AsyncTestDelegate delegatedAct = new AsyncTestDelegate( async () => { 
                await Task.Run(() => sutHelpService.help(mockCmdCntx.Object, typeof(ShooterMcGavinBot.Main.Program)));
            });
            //ASSERT      
            BotGeneraicException except = Assert.ThrowsAsync<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("No command modules in assembly or namespace"));
        }
    }
}