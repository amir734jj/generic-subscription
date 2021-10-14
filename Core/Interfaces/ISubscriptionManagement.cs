using System;

namespace Core.Interfaces
{
    public interface ISubscriptionManagement<in TArg, out TResult> : IDisposable where TResult: IDisposable
    {
        ISubscription<TResult> InstanceOf(TArg arg);
    }
}