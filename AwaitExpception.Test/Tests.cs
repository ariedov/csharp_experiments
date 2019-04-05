using System;
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

        // This test fails the process with exception, not yet found a way to run it normally as a test.
        // Comment that out if need to run other tests
        [Test]
        public void TestExceptionThrowInAsyncVoid()
        {
            var thrower = new ExceptionThrower();
            AppDomain.CurrentDomain.UnhandledException += (object sender, UnhandledExceptionEventArgs e) => 
            {
                // Unfortunately I haven't yet found the way to not fail here. 
                // Looks like the exception is caught but the process fails despite of that fact
                Console.WriteLine("The exception is actually caught globally");

                // Make sure thrower finished async operation
                Assert.True(thrower.AwaitCompleted, "Exception handled globally");
            };

            // We try to assure exception is thrown here
            Assert.Throws<AwaitException>(() => thrower.VoidDelayAndThrow(5));

            // Make sure the thrower worked correctly
            Assert.True(thrower.AwaitCompleted, "Exception handled locally");
        }
    }
}