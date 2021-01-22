namespace Subscriptions.Infrastructure.Persistence
{
    public class RetryResult<TResult>
    {
        internal TResult Result { get; set; }
        
        internal bool Success { get; set; }
    }
}