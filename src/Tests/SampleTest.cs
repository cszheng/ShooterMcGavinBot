using NUnit.Framework;

namespace Tests.Main
{
    [TestFixture]
    public class SampleTests : TestsBase
    {
        [Test]
        public void ShouldSucceed()
        {
            //assert that test pass is working
            Assert.That(1 + 2, Is.EqualTo(3));
        }

        [Test]
        public void ShouldFail()
        {
            //assert that test fail is working
            Assert.That(1 + 2, Is.EqualTo(4));
        }
    }
}