using System;

namespace Core.Interfaces
{
    public interface ISubscription<out T> : IDisposable
    {
        public T Instance { get; }
    }
}