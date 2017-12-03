using System;
using NUnit.Framework;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    [TestFixture]
    public class ExceptionsTests : TestsBase
    {
        [Test]
        public void ThrowBotGenericException()
        {
            //ARRANGE
            string exceptMsg = "Exception message";
            BotGeneraicException sutBotExcept = new BotGeneraicException(exceptMsg);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { throw sutBotExcept; });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo(exceptMsg));
        }

        [Test]
        public void ThrowBotGenericExceptionInnerException()
        {   
             //ARRANGE
            string exceptMsg = "Exception message";
            string innerExceptMsg = "Inner exception message";
            BotGeneraicException sutBotExcept = new BotGeneraicException(exceptMsg, new Exception(innerExceptMsg));
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { throw sutBotExcept; });
            //ASSERT
            BotGeneraicException except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.InnerException.Message, Is.EqualTo(innerExceptMsg));
        }
    }
}