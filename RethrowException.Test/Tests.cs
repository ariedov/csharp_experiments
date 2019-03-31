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
        public void TestRethrowExplicitlyExceptionTrace()
        {
            var thrower = new ExceptionThrower();

            try
            {
                thrower.RethrowExplicitly();
            }
            catch (Project.RethrowException e)
            {
                var trace = new StackTrace(e, true);
                
                // the initial trace was 5 frames, but after rethrow it contains only the caller method + test = 2
                Assert.AreEqual(2, trace.FrameCount);   
            }
        }
        
        [Test]
        public void TestRethrowHiddenExceptionTrace()
        {
            var thrower = new ExceptionThrower();

            Project.RethrowException exception = null;
            try
            {
                thrower.Rethrow();
            }
            catch (Project.RethrowException e)
            {
                var trace = new StackTrace(e, true);
                
                // the initial trace was 5 frames, after rethrow it adds the rethrow method
                Assert.AreEqual(6, trace.FrameCount);
            }
        }
    }
}