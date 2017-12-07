using System;
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
    public class BotServiceTests : TestsBase
    {       
        [Test]
        public async Task CommandModuleAttributesExist()
        {
            //ARRANGE
            //string for capturing command context message
            string cntxMsg = "";          
            //setup common botstrings
            Dictionary<string, string> commonBotStrings = new Dictionary<string, string>();
            commonBotStrings.Add("section_breaks", "**--------------------------------------------------------------------------------**");
            commonBotStrings.Add("command_description", "**{0}** - *{1}*");
            commonBotStrings.Add("method_description", "**{0}** - *{1}*");
            commonBotStrings.Add("parameter_description", "    __{0}__ - *{1}*");
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
            //ARRANGE
            BotService sutBotSvc = new BotService(mockBotStringsCntr.Object);
            //ACT
            await Task.Run(() => sutBotSvc.help(mockCmdCntx.Object, typeof(ShooterModule)));           
            //ASSERT
            //should have the module's method attributes text
            string embedDesc = cntxMsg;
            Assert.That(embedDesc.Contains("Shows options of shooter command."), Is.EqualTo(true));      
            Assert.That(embedDesc.Contains("Shooter will roast someone."), Is.EqualTo(true));  
            Assert.That(embedDesc.Contains("(optional) The person that Shooter will roast. [@mention]"), Is.EqualTo(true));                      
        }

        [Test]
        public void CommandModuleAttributesNotExist()
        {
            //ARRANGE
            //mock botstrings container
            Mock<IBotStringsContainer> mockBotStringsCntr = new Mock<IBotStringsContainer>();
            //mock command context
            Mock<ICommandContext> mockCmdCntx = new Mock<ICommandContext>();
            //no setups on mocks needed because it should fail earlier
            BotService sutBotService = new BotService(mockBotStringsCntr.Object);
            //ACT** Delegated action      
            Type typeObj = typeof(HelpModule);
            AsyncTestDelegate delegatedAct = new AsyncTestDelegate( async () => { 
                await Task.Run(() => sutBotService.help(mockCmdCntx.Object, typeObj));
            });
            //ASSERT      
            BotGeneraicException except = Assert.ThrowsAsync<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo($"No commands in type {typeObj.Name}"));
        }
    }
}