using System.Diagnostics;
using NUnit.Framework;
using RethrowException.Project;

namespace RethrowException.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestExceptionThrown()
        {
            var thrower = new ExceptionThrower();
            // exception is actually thrown
            Assert.Throws<Project.RethrowException>(() => thrower.CreateTraceAndThrow());
        }

        [Test]
        public void TestExceptionTrace()
        {
            var thrower = new ExceptionThrower();
            try
            {
                thrower.CreateTraceAndThrow();
            }
            catch (Project.RethrowException e)
            {
                var trace = new StackTrace(e, true);
                
                // the trace contains 4 methods inside the class + the caller = 5
                Assert.AreEqual(5, trace.FrameCount);
            }
        }

        [Test]
        public void TestRethrowExceptionTrace()
        {
            var thrower = new ExceptionThrower();

            var exception = CreateThrownException(thrower);
            try
            {
                thrower.Rethrow(exception);
            }
            catch (Project.RethrowException e)
            {
                var trace = new StackTrace(e, true);
                
                // the initial trace was 5 frames, but after rethrow it contains only the caller method = 1
                Assert.AreEqual(1, trace.FrameCount);   
            }
        }

        private static Project.RethrowException CreateThrownException(ExceptionThrower thrower)
        {
            Project.RethrowException exception = null;
            try
            {
                thrower.CreateTraceAndThrow();
            }
            catch (Project.RethrowException e)
            {
                exception = e;
            }

            return exception;
        }
    }
}