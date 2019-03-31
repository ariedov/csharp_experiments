using System.Threading.Tasks;

namespace Project
{
    public class ExceptionThrower
    {
        public async Task DelayAndThrow(int sleepForMilliseconds)
        {
            await Task.Delay(sleepForMilliseconds);

            throw new AwaitException();
        }
    }
}