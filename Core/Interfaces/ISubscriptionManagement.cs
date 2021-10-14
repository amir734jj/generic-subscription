using System;

namespace GenericSubscription.Interfaces
{
    public interface ISubscriptionManagement<in TArg, out TResult> : IDisposable where TResult: IDisposable
    {
        ISubscription<TResult> InstanceOf(TArg arg);
    }
}