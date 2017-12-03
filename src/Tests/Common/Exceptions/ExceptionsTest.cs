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
            var exceptMsg = "Exception message";
            var sutBotExcept = new BotGeneraicException(exceptMsg);
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { throw sutBotExcept; });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.Message, Is.EqualTo(exceptMsg));
        }

        [Test]
        public void ThrowBotGenericExceptionInnerException()
        {   
             //ARRANGE
            var exceptMsg = "Exception message";
            var innerExceptMsg = "Inner exception message";
            var sutBotExcept = new BotGeneraicException(exceptMsg, new Exception(innerExceptMsg));
            //ACT** Delegated action
            TestDelegate delegatedAct = new TestDelegate(() => { throw sutBotExcept; });
            //ASSERT
            var except = Assert.Throws<BotGeneraicException>(delegatedAct);
            Assert.That(except.InnerException.Message, Is.EqualTo(innerExceptMsg));
        }
    }
}