using System;
using System.Threading;
using System.Threading.Tasks;
using GenericSubscription;
using GenericSubscription.Interfaces;
using Xunit;

namespace Core.Tests
{
    public class SubscriptionMgmtTest : IDisposable
    {
        private readonly ISubscriptionManagement<int, SomeTask> _subscriptionMgmt;

        public class SomeTask : IDisposable
        {
            private readonly CancellationTokenSource _cancellation;

            public bool Stopped { get; set; }

            public SomeTask(int seconds)
            {
                _cancellation = new CancellationTokenSource();
                
                Task.Factory.StartNew(() =>
                {
                    while (!_cancellation.IsCancellationRequested)
                    {
                        Console.WriteLine("Working ...");
                        Thread.Sleep(seconds * 1000);
                    }

                    Stopped = true;
                });
            }
            
            public void Dispose()
            {
                _cancellation.Cancel();
            }
        }

        public SubscriptionMgmtTest()
        {
            _subscriptionMgmt = SubscriptionMgmtBuilder.AsSubscriptionMgmt((int seconds) => new SomeTask(seconds));
        }
        
        [Fact]
        public void Test1()
        {
            // Arrange: create generic instances
            var i1 = _subscriptionMgmt.InstanceOf(1000);
            var ii1 = _subscriptionMgmt.InstanceOf(1000);
            var i3 = _subscriptionMgmt.InstanceOf(2000);

            // Assert: instances with the same argument are unique (or reused)
            Assert.Equal(i1.Instance, ii1.Instance);
            Assert.NotEqual(i1.Instance, i3.Instance);
            
            // Assert: instances are not disposed and indeed running
            Assert.False(i1.Instance.Stopped);
            Assert.False(ii1.Instance.Stopped);
            Assert.False(ii1.Instance.Stopped);

            // Act: dispose instances
            i1.Dispose();
            ii1.Dispose();
            i3.Dispose();

            // Assert: make sure instances are indeed stopped
            Assert.False(i1.Instance.Stopped);
            Assert.False(ii1.Instance.Stopped);
            Assert.False(i3.Instance.Stopped);
            
            // Assert: make sure new batch of instance are not the same as disposed instances
            Assert.NotEqual(_subscriptionMgmt.InstanceOf(1000), i1);
            Assert.NotEqual(_subscriptionMgmt.InstanceOf(1000), ii1);
            Assert.NotEqual(_subscriptionMgmt.InstanceOf(2000), i3);
        }

        public void Dispose()
        {
            _subscriptionMgmt.Dispose();
        }
    }
}