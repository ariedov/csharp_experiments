using System;

namespace RethrowException.Project
{
    public class ExceptionThrower
    {
        public void CreateTraceAndThrow()
        {
            TraceMethod1();   
        }

        private void TraceMethod1()
        {
            TraceMethod2();
        }

        private void TraceMethod2()
        {
            TraceMethod3();
        }

        private void TraceMethod3()
        {
            throw new RethrowException();
        }

        public void RethrowExplicitly()
        {
            try
            {
                CreateTraceAndThrow();
            }
            catch (RethrowException e)
            {
                throw e;
            }
        }

        public void Rethrow()
        {
            try
            {
                CreateTraceAndThrow();
            }
            catch (RethrowException e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}