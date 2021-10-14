using System;

namespace GenericSubscription.Interfaces
{
    public interface ISubscription<out T> : IDisposable
    {
        public T Instance { get; }
    }
}