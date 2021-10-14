using System;
using Core.Interfaces;

namespace Core.Logic
{
    internal class Subscription<T> : ISubscription<T> where T: IDisposable
    {
        public T Instance { get; }

        private readonly Action _disposed;

        public Subscription(T instance, Action disposed)
        {
            Instance = instance;
            _disposed = disposed;
        }

        public void Dispose()
        {
            _disposed();
        }
    }
}