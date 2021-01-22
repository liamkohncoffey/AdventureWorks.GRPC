using System;
using System.Threading.Tasks;
using Subscriptions.Infrastructure.Persistence;

namespace ShipWrecks.Infrastructure.Persistence
{
    public static class RetryHelper
    {
        public static async Task<TResult> Retry<TResult>(int retryCount, Func<Task<RetryResult<TResult>>> operation)
        {
            var attempt = 0;

            while (attempt <= retryCount)
            {
                var result = await operation();

                if (result.Success)
                {
                    return result.Result;
                }

                attempt++;
            }

            throw new Exception($"Failed to perform operation, {retryCount} attempts failed");
        }
    }
}