using System.Threading.Tasks;
using NUnit.Framework;
using Project;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestAwaitExceptionThrown()
        {
            var thrower = new ExceptionThrower();
            // the exception is thrown when we await
            Assert.ThrowsAsync<AwaitException>(async () => await thrower.DelayAndThrow(5));

            // make sure the method was called ok
            Assert.True(thrower.AwaitCompleted);
        }

        [Test]
        public async Task TestFreeFallExceptionThrow()
        {
            var thrower = new ExceptionThrower();

#pragma warning disable 4014
            // we throw the exception and not awaiting the method
            // the exception gets lost in the universe
            thrower.DelayAndThrow(5);

            // wait for the test to finish
            await Task.Delay(10);

            // make sure we have waited long enough
            Assert.True(thrower.AwaitCompleted);
        }
    }
}