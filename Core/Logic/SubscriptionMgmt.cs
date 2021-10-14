using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using GenericSubscription.Interfaces;

namespace GenericSubscription.Logic
{
    internal class SubscriptionMgmt<TArg, TResult> : ISubscriptionManagement<TArg, TResult> where TResult: IDisposable
    {
        private readonly Func<TArg,TResult> _getInstance;

        private readonly ConcurrentDictionary<TArg, TResult> _instances;
        private readonly ConcurrentDictionary<TArg, int> _subscriptions;

        public SubscriptionMgmt(Func<TArg, TResult> getInstance)
        {
            _getInstance = getInstance;
            _instances = new ConcurrentDictionary<TArg, TResult>();
            _subscriptions = new ConcurrentDictionary<TArg, int>();
        }

        public ISubscription<TResult> InstanceOf(TArg arg)
        {
            if (_instances.ContainsKey(arg))
            {
                _subscriptions[arg]++;
                return Wrap(arg, _instances[arg]);
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                _instances[arg] = _getInstance(arg);
                _subscriptions[arg] = 1;
                return Wrap(arg, _instances[arg]);
            }
        }

        private Subscription<TResult> Wrap(TArg arg, TResult instance)
        {
            return new Subscription<TResult>(instance, () =>
            {
                _subscriptions[arg]--;
                // ReSharper disable once InvertIf
                if (_subscriptions[arg] <= 0)
                {
                    _instances[arg].Dispose();
                    _instances.Remove(arg, out _);
                    _subscriptions.Remove(arg, out _);
                }
            });
        }

        public void Dispose()
        {
            foreach (var (_, instance) in _instances)
            {
                instance.Dispose();
            }

            _subscriptions.Clear();
            _instances.Clear();
        }
    }
}