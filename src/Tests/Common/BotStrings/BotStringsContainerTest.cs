using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Moq;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    [TestFixture]
    public class BotStringsContainerTest : TestsBase
    {
        [Test]
        public void LoadJsonFilesDirectoryExist()
        {   
            //ARRANGE
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_testDir}/_testfiles/BotStringsContainerFiles");    
            //ACT
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(mockConfig.Object);                 
            //ASSERT
            Dictionary<string, IBotStrings> containers = sutBotStringsCntr.Containers;
            Assert.That(containers.Count, Is.EqualTo(2));
            Assert.That(containers.ContainsKey("file1"), Is.EqualTo(true));
            Assert.That(containers.ContainsKey("file2"), Is.EqualTo(true));
        }

        [Test]
        public void LoadJsonFileDirectoryNotExist()
        {
            //ARRANCE
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>(); 
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_testDir}/_testfiles/BadDirectory");
            //ACT
            TestDelegate delegatedAct = new TestDelegate(() => { new BotStringsContainer(mockConfig.Object); });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("Directory not found"));
        }

        [Test]
        public void LoadJsonFileDirectoryNoFiles()
        {
            //ARRANCE
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>(); 
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_testDir}/_testfiles/BotStringsContainerNoJsonFiles");
            //ACT
            TestDelegate delegatedAct = new TestDelegate(() => { new BotStringsContainer(mockConfig.Object); });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("No json files in directory"));
        }

        [Test]
        public void GetContainerExist()
        {
            //ARRANGE
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_testDir}/_testfiles/BotStringsContainerFiles");
            //setup container
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(mockConfig.Object);     
            //ACT
            IBotStrings container1 = sutBotStringsCntr.getContainer("file1");
            IBotStrings container2 = sutBotStringsCntr.getContainer("file2");
            //ASSERT
            Assert.That(container1.Container.Count, Is.EqualTo(3));
            Assert.That(container2.Container.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetContainerNotExist()
        {   
            //ARRANGE
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_testDir}/_testfiles/BotStringsContainerFiles");
            //setup container
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(mockConfig.Object);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutBotStringsCntr.getContainer("file3"); });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("file3 container not found"));
        }

        [Test]
        public void GetContainerStringExist()
        {   
            //ARRANGE
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_testDir}/_testfiles/BotStringsContainerFiles");
            //setup container
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(mockConfig.Object);
            //ACT
            string file1string1 = sutBotStringsCntr.getString("file1", "file1test1");
            string file2string2 = sutBotStringsCntr.getString("file2", "file2test2");
            //ASSERT
            Assert.That(file1string1, Is.EqualTo("File 1 Test 1"));
            Assert.That(file2string2, Is.EqualTo("File 2 Test 2"));
        }

        [Test]
        public void GetContainerStringNotExist()
        {
            //ARRANGE
            //mock configuration
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(x => x[It.IsAny<string>()]).Returns($"{_testDir}/_testfiles/BotStringsContainerFiles");
            //setup container
            BotStringsContainer sutBotStringsCntr = new BotStringsContainer(mockConfig.Object);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { sutBotStringsCntr.getString("file3","file3test1"); });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo("file3 container not found"));
        }
    }
}