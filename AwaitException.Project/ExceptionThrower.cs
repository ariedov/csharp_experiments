using System.Threading.Tasks;

namespace Project
{
    public class ExceptionThrower
    {
        private bool awaitCompleted;

        public bool AwaitCompleted => awaitCompleted;

        public async Task DelayAndThrow(int sleepForMilliseconds)
        {
            await Task.Delay(sleepForMilliseconds);

            awaitCompleted = true;

            throw new AwaitException();
        }

        public async void VoidDelayAndThrow(int sleepForMilliseconds)
        {
            await Task.Delay(sleepForMilliseconds);

            awaitCompleted = true;

            throw new AwaitException();
        }
    }
}