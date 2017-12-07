using System.Collections.Generic;
using NUnit.Framework;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    [TestFixture]
    public class BotStringsTest : TestsBase
    {
        [Test]
        public void LoadJsonFileExist()
        {   
            //ARRANGE
            string testJsonPath = $"{_testDir}/_testfiles/BotStringsFiles/test.json";
            BotStrings sutBotStrings = new BotStrings(testJsonPath);     
            //ACT
            Dictionary<string, string> containers = sutBotStrings.Container;           
            //ASSERT
            Assert.That(containers["test1"], Is.EqualTo("Test 1"));
            Assert.That(containers["test2"], Is.EqualTo("Test 2"));
            Assert.That(containers["test3"], Is.EqualTo("Test 3"));
        }

        [Test]
        public void LoadJsonFileNotExist()
        {
            //ARRANGE
            string testJsonPath = $"{_testDir}/_testfiles/BotStringsFiles/BadFile.json";
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { new BotStrings(testJsonPath); });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("Json file not found"));
        }

        [Test]
        public void GetStringExist()
        {
            //ARRANGE
            string testJsonPath = $"{_testDir}/_testfiles/BotStringsFiles/test.json";            
            BotStrings sutBotStrings = new BotStrings(testJsonPath);     
            //ACT
            string test1String = sutBotStrings.getString("test1");
            string test2String = sutBotStrings.getString("test2");
            string test3String = sutBotStrings.getString("test3");
            //ASSERT
            Assert.That(test1String, Is.EqualTo("Test 1"));
            Assert.That(test2String, Is.EqualTo("Test 2"));
            Assert.That(test3String, Is.EqualTo("Test 3"));
        }

        [Test]
        public void GetStringNotExist()
        {
            //ARRANGE
            string testJsonPath = $"{_testDir}/_testfiles/BotStringsFiles/test.json";             
            BotStrings sutBotStrings = new BotStrings(testJsonPath);             
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutBotStrings.getString("test4"); });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("test4 botstring not found"));
        }
    }
}