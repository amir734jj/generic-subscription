# generic-subscription-management

Wrap a lambda function `Func<TArg, TResult` with n-many argument as a thread-safe object instance subscription service.

Example:

```csharp
// Instance builder
Func<int, Foo> builder = x => new Foo(x);

// Create an instance of subscription manager
var subscriptionMgmt = SubscriptionMgmtBuilder.AsSubscriptionMgmt(builder);

// Create instances
var i1 = subscriptionMgmt.InstanceOf(100);
var i2 = subscriptionMgmt.InstanceOf(100);
var i3 = subscriptionMgmt.InstanceOf(200);

// Instances with the same argument are unique
Console.WriteLine(i1.Instance == i2.Instance) // => true
Console.WriteLine(i1.Instance == i3.Instance) // => false

// i1 is disposed but i2 has not, so its still running
i1.Dispose();
Console.WriteLine(i1.Instance == subscriptionMgmt.InstanceOf(100)) // => true

// i2 is not disposed, so any request to get an instance with argument of 100 
// will result in a new instance being called
i2.Dispose()
Console.WriteLine(i1.Instance == subscriptionMgmt.InstanceOf(100)) // => false

// Dispose all running instances
subscriptionMgmt.Dispose();
```

TODO:
 - make it possible to specify max number of concurrent instances with given argument