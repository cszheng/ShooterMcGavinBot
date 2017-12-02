using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Moq;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    [TestFixture]
    public class BotStringsContainerTest : TestsBase
    {
        private Mock<IConfiguration> _mockConfig;

        [SetUp]
        public void Setup()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_projectDir}/_testfiles/BotStringsContainerFiles");
        }

        [Test]
        public void LoadJsonFilesDirectoryExist()
        {   
            //ARRANGE
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(_mockConfig.Object);     
            //ACT
            var containers = sutBotStringsCntr.Containers;      
            //ASSERT
            Assert.That(containers.Count, Is.EqualTo(2));
            Assert.That(containers.ContainsKey("file1"), Is.EqualTo(true));
            Assert.That(containers.ContainsKey("file2"), Is.EqualTo(true));
        }

        [Test]
        public void LoadJsonFileDirectoryNotExist()
        {
            //ARRANCE
            var mockNoDirConfig = new Mock<IConfiguration>(); 
            mockNoDirConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_projectDir}/_testfiles/BadDirectory");
            //ACT
            TestDelegate delegatedAct = new TestDelegate(() => { new BotStringsContainer(mockNoDirConfig.Object); });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("Directory not found"));
        }

        [Test]
        public void LoadJsonFileDirectoryNoFiles()
        {
            //ARRANCE
            var mockNoDirConfig = new Mock<IConfiguration>(); 
            mockNoDirConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_projectDir}/_testfiles/BotStringsContainerNoJsonFiles");
            //ACT
            TestDelegate delegatedAct = new TestDelegate(() => { new BotStringsContainer(mockNoDirConfig.Object); });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("No json files in directory"));
        }

        [Test]
        public void GetContainerExist()
        {   //get container that exists
            //ARRANGE
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(_mockConfig.Object);     
            //ACT
            var container1 = sutBotStringsCntr.getContainer("file1");
            var container2 = sutBotStringsCntr.getContainer("file2");
            //ASSERT
            Assert.That(container1.Container.Count, Is.EqualTo(3));
            Assert.That(container2.Container.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetContainerNotExist()
        {   
            //ARRANGE
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(_mockConfig.Object);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutBotStringsCntr.getContainer("file3"); });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("file3 container not found"));
        }

        [Test]
        public void GetContainerStringExist()
        {   
            //ARRANGE
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(_mockConfig.Object);
            //ACT
            var file1string1 = sutBotStringsCntr.getString("file1", "file1test1");
            var file2string2 = sutBotStringsCntr.getString("file2", "file2test2");
            //ASSERT
            Assert.That(file1string1, Is.EqualTo("File 1 Test 1"));
            Assert.That(file2string2, Is.EqualTo("File 2 Test 2"));
        }

        [Test]
        public void GetContainerStringNotExist()
        {
            //ARRANGE
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(_mockConfig.Object);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutBotStringsCntr.getString("file3","file3test1"); });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("file3 container not found"));
        }
    }
}