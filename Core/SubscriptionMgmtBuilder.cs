using System;
using Core.Interfaces;
using Core.Logic;

namespace Core
{
    public static class SubscriptionMgmtBuilder
    {
        /// <summary>
        /// Subscription management given lambda with no argument meaning each instance is unique
        /// </summary>
        /// <param name="getInstance"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static ISubscriptionManagement<Guid, TResult> AsSubscriptionMgmt<TResult>(Func<Guid, TResult> getInstance) where TResult: IDisposable
        {
            var subscriptionMgmt = new SubscriptionMgmt<Guid, TResult>(getInstance);

            return subscriptionMgmt;
        }
        
        /// <summary>
        /// Subscription management given lambda with one argument
        /// </summary>
        /// <param name="getInstance"></param>
        /// <typeparam name="TArg"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static ISubscriptionManagement<TArg, TResult> AsSubscriptionMgmt<TArg, TResult>(Func<TArg, TResult> getInstance) where TResult: IDisposable
        {
            var subscriptionMgmt = new SubscriptionMgmt<TArg, TResult>(getInstance);

            return subscriptionMgmt;
        }
        
        /// <summary>
        /// Subscription management given lambda with two arguments
        /// </summary>
        /// <param name="getInstance"></param>
        /// <typeparam name="TArg1"></typeparam>
        /// <typeparam name="TArg2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static ISubscriptionManagement<(TArg1, TArg2), TResult> AsSubscriptionMgmt<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> getInstance) where TResult: IDisposable
        {
            var subscriptionMgmt = new SubscriptionMgmt<(TArg1, TArg2), TResult>(x => getInstance(x.Item1, x.Item2));

            return subscriptionMgmt;
        }

        /// <summary>
        /// Subscription management given lambda with three arguments
        /// </summary>
        /// <param name="getInstance"></param>
        /// <typeparam name="TArg1"></typeparam>
        /// <typeparam name="TArg2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TArg3"></typeparam>
        /// <returns></returns>
        public static ISubscriptionManagement<(TArg1, TArg2, TArg3), TResult> AsSubscriptionMgmt<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> getInstance) where TResult: IDisposable
        {
            var subscriptionMgmt = new SubscriptionMgmt<(TArg1, TArg2, TArg3), TResult>(x => getInstance(x.Item1, x.Item2, x.Item3));

            return subscriptionMgmt;
        }
    }
}