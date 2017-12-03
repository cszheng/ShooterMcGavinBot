using System;
using NUnit.Framework;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    [TestFixture]
    public class BotStringsTest : TestsBase
    {
        private string _testJsonPath;

        [SetUp]
        public void Setup()
        {
            _testJsonPath = $"{_testDir}/_testfiles/BotStringsFiles/test.json";
        }

        [Test]
        public void LoadJsonFileExist()
        {   
            //ARRANGE
            BotStrings sutBotStrings = new BotStrings(_testJsonPath);     
            //ACT
            var containers = sutBotStrings.Container;           
            //ASSERT
            Assert.That(containers["test1"], Is.EqualTo("Test 1"));
            Assert.That(containers["test2"], Is.EqualTo("Test 2"));
            Assert.That(containers["test3"], Is.EqualTo("Test 3"));
        }

        [Test]
        public void LoadJsonFileNotExist()
        {
            //ARRANGE
            _testJsonPath = $"{_testDir}/_testfiles/BotStringsFiles/BadFile.json";
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { new BotStrings(_testJsonPath); });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("Json file not found"));
        }

        [Test]
        public void GetStringExist()
        {
            //ARRANGE            
            BotStrings sutBotStrings = new BotStrings(_testJsonPath);     
            //ACT
            String test1String = sutBotStrings.getString("test1");
            String test2String = sutBotStrings.getString("test2");
            String test3String = sutBotStrings.getString("test3");
            //ASSERT
            Assert.That(test1String, Is.EqualTo("Test 1"));
            Assert.That(test2String, Is.EqualTo("Test 2"));
            Assert.That(test3String, Is.EqualTo("Test 3"));
        }

        [Test]
        public void GetStringNotExist()
        {
            //ARRANGE            
            BotStrings sutBotStrings = new BotStrings(_testJsonPath);             
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutBotStrings.getString("test4"); });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("test4 botstring not found"));
        }
    }
}